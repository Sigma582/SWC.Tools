using System;

namespace SWC.Tools.Common.Util
{
    public class TimeHelper
    {
        //todo: this is lame
        public static double LastLoginTimeSec { get; set; }

        public static double GetTimestampMs()
        {
            var timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            return Math.Round(timestamp);
        }

        public static double GetTimestampSec()
        {
            var timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds / 1000;
            return Math.Round(timestamp);
        }

        public static DateTime FromSeconds(double seconds)
        {
            return DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc).AddSeconds(seconds);
        }
    }
}