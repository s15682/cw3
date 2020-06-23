using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.DTOs.Requests;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IDbService dbService;

        public EnrollmentsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var studies = dbService.GetStudy(request.Studies);
            if (studies == null) return BadRequest("Nie znaleziono kierunku studiów o nazwie " + request.Studies);
            Enrollment enroll = dbService.EnrollStudent(request, studies.IdStudy);
            if (enroll == null)
            {
                return BadRequest(); 
            }
            return Ok(enroll); 
        }


    }
}
