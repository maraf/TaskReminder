using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public class Address : BaseEntity
    {
        [Display(Name="Ulice")]
        [Required(ErrorMessage="Prosím, vyplňte ulici")]
        public string Street { get; set; }


        [Display(Name="Číslo popisné")]
        [Required(ErrorMessage = "Prosím, vyplňte číslo popisné")]
        public string HouseNumber { get; set; }


        [Display(Name="Město")]
        [Required(ErrorMessage = "Prosím, vyplňte město")]
        public string City { get; set; }


        [Display(Name="Smerovací číslo")]
        [Required(ErrorMessage = "Prosím, vyplňte směrovací číslo")]
        public string PostalCode { get; set; }
    }
}
