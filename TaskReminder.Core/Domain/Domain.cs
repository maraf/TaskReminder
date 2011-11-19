using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Reprezentuje jednu "instanci" aplikace.
    /// </summary>
    public class Domain : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
