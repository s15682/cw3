using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using Cw3.DAL;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsControler : ControllerBase
    {
        private readonly IDbService dbService; 

        public StudentsControler(IDbService dbService)
        {
            this.dbService = dbService; 
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            String response;
            if (dbService.GetStudentEnrollment(id, out response)) {
                return Ok(response); 
            } 
            return NotFound("Nie znaleziono studenta");
        }

        [HttpGet]
        public IActionResult GetStudents(string orderby)
        {
            IEnumerable<Student> students = dbService.GetStudents();
            IEnumerable<String> studentsFullList = createResponseList(students); 
            return Ok(studentsFullList); 
        }

        private IEnumerable<string> createResponseList(IEnumerable<Student> students)
        {
            List<string> responseList = new List<string>();
            int i = 1; 
            foreach( Student st in students)
            {
                responseList.Add( "Lp: "+(i++)+" "+st.ToString());
            }
            return responseList; 
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            // add to database
            // generate 
            /*
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student); 
            */
            return Ok(); 
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id)
        {
            return Ok("Aktualizacja ukończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }

    }
}
