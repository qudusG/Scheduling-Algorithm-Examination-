using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamTimetable
{
    public class DayAllocate
    {
        private List<string> allCourses;
        private readonly CourseData Course;
        private readonly Department[] departments;
        public List<DayOfWeek> daySlot;
        private readonly int coursesPerDay;
        public DayAllocate(int numDays, int maxCoursesPerDay)
        {
            Course = new CourseData();
            departments = new Department[Course.Subjects.Count];
            allCourses = new List<string> { };
            daySlot = new List<DayOfWeek>(numDays);
            coursesPerDay = maxCoursesPerDay;
        }

        public List<DayOfWeek> GenerateDays()
        {
            for (int d = 0; d < daySlot.Capacity; d++)
            {
                daySlot.Add(new DayOfWeek
                {
                    StartDate = DateTime.Today,
                    Day = new List<string>(coursesPerDay)
                });
            }
            return daySlot;
        }
        public Department[] GenerateDepartments()
        {
            int n = 0;
            foreach (List<string> subject in Course.Subjects)
            {
                departments[n] = new Department
                {
                    DepartmentLevel = $"Dep {n}",
                    Courses = subject
                };
                allCourses.AddRange(subject);
                n++;
            }
            return departments;
        }
        public List<DayOfWeek> DistributeCourses()
        {
            var random = new Random();
            var all_Courses = allCourses.OrderBy(course => random.Next()).Distinct().ToList();
            while (all_Courses.Count != 0)
            {
                bool gotoNextCourse = false;
                string drawnCourse = all_Courses[0];
                foreach (Department department in departments.Where(d => d.Courses.Contains(drawnCourse)))
                {
                    foreach (DayOfWeek day in daySlot)
                    {
                        //Check for the presence of a unique course between deparment and current day.
                        if (!department.Courses.Intersect(day.Day).Any() &&
                            !CheckUniqueCourseInAllDepartments(drawnCourse, day, department, departments) && day.Day.Count < coursesPerDay)
                        {
                            day.Day.Add(drawnCourse);
                            all_Courses.Remove(drawnCourse);
                            gotoNextCourse = true;
                            break;
                        }
                    }
                    if (gotoNextCourse)
                    {
                        break;
                    }
                }
            }
            return daySlot;
        }
        private bool CheckUniqueCourseInAllDepartments(string drawnCourse, DayOfWeek day, Department currentDepartment, Department[] departments)
        {
            bool status = false;
            foreach (Department department in departments)
            {
                if (department.DepartmentLevel != currentDepartment.DepartmentLevel)
                {
                    if (department.Courses.Intersect(day.Day).Any())
                    {
                        if (department.Courses.Contains(drawnCourse))
                        {
                            status = true;
                            break;
                        }
                    }
                }
            }
            return status;
        }
        public string TableDisplay()
        {
            var table = new StringBuilder();
            table.AppendLine("Day\nCourse");

            foreach (var day in daySlot)
            {
                table.AppendLine($"\n{string.Join(" ,", day.Day)}");
            }
            return table.ToString();
        }
    }
}
