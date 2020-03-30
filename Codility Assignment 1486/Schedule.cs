using System.Collections.Generic;

namespace Codility_Assignment_1486
{
    /// <summary>
    /// A schedule that stores calendar events.
    /// </summary>
    class Schedule
    {
        public SortedSet<ICalendarEvent> Events { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Schedule"/> class.
        /// </summary>
        public Schedule()
        {
            Events = new SortedSet<ICalendarEvent>(); // O(log(n)) for insert, delete and lookup
        }

        public void Add(ICalendarEvent eventObject)
        {
            Events.Add(eventObject);
        }
    }
}
