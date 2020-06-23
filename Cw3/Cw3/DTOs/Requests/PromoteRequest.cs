using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class PromoteRequest
    {
        [Required]
        [MaxLength(255)]
        public string StudyName { get; set; }

        [Required]
        public int Semester { get; set; }
    }
}
