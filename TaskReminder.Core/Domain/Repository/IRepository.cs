using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IRepository : IDomainRepository, IUserRepository, ITaskRepository, IOfficeRepository, ICompanyRepository, IPropertyKeyRepository, ITaskStateRepository, IAttachmentRepository
    {
    }
}
