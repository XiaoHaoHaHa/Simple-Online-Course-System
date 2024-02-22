using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;
using CoreLib.Interface;

namespace CoreLib
{
    public class LoginCheck : ILoginCheck
    {
        private IUserRepository _userRepository;

        public LoginCheck(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel CheckResult(string email, string password)
        {
            UserModel user = _userRepository.GetUserData(email);
            if (user == null)
            {
                return null;
            }

            var hashpwd = Sha256Hash.PwdSHA256(password, user.Id.ToString());
            if (user.Password == hashpwd)
            {
                return user;
            }

            return null;
        }
    }
}
