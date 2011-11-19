using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskReminder.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Uživ.jméno")]
        public string Username { get; set; }

        [DisplayName("Heslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}