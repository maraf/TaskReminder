using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Uživatel, zaměstnanec
    /// </summary>
    public class User : BaseEntity, IDomain
    {
        [Display(Name="Jméno")]
        [Required(ErrorMessage="Prosím, vyplňte jméno")]
        public string FirstName { get; set; }

        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Prosím, vyplňte příjmení")]
        public string LastName { get; set; }

        [Display(Name = "Uživatelské jméno")]
        [Required(ErrorMessage = "Prosím, vyplňte uživatelské jméno")]
        public string Username { get; set; }

        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        [Display(Name = "Povolený")]
        public bool Enabled { get; set; }

        [Display(Name = "Nadřízený")]
        [ForeignKey("Boss")]
        public int? BossID { get; set; }

        public virtual User Boss { get; set; }

        public Domain Domain { get; set; }

        public User()
        {
            Enabled = true;
        }
    }
}
