using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;

namespace CoreLib.Interface
{
    public interface IUserRegister
    {
        bool RegisterProcess(UserModel UserInput);
    }
}
