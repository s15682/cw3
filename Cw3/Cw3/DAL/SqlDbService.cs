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
                com.CommandText = " select Student.FirstName, Student.LastName, Student.IndexNumber, Student.IdEnrollment "
                                    + "From Student;";

                client.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    studentList.Add(new Student(dr["IndexNumber"].ToString(),
                                                dr["FirstName"].ToString(),
                                                dr["LastName"].ToString(),
                                                (int)dr["IdEnrollment"]
                                                )); 
                }
            }
            return studentList; 
        }

        public bool GetStudentEnrollment(string id, out string response)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = " select  Student.IndexNumber, Enrollment.Semester, Studies.Name "
                                    + "From Student JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment "
                                    + "JOIN Studies ON Enrollment.IdStudy = Studies.IdStudy "
                                    + "Where Student.IndexNumber = @id";
                com.Parameters.AddWithValue("id", id); 
                client.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    response = "Student jest wpisany na semestr " + dr["Semester"] + " studia: " + dr["Name"];
                    return true; 
                }
            }
            response = ""; 
            return false; 
        }

        public Studies GetStudy(string name)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = " select Studies.IdStudy, Studies.Name from Studies Where Studies.Name = @name; "; 
                com.Parameters.AddWithValue("name", name);
                client.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return new Studies((int)dr["IdStudy"], dr["Name"].ToString());
                }
            }
            return null; 
        }
    }
}
