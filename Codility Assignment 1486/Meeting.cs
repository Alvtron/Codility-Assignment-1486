using System;

namespace Codility_Assignment_1486
{
    /// <summary>
    /// A calendar event representing a meeting.
    /// </summary>
    /// <seealso cref="Codility_Assignment_1486.CalendarEvent" />
    class MeetingEvent : CalendarEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingEvent"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        public MeetingEvent(TimeSpan startTime, TimeSpan endTime)
            : base(startTime, endTime)
        {
        }
    }
}
