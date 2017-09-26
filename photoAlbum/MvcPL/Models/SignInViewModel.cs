using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{
    public class SignInViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "login can't be blank")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "login must contain at least {2} characters")]
        [Remote("ValidateSignIn", "Account")]
        public string Login { get; set; }

        [Required(ErrorMessage = "password can't be blank")]
        [StringLength(100, ErrorMessage = "The password must contain at least {2} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}