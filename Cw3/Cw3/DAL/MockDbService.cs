using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> studentsCollection;  

        static MockDbService()
        {
            studentsCollection = new List<Student>
            {
                new Student{ IndexNumber ="s0001", FirstName= "Jan", LastName ="Kowalski"},
                new Student{ IndexNumber ="s0002", FirstName= "Marian", LastName ="Paździoch"},
                new Student{ IndexNumber ="s0003", FirstName= "Ferdynand", LastName ="Kiepski"},
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return studentsCollection; 
        }

        public bool GetStudentEnrollment(string id, out string response)
        {
            response = ""; 
            return false; 
        }
    }
}
