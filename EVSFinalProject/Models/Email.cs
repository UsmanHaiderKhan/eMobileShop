using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVSFinalProject.Models
{
    public class Email
    {
        [Required]
        [EmailAddress()]
        [Display(Name = "Email")]
        public string email { get; set; }

    }
}