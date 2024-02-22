using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.ClientViewModel
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }
        public string Phone { get; set; }

        public UserModel()
        {
            Id = Guid.NewGuid();
        }
    }
}
