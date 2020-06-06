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
            if (LoginFieldsNotNullStatic(LoginInformation.userLoginModel))
            {
                if (ArePlainEmailAndPasswordHashMatchingLoginInformationStatic(LoginInformation.userLoginModel, dbContext))
                {
                    //success
                    return true;
                }
            }
            return false;
        }

        public static bool LoginFieldsNotNullStatic(UserLoginModel LoginInformation)
        {
            bool outcome = true;
            if (String.IsNullOrEmpty(LoginInformation.Email)) { outcome = false; }
            if (String.IsNullOrEmpty(LoginInformation.Password)) { outcome = false; }
            return outcome;
        }

        public static bool ArePlainEmailAndPasswordHashMatchingLoginInformationStatic(UserLoginModel LoginInformation, BoardWebAppContext dbContext)
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
            return false;
        }

        public static string CalculateHashForCookieForUserEmailAndDBContext(string userEmail, BoardWebAppContext dbContext)
        {
            string cookieValue = "";

            User userForEmailParameter = dbContext.User.Where(user => user.Email == userEmail).FirstOrDefault(); // password hash related to the incoming email 
            if(userForEmailParameter != null)
            {
                string first64Characters = userForEmailParameter.EmailHash;
                string last64Characters = Models.User.ComputeSha256HashForString(userForEmailParameter.EmailHash + userForEmailParameter.Password);
                // This statement will produce a string that's 128 chars long.
                // First 64 chars will be the EmailHash that's unique for a user - this EmailHash will serve a identifier, to find the user that needs to be authenticated.
                // Last 64 chars will be a hash that will serve as the authentication for all actions that require a certain degree of access priviliges.
                cookieValue = first64Characters + last64Characters;
            }
            
            return cookieValue;
        }
    }
}
