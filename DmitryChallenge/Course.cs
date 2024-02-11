namespace DmitryChallenge
{
    public class Course
    {
        private const int CAPACITY = 3;
        private static int _numberOfCourses;
        private int _courseId;
        private string _courseName;
        private List<Student> _enrolledStudents;
        private CourseLevel _courseLevel;

        public Course(string courseName, CourseLevel courseLevel)
        {
            _courseId = ++_numberOfCourses;
            _courseName = courseName;
            _courseLevel = courseLevel;
            _enrolledStudents = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            _enrolledStudents.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            _enrolledStudents.Remove(student);
        }

        public void ShowCourseInfo()
        {
            Console.Write("Id: ");
            Console.WriteLine(_courseId);
            Console.Write("Name: ");
            Console.WriteLine(_courseName);
            Console.Write("Level: ");
            Console.WriteLine(_courseLevel);
            Console.WriteLine("");
        }

        public void PrintEnrolledStudents()
        {
            for (int i = 0; i < _enrolledStudents.Count; i++)
            {
                Console.Write("Name: ");
                _enrolledStudents[i].PrintName();
                Console.Write("Id: ");
                _enrolledStudents[i].PrintId();
                Console.WriteLine();
            }
        }

        public bool isFull()
        {
            return _enrolledStudents.Count > CAPACITY;
        }
    }
}
