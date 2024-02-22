using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.Interface;
using CoreLib.ClientViewModel;

namespace CoreLib
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool ChangePassword(ChangePasswordModel input)
        {
            var user = _userRepository.GetUserData(Guid.Parse(input.Id));
            if(user == null)
                return false;
            string inputHashPwd = Sha256Hash.PwdSHA256(input.OldPwd, input.Id.ToUpper());
            if(inputHashPwd != user.Password)
                return false;
            return true;
        }

        public bool UpdateProfileToDataBase(UserModel input)
        {
           return _userRepository.UpdatePersonalData(input);
        }

        public void UpdateToDataBase(ChangePasswordModel input)
        {
            var HashPwd = Sha256Hash.PwdSHA256(input.NewPwd, input.Id);
            _userRepository.UpdatePasswordChange(Guid.Parse(input.Id), HashPwd);
        }

    }
}
