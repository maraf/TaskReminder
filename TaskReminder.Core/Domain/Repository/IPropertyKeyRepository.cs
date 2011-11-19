using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IPropertyKeyRepository
    {
        IQueryable<PropertyKey> PropertyKeys { get; }

        void Save(PropertyKey propertyKey);

        void Delete(PropertyKey propertyKey);
    }
}
