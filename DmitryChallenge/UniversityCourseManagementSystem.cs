using System.Text.RegularExpressions;

namespace DmitryChallenge
{
    public class UniversityCourseManagementSystem
    {
        private static List<UniversityMember> _universityMembers;
        private static List<Course> _courses;

        private static void Main(string[] args)
        {
            #region preInput
            Student student;
            Professor professor;
            Course course;
            UniversityCourseManagementSystem managementSystem = new UniversityCourseManagementSystem();
            managementSystem.FillInitialData();
            managementSystem.ShowAllInfo();
            #endregion

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tWrite a command from the list: \n'course' - add a new course\n'student' - add a new student\n'professor' - add a new professor\n'enroll' - enroll a student to course\n'drop' - drop a student from course\n'teach' - make professor to teach a course\n'exempt' - make professor exempt from course");
                Console.ForegroundColor = ConsoleColor.White;
                string userInput = managementSystem.TryToGetCorrectInput();

                switch (userInput)
                {
                    case "course":
                        Console.Write("Write course name: ");
                        string courseName = managementSystem.TryToGetCorrectInput();
                        Console.Write("Choose level: Bachelor or Master ");
                        string courseLevel = Console.ReadLine().ToUpper().Replace(" ", "");

                        if (Enum.TryParse(courseLevel, out CourseLevel level))
                        {
                            _courses.Add(new Course(courseName, level));
                            Console.WriteLine("Added successfully!");
                        }
                        else
                        {
                            PrintError("Incorrect level input!");
                        }

                        Console.ReadKey();
                        break;
                    case "student":
                        Console.Write("Write student name: ");
                        string studentName = managementSystem.TryToGetCorrectInput();
                        _universityMembers.Add(new Student(studentName));
                        Console.WriteLine("Added successfully");
                        Console.ReadKey();
                        break;
                    case "professor":
                        Console.Write("Write professor name: ");
                        string professorName = managementSystem.TryToGetCorrectInput();
                        _universityMembers.Add(new Professor(professorName));
                        Console.WriteLine("Added successfully");
                        Console.ReadKey();
                        break;
                    case "enroll":
                        student = _universityMembers[managementSystem.GetMemberId()] as Student;
                        if (student == null)
                            PrintError("This is not a student!");
                        course = _courses[managementSystem.GetCourseId()];
                        student.Enroll(course);
                        Console.WriteLine("Enrolled successfully");
                        Console.ReadKey();
                        break;
                    case "drop":
                        student = _universityMembers[managementSystem.GetMemberId()] as Student;
                        if (student == null)
                            PrintError("This is not a student!");
                        course = _courses[managementSystem.GetCourseId()];
                        student.Drop(course);
                        Console.WriteLine("Dropped succsessfully");
                        Console.ReadKey();
                        break;
                    case "teach":
                        professor = _universityMembers[managementSystem.GetMemberId()] as Professor;
                        if (professor == null)
                            PrintError("This is not a professor!");
                        course = _courses[managementSystem.GetCourseId()];
                        professor.Teach(course);
                        Console.WriteLine("Professor is succsessfully assigned to teach this course");
                        Console.ReadKey();
                        break;
                    case "exempt":
                        professor = _universityMembers[managementSystem.GetMemberId()] as Professor;
                        if (professor == null)
                            PrintError("This is not a professor!");
                        course = _courses[managementSystem.GetCourseId()];
                        professor.Exempt(course);
                        Console.WriteLine("Professor is exempted");
                        Console.ReadKey();
                        break;
                    default:
                        PrintError("Error!");
                        return;
                }

                #region printResult
                Console.WriteLine("========================================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tRESULT WITH CHANGES: ");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                managementSystem.ShowAllInfo();

                Console.WriteLine("Done? just tap 'enter', otherwise other key");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    return;
                #endregion
            }
        }

        private void FillInitialData()
        {
            _courses = new List<Course>()
            {
                new Course("java_beginner", CourseLevel.BACHELOR),
                new Course("java_intermediate", CourseLevel.BACHELOR),
                new Course("python_basics", CourseLevel.BACHELOR),
                new Course("algorithms", CourseLevel.MASTER),
                new Course("advanced_programming", CourseLevel.MASTER),
                new Course("mathematical_analisis", CourseLevel.MASTER),
                new Course("computer_vision", CourseLevel.MASTER),
            };
            _universityMembers = new List<UniversityMember>()
            {
                new Student("Alice", new List<Course> { _courses[0], _courses[1], _courses[2] }),
                new Student("Bob", new List<Course> { _courses[0], _courses[3] }),
                new Student("Alex", new List<Course> { _courses[4] }),
                new Professor("Ali", new List<Course> { _courses[0], _courses[1] }),
                new Professor("Ahmed", new List<Course> { _courses[2], _courses[4] }),
                new Professor("Andrey", new List<Course> { _courses[5] }),
            };
        }

        private void ShowAllInfo()
        {
            ShowStudentsInfo();
            ShowProfessorsInfo();
            ShowCoursesInfo();
        }

        private void ShowStudentsInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tSTUDENTS: ");
            Console.ForegroundColor = ConsoleColor.White;

            Student student;

            for (int i = 0; i < _universityMembers.Count; i++)
            {
                student = _universityMembers[i] as Student;

                if (student != null)
                {
                    Console.Write("Id: ");
                    student.PrintId();
                    Console.Write("Name: ");
                    student.PrintName();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Courses enrolled: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    student.ShowEnrolledCourses();
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public void ShowProfessorsInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tPROFESSORS: ");
            Console.ForegroundColor = ConsoleColor.White;

            Professor professor;

            for (int i = 0; i < _universityMembers.Count; i++)
            {
                professor = _universityMembers[i] as Professor;

                if (professor != null)
                {
                    Console.Write("Id: ");
                    professor.PrintId();
                    Console.Write("Name: ");
                    professor.PrintName();
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Courses assigned: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    professor.ShowAssignedCourses();
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public void ShowCoursesInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tCOURSES: ");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < _courses.Count; i++)
            {
                _courses[i].ShowCourseInfo();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enrolled students: ");
                Console.ForegroundColor = ConsoleColor.White;
                _courses[i].PrintEnrolledStudents();
                Console.WriteLine("----------------------------------------");
            }
        }

        private int GetMemberId()
        {
            Console.Write("Write ID of member: ");
            int memberId = 0;

            try
            {
                memberId = int.Parse(Console.ReadLine());
                memberId--;
            }
            catch
            {
                PrintError("Inappropriate value");
            }

            if (memberId > _universityMembers.Count || memberId < 0)
            {
                PrintError("This member doesn't exists!");
                return 0;
            }

            return memberId;
        }

        private int GetCourseId()
        {
            Console.Write("Write ID of course: ");
            int courseId = 0;

            try
            {
                courseId = int.Parse(Console.ReadLine());
                courseId--;
            }
            catch
            {
                PrintError("Inappropriate value");
            }

            if (courseId > _universityMembers.Count || courseId < 0)
            {
                PrintError("This course doesn't exists!");
                return 0;
            }

            return courseId;
        }

        private string TryToGetCorrectInput()
        {
            Regex regex;
            string input = Console.ReadLine().ToLower().Replace(" ", "");

            regex = new Regex("^[a-zA-Z]+(_[a-zA-Z]+)*$", RegexOptions.IgnorePatternWhitespace);

            if (regex.IsMatch(input))
            {
                return input;
            }
            else 
            {
                PrintError("Inappropriate value!");
                return "";
            }
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
        }

        public static void PrintColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
