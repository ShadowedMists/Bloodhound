using Bloodhound.Core.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloodhound.Core
{
    public class TimeFormatter
    {
        /// <summary>
        /// For the given DateTimeOffset, returns a string that is the time span formatted for easy user comprehension.
        /// </summary>
        /// <param name="datetimeoffset">The DateTimeOffset to use to calculate the time span.</param>
        /// <returns>A formatted string that represents the time span.</returns>
        public static string FormatRelative(DateTimeOffset? datetimeoffset)
        {
            if (datetimeoffset == null)
                return null;

            TimeSpan span = DateTimeOffset.Now - datetimeoffset.Value;

            if (span.TotalMinutes < 2)
                return TimeResources.Time_MinuteSingular;
            if (span.TotalHours < 1)
                return string.Format(TimeResources.Time_MinutesFormat, (int)span.TotalMinutes);
            if (span.TotalHours < 2)
                return TimeResources.Time_HourSingular;
            if (span.TotalDays < 1)
                return string.Format(TimeResources.Time_HoursFormat, (int)span.TotalHours);
            if (span.TotalDays < 2)
                return TimeResources.Time_DaySingular;
            if (span.TotalDays < 31)
                return string.Format(TimeResources.Time_DaysFormat, (int)span.TotalDays);
            if (span.TotalDays < 62)
                return TimeResources.Time_MonthSingular;
            if (span.TotalDays < 365)
                return string.Format(TimeResources.Time_MonthsFormat, (int)(span.TotalDays / 31));
            if (span.TotalDays < 730)
                return TimeResources.Time_YearSingular;
            return string.Format(TimeResources.Time_YearsFormat, (int)(span.TotalDays / 365));
        }
    }
}
