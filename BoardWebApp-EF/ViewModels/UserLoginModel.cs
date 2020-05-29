using BoardWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoardWebApp.ViewModels
{
    public class UserLoginModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "No email - no access!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "At least try guessing the password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public static bool LoginCredentialsMatchDatabaseRecord(SendLoginModel LoginInformation, BoardWebAppContext dbContext)
        {
            List<string> ValidationErrorMessages = LoginInformation.ValidationErrorMessages;
            if (LoginFieldsNotNullStatic(LoginInformation.userLoginModel, ValidationErrorMessages))
            {
                if (ArePlainEmailAndPasswordHashMatchingLoginInformationStatic(LoginInformation.userLoginModel, ValidationErrorMessages, dbContext))
                {
                    //success
                    return true;
                }
            }
            return false;
        }

        public static bool LoginFieldsNotNullStatic(UserLoginModel LoginInformation, List<string> ValidationErrorMessages)
        {
            bool outcome = true;
            if (String.IsNullOrEmpty(LoginInformation.Email)) { ValidationErrorMessages.Add("No email - no access!"); outcome = false; }
            if (String.IsNullOrEmpty(LoginInformation.Password)) { ValidationErrorMessages.Add("At least try guessing the password!"); outcome = false; }
            return outcome;
        }

        public static bool ArePlainEmailAndPasswordHashMatchingLoginInformationStatic(UserLoginModel LoginInformation, List<string> ValidationErrorMessages, BoardWebAppContext dbContext)
        {
            User userFromQuery = dbContext.User.Where(user => user.Email == LoginInformation.Email).FirstOrDefault();
            if(userFromQuery != null)
            {
                string passwordHashForLoginEmail = userFromQuery.Password;
                if (String.IsNullOrEmpty(passwordHashForLoginEmail) == false)
                {
                    string LoginPasswordHash = User.ComputeSha256HashForString(LoginInformation.Password);
                    if (String.Equals(passwordHashForLoginEmail, LoginPasswordHash) == true)
                    {
                        return true;
                    }
                }
            }
            ValidationErrorMessages.Add("Invalid credentials - log in failed!");
            return false;
        }

        public static string CalculateHashForCookieForUserEmailAndDBContext(string LoginEmail, BoardWebAppContext dbContext)
        {
            string authenticationCookieHash = "";
            User userForLoginEmail = dbContext.User.Where(user => user.Email == LoginEmail).FirstOrDefault(); // get the user that has the same email as the paramater
            if(userForLoginEmail != null)
            {
                // This statement will produce a string that's 128 chars long.
                // First 64 chars will be the EmailHash that's unique for a user - this EmailHash will serve a identifier, to find the user that needs to be authenticated.
                // Last 64 chars will be a hash that will serve as the authentication for all actions that require a certain degree of access priviliges.
                authenticationCookieHash = userForLoginEmail.EmailHash + User.ComputeSha256HashForString(userForLoginEmail.Password + userForLoginEmail.EmailHash);
            }
            
            return authenticationCookieHash;
        }
    }
}
