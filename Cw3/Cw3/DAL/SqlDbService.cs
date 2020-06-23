using Cw3.DTOs.Requests;
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

        Enrollment EnrollStudent(EnrollStudentRequest request, int idStudy)
        {
            using (var client = new SqlConnection(ConString))
            {
                client.Open();
                var transaction = client.BeginTransaction();
                Enrollment enrollment = new Enrollment();
                try
                {
                    var chceckforEnrollmentCommand = CreateGetEnrollmentCommand(request.Studies, 1);
                    chceckforEnrollmentCommand.Connection = client;
                    var dr = chceckforEnrollmentCommand.ExecuteReader();
                    while (!dr.Read())
                    {
                        var createCommand = CreateEnrollmentCreateCommand(idStudy);
                        createCommand.Connection = client;
                        createCommand.ExecuteNonQuery(); 
                        dr = chceckforEnrollmentCommand.ExecuteReader();
                    } 
                    enrollment = SetEnrollments(dr);
                    Student st = new Student(request.IndexNumber, request.FirstName, request.LastName, enrollment.IdEnrollment);
                    var addCommand = AddStudentCommand(st);
                    addCommand.Connection = client;
                    addCommand.ExecuteNonQuery(); 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return null; 
                }
                return enrollment; 
            }
        }

        SqlCommand CreateGetEnrollmentCommand(string studies, int semester)
        {
            var com = new SqlCommand();
            com.CommandText = " select * from Enrollment Join Studies On Enrollment.IdStudy = Studies.IdStudy" +
                              " Where Studies.Name = @name AND Enrollment.Semester = @semester" +
                              " Order by Enrollment.StartDate;";
            com.Parameters.AddWithValue("name", studies);
            com.Parameters.AddWithValue("semester", semester); 
            return com; 
        }

        Enrollment SetEnrollments ( SqlDataReader dr)
        {
            return new Enrollment((int)dr["IdEnrollment"],(int)dr["Semester"],new Studies((int)dr["IdStudy"],dr["Name"].ToString()), dr["StartDate"].ToString()); 
        }

        SqlCommand CreateEnrollmentCreateCommand(int idStudy)
        {
            var com = new SqlCommand();
            com.CommandText = " Insert into Enrollment " +
                              " Values (@studies,@semester,@data);"; 
            com.Parameters.AddWithValue("studies", idStudy);
            com.Parameters.AddWithValue("semester",1);
            return com;
        }


        private SqlCommand AddStudentCommand(Student st)
        {
            var com = new SqlCommand();
            com.CommandText = " Insert into Student " +
                              " Values (@index,@name,@lastName,@IdEnrollment);";
            com.Parameters.AddWithValue("index", st.IndexNumber);
            com.Parameters.AddWithValue("name", st.FirstName);
            com.Parameters.AddWithValue("lastname", st.LastName);
            com.Parameters.AddWithValue("IdEnrollment", st.IdEnrollment);
            return com;
        }

        Enrollment IDbService.EnrollStudent(EnrollStudentRequest request, int idStudy)
        {
            throw new NotImplementedException();
        }

        Enrollment IDbService.GetEnrollment(string studyName, int semester)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = " Select * " +
                                  " From Studies Join Enrollment On Studies.IdStudy = Enrollment.IdStudy" +
                                  " Where Studies.Name = @name And Enrollment.Semester = @semester;";
                com.Parameters.AddWithValue("name", studyName);
                com.Parameters.AddWithValue("semester", semester);
                client.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return new Enrollment((int)dr["IdEnrollment"], (int)dr["Semester"], new Studies((int)dr["IdStudy"], dr["Name"].ToString()), dr["StartDate"].ToString());
                }
            }
            return null;
        }

        Enrollment IDbService.Promote(Enrollment enroll)
        {
            using (var client = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = " Exec promoteStudents @idEnrollment;";
                com.Parameters.AddWithValue("idEnrollment", enroll.IdEnrollment);
                client.Open();
                var dr = com.ExecuteReader();
                if (dr.Read())
                {
                    return new Enrollment((int)dr["IdEnrollment"], (int)dr["Semester"], new Studies((int)dr["IdStudy"], dr["Name"].ToString()), dr["StartDate"].ToString());
                }
            }
            return null;
        }
    }
}
