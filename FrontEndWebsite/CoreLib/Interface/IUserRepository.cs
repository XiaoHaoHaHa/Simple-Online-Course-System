using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;

namespace CoreLib.Interface
{
    public interface IUserRepository
    {
        UserModel GetUserData(string email);
        UserModel GetUserData(Guid input);
        void UpdatePasswordChange(Guid inputId, string password);
        bool UpdatePersonalData(UserModel inputUser);
        bool CreateUserData(UserModel user);
    }
}
