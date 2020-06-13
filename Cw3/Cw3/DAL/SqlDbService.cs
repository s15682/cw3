using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class SqlDbService : IDbService
    {
        private const string ConString = "Data Source=DESKTOP-LVH8UIJ;Initial Catalog=APBD_DB;Integrated Security=True";

        public IEnumerable<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>(); 
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = " select Student.FirstName, Student.LastName, Student.IndexNumber, Student.BirthDate, Studies.Name, Enrollment.Semester "
                                    + "From Student JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment "
                                    + "JOIN Studies ON Enrollment.IdStudy = Studies.IdStudy;";

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    studentList.Add(new Student(dr["IndexNumber"].ToString(),
                                                dr["FirstName"].ToString(),
                                                dr["LastName"].ToString(),
                                                dr["Name"].ToString(),
                                                (int)dr["Semester"]
                                                )); 
                }
            }
            return studentList; 
        }
    }
}

/*
 * string msg =""; 
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
*/