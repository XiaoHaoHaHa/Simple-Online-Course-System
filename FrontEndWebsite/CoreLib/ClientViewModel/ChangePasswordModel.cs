using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLib.ClientViewModel
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string OldPwd { get; set; }
        public string NewPwd { get; set; }
        public string ComfirmPwd { get; set; }
    }
}
