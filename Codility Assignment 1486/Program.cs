using System;
using System.IO;

/**
 * Implementation by Thomas Angeland (thomas.angeland@gmail.com)
 * 
 * Note: I would have named the Solution class more descriptevly,
 * but the final name was decided by the codility asssignment 1486.
 * 
 * While I really wanted to use the TimeSpan Parser as it makes formatting strings more 
 * streamlined, this does not support time equal or above 24 hours.
 * 
 * Instead, I ended up using regex match grouping to extract the individual day and time data and
 * created the TimeParse objects using its constructor.
 * 
 * In an optimal case, I would have adressed the syntax used by James to support ISO 8601.
 * 
 * Each meeting is stored as a Calendar Event with a start- and end time, then added to
 * a Schedule-object which stores the events in a binary search tree (self-balancing red-black
 * tree with O(log(n)) complexity). Each event implements the IComparable interface.
 * 
 * Then I iterate over all events from first to last and find the largest interval by subtracting
 * the end time for the next event to the start time of the current event. 
 */

namespace Codility_Assignment_1486
{

    class Program
    {
        static void Main(string[] args)
        {
            var scheduleA = File.ReadAllText("schedule_a.txt");
            var schedulaAInterval = Solution.solution(scheduleA);
            Console.WriteLine($"Schedule A: James can sleep for {schedulaAInterval} minutes.");

            var scheduleB = File.ReadAllText("schedule_b.txt");
            var schedulaBInterval = Solution.solution(scheduleB);
            Console.WriteLine($"Schedule B: James can sleep for {schedulaBInterval} minutes.");
        }
    }
}
