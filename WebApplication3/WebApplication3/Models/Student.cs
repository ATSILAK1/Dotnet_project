using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Student
    {
        public int StudentsId { get; set; }
        public string StudentName { get; set; }
        public Grade grade { get; set; }
    }
}