using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;

namespace CoreLib.Interface
{
    public interface IUserService
    {
        bool ChangePassword(ChangePasswordModel input);
        void UpdateToDataBase(ChangePasswordModel input);
        bool UpdateProfileToDataBase(UserModel input);
    }
}
