using Cw3.DTOs.Requests;
using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();

        Studies GetStudy(string name);

        bool GetStudentEnrollment(string id, out string response);

        Enrollment EnrollStudent(EnrollStudentRequest request, int idStudy);

        Enrollment GetEnrollment(string studyName, int semester);
        Enrollment Promote(Enrollment enroll);
    }
}
