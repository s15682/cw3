using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class MockDbService : IdbService
    {
        private static IEnumerable<Student> studentsCollection;  

        static MockDbService()
        {
            studentsCollection = new List<Student>
            {
                new Student{ IdStudent =1, FirstName= "Jan", LastName ="Kowalski"},
                new Student{ IdStudent =1, FirstName= "Marian", LastName ="Paździoch"},
                new Student{ IdStudent =1, FirstName= "Ferdynand", LastName ="Kiepski"},
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return studentsCollection; 
        }
    }
}
