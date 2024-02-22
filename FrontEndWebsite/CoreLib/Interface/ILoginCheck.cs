using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;

namespace CoreLib.Interface
{
    public interface ILoginCheck
    {
        UserModel CheckResult(string email, string password);
    }
}
