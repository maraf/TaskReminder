using System.ComponentModel.DataAnnotations;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Models
{
    public class UserChangeViewModel
    {
        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Prosím, vyplňte jméno")]
        public string FirstName { get; set; }

        [Display(Name = "Příjmení")]
        [Required(ErrorMessage = "Prosím, vyplňte příjmení")]
        public string LastName { get; set; }

        public UserChangeViewModel()
        {

        }

        public UserChangeViewModel(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}