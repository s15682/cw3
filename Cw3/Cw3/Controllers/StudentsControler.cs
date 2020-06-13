using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsControler : ControllerBase
    {
        [HttpGet]
        public string GetStudent()
        {
            return "Kowalski, Malewski, Adrzejewski"; 
        }
    }
}
