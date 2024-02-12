﻿using System.Text.RegularExpressions;

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
                PrintColoredMessage("\tWrite a command from the list: " +
                    "\n'course' - add a new course" +
                    "\n'student' - add a new student" +
                    "\n'professor' - add a new professor" +
                    "\n'enroll' - enroll a student to course" +
                    "\n'drop' - drop a student from course" +
                    "\n'teach' - make professor to teach a course" +
                    "\n'exempt' - make professor exempt from course", 
                    ConsoleColor.Yellow);
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
                            PrintErrorAndExit("Incorrect level input!");
                        }

                        Console.ReadKey();
                        break;
                    case "student": //refactor
                        Console.Write($"Write student name: ");
                        string studentName = managementSystem.TryToGetCorrectInput();
                        student = new Student(studentName);
                        managementSystem.CreateMember(student);
                        break;
                    case "professor":
                        Console.Write($"Write professor name: ");
                        string professorName = managementSystem.TryToGetCorrectInput();
                        professor = new Professor(professorName);
                        managementSystem.CreateMember(professor);
                        break;
                    case "enroll":
                        student = _universityMembers[managementSystem.GetId("member")] as Student;
                        if (student == null)
                            PrintErrorAndExit("This is not a student!");
                        course = _courses[managementSystem.GetId("course")];
                        student.Enroll(course);
                        Console.WriteLine("Enrolled successfully");
                        Console.ReadKey();
                        break;
                    case "drop":
                        student = _universityMembers[managementSystem.GetId("member")] as Student;
                        if (student == null)
                            PrintErrorAndExit("This is not a student!");
                        course = _courses[managementSystem.GetId("course")];
                        student.Drop(course);
                        Console.WriteLine("Dropped succsessfully");
                        Console.ReadKey();
                        break;
                    case "teach":
                        professor = _universityMembers[managementSystem.GetId("member")] as Professor;
                        if (professor == null)
                            PrintErrorAndExit("This is not a professor!");
                        course = _courses[managementSystem.GetId("course")];
                        professor.Teach(course);
                        Console.WriteLine("Professor is succsessfully assigned to teach this course");
                        Console.ReadKey();
                        break;
                    case "exempt":
                        professor = _universityMembers[managementSystem.GetId("member")] as Professor;
                        if (professor == null)
                            PrintErrorAndExit("This is not a professor!");
                        course = _courses[managementSystem.GetId("course")];
                        professor.Exempt(course);
                        Console.WriteLine("Professor is exempted");
                        Console.ReadKey();
                        break;
                    default:
                        PrintErrorAndExit("Error!");
                        return;
                }

                #region printResult
                Console.Clear();
                managementSystem.ShowAllInfo();

                Console.WriteLine("Done? just tap 'enter', otherwise other key");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    return;
                #endregion
            }
        }

        private void CreateMember(UniversityMember member)
        {
            _universityMembers.Add(member);
            Console.WriteLine("Added successfully");
            Console.ReadKey();
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
            PrintColoredMessage("\t\tSTUDENTS: ", ConsoleColor.Green);

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
                    PrintColoredMessage("Courses enrolled: ", ConsoleColor.Green);
                    student.ShowEnrolledCourses();
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public void ShowProfessorsInfo()
        {
            PrintColoredMessage("\t\tPROFESSORS: ", ConsoleColor.Green);

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
                    PrintColoredMessage("Courses assigned: ", ConsoleColor.Red);
                    professor.ShowAssignedCourses();
                    Console.WriteLine("----------------------------------------");
                }
            }
        }

        public void ShowCoursesInfo()
        {
            PrintColoredMessage("\t\tCOURSES: ", ConsoleColor.Green);

            for (int i = 0; i < _courses.Count; i++)
            {
                _courses[i].ShowCourseInfo();
                PrintColoredMessage("Enrolled students: ", ConsoleColor.Red);
                _courses[i].PrintEnrolledStudents();
                Console.WriteLine("----------------------------------------");
            }
        }

        private int GetId(string objectName)
        {
            Console.Write($"Write ID of {objectName}: ");
            int Id = 0;

            try
            {
                Id = int.Parse(Console.ReadLine());
                Id--;
            }
            catch
            {
                PrintErrorAndExit("Inappropriate value");
            }

            if (Id > _universityMembers.Count || Id < 0)
            {
                PrintErrorAndExit($"This {objectName} doesn't exists!");
                return 0;
            }

            return Id;
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
                PrintErrorAndExit("Inappropriate value!");
                return "";
            }
        }

        public static void PrintErrorAndExit(string message)
        {
            PrintColoredMessage(message, ConsoleColor.DarkRed);
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
