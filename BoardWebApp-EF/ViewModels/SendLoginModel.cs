using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.ViewModels
{
    public class SendLoginModel
    {
        public UserLoginModel userLoginModel { get; set; }
        public List<string> ValidationErrorMessages { get; set; }

        public SendLoginModel() { }
    }
}
