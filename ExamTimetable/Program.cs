using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamTimetable
{
    class Program
    {
        static void Main(string[] args)
        {
            int numdays = 4;
            int maxcoursesperday = 3;
            //Number of courses must be less than (numdays X maxcoursesperday)
            var createdays = new DayAllocate(numdays, maxcoursesperday);
            createdays.GenerateDays();
            createdays.GenerateDepartments();
            List<DayOfWeek> scheduleDays = createdays.DistributeCourses();

            foreach (var examDay in scheduleDays)
            {
                Console.WriteLine(string.Join(",", examDay.Day));
            }
        }
    }
}
