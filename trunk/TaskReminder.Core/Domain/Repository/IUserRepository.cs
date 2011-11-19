using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskReminder.Core.Domain.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        void Save(User user);

        void Delete(User user);
    }
}
