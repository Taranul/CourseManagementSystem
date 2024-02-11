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

        public bool Teach(Course course)
        {
            if (!_assignedCourses.Contains(course))
            {
                if (_assignedCourses.Count < MAX_LOAD)
                {
                    _assignedCourses.Add(course);
                    return true;
                }

                UniversityCourseManagementSystem.PrintError("This professor workload is too high!");
                return false;
            }

            UniversityCourseManagementSystem.PrintError("This professor already teaches this course!");
            return false;
        }

        public bool Exempt(Course course)
        {
            if (_assignedCourses.Contains(course))
            {
                _assignedCourses.Remove(course);
                return true;
            }

            UniversityCourseManagementSystem.PrintError("This professor doesn't teach this course yet!");
            return false;
        }
    }
}
