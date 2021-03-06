﻿using System;

namespace Codility_Assignment_1486
{
    static class Solution
    {
        private const int DAYS_IN_WEEK = 7;

        /// <summary>
        /// Given a string representing the schedule, returns the length of the longest time interval (in minutes) that is not occupied.
        /// Note: I would have named this class more descriptevly, but the final name was decided by the codility asssignment.
        /// </summary>
        /// <param name="S">The schedule.</param>
        /// <returns></returns>
        public static int solution(string S)
        {
            // create calendar event parser
            const string MATCH_FORMAT = "^(?<day>.*?) (?<h1>.*?):(?<m1>.*?)-(?<h2>.*?):(?<m2>.*?)$";
            var calendarParser = new CalendarEventParser<MeetingEvent>(MATCH_FORMAT);
            
            // create schedule
            var schedule = new Schedule();
            foreach (var entry in S.Split(Environment.NewLine))
            {
                var meeting = calendarParser.Parse(entry);
                schedule.Add(meeting);
            }

            // find highest break
            var largestInterval = TimeSpan.Zero;
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
                    var intervalToMidnight = TimeSpan.FromDays(DAYS_IN_WEEK) - calendarEvent.EndTime;
                    largestInterval = intervalToMidnight > largestInterval ? intervalToMidnight : largestInterval;
                }
                // store current event for next iteration
                lastMeeting = calendarEvent;
            }
            return (int)largestInterval.TotalMinutes;
        }
    }
}
