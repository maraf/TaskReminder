using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Pobočka zákazníka.
    /// </summary>
    public class Office : BaseEntity, IAddress
    {
        [Display(Name="Název")]
        [Required(ErrorMessage = "Prosím, vyplňte název")]
        public string Name { get; set; }

        public virtual Address Address { get; set; }

        [ForeignKey("Company")]
        [Display(Name="Zákazník")]
        [Required(ErrorMessage= "Prosím, vyberte společnost")]
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}
