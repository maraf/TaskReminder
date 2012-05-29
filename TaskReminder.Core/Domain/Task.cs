using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Úkol.
    /// </summary>
    public class Task : BaseEntity, IDomain
    {
        [Display(Name="Název úkolu")]
        [Required(ErrorMessage = "Prosím, vyplňte jméno úkolu")]
        public string Name { get; set; }

        [Display(Name="Popis")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Prosím, vyplňte popis")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Vytvořeno")]
        public DateTime Created { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name="Přiřazeno")]
        public DateTime? Assigned { get; set; }

        [Display(Name = "Datum požadovaného dokončení")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ToComplete { get; set; }

        [Display(Name = "Datum skutečného dokončení")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? Completed { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Domain Domain { get; set; }

        [ForeignKey("Office")]
        [Display(Name="Provozovna")]
        [Required(ErrorMessage = "Prosím, vyplňte provozovnu zákazníka")]
        public int OfficeID { get; set; }
        public virtual Office Office { get; set; }

        [Display(Name="Vytvořil")]
        public int CreatedByID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("AssignedTo")]
        [Display(Name="Pracovník")]
        public int? AssignedToID { get; set; }
        public virtual User AssignedTo { get; set; }

        [ForeignKey("TaskState")]
        [Display(Name="Stav")]
        public int TaskStateID { get; set; }
        public virtual TaskState TaskState { get; set; }

        [ForeignKey("TaskTemplate")]
        public int? TaskTemplateID { get; set; }
        public virtual TaskTemplate TaskTemplate { get; set; }
    }
}
