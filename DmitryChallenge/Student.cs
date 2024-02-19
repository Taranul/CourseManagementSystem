namespace DmitryChallenge
{
    public class Student : UniversityMember, Enrollable
    {
        private const int MAX_ENROLLMENT = 3;
        private List<Course> _enrolledCourses;

        public Student(string memberName, List<Course> enrolledCourses = null) : base(memberName)
        {
            _enrolledCourses = new List<Course>();

            if (enrolledCourses != null)
            {
                foreach (Course course in enrolledCourses)
                {
                    _enrolledCourses.Add(course);
                    course.AddStudent(this);
                }
            }
        }

        public void ShowEnrolledCourses()
        {
            for (int i = 0; i < _enrolledCourses.Count; i++)
            {
                _enrolledCourses[i].ShowCourseInfo();
            }
        }

        // refactor, because i don't use bool type of method, lol
        public bool Enroll(Course course)
        {
            if (!_enrolledCourses.Contains(course))
            {
                if (!course.isFull())
                {
                    if (_enrolledCourses.Count < MAX_ENROLLMENT)
                    {
                        _enrolledCourses.Add(course);
                        course.AddStudent(this);
                        Console.WriteLine("Enrolled successfully");
                        return true;
                    }

                    UniversityCourseManagementSystem.PrintErrorAndExit("This student is busy enough. He can't enroll more courses!");
                    return false;
                }

                UniversityCourseManagementSystem.PrintErrorAndExit("This course is full!");
                return false;
            }

            UniversityCourseManagementSystem.PrintErrorAndExit("This course has already been enrolled");
            return false;
        }

        public bool Drop(Course course)
        {
            if (_enrolledCourses.Contains(course))
            {
                _enrolledCourses.Remove(course);
                course.RemoveStudent(this);
                Console.WriteLine("Dropped succsessfully");
                return true;
            }

            UniversityCourseManagementSystem.PrintErrorAndExit("This student doesn't study on this course yet!");
            return false;
        }
    }
}