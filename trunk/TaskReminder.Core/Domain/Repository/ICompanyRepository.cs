using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface ICompanyRepository
    {
        IQueryable<Company> Companies { get; }

        IQueryable<CompanyProperty> CompanyProperties { get; }

        void Save(Company company);

        void Save(params CompanyProperty[] properties);

        void Delete(Company company);
    }
}
