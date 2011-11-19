using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IOfficeRepository
    {
        IQueryable<Office> Offices { get; }

        void Save(Office offise);

        void Delete(Office office);
    }
}
