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
    public class CourseRepository
    {
        private string _connectionString;

        public CourseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObservableCollection<CourseModel> GetCourse(string name = null)
        {
            var result = new ObservableCollection<CourseModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("select * from Course");
                command.Connection = connection;

                string header = " select * from Course ";
                string condition = string.Empty;

                if (!string.IsNullOrEmpty(name))
                {
                    condition += " CourseName = @name ";
                    command.Parameters.AddWithValue("@name", name);
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
                    var course = new CourseModel();
                    course.Id = reader["courseID"].ToString();
                    course.CourseName = reader["CourseName"].ToString();
                    course.Hour = int.Parse(reader["courseHour"].ToString());
                    result.Add(course);
                }
                reader.Close();
            }
            return result;
        }

        public void UpdateCourseData(CourseModel course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                string header = " update Course set ";
                string condition = string.Empty;
                if (!string.IsNullOrEmpty(course.CourseName))
                {
                    condition += " courseName = @courseName ";
                    command.Parameters.AddWithValue("@courseName", course.CourseName);
                }

                if (!string.IsNullOrEmpty(course.Hour.ToString()))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , courseHour = @hour";
                        command.Parameters.AddWithValue("@hour", course.Hour);
                    }
                    else
                    {
                        condition += " courseHour = @hour";
                        command.Parameters.AddWithValue("@hour", course.Hour);
                    }
                }

                if (string.IsNullOrEmpty(condition))
                {
                    condition += " courseName = @courseName ";
                    command.Parameters.AddWithValue("@courseName", "");
                    condition += " ,courseHour = @hour";
                    command.Parameters.AddWithValue("@hour", "");
                }
                string resultSQL = header + condition + " where courseID = @id";
                command.Parameters.AddWithValue("@id", course.Id);
                command.CommandText = resultSQL;
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCourse(CourseModel course)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" delete from Course where courseID = @id ");
                command.Parameters.AddWithValue("@id", course.Id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddCourse(CourseModel newCourse)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" insert into Course(courseID, courseName, courseHour) " +
                " values(@id, @name, @hour)");
                command.Parameters.AddWithValue("@name", newCourse.CourseName);
                command.Parameters.AddWithValue("@hour", newCourse.Hour);
                newCourse.Id = Guid.NewGuid().ToString();
                command.Parameters.AddWithValue("@id", Guid.Parse(newCourse.Id));
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
