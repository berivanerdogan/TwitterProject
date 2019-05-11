using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterProject.UI.Models.VM
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "FirstName Error!!")]
        public string FirsName { get; set; }
        [Required(ErrorMessage = "LastName Error!!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Error!!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "UserName Error!!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password   Error!!")]
        public string Password { get; set; }
    }
}