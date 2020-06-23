using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStudents(string orderby)
        {
            return Ok("coś");
        }

        [HttpPost]
        public IActionResult EnrollStudent()
        {
            return Ok(); 
        }


    }
}
