using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Entita, která je přiřazena k doméně.
    /// </summary>
    public interface IDomain
    {
        Domain Domain { get; set; }
    }
}
