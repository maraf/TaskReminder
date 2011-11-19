using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TaskReminder.Core.Domain;

namespace TaskReminder.Core.EntityFramework
{
    public class DataContext : DbContext
    {
        public DbSet<Domain.Domain> Domains { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskAttachment> TaskAttachments { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyProperty> CompanyProperties { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<PropertyKey> PropertyKeys { get; set; }

        public DbSet<TaskState> TaskStates { get; set; }
    }
}
