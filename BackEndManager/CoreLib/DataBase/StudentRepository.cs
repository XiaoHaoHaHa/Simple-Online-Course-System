using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DataBase
{
    public class StudentRepository
    {
        private string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObservableCollection<StudentModel> StudentQuery(string email, string name)
        {
            var result = new ObservableCollection<StudentModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                string header = " select * from Student ";
                string condition = string.Empty;

                if (!string.IsNullOrEmpty(email))
                {
                    condition += " stuemail = @email ";
                    command.Parameters.AddWithValue("@email", email);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " and stuname = @name";
                        command.Parameters.AddWithValue("@name", name);
                    }
                    else
                    {
                        condition += " stuname = @name";
                        command.Parameters.AddWithValue("@name", name);
                    }
                }

                string resultSQL = header;
                if (!string.IsNullOrEmpty(condition))
                {
                    resultSQL += " where " + condition;
                }

                command.CommandText = resultSQL;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var model = new StudentModel();
                    model.Name = reader["stuname"].ToString();
                    model.Email = reader["stuemail"].ToString();
                    model.Id = reader["stuid"].ToString();
                    model.Phone = reader["stuphone"].ToString();
                    result.Add(model);
                }
            }
            return result;
        }

        public void DeleteStudent(StudentModel student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" delete from Student where stuid = @id ");
                command.Parameters.AddWithValue("@id", student.Id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<StuSelectModel> StuSelect(string stuID)
        {
            var result = new ObservableCollection<StuSelectModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" select Course.courseName, Teacher.teaName, Course.courseHour, Class.sDate, Class.eDate, Class.location " +
                    "from StuSelect inner join Class on Class.id = StuSelect.classID " +
                    "inner join Student on Student.stuID = StuSelect.stuID " +
                    "inner join Teacher on Teacher.teaID = Class.teaID " +
                    "inner join Course on Course.courseID = Class.courseID " +
                    "where StuSelect.stuID = @stuID ");

                command.Parameters.AddWithValue("@stuID", Guid.Parse(stuID));
                command.Connection = connection;
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var model = new StuSelectModel();
                    model.CourseName = reader["courseName"].ToString();
                    model.TeacherName = reader["teaName"].ToString();
                    model.Hour = int.Parse(reader["courseHour"].ToString());
                    model.SDate = DateTime.Parse(reader["sDate"].ToString());
                    model.EDate = DateTime.Parse(reader["eDate"].ToString());
                    model.Location = reader["location"].ToString();
                    result.Add(model);
                }
            }
            return result;
        }
    }
}
