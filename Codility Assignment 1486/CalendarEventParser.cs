using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codility_Assignment_1486
{
    /// <summary>
    /// Parser for converting strings to CalendarEvent.
    /// </summary>
    public class CalendarEventParser<T> where T : ICalendarEvent
    {
        private const string KEYWORD_DAY = "day";
        private const string KEYWORD_START_HOUR = "h1";
        private const string KEYWORD_START_MINUTE = "m1";
        private const string KEYWORD_START_SECONDS = "s1";
        private const string KEYWORD_END_HOUR = "h2";
        private const string KEYWORD_END_MINUTE = "m2";
        private const string KEYWORD_END_SECONDS = "s2";

        private string MatchFormat { get; set; }

        public CalendarEventParser(string matchFormat)
        {
            MatchFormat = matchFormat ?? throw new ArgumentNullException(nameof(matchFormat));
        }

        /// <summary>
        /// Parses a string to calendar event meeting according to the syntax defined in this class.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Calendar event meeting</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public T Parse(string input)
        {
            input = input ?? throw new ArgumentNullException();

            var regex = new Regex(MatchFormat);
            var match = regex.Match(input);

            //exctract day
            var day = GetMatchedValue<string>(match.Groups, KEYWORD_DAY);
            var dayOfWeek = ConvertStringToDayOfWeek(day);

            // extract time
            var startHours = GetMatchedValue<int>(match.Groups, KEYWORD_START_HOUR);
            var startMinutes = GetMatchedValue<int>(match.Groups, KEYWORD_START_MINUTE);
            var startSeconds = GetMatchedValue<int>(match.Groups, KEYWORD_START_SECONDS);
            var endHours = GetMatchedValue<int>(match.Groups, KEYWORD_END_HOUR);
            var endMinutes = GetMatchedValue<int>(match.Groups, KEYWORD_END_MINUTE);
            var endSeconds = GetMatchedValue<int>(match.Groups, KEYWORD_END_SECONDS);

            var startTimeSpan = CreateTimeSpan(dayOfWeek, startHours, startMinutes, startSeconds);
            var endTimeSpan = CreateTimeSpan(dayOfWeek, endHours, endMinutes, endSeconds);

            return (T)Activator.CreateInstance(typeof(T), startTimeSpan, endTimeSpan);
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
        /// Creates the time span.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="hours">The hours.</param>
        /// <param name="minutes">The minutes.</param>'
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        private static TimeSpan CreateTimeSpan(DayOfWeek dayOfWeek, int hours, int minutes, int seconds)
        {
            return new TimeSpan((int)dayOfWeek, hours, minutes, seconds);
        }

        /// <summary>
        /// Gets the matched value.
        /// </summary>
        /// <typeparam name="F">The excpected type of the extracted value.</typeparam>
        /// <param name="match">The match.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static F GetMatchedValue<F>(GroupCollection match, string key) where F : IConvertible
        {
            if (!match.TryGetValue(key, out var group))
            {
                return default;
            }
            return (F)Convert.ChangeType(group.Value, typeof(F));
        }
    }
}
