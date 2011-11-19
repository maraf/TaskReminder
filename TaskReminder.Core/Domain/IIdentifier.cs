using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Pomocí ID identifikovatelný objekt.
    /// </summary>
    public interface IIdentifier
    {
        int ID { get; set; }
    }
}
