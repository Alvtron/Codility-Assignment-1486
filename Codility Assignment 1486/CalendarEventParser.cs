using System;
using System.Text.RegularExpressions;

namespace Codility_Assignment_1486
{
    /// <summary>
    /// Parser for converting strings to CalendarEvent.
    /// </summary>
    public class CalendarEventParser
    {
        private string DayTimeMatchFormat { get; set; }
        private string TimeMatchFormat { get; set; }
        private string HoursMinutesMatchFormat { get; set; }

        public CalendarEventParser(string dayTimeMatchFormat, string timeMatchFormat, string hoursMinutesMatchFormat)
        {
            DayTimeMatchFormat = dayTimeMatchFormat ?? throw new ArgumentNullException(nameof(dayTimeMatchFormat));
            TimeMatchFormat = timeMatchFormat ?? throw new ArgumentNullException(nameof(timeMatchFormat));
            HoursMinutesMatchFormat = hoursMinutesMatchFormat ?? throw new ArgumentNullException(nameof(hoursMinutesMatchFormat));
        }

        /// <summary>
        /// Converts the string to day of week.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The day of the week</returns>
        /// <exception cref="ArgumentException">Day string abbreviation is not valid.</exception>
        private static DayOfWeek ConvertStringToDayOfWeek(string input)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                if (day.ToString().StartsWith(input, StringComparison.InvariantCultureIgnoreCase))
                {
                    return day;
                }
            }
            throw new ArgumentException("Day string abbreviation is not valid.");
        }

        /// <summary>
        /// Extracts the day and time interval with regex.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="day">The day.</param>
        /// <param name="time">The time interval.</param>
        private void ExtractDayAndTimeInterval(string input, out string day, out string time)
        {
            var dayTimeRegex = new Regex(DayTimeMatchFormat);
            var dayTimeMatch = dayTimeRegex.Match(input);
            day = dayTimeMatch.Groups[1].Value;
            time = dayTimeMatch.Groups[2].Value;
        }

        /// <summary>
        /// Extracts the time interval.
        /// </summary>
        /// <param name="time">The time interval.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        private void ExtractTime(string timeInterval, out string startTime, out string endTime)
        {
            var timeRegex = new Regex(TimeMatchFormat);
            var timeMatch = timeRegex.Match(timeInterval);
            startTime = timeMatch.Groups[1].Value;
            endTime = timeMatch.Groups[2].Value;
        }

        /// <summary>
        /// Extracts the hours and minutes with regex.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        private void ExtractHourAndMinutes(string input, out int hours, out int minutes)
        {
            var hourMinuteRegex = new Regex(HoursMinutesMatchFormat);
            var startTimeMatch = hourMinuteRegex.Match(input);
            hours = int.Parse(startTimeMatch.Groups[1].Value);
            minutes = int.Parse(startTimeMatch.Groups[2].Value);
        }

        /// <summary>
        /// Creates the time span.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>
        /// <returns></returns>
        private TimeSpan CreateTimeSpan(DayOfWeek dayOfWeek, int hours, int minutes)
        {
            var startTimeSpan = new TimeSpan(hours, minutes, seconds: 0);
            startTimeSpan += TimeSpan.FromHours(24) * (int)dayOfWeek;
            return startTimeSpan;
        }

        /// <summary>
        /// Parses a string to calendar event meeting according to the syntax defined in this class.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Calendar event meeting</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ICalendarEvent ParseMeeting(string input)
        {
            input = input ?? throw new ArgumentNullException();
            // extract day and time interval with regex
            ExtractDayAndTimeInterval(input, out string day, out string timeInterval);
            // convert day string to day of the week enum
            var dayOfWeek = ConvertStringToDayOfWeek(day);
            // extract start- and end time with regex
            ExtractTime(timeInterval, out string startTime, out string endTime);
            // extract hours and minutes
            ExtractHourAndMinutes(startTime, out int startHours, out int startMinutes);
            ExtractHourAndMinutes(endTime, out int endHours, out int endMinutes);
            // create time spans
            var startTimeSpan = CreateTimeSpan(dayOfWeek, startHours, startMinutes);
            var endTimeSpan = CreateTimeSpan(dayOfWeek, endHours, endMinutes);
            return new MeetingEvent(startTimeSpan, endTimeSpan);
        }
    }
}
