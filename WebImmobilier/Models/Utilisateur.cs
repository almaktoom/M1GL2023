using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace WebImmobilier.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessageResourceName = "NomUserRequiredError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [MaxLength(100, ErrorMessageResourceName = "NomUserMaxLengthError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [Display(Name = "NomUser", ResourceType = typeof(ResourceImmo))]
        public string NomUser { get; set; }

        [Required(ErrorMessageResourceName = "PrenomUserRequiredError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [MaxLength(100, ErrorMessageResourceName = "PrenomUserMaxLengthError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [Display(Name = "PrenomUser", ResourceType = typeof(ResourceImmo))]
        public string PrenomUser { get; set; }

        [Required(ErrorMessageResourceName = "LoginRequiredError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [MaxLength(100, ErrorMessageResourceName = "LoginMaxLengthError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [Display(Name = "Login", ResourceType = typeof(ResourceImmo))]
        public string Login { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequiredError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [MaxLength(50, ErrorMessageResourceName = "PasswordMaxLengthError", ErrorMessageResourceType = typeof(ResourceImmo))]
        [Display(Name = "Password", ResourceType = typeof(ResourceImmo))]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessageResourceName = "PasswordFormatError", ErrorMessageResourceType = typeof(ResourceImmo))]
        public string Password { get; set; }

        //[Required(ErrorMessageResourceName = "EmailRequiredError", ErrorMessageResourceType = typeof(ResourceImmo))]
        //[EmailAddress(ErrorMessageResourceName = "EmailFormatError", ErrorMessageResourceType = typeof(ResourceImmo))]
        //public string Email { get; set; }
    }
}

