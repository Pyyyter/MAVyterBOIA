using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleExample
{
    internal static class GPRMCParser
    {
        public static GPRMCData Parse(string sentence)
        {
            if (string.IsNullOrEmpty(sentence))
                throw new ArgumentNullException(nameof(sentence));

            // Check sentence identifier
            if (!sentence.StartsWith("$GPRMC"))
                throw new ArgumentException("Invalid GPRMC sentence", nameof(sentence));

            // Remove leading '$' and split the sentence into fields
            string[] fields = sentence.Substring(1).Split(',');
            
            // Check the number of fields
            if (fields.Length < 12)
                throw new ArgumentException("Invalid number of fields in GPRMC sentence", nameof(sentence));

            fields = fields.Skip(1).ToArray();

            // Extract data from fields
            string utcTime = fields[0];
            string status = fields[1];
            double latitude = ParseLatitude(fields[2], fields[3]);
            double longitude = ParseLongitude(fields[4], fields[5]);
            double speed = double.Parse(fields[6]);
            double course = double.Parse(fields[7]);
            DateTime date = ParseDate(fields[8]);

            return new GPRMCData(utcTime, status, latitude, longitude, speed, course, date);
        }

        private static double ParseLatitude(string value, string hemisphere)
        {
            double latitude = double.Parse(value, CultureInfo.InvariantCulture) / 100f;
            if (hemisphere == "S")
                latitude *= -1;
            return latitude;
        }

        private static double ParseLongitude(string value, string hemisphere)
        {
            double longitude = double.Parse(value, CultureInfo.InvariantCulture) / 100f;
            if (hemisphere == "W")
                longitude *= -1;
            return longitude;
        }

        private static DateTime ParseDate(string value)
        {
            int day = int.Parse(value.Substring(0, 2));
            int month = int.Parse(value.Substring(2, 2));
            int year = int.Parse(value.Substring(4, 2)) + 2000;
            return new DateTime(year, month, day);
        }
    }

    public class GPRMCData
    {
        public string UtcTime { get; }
        public string Status { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public double Speed { get; }
        public double Course { get; }
        public DateTime Date { get; }

        public GPRMCData(string utcTime, string status, double latitude, double longitude, double speed, double course, DateTime date)
        {
            UtcTime = utcTime;
            Status = status;
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            Course = course;
            Date = date;
        }
    }

}

