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
        public IActionResult GetStudents(string orderby)
        {
            string msg =""; 
            using (var client = new SqlConnection("Data Source=DESKTOP-LVH8UIJ;Initial Catalog=APBD_DB;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select * from Student";

                client.Open();
                var dr = com.ExecuteReader(); 
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    msg += st.FirstName+" "; 
                    
                }
            }
            return Ok(msg); 
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
