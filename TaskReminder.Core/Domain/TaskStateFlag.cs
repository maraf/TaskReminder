using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Označení speciálních stavů.
    /// </summary>
    public static class TaskStateFlag
    {
        /// <summary>
        /// Bez příznaku
        /// </summary>
        public static readonly int None = 0;

        /// <summary>
        /// Základní stav po vytvoření
        /// </summary>
        public static readonly int Created = 1;
        
        /// <summary>
        /// Přiřaný
        /// </summary>
        public static readonly int Assigned = 2;

        /// <summary>
        /// Odmítnutý
        /// </summary>
        public static readonly int Rejected = 3;

        /// <summary>
        /// Pr8vě zpracovávaný
        /// </summary>
        public static readonly int Handled = 4;
        
        /// <summary>
        /// Hotový
        /// </summary>
        public static readonly int Completed = 5;

        /// <summary>
        /// Hotový, schválený vedoucím, k vyůčtování
        /// </summary>
        public static readonly int Approved = 6;

        /// <summary>
        /// Archivovaný, vyúčtovaný
        /// </summary>
        public static readonly int Archived = 7;
    }
}
