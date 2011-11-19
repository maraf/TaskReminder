using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Klíč vlastnosti.
    /// </summary>
    public class PropertyKey : BaseEntity, IIdentifier, IDomain
    {
        public string Target { get; set; }
        public string Name { get; set; }

        public Domain Domain { get; set; }
    }
}
