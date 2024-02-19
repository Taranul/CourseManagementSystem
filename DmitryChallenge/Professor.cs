namespace DmitryChallenge
{
    public class Professor : UniversityMember
    {
        private const int MAX_LOAD = 2;
        private List<Course> _assignedCourses;

        public Professor(string memberName, List<Course> enrolledCourses = null) : base(memberName)
        {
            _assignedCourses = new List<Course>();

            if (enrolledCourses != null)
            {
                foreach (Course course in enrolledCourses)
                {
                    _assignedCourses.Add(course);
                }
            }
        }

        public void ShowAssignedCourses()
        {
            for (int i = 0; i < _assignedCourses.Count; i++)
            {
                _assignedCourses[i].ShowCourseInfo();
            }
        }

        // the same as in student - methods are bool, but i don't use it
        public bool Teach(Course course)
        {
            if (!_assignedCourses.Contains(course))
            {
                if (_assignedCourses.Count < MAX_LOAD)
                {
                    _assignedCourses.Add(course);
                    Console.WriteLine("Professor is succsessfully assigned to teach this course");
                    return true;
                }

                UniversityCourseManagementSystem.PrintErrorAndExit("This professor workload is too high!");
                return false;
            }

            UniversityCourseManagementSystem.PrintErrorAndExit("This professor already teaches this course!");
            return false;
        }

        public bool Exempt(Course course)
        {
            if (_assignedCourses.Contains(course))
            {
                _assignedCourses.Remove(course);
                Console.WriteLine("Professor is exempted");
                return true;
            }

            UniversityCourseManagementSystem.PrintErrorAndExit("This professor doesn't teach this course yet!");
            return false;
        }
    }
}