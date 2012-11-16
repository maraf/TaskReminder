using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public class TaskTemplate : BaseEntity
    {
        [Display(Name = "Název úkolu")]
        [Required(ErrorMessage = "Prosím, vyplňte jméno úkolu")]
        public string Name { get; set; }

        [Display(Name = "Popis")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Prosím, vyplňte popis")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Vytvořeno")]
        public DateTime Created { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Přiřazeno")]
        public DateTime? Assigned { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Domain Domain { get; set; }

        [ForeignKey("Office")]
        [Display(Name = "Provozovna")]
        [Required(ErrorMessage = "Prosím, vyplňte provozovnu zákazníka")]
        public int OfficeID { get; set; }
        public virtual Office Office { get; set; }

        [ForeignKey("CreatedBy")]
        [Display(Name = "Vytvořil")]
        public int CreatedByID { get; set; }
        public virtual User CreatedBy { get; set; }

        [ForeignKey("AssignedTo")]
        [Display(Name = "Pracovník")]
        public int? AssignedToID { get; set; }
        public virtual User AssignedTo { get; set; }

        [ForeignKey("TaskState")]
        [Display(Name = "Stav")]
        public int TaskStateID { get; set; }
        public virtual TaskState TaskState { get; set; }

        [Display(Name = "Automaticky opakovat")]
        public bool AutoRepeat { get; set; }

        [Display(Name = "Opakování")]
        public int Period { get; set; }

        [Display(Name = "Dokončit v měsíci")]
        public int CompleteInMonth { get; set; }

        [Display(Name = "Dokončit v den")]
        [Range(1, 31, ErrorMessage = "Hodnota musí být platný den v měsíci")]
        public int CompleteInDay { get; set; }

        [Display(Name = "Připomenout dní před")]
        public int? RemindDaysBefore { get; set; }


        public Task AsTask()
        {
            return new Task
            {
                Name = Name,
                Description = Description,
                Created = Created,
                Assigned = Assigned,
                Domain = Domain,
                OfficeID = OfficeID,
                Office = Office,
                CreatedByID = CreatedByID,
                CreatedBy = CreatedBy,
                AssignedToID = AssignedToID,
                AssignedTo = AssignedTo,
                TaskStateID = TaskStateID,
                TaskState = TaskState,
                TaskTemplateID = ID
            };
        }
    }

    public static class TemplatePeriods
    {
        /// <summary>
        /// Měsíční opakování.
        /// </summary>
        public const int Monthly = 1;

        /// <summary>
        /// Čtvrtletní opakování.
        /// </summary>
        public const int Quarterly = 3;

        /// <summary>
        /// Pololetní opakování.
        /// </summary>
        public const int Biannually = 6;

        /// <summary>
        /// Roční opakování.
        /// </summary>
        public const int PerAnnum = 12;

        public static IEnumerable<SelectListItem> AsSelectList(int? value = null)
        {
            yield return new SelectListItem
            {
                Text = "Měsíčně",
                Value = Monthly.ToString(),
                Selected = value != null && value.Value == Monthly
            };
            yield return new SelectListItem
            {
                Text = "Čtvrtletně",
                Value = Quarterly.ToString(),
                Selected = value != null && value.Value == Quarterly
            };
            yield return new SelectListItem
            {
                Text = "Polopetně",
                Value = Biannually.ToString(),
                Selected = value != null && value.Value == Biannually
            };
            yield return new SelectListItem
            {
                Text = "Ročně",
                Value = PerAnnum.ToString(),
                Selected = value != null && value.Value == PerAnnum
            };
        }
    }
}
