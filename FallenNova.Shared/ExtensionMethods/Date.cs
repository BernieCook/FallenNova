using System;

namespace FallenNova.Shared.ExtensionMethods
{
    public static class Date
    {
        private const string ConstTimezoneInfoGmt = "GMT Standard Time";

        /// <summary>
        /// Return the current GMT date and time.
        /// </summary>
        /// <param name="dateTime">Date time instance.</param>
        /// <returns>Current GMT date and time.</returns>
        public static DateTime ToGmtDateTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(ConstTimezoneInfoGmt));
        }
    }
}
