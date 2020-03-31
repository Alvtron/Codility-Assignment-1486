using System;

namespace Codility_Assignment_1486
{
    static class Solution
    {
        /// <summary>
        /// Given a string representing the schedule, returns the length of the longest time interval (in minutes) that is not occupied.
        /// Note: I would have named this class more descriptevly, but the final name was decided by the codility asssignment.
        /// </summary>
        /// <param name="S">The schedule.</param>
        /// <returns></returns>
        public static int solution(string S)
        {
            // create calendar event parser
            const string DAY_TIME_MATCH_FORMAT = "^(.*?) (.*?)$"; // 'ddd time'
            const string TIME_MATCH_FORMAT = "^(.*?)-(.*?)$"; // 'start-end'
            const string HOUR_MINUTE_MATCH_FORMAT = "^(.*?):(.*?)$"; // 'hour:minute'
            var calendarParser = new CalendarEventParser<MeetingEvent>(DAY_TIME_MATCH_FORMAT, TIME_MATCH_FORMAT, HOUR_MINUTE_MATCH_FORMAT);
            
            // create schedule
            var schedule = new Schedule();
            foreach (var entry in S.Split(Environment.NewLine))
            {
                var meeting = calendarParser.Parse(entry);
                schedule.Add(meeting);
            }

            // find highest break
            var largestInterval = TimeSpan.FromHours(0);
            ICalendarEvent lastMeeting = null;
            var counter = 0;
            foreach (var calendarEvent in schedule.Events)
            {
                counter++;
                // if first event, calculate duration between midnight and start time
                if (lastMeeting == null) 
                {
                    largestInterval = calendarEvent.StartTime;
                }
                // if not first, calculate duration between last end time and current start time
                else
                {
                    var currentInterval = calendarEvent.StartTime - lastMeeting.EndTime;
                    largestInterval = currentInterval > largestInterval ? currentInterval : largestInterval;
                }
                // if last event, calculate duration between end time and midnight
                if (counter == schedule.Events.Count) 
                {
                    var currentInterval = TimeSpan.FromHours(24 - (calendarEvent.EndTime.TotalHours % 24));
                    largestInterval = currentInterval > largestInterval ? currentInterval : largestInterval;
                }
                // store current event for next iteration
                lastMeeting = calendarEvent; 
            }
            return (int)largestInterval.TotalMinutes;
        }
    }
}
