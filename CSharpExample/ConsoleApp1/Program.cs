using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {

        static List<Student> studentList = new List<Student>();
        static List<Course> courseList = new List<Course>();
        static List<Teacher> teacherList = new List<Teacher>();
        static List<Season> seasonList = new List<Season>();
        static List<TeacherCourse> teacherCourseList = new List<TeacherCourse>();
        static List<StudentCourse> studentCourseList = new List<StudentCourse>();


        static void Main(string[] args)
        {
            LoadData();

            Console.WriteLine("Seasons are listed. Choose a season");

            foreach (var s in seasonList)
            {
                Console.WriteLine(s.Name);
            }

            var seasonName = Console.ReadLine();
            var season = GetSeasonByName(seasonName);

            if (season != null)
            {
                Console.WriteLine("Season courses are listed. Choose a course");

                var seasonCoureses = GetCoursesBySeasonId(season.Id);

                foreach (var seasonCourse in seasonCoureses)
                {
                    Console.WriteLine(seasonCourse.Name);
                }

                var courseName = Console.ReadLine();
                var course = GetCourseByName(courseName);

                if (course != null)
                {
                    var teacherCourse = GetTeacherCourse(season.Id, course.Id);
                    var teacher = GetTeacherById(teacherCourse.TeacherId);
                    var studentList = GetStudentByTeacherCourse(teacherCourse.Id);

                    Console.WriteLine(teacher.Name + " " + teacher.Surname);
                    foreach (var std in studentList)
                    {
                        Console.WriteLine(std.Name + " " + std.Surname);

                    }
                }
            }

            Console.ReadLine();
        }

        static void LoadData()
        {
            #region Teacher
            teacherList.Add(new Teacher()
            {
                Id = 1,
                Name = "Ahmet",
                Surname = "Acar"
            });

            teacherList.Add(new Teacher()
            {
                Id = 2,
                Name = "Selçuk",
                Surname = "Şener"
            });

            teacherList.Add(new Teacher()
            {
                Id = 3,
                Name = "İhsan",
                Surname = "İçiuyan"
            });
            #endregion

            #region Student

            studentList.Add(new Student() { Id = 1, Name = "Yusuf", Surname = "Ünlü" });
            studentList.Add(new Student() { Id = 2, Name = "Ramazan", Surname = "Oruç" });
            studentList.Add(new Student() { Id = 3, Name = "Abdullah", Surname = "Pepeç" });


            #endregion

            #region Season
            seasonList.Add(new Season() { Id = 1, Name = "22-23 W" });

            #endregion

            #region Course
            courseList.Add(new Course() { Id = 1, Name = "OS" });
            courseList.Add(new Course() { Id = 2, Name = "VP" });
            courseList.Add(new Course() { Id = 1, Name = "DS" });

            #endregion

            #region TeacherCourse
            teacherCourseList.Add(new TeacherCourse() { Id = 1, CourseId = 1, SeasonId = 1, TeacherId = 1 });
            teacherCourseList.Add(new TeacherCourse() { Id = 2, CourseId = 2, SeasonId = 1, TeacherId = 2 });
            teacherCourseList.Add(new TeacherCourse() { Id = 3, CourseId = 3, SeasonId = 1, TeacherId = 3 });
            #endregion

            #region StudentCourse
            studentCourseList.Add(new StudentCourse() { Id = 1, StudentId = 1, TeacherCourseId = 1 });
            studentCourseList.Add(new StudentCourse() { Id = 2, StudentId = 2, TeacherCourseId = 2 });
            studentCourseList.Add(new StudentCourse() { Id = 3, StudentId = 2, TeacherCourseId = 3 });
            #endregion
        }

        static Course GetCourseByName(string _name)
        {
            var course = courseList.FirstOrDefault(x => x.Name == _name);
            return course;
        }
        static Season GetSeasonByName(string _seasonName)
        {
            var season = seasonList.FirstOrDefault(x => x.Name == _seasonName);
            return season;

        }

        static List<Course> GetCoursesBySeasonId(int _seasonId)
        {
            var seasonCourses = teacherCourseList.Where(x => x.SeasonId == _seasonId);

            var courseListSelect = new List<Course>();

            foreach (var item in seasonCourses)
            {
                var course = courseList.FirstOrDefault(x => x.Id == item.CourseId);
                if (course != null)
                {
                    courseListSelect.Add(course);
                }
            }

            return courseListSelect;
        }

        static Teacher GetTeacherById(int _teacherId)
        {
            var teacher = teacherList.FirstOrDefault(x => x.Id == _teacherId);
            return teacher;
        }

        static TeacherCourse GetTeacherCourse(int _seasonId, int _courseId)
        {
            var teacherCourse = teacherCourseList.FirstOrDefault(x => x.SeasonId == _seasonId && x.CourseId == _courseId);

            return teacherCourse;
        }

        static List<Student> GetStudentByTeacherCourse(int _teacherCourseId)
        {
            var studentCourseListSelect = studentCourseList.Where(x => x.TeacherCourseId == _teacherCourseId).ToList();
            var studentListSelect = new List<Student>();

            foreach (var studentCourse in studentCourseListSelect)
            {
                var student = studentList.FirstOrDefault(x => x.Id == studentCourse.StudentId);

                if (student != null)
                {
                    studentListSelect.Add(student);
                }
            }

            return studentListSelect;
        }
    }
}
