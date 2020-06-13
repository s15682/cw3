using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int EnrolmentId { get; set; }
        public string StudyName { get; set; }

        public int Semester { get; set; }


        public Student (string index, string name, string lastName, string studyName, int semester)
        {
            IndexNumber = index;
            FirstName = name;
            LastName = lastName;
            StudyName = studyName;
            Semester = semester; 
        }

        public Student() { }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + IndexNumber + " " + StudyName + " " + Semester; 
        }

    }
}
