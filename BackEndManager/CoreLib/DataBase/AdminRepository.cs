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
using static System.Net.Mime.MediaTypeNames;

namespace CoreLib.DataBase
{
    public class AdminRepository
    {
        private string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;   
        }

        public AdminModel GetAdminData(string email)
        {
            AdminModel user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("select * from Sysadmin where email = @email");
                command.Parameters.AddWithValue("@email", email);
                command.Connection = connection;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new AdminModel();
                    user.Id = reader["id"].ToString();
                    user.Email = reader["email"].ToString();
                    user.Password = reader["password"].ToString();
                    user.Name = reader["username"].ToString();
                    user.Initdate = reader["initdate"].ToString();
                    user.Modifydate = reader["modifydate"].ToString();
                    user.Priority = int.Parse(reader["priority"].ToString());
                }
                reader.Close();
            }

            return user;
        }

        public void UpdateAdminData(string id, string newPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("update Sysadmin set password = @pwd where id = @id");
                command.Parameters.AddWithValue("@pwd", newPassword);
                command.Parameters.AddWithValue("@id", id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAdminData(AdminModel user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                string header = " update Sysadmin set ";
                string condition = string.Empty;
                if (!string.IsNullOrEmpty(user.Name))
                {
                    condition += " username = @username ";
                    command.Parameters.AddWithValue("@username", user.Name);
                }

                if (!string.IsNullOrEmpty(user.Phone))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " , phone = @phone";
                        command.Parameters.AddWithValue("@phone", user.Phone);
                    }
                    else
                    {
                        condition += " phone = @phone";
                        command.Parameters.AddWithValue("@phone", user.Phone);
                    }
                }

                if (string.IsNullOrEmpty(condition))
                {
                    condition += " username = @username ";
                    command.Parameters.AddWithValue("@username", "");
                    condition += " ,phone = @phone";
                    command.Parameters.AddWithValue("@phone", "");
                }
                string resultSQL = header + condition + " , modifydate = GETDATE()" + " where id = @id";
                command.Parameters.AddWithValue("@id", user.Id);
                command.CommandText = resultSQL;
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ObservableCollection<AdminModel> AdminQuery(string email, string name)
        {
            var result = new ObservableCollection<AdminModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                string header = " select * from Sysadmin ";
                string condition = string.Empty;

                if (!string.IsNullOrEmpty(email))
                {
                    condition += " email = @email ";
                    command.Parameters.AddWithValue("@email", email);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " and username = @name";
                        command.Parameters.AddWithValue("@name", name);
                    }
                    else
                    {
                        condition += " username = @name";
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
                    var model = new AdminModel();
                    model.Name = reader["username"].ToString();
                    model.Email = reader["email"].ToString();
                    model.Id = reader["id"].ToString();
                    model.Phone = reader["phone"].ToString();
                    model.Initdate = reader["initdate"].ToString();
                    model.Modifydate = reader["modifydate"].ToString();
                    model.Priority = int.Parse(reader["priority"].ToString());
                    result.Add(model);
                }
            }
            return result;
        }

        public void AddAdmin(AdminModel newUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" insert into Sysadmin(id, username, email, password, initdate, phone, priority) " +
                " values(@id, @name, @email, @password, GETDATE(), @phone, @priority)");
                command.Parameters.AddWithValue("@name", newUser.Name);
                command.Parameters.AddWithValue("@email", newUser.Email);
                command.Parameters.AddWithValue("@phone", newUser.Phone);
                newUser.Id = Guid.NewGuid().ToString();
                command.Parameters.AddWithValue("@id", Guid.Parse(newUser.Id));
                var hashPwd = HashService.PwdSHA256Demo("123456", newUser.Id);
                command.Parameters.AddWithValue("@password", hashPwd);
                command.Parameters.AddWithValue("@priority", 1);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAdmin(AdminModel user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(" delete from Sysadmin where id = @id ");
                command.Parameters.AddWithValue("@id", user.Id);
                command.Connection = connection;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
