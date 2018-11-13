using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProfileApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Enter email in correct format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password, ErrorMessage = "Please enter valid password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of the name should be 2 - 25 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter surname")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of the surname should be 2 - 25 characters long")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter login")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of the login should be 2 - 25 characters long")]
        public string Login { get; set; }
        public HttpPostedFileBase Photo { get; set; }
    }
}