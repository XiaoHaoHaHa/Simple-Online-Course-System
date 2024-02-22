using System;
using System.Collections.Generic;
using System.Text;
using CoreLib.ClientViewModel;
using CoreLib.Interface;

namespace CoreLib
{
    public class UserRegister : IUserRegister
    {
        private IUserRepository _dbRepository;

        public UserRegister(IUserRepository repository)
        {
            _dbRepository = repository;
        }
        public bool RegisterProcess(UserModel UserInput)
        {
            UserModel user = _dbRepository.GetUserData(UserInput.Email);

            //Check email account and insert into database
            if (user == null)
            {
                UserInput.Password = Sha256Hash.PwdSHA256(UserInput.Password, UserInput.Id.ToString());
                return _dbRepository.CreateUserData(UserInput); ;
            }

            return false;
        }
    }
}
