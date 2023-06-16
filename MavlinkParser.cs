using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

public partial class Mavlink
{
   
    public class MavlinkParser
    {
        public int SequenceCounterRX { get; set; } = 0;
        public int SequenceCounterTX { get; set; } = 0;
        public int BadCrc { get; set; } = 0;
        public int BadLength { get; set; } = 0;

        public bool HasTimeStamp = false;

        public MavlinkParser(bool hasTimestamp = false)
        {
            this.HasTimeStamp = hasTimestamp;
        }

        public static void ReadWithTimeout(Stream BaseStream, byte[] buffer, int startingPositionindex, int count)
        {
            int timeout = 500;

            if (BaseStream.CanSeek)
            {
                timeout = 0;

                if((BaseStream.Position + count) > BaseStream.Length)
                    throw new EndOfStreamException("End of data");
            }

            if (BaseStream.CanTimeout)
            {
                timeout = BaseStream.ReadTimeout;

                if (timeout == -1)
                    timeout = 60000;
            }

            DateTime to = DateTime.Now.AddMilliseconds(timeout);

            int bytesToRead = count;
            int positionindex = startingPositionindex;

            while (true)
            {
                // read from stream
                int bytesRead = BaseStream.Read(buffer, positionindex, bytesToRead);                

                // update counter
                bytesToRead -= bytesRead;
                positionindex += bytesRead;

                // reset timeout if we get data
                if (bytesRead > 0)
                    to = DateTime.Now.AddMilliseconds(timeout);

                if (bytesToRead == 0)
                    break;

                if (DateTime.Now > to)
                {
                    throw new TimeoutException("Timeout waiting for data");
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        public MavlinkMessage ReadPacket(Stream baseStream)
        {
            byte[] buffer = new byte[Mavlink.MAVLINK2_MAX_PACKET_LEN];

            DateTime packetTime = DateTime.MinValue;

            if (HasTimeStamp)
            {
                byte[] dateArray = new byte[8];

                int tem = baseStream.Read(dateArray, 0, dateArray.Length);

                Array.Reverse(dateArray);

                DateTime date1 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                UInt64 dateint = BitConverter.ToUInt64(dateArray, 0);

                if ((dateint / 1000 / 1000 / 60 / 60) < 9999999)
                {
                    date1 = date1.AddMilliseconds(dateint / 1000);

                    packetTime = date1.ToLocalTime();
                }
            }

            int readCount = 0;

            while (readCount <= Mavlink.MAVLINK2_MAX_PACKET_LEN)
            {
                // read STX byte
                ReadWithTimeout(baseStream, buffer, 0, 1);               

                if (buffer[0] == Mavlink.MAVLINK2_STX || buffer[0] == MAVLINK1_STX)
                    break;

                readCount++;
            }

            if (readCount >= Mavlink.MAVLINK2_MAX_PACKET_LEN)
            {
                Console.WriteLine("No header found");
                throw new InvalidDataException("No header found in data");
            }

            var headerlength = buffer[0] == MAVLINK2_STX ? MAVLINK2_CORE_HEADER_LEN : MAVLINK1_CORE_HEADER_LEN;
            var headerLengthSTX = headerlength + 1;

            // read header
            try {
                ReadWithTimeout(baseStream, buffer, 1, headerlength);
            }
            catch (EndOfStreamException)
            {
                return null;
            }

            // packet length
            const int numCRCBytes = 2;
            int lengthToRead = buffer[1] + numCRCBytes;
            
            try
            {
                //read rest of packet      
                ReadWithTimeout(baseStream, buffer, headerLengthSTX, lengthToRead);               
            }
            catch (EndOfStreamException)
            {
                return null;
            }            

            // resize the packet to the correct length
            Array.Resize<byte>(ref buffer, headerLengthSTX + lengthToRead);           
            MavlinkMessage message = new MavlinkMessage(buffer, packetTime);

            // calc crc
            ushort crc = MavlinkCRC.AccumulateCRC(buffer, buffer.Length - 2);

            // calc extra bit of crc for mavlink 1.0+
            if (message.Header == MAVLINK2_STX || message.Header == MAVLINK1_STX)
            {
                crc = MavlinkCRC.CalculateCRC(MavlinkMessageInfoTable.GetMessageInfo(message.MsgID).Crc, crc);
            }

            // check crc
            if ((message.Crc16 >> 8) != (crc >> 8) || (message.Crc16 & 0xff) != (crc & 0xff))
            {
                BadCrc++;
                // crc fail
                return null;
            }

            return message;
        }

        public byte[] GenerateMAVLinkPacket10(MAVLINK_MSG_ID messageType, object payloadStructure, byte sysID = 255, byte compID = (byte)MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER, int sequence = -1)
        {
            byte[] serializedPayloadData; // Represents the payload struct in byte array form

            serializedPayloadData = MavlinkUtil.StructureToByteArray(payloadStructure);
            
            byte[] serializedMessage = new byte[serializedPayloadData.Length + 6 + 2]; // Represents the entire message that also encapsulates the payload byte array

            serializedMessage[0] = MAVLINK1_STX;
            serializedMessage[1] = (byte)serializedPayloadData.Length;
            serializedMessage[2] = (byte)SequenceCounterRX;
            if (sequence != -1)
                serializedMessage[2] = (byte)sequence;

            SequenceCounterRX++;

            serializedMessage[3] = sysID; // this is always 255 - MYGCS
            serializedMessage[4] = compID;
            serializedMessage[5] = (byte)messageType;

            int i = 6;
            foreach (byte b in serializedPayloadData)
            {
                serializedMessage[i] = b;
                i++;
            }

            ushort checksum = MavlinkCRC.AccumulateCRC(serializedMessage, serializedMessage[1] + 6);
            checksum = MavlinkCRC.CalculateCRC(MavlinkMessageInfoTable.GetMessageInfo((uint)messageType).Crc, checksum);

            byte checksumLow = (byte)(checksum & 0xFF); // Low byte
            byte checksumHigh = (byte)(checksum >> 8); // High byte

            serializedMessage[i++] = checksumLow;            
            serializedMessage[i] = checksumHigh;
            
            return serializedMessage;
        }

        public byte[] GenerateMAVLinkPacket20(MAVLINK_MSG_ID messageType, object indata, bool sign = false, byte sysid = 255, byte compid= (byte)MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER, int sequence = -1)
        {
            byte[] data;

            data = MavlinkUtil.StructureToByteArray(indata);

            MavlinkUtil.TrimPayloadNullBytes(ref data);

            int extra = 0;
            if (sign)
                extra = MAVLINK2_SIGNATURE_BLOCK_LEN;

            byte[] packet = new byte[data.Length + MAVLINK2_NUM_NON_PAYLOAD_BYTES + extra];

            packet[0] = MAVLINK2_STX;
            packet[1] = (byte)data.Length;
            packet[2] = 0;//incompat  signing
            if (sign)
                packet[2] |= MAVLINK_IFLAG_SIGNED;
            packet[3] = 0;//compat
            packet[4] = (byte)SequenceCounterRX;
            if (sequence != -1)
                packet[4] = (byte)sequence;
            SequenceCounterRX++;

            packet[5] = sysid;
            packet[6] = compid;
            packet[7] = (byte)((UInt32)messageType);
            packet[8] = (byte)((UInt32)messageType >> 8);
            packet[9] = (byte)((UInt32)messageType >> 16);

            int i = MAVLINK2_NUM_HEADER_BYTES;
            foreach (byte b in data)
            {
                packet[i] = b;
                i++;
            }

            ushort checksum = MavlinkCRC.AccumulateCRC(packet, data.Length + MAVLINK2_NUM_HEADER_BYTES);

            checksum = MavlinkCRC.CalculateCRC(MavlinkMessageInfoTable.GetMessageInfo((uint)messageType).Crc, checksum);

            byte ck_a = (byte)(checksum & 0xFF); ///< High byte
            byte ck_b = (byte)(checksum >> 8); ///< Low byte

            packet[i] = ck_a;
            i += 1;
            packet[i] = ck_b;
            i += 1;

            if (sign)
            {
                //https://docs.google.com/document/d/1ETle6qQRcaNWAmpG2wz0oOpFKSF_bcTmYMQvtTGI8ns/edit

                /*
                8 bits of link ID
                48 bits of timestamp
                48 bits of signature
                */

                // signature = sha256_48(secret_key + header + payload + CRC + link-ID + timestamp)

                var timestamp = (UInt64)((DateTime.UtcNow - new DateTime(2015, 1, 1)).TotalMilliseconds * 100);

                if (timestamp == LastTimeStamp)
                    timestamp++;

                LastTimeStamp = timestamp;

                var timebytes = BitConverter.GetBytes(timestamp);

                var sig = new byte[7]; // 13 includes the outgoing hash
                sig[0] = SendLinkID;
                Array.Copy(timebytes, 0, sig, 1, 6); // timestamp

                //Console.WriteLine("gen linkid {0}, time {1} {2} {3} {4} {5} {6} {7}", sig[0], sig[1], sig[2], sig[3], sig[4], sig[5], sig[6], timestamp);
                
                if (SigningKey == null || SigningKey.Length != 32)
                {
                    SigningKey = new byte[32];
                }
                
                using (SHA256CryptoServiceProvider signit = new SHA256CryptoServiceProvider())
                {
                    MemoryStream ms = new MemoryStream();
                    ms.Write(SigningKey, 0, SigningKey.Length);
                    ms.Write(packet, 0, i);
                    ms.Write(sig, 0, sig.Length);

                    var ctx = signit.ComputeHash(ms.GetBuffer());
                    // trim to 48
                    Array.Resize(ref ctx, 6);

                    foreach (byte b in sig)
                    {
                        packet[i] = b;
                        i++;
                    }

                    foreach (byte b in ctx)
                    {
                        packet[i] = b;
                        i++;
                    }
                }
            }

            return packet;
        }

        public byte SendLinkID { get; set; }

        public ulong LastTimeStamp { get; set; }

        public byte[] SigningKey { get; set; }
    }

    public static string GetUnit(string fieldName, Type packetType = null, string name = "", uint msgID = UInt32.MaxValue)
    {
        try
        {
            MessageInfo msgInfo = new MessageInfo();
            if (packetType != null)
                msgInfo = Mavlink.MavlinkMessageInfoTable.First(a => a.Type == packetType);
            if (msgID != UInt32.MaxValue)
                msgInfo = Mavlink.MavlinkMessageInfoTable.First(a => a.MsgID == msgID);
            if (!string.IsNullOrEmpty(name))
                msgInfo = Mavlink.MavlinkMessageInfoTable.First(a => a.Name == name);

            if (msgInfo.Name == "")
                return "";

            var typeOfThing = msgInfo.Type.GetField(fieldName);
            if (typeOfThing != null)
            {
                var attrib = typeOfThing.GetCustomAttributes(false);
                if (attrib.Length > 0)
                    return attrib.OfType<Mavlink.Units>().First().Unit;
            }
        }
        catch
        {
        }

        return "";
    }

    public class Units : Attribute
    {
        public Units(string unit)
        {
            Unit = unit;
        }

        public string Unit { get; set; }
    }

    public class Description : Attribute
    {
        public Description(string desc)
        {
            Text = desc;
        }

        public string Text { get; set; }
    }

}
