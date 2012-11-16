using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Zákazník v doméně
    /// </summary>
    public class Company : BaseEntity, IDomain, IAddress
    {
        [Display(Name="Název")]
        [Required(ErrorMessage = "Prosím, vyplňte název")]
        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public virtual Domain Domain { get; set; }

        public bool Deleted { get; set; }
    }
}
