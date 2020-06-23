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
    [Route("api/enrollments/promotions")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {

        private readonly IDbService dbService;

        public PromotionsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpPost]
        public IActionResult Promote(PromoteRequest request)
        {
            Enrollment enroll = dbService.GetEnrollment(request.StudyName, request.Semester);
            if (enroll == null)
            {
                return NotFound("Enrollment not found");
            }
            enroll = dbService.Promote(enroll);
            if (enroll == null)
            {
                return StatusCode(500); 
            }
            return 
        }
    }
}
