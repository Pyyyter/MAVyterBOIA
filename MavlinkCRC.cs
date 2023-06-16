public partial class Mavlink
{
    public class MavlinkCRC
    {
        const int X25_INIT_CRC = 0xffff;
        
        public static ushort CalculateCRC(byte targetByte, ushort inputChecksum)
        {
            //If one operand is a uint and the second is of type sbyte, short, or int, both are promoted to long.
            unchecked
            {
                byte checksum = (byte)(targetByte ^ (byte)(inputChecksum & 0x00ff));
                checksum = (byte)(checksum ^ (checksum << 4));
                return (ushort)((inputChecksum >> 8) ^ (checksum << 8) ^ (checksum << 3) ^ (checksum >> 4));
            }
        }
        
        public static ushort AccumulateCRC(byte[] targetBuffer, int length)
        {
            if (length < 1)
            {
                return 0xffff;
            }
            
            ushort accumulatedCRC = X25_INIT_CRC;
                  
            for (int i = 1; i < length; i++) // skips STX byte
            {
                accumulatedCRC = CalculateCRC(targetBuffer[i], accumulatedCRC);                
            }

            return (accumulatedCRC);
        }

    }
}