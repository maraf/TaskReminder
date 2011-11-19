using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskReminder.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Display(Name="Nové heslo")]
        [Required(ErrorMessage = "Prosím, zadejte nové heslo o délce alespoň 5 znaků")]
        [MinLength(5, ErrorMessage="Prosím, zadejte heslo o délce alespoň 5 znaků")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name="Nové heslo znovu")]
        [Required(ErrorMessage = "Prosím, zadejte nové heslo")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage="Hesla se musejí shodovat")]
        public string NewPasswordAgain { get; set; }
    }
}