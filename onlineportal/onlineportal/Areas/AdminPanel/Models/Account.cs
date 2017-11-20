using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin2.Models
{
    public class Account
    {
        public int id { get; set; }
        [Required (ErrorMessage ="Enter Name")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter User Name")]

        public string User { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }
    }
}