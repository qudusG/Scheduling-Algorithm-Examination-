using System;
using System.Collections.Generic;
using System.Text;

namespace ExamTimetable
{
    public class CourseData
    {
        public List<List<string>> Subjects;
        public CourseData()
        {
            Subjects = new List<List<string>>
            {
                new List<string> { "Thermodynamics", "Engineering Maths","CFD"},
                new List<string> { "Machine Design", "Python","Thermodynamics"},
                new List<string> { "Strength of Materials", "CFD","Python","Machine Design"},
                new List<string> { "Production Engineering", "Science of Materials", "Python"},
            };
        }
    }
}
