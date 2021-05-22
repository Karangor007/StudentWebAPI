using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentWebAPI.Models
{
    public class StudentModel
    {
        public ICollection<Student> StudentList { get; set; }
    }

    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string rollno { get; set; }
    }

    public class Marks
    {
        public int id { get; set; }
        public int studentId { get; set; }        
        public int marks { get; set; }
    }
}