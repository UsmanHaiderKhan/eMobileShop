using EVSFinalProjectClasses.UserManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace EVSFinalProject.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Login field is required")] 
        [Display(Name = "Login Id")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email Not Varified !!")]
        public string Loginid { get; set; }
        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}