using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TaskReminder.Core.Domain
{
    public abstract class BaseEntity : IIdentifier
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public bool IsNew
        {
            get { return ID == 0; }
        }
    }
}
