using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.ViewModels
{
    public class SendRegisterationModel
    {
        public UserRegistrationModel userRegistrationModel { get; set; }
        public List<string> ValidationErrorMessages { get; set; }

        public SendRegisterationModel() {  }
    }
}
