using CoreLib.Model;
using CoreLib.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreLib.DataBase
{
    public class ClassRepository
    {
        private string _connectionString;

        public ClassRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObservableCollection<ClassModel> GetClassData(string course, string teacher)
        {
            var result = new ObservableCollection<ClassModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                string header = " select Class.id, Course.courseName, Teacher.teaName, Class.sDate, Class.eDate, Class.location, Class.description from Class inner join Course on Class.courseID = Course.courseID inner join Teacher on Class.teaID = Teacher.teaID ";
                string condition = string.Empty;

                if (!string.IsNullOrEmpty(course))
                {
                    condition += " Course.courseName like '%'+@course+'%' ";
                    command.Parameters.AddWithValue("@course", course);
                }

                if (!string.IsNullOrEmpty(teacher))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " and Teacher.teaName like '%'+@teacher+'%' ";
                        command.Parameters.AddWithValue("@teacher", teacher);
                    }
                    else
                    {
                        condition += " Teacher.teaName like '%'+@teacher+'%' ";
                        command.Parameters.AddWithValue("@teacher", teacher);
                    }
                }

                //Combine
                string resultSQL = header;
                if (!string.IsNullOrEmpty(condition))
                {
                    resultSQL += " where " + condition;
                }
                resultSQL += " order by courseName ";

                command.CommandText = resultSQL;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var model = new ClassModel();
                    model.Id = reader["id"].ToString();
                    model.CourseName = reader["courseName"].ToString();
                    model.TeacherName = reader["teaName"].ToString();
                    model.SDate = DateTime.Parse(reader["sDate"].ToString());
                    model.EDate = DateTime.Parse(reader["eDate"].ToString());
                    model.Location = reader["location"].ToString();
                    model.Description = reader["description"].ToString();
                    result.Add(model);
                }

            }
            return result;
        }

        public void UpdateClassData(ClassModel classData)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                string header = " update Class set ";
                string condition = string.Empty;
                if (!string.IsNullOrEmpty(classData.SDate.ToString()))
                {
                    condition += " sDate = @sDate ";
                    command.Parameters.AddWithValue("@sDate", classData.SDate);
                }

                if (!string.IsNullOrEmpty(classData.EDate.ToString()))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , eDate = @eDate";
                        command.Parameters.AddWithValue("@eDate", classData.EDate);
                    }
                    else
                    {
                        condition += " eDate = @eDate";
                        command.Parameters.AddWithValue("@eDate", classData.EDate);
                    }
                }

                if (!string.IsNullOrEmpty(classData.Location))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , location = @location";
                        command.Parameters.AddWithValue("@location", classData.Location);
                    }
                    else
                    {
                        condition += " location = @location";
                        command.Parameters.AddWithValue("@location", classData.Location);
                    }
                }

                if (!string.IsNullOrEmpty(classData.Description))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , description = @description";
                        command.Parameters.AddWithValue("@description", classData.Description);
                    }
                    else
                    {
                        condition += " description = @description";
                        command.Parameters.AddWithValue("@description", classData.Description);
                    }
                }

                if (string.IsNullOrEmpty(condition))
                {
                    condition += " sDate = @sDate ";
                    command.Parameters.AddWithValue("@sDate", "");
                    condition += " ,eDate = @eDate";
                    command.Parameters.AddWithValue("@eDate", "");
                    condition += " ,location = @location";
                    command.Parameters.AddWithValue("@location", "");
                }
                string resultSQL = header + condition + " where id = @id";
                command.Parameters.AddWithValue("@id", classData.Id);
                command.CommandText = resultSQL;
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddClass(ClassModel input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" insert into Class(id, courseID, teaID, sDate, eDate, location, description) " +
                " values(@id, @cid, @tid, @sdate, @edate, @location, @description)");
                command.Parameters.AddWithValue("@id", Guid.Parse(input.Id));
                command.Parameters.AddWithValue("@cid", input.CourseID);
                command.Parameters.AddWithValue("@tid", input.TeacherID);
                command.Parameters.AddWithValue("@sdate", input.SDate);
                command.Parameters.AddWithValue("@edate", input.EDate);
                command.Parameters.AddWithValue("@location", input.Location);
                if (string.IsNullOrEmpty(input.Description))
                {
                    command.Parameters.AddWithValue("@description", "");
                }
                else
                {
                    command.Parameters.AddWithValue("@description", input.Description);
                }
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteClass(ClassModel classModel)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" delete from Class where id = @id ");
                command.Parameters.AddWithValue("@id", classModel.Id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
