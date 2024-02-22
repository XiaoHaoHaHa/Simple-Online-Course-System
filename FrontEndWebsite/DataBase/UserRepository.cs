using DataBase.DB;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using DataBase.Tables;
using CoreLib.Interface;
using CoreLib.ClientViewModel;

namespace DataBase
{
    public class UserRepository : IUserRepository
    {
        private TainanNetContext _database;

        public UserRepository(TainanNetContext database)
        {
            _database = database;
        }
        public bool CreateUserData(UserModel user)
        {
            Student student = new Student()
            {
                StuId = user.Id,
                StuName = user.Name,
                StuEmail = user.Email,
                StuPhone = user.Phone,
                Password = user.Password
            };
            _database.Student.Add(student);
            _database.SaveChanges();
            return true;
        }

        public UserModel GetUserData(string email)
        {
            var user = _database.Student.Where(i => i.StuEmail.ToUpper() == email.ToUpper()).FirstOrDefault();
            if (user == null)
                return null;
            return new UserModel() { Id = user.StuId, Name = user.StuName, Email = user.StuEmail, Phone = user.StuPhone, Password = user.Password };
        }

        public UserModel GetUserData(Guid input)
        {
            var user = _database.Student.Where(i => i.StuId.ToString() == input.ToString()).FirstOrDefault();
            if (user == null)
                return null;
            return new UserModel() { Id = user.StuId, Name = user.StuName, Email = user.StuEmail, Phone = user.StuPhone, Password = user.Password };
        }

        public bool UpdatePersonalData(UserModel inputUser)
        {
            var user = _database.Student.Where(i => i.StuId.ToString() == inputUser.Id.ToString()).FirstOrDefault();
            if (!string.IsNullOrEmpty(inputUser.Name))
            {
                user.StuName = inputUser.Name;
            }

            if (!string.IsNullOrEmpty(inputUser.Phone))
            {
                user.StuPhone = inputUser.Phone;
            }

            _database.SaveChanges();
            return true;
        }

        public void UpdatePasswordChange(Guid inputId, string password)
        {
            var user = _database.Student.Where(i => i.StuId.ToString() == inputId.ToString()).FirstOrDefault();
            user.Password = password;
            _database.SaveChanges();
        }
    }
}
