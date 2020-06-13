using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using Cw3.DAL; 

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsControler : ControllerBase
    {
        private readonly IdbService dbService; 

        public StudentsControler(IdbService dbService)
        {
            this.dbService = dbService; 
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
             return Ok("Kowalski");
            }
            else if (id==2){
                return Ok("Malewski"); 
            }
            return NotFound("Nie znaleziono studenta"); 
        }

        [HttpGet]
        public IActionResult GetStudentByParam(string orderby)
        {
            return Ok(dbService.GetStudents()); 
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student) {
            // add to database
            // generate 
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student); 
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
