using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282Project
{
    internal class Student
    {
        int studentID;
        string fname,courseID;
        int age;

        public Student(int studentID, string fname, string courseID, int age)
        {
            this.studentID = studentID;
            this.fname = fname;
            this.courseID = courseID;
            this.age = age;
        }

        public override string ToString()
        {
            return "ID";
        }

        public int StudentID { get => studentID; set => studentID = value; }
        public string Fname { get => fname; set => fname = value; }
        public string CourseID { get => courseID; set => courseID = value; }
        public int Age { get => age; set => age = value; }
    }
}
