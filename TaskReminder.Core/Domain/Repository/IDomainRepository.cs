using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IDomainRepository
    {
        IQueryable<Domain> Domains { get; }

        void Save(Domain domain);

        void Delete(Domain domain);
    }
}
