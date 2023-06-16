
using System;

public partial class Mavlink
{
    public class MavlinkMessage
    {
        public static readonly MavlinkMessage Invalid = new MavlinkMessage();
        object _locker = new object();

        public DateTime RxTime { get; set; }
        public byte Header { get; internal set; }
        public byte PayloadLength { get; internal set; }

        public byte IncompatibleFlags { get; internal set; }
        public byte CompatibleFlags { get; internal set; }

        public byte Sequence { get; internal set; }
        public byte SysID { get; internal set; }
        public byte CompID { get; internal set; }

        public uint MsgID { get; internal set; }

        public bool IsMavlink2 {
            get
            {
                if (SerializationMessageBuffer != null && SerializationMessageBuffer.Length > 0)
                    return (SerializationMessageBuffer[0] == MAVLINK2_STX);

                return false;
            }
        }

        public string MsgTypename
        {
            get { return MavlinkMessageInfoTable.GetMessageInfo(MsgID).Name; }
        }

        private byte[] _serializationMessageBuffer;

        public byte[] SerializationMessageBuffer
        {
            get { return _serializationMessageBuffer; }
            set
            {
                _serializationMessageBuffer = value;
                ProcessBuffer(_serializationMessageBuffer);
            }
        }

        private object _payload;
        public object Payload
        {
            get
            {
                // lock the entire creation of the packet. to prevent returning a incomplete packet.
                lock (_locker)
                {
                    if (_payload != null)
                        return _payload;

                    var messageInfo = MavlinkMessageInfoTable.GetMessageInfo(MsgID); // msgid refers to current instance of Message

                    if (messageInfo.Type == null)
                        return null;

                    _payload = Activator.CreateInstance(messageInfo.Type); // create instance of the message type at runtime

                    try
                    {
                        if (PayloadLength == 0)
                            return _payload;

                        // fill in the data of the object
                        if (IsMavlink2)
                        {
                            MavlinkUtil.ByteArrayToStructure(SerializationMessageBuffer, ref _payload, MAVLINK2_NUM_HEADER_BYTES, PayloadLength);
                        }
                        else
                        {
                            MavlinkUtil.ByteArrayToStructure(SerializationMessageBuffer, ref _payload, 6, PayloadLength);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }

                return _payload;
            }
        }

        public T ToStructure<T>()
        {
            return (T)Payload;
        }

        public ushort Crc16 { get; internal set; }

        public byte[] Signature { get; internal set; }

        public byte SigLinkID
        {
            get
            {
                if (Signature != null)
                {
                    return Signature[0];
                }

                return 0;
            }
        }

        public ulong SignatureTimeStamp 
        {
            get
            {
                if (Signature != null)
                {
                    byte[] temp = new byte[8];
                    Array.Copy(Signature, 1, temp, 0, 6);
                    return BitConverter.ToUInt64(temp, 0);
                }

                return 0;
            }
        }

        public int Length
        {
            get
            {
                if (SerializationMessageBuffer == null) return 0;
                return SerializationMessageBuffer.Length;
            }
        }

        public MavlinkMessage()
        {
            this.RxTime = DateTime.MinValue;
        }

        public MavlinkMessage(byte[] buffer): this(buffer, DateTime.UtcNow)
        {
        }

        public MavlinkMessage(byte[] buffer, DateTime rxTime)
        {
            this.SerializationMessageBuffer = buffer;
            this.RxTime = rxTime;

            ProcessBuffer(buffer);
        }

        internal void ProcessBuffer(byte[] buffer)
        {
            _payload = null;

            if (buffer[0] == MAVLINK2_STX)
            {
                if (buffer.Length < 10)
                {
                    return;
                }
                Header = buffer[0];
                PayloadLength = buffer[1];
                IncompatibleFlags = buffer[2];
                CompatibleFlags = buffer[3];
                Sequence = buffer[4];
                SysID = buffer[5];
                CompID = buffer[6];
                MsgID = (uint) ((buffer[9] << 16) + (buffer[8] << 8) + buffer[7]);

                var crc1 = MAVLINK2_CORE_HEADER_LEN + PayloadLength + 1;
                var crc2 = MAVLINK2_CORE_HEADER_LEN + PayloadLength + 2;

                Crc16 = (ushort) ((buffer[crc2] << 8) + buffer[crc1]);

                if ((IncompatibleFlags & MAVLINK_IFLAG_SIGNED) > 0)
                {
                    Signature = new byte[MAVLINK2_SIGNATURE_BLOCK_LEN];
                    Array.ConstrainedCopy(buffer, buffer.Length - MAVLINK2_SIGNATURE_BLOCK_LEN, Signature, 0,
                        MAVLINK2_SIGNATURE_BLOCK_LEN);
                }
            }
            else
            {
                if (buffer.Length < 6)
                {
                    return;
                }
                Header = buffer[0];
                PayloadLength = buffer[1];
                Sequence = buffer[2];
                SysID = buffer[3];
                CompID = buffer[4];
                MsgID = buffer[5];

                var crc1 = MAVLINK1_CORE_HEADER_LEN + PayloadLength + 1;
                var crc2 = MAVLINK1_CORE_HEADER_LEN + PayloadLength + 2;

                Crc16 = (ushort) ((buffer[crc2] << 8) + buffer[crc1]);
            }
        }

        public override string ToString()
        {
            return String.Format("{5},{4},{0},{1},{2},{3}", SysID, CompID, MsgID, MsgTypename, IsMavlink2, RxTime);
        }
    }
}
