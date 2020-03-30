using System;

namespace Codility_Assignment_1486
{
    /// <summary>
    /// An calendar event representing a time interval. 
    /// </summary>
    /// <seealso cref="System.IComparable" />
    public interface ICalendarEvent : IComparable
    {
        TimeSpan StartTime { get; }
        TimeSpan EndTime { get; }
    }
}
