using System.ComponentModel.DataAnnotations;

namespace EVSFinalProjectClasses.ProductMgmt
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        [RegularExpression(@"^((\+92)|(0092))-{0,1}\d{3}-{0,1}\d{7}$|^\d{11}$|^\d{4}-\d{7}$", ErrorMessage = "Your Phone Number is not Valid")]
        public long Phone { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Your Email Formate is Not Valid")]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
