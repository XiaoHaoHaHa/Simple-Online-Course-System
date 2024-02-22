using CoreLib.Model;
using CoreLib.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DataBase
{
    public class TeacherRepository
    {
        private string _connectionString;

        public TeacherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObservableCollection<TeacherModel> GetTeacher(string email = null, string name = null)
        {
            var result = new ObservableCollection<TeacherModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                string header = " select * from Teacher ";
                string condition = string.Empty;

                if (!string.IsNullOrEmpty(email))
                {
                    condition += " teaEmail = @email ";
                    command.Parameters.AddWithValue("@email", email);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " and teaName = @name";
                        command.Parameters.AddWithValue("@name", name);
                    }
                    else
                    {
                        condition += " teaName = @name";
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
                    var teacher = new TeacherModel();
                    teacher.Id = reader["teaID"].ToString();
                    teacher.TeacherName = reader["teaName"].ToString();
                    teacher.Email = reader["teaEmail"].ToString();
                    teacher.Phone = reader["teaPhone"].ToString();
                    result.Add(teacher);
                }
                reader.Close();
            }
            return result;
        }

        public void UpdateTeacherData(TeacherModel teacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                string header = " update Teacher set ";
                string condition = string.Empty;
                if (!string.IsNullOrEmpty(teacher.TeacherName))
                {
                    condition += " teaName = @teacherName ";
                    command.Parameters.AddWithValue("@teacherName", teacher.TeacherName);
                }

                if (!string.IsNullOrEmpty(teacher.Phone))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , teaPhone = @phone";
                        command.Parameters.AddWithValue("@phone", teacher.Phone);
                    }
                    else
                    {
                        condition += " teaPhone = @phone";
                        command.Parameters.AddWithValue("@phone", teacher.Phone);
                    }
                }

                if (string.IsNullOrEmpty(condition))
                {
                    condition += " teaName = @teacherName ";
                    command.Parameters.AddWithValue("@teacherName", "");
                    condition += " ,teaPhone = @phone";
                    command.Parameters.AddWithValue("@phone", "");
                }
                string resultSQL = header + condition + " where teaID = @id";
                command.Parameters.AddWithValue("@id", teacher.Id);
                command.CommandText = resultSQL;
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTeacher(TeacherModel teacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" delete from Teacher where teaID = @id ");
                command.Parameters.AddWithValue("@id", teacher.Id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddTeacher(TeacherModel newTeacher)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" insert into Teacher(teaID, teaName, teaEmail, teaPhone) " +
                " values(@id, @name, @email, @phone)");
                command.Parameters.AddWithValue("@name", newTeacher.TeacherName);
                command.Parameters.AddWithValue("@email", newTeacher.Email);
                command.Parameters.AddWithValue("@phone", newTeacher.Phone);
                newTeacher.Id = Guid.NewGuid().ToString();
                command.Parameters.AddWithValue("@id", Guid.Parse(newTeacher.Id));
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
