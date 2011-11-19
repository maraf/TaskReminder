using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Models
{
    public class CompanyEditViewModel
    {
        public Company Item { get; set; }

        public IEnumerable<PropertyKey> Keys { get; set; }

        public IEnumerable<CompanyProperty> Properties { get; set; }
    }
}