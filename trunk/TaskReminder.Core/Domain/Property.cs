using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain
{
    /// <summary>
    /// Znovu použivatelná vlastnost, váže se na <see cref="PropertyKey"/>.
    /// </summary>
    /// <typeparam name="T">Cílová entita ke které je přiřazena</typeparam>
    public abstract class Property<T> : BaseEntity where T : IIdentifier
    {
        public T Target { get; set; }
        public PropertyKey Key { get; set; }
        public string Value { get; set; }
    }
}
