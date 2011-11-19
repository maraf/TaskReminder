using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public abstract class Attachment : BaseEntity
    {
        [Required(ErrorMessage="Prosím, vyplňte název")]
        [Display(Name="Název")]
        public string Name { get; set; }

        [Display(Name="Popis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [HiddenInput(DisplayValue=false)]
        public string ContentType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ContentLength { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FileName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [ForeignKey("Creator")]
        [HiddenInput(DisplayValue=false)]
        public int CreatorID { get; set; }

        public User Creator { get; set; }
    }
}
