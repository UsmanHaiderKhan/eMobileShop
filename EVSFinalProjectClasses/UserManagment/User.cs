using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EVSFinalProjectClasses.UserManagment
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Login Id")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email Not Varified !!")]
        public string LoginId { get; set; }


        [Display(Name = "email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "email not varified !!")]

        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Your Password To Short....!")]
        public string Password { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string Country { get; set; }

        public City City { get; set; }//Associate
                                      //  
                                      //public string ImageUrl { get; set; }
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }


        public DateTime? BirthDate { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}?", ErrorMessage = "Your Phone No Is Not Valid")]
        public double Phone { get; set; }

        [Display(Name = "Security Question")]
        public string SecurityQuestion { get; set; }

        [Display(Name = "Security Answer")]
        public string SecurityAnswer { get; set; }
        public virtual Gender Gender { get; set; }
        public Role Role { get; set; }
        public bool IsInRole(int id)
        {
            return (Role != null && Role.Id == id);
        }
    }
}

