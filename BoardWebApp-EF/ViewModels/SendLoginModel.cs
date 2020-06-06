﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardWebApp.ViewModels
{
    public class SendLoginModel
    {
        public UserLoginModel userLoginModel { get; set; }
        public string ErrorMessage { get; set; }
        public string LogoutMessage { get; set; }

        public SendLoginModel() {
            
        }
        public SendLoginModel(string errorMessage) {
            ErrorMessage = errorMessage;
        }
    }
}
