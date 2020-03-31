using System;

namespace Codility_Assignment_1486
{
    class CalendarEvent : ICalendarEvent
    {
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarEvent"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <exception cref="ArgumentException">Start time is later than the end time.</exception>
        public CalendarEvent(TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime > endTime)
            {
                throw new ArgumentException("Start time is later than the end time.");
            }

            StartTime = startTime;
            EndTime = endTime;
        }

        /// <summary>
        /// Compare calendar events.
        /// </summary>
        /// <param name="other">The other event.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Events may only be compared with other events.</exception>
        public int CompareTo(object other)
        {
            if (!(other is ICalendarEvent otherEvent))
            {
                throw new ArgumentException("Events may only be compared with other events.");
            }

            return StartTime.CompareTo(otherEvent.StartTime);
        }
    }
}
