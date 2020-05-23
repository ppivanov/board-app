using BoardWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoardWebApp.ViewModels
{
    public class UserRegistrationModel
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        //private string _Email;

        [Display(Name = "Email address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //This regex allows only strings that are 8 to 32 characters long and match any 3 of the conditions:
        //has 1 lower case letter
        //has 1 upper case letter
        //has 1 numeric character
        //has 1 special character
        //Acceptable password - P@ssword
        // Still not completely secure but it's going to waste a few minutes of someone's time
        [RegularExpression(@"(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).{8,32}$)?(^(?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?",
            ErrorMessage = "Password must be 8 to 32 characters long and must match at least 3 of the conditions:<br/>- has 1 lower case letter<br/>- has 1 upper case letter<br/>- has 1 numeric character<br/>- has 1 special character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public static bool SaveUser(UserRegistrationModel newUserData, BoardWebAppContext dbContext)
        {
            try
            {
                string hashedPassword = Models.User.ComputeSha256HashForString(newUserData.Password);
                string hashedEmail = Models.User.ComputeSha256HashForString(newUserData.Email);
                var newUser = new User {
                    Email = newUserData.Email,
                    FirstName = newUserData.FirstName,
                    LastName = newUserData.LastName,
                    Password = hashedPassword,
                    EmailHash = hashedEmail
                };

                dbContext.Add<User>(newUser);
                dbContext.SaveChanges();

                return true;
            } 
            catch
            {
                return false;
            }
        }

        /* Back end Validations */
        public static bool PassedAllRegistrationValidations(List<string> ValidationErrorMessages)
        {
            if (ValidationErrorMessages == null || ValidationErrorMessages.Count == 0) 
                return true;
            else
                return false;
        }

        // This method groups all validations for the fields on the Registration page.
        // It is meant to be called from AccountController that will pass in the parameters:
        //  - registrationInformation     => Holds all the data from the page. Has a List<string> meant for validation error messages.
        //  - dbContext                   => Holds the BoardWebAppContext used to run queries against the database.
        public static void UserRegistrationValidationsStatic(SendRegisterationModel registrationInformation, BoardWebAppContext dbContext)
        {
            UserRegistrationModel newUserData = registrationInformation.userRegistrationModel;
            List<string> ValidationErrorMessages = registrationInformation.ValidationErrorMessages;

            if(FieldsNotEmptyValidationsStatic(newUserData, ValidationErrorMessages) == true)
            {
                EmailValidationsStatic(newUserData.Email, ValidationErrorMessages, dbContext);
                PasswordValidatonsStatic(newUserData.Password, newUserData.ConfirmPassword, ValidationErrorMessages);
            }
        }

        /************ ------------ FIELDS NOT EMPTY VALIDATIONS ------------ ************/
        public static bool FieldsNotEmptyValidationsStatic(UserRegistrationModel newUserData, List<string> ValidationErrorMessages)
        {
            bool fieldsNotEmpty = true;
            if (String.IsNullOrEmpty(newUserData.Email)) { ValidationErrorMessages.Add("Email is required"); fieldsNotEmpty = false; }
            if (String.IsNullOrEmpty(newUserData.FirstName)) { ValidationErrorMessages.Add("First name is required"); fieldsNotEmpty = false; }
            if (String.IsNullOrEmpty(newUserData.LastName)) { ValidationErrorMessages.Add("Last name is required"); fieldsNotEmpty = false; }
            if (String.IsNullOrEmpty(newUserData.Password)) { ValidationErrorMessages.Add("Password is required"); fieldsNotEmpty = false; }
            if (String.IsNullOrEmpty(newUserData.ConfirmPassword)) { ValidationErrorMessages.Add("Please confirm your password"); fieldsNotEmpty = false; }
            return fieldsNotEmpty;
        }
        /************ ------------ END OF FIELDS NOT EMPTY VALIDATIONS ------------ ************/



        /************ ------------ EMAIL VALIDATIONS ------------ ************/
        // This method runs all email validations.
        // dbContext is passed to all validating 'submethods' from AccountController to avoid creating multiple instances.
        public static void EmailValidationsStatic(string incomingEmail, List<string> ValidationErrorMessages, BoardWebAppContext dbContext) {

            // If the incoming email fails any Validations, the ValidationErrorMessages list will be updated.
            IsIncomingEmailInValidEmailFormatForErrorMessage(incomingEmail, ValidationErrorMessages);
            IsEmailUsedByAnotherUserStaticForErrorMessage(incomingEmail, dbContext, ValidationErrorMessages);
        }

        // This method takes in paramaters for:
        //  - incomingEmail             => The email you wish to run the validation for.
        //  - ValidationErrorMessage    => The list of error messages that will be modified if the incomingEmail doesn't pass the validation 
        // NB:Lists are passed by reference, so updating a List<> or array in a method is actually updating the original collection;
        public static void IsIncomingEmailInValidEmailFormatForErrorMessage(string incomingEmail, List<string> ValidationErrorMessages)
        {
            if (IsIncomingEmailInValidEmailFormatStatic(incomingEmail) == false)
            {
                ValidationErrorMessages.Add("Please enter a valid email address!");
            }
        }

        // Returns true if the email is in an acceptable form and false otherwise.
        public static bool IsIncomingEmailInValidEmailFormatStatic(string incomingEmail)
        {
            try
            {
                var email = new System.Net.Mail.MailAddress(incomingEmail);
                return (email.Address == incomingEmail);
            }
            catch
            {
                return false;
            }
        }

        // This method takes in parameters for:
        //  - incomingEmail             => The email you wish to run the validation for.
        //  - dbContext                 => The BoardWebAppContext that will be used to run the query.
        //  - ValidationErrorMessage    => The list of error messages that will be modified if the incomingEmail doesn't pass the validation 
        public static void IsEmailUsedByAnotherUserStaticForErrorMessage(string incomingEmail, BoardWebAppContext dbContext, List<string> ValidationErrorMessages)
        {
            if (IsEmailUsedByAnotherUserStatic(incomingEmail, dbContext))
            {
                ValidationErrorMessages.Add("Email is already in use by another user!");
            }
        }

        // This method will return true if the incomingEmail is acossiated with an existing User and false otherwise.
        // Have to pass in BoardWebAppContext instance to run the query.
        public static bool IsEmailUsedByAnotherUserStatic(string incomingEmail, BoardWebAppContext dbContext)
        {
            var UserStore = dbContext.User.Where(user => user.Email == incomingEmail);
            var userFromQuery = UserStore.FirstOrDefault();
            //If an email is already acossiated with another user 
            if (userFromQuery != null)
                return true;
            // else the email is not related to any users and is free to use
            else
                return false;
        }
        /************ ------------ END OF EMAIL VALIDATIONS ------------ ************/

        /************ ------------ PASSWORD VALIDATIONS ------------ ************/

        public static void PasswordValidatonsStatic(string password, string confirmPassword, List<string> ValidationErrorMessages)
        {
            if (PasswordMatchesAtLeastThreeComplexityConditionsStatic(password) == false)
            {
                ValidationErrorMessages.Add("Password must be 8 to 32 characters long and must match at least 3 of the conditions:" +
                    "<br/>- has 1 lower case letter<br/>- has 1 upper case letter<br/>- has 1 numeric character<br/>- has 1 special character");
            }

            if (String.Equals(password, confirmPassword) == false)
            {
                ValidationErrorMessages.Add("'Confirm password' and 'Password' do not match.");
            }
        }

        public static bool PasswordMatchesAtLeastThreeComplexityConditionsStatic(string password) {

            if (Regex.IsMatch(password,
                @"(^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$)?(^(?=.*\d)(?=.*[a-z])(?=.*[@#$%^&+=]).{8,32}$)?(^
                (?=.*\d)(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?(^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,32}$)?"))
                    return true;
            else 
                return false;
        }

/************ ------------ END PASSWORD VALIDATIONS ------------ ************/
    }
}
