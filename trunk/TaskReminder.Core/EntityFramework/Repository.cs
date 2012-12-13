using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TaskReminder.Core.Domain;
using TaskReminder.Core.Domain.Repository;

namespace TaskReminder.Core.EntityFramework
{
    public class Repository : BaseRepository, IRepository
    {
        #region Queryable

        public IQueryable<Domain.Domain> Domains
        {
            get { return context.Domains; }
        }

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public IQueryable<Task> Tasks
        {
            get { return context.Tasks.OfType<Task>(); }
        }

        public IQueryable<TaskTemplate> TaskTemplates
        {
            get { return context.TaskTemplates.OfType<TaskTemplate>(); }
        }

        public IQueryable<Company> Companies
        {
            get { return context.Companies.Where(c => !c.Deleted); }
        }

        public IQueryable<CompanyProperty> CompanyProperties
        {
            get { return context.CompanyProperties; }
        }

        public IQueryable<Office> Offices
        {
            get { return context.Offices.Where(o => !o.Deleted); }//.Include("Address"); }
        }

        public IQueryable<PropertyKey> PropertyKeys
        {
            get { return context.PropertyKeys; }
        }

        public IQueryable<TaskState> TaskStates
        {
            get { return context.TaskStates; }
        }

        public IQueryable<TaskAttachment> TaskAttachments
        {
            get { return context.TaskAttachments; }
        }

        #endregion

        #region Save

        public void Save(Domain.Domain domain)
        {
            Save(domain, d => context.Domains.Add(d), d => context.Entry(d).State = EntityState.Modified);
        }

        public void Save(User user)
        {
            Save(user, u => context.Users.Add(u), u => context.Entry(u).State = EntityState.Modified);
        }

        public void Save(Task task)
        {
            Save(task, t => context.Tasks.Add(t), t => context.Entry(t).State = EntityState.Modified);
        }

        public void Save(TaskTemplate task)
        {
            Save(task, t => context.TaskTemplates.Add(t), t => context.Entry(t).State = EntityState.Modified);
        }

        public void Save(TaskAttachment attachment)
        {
            Save(attachment, a => context.TaskAttachments.Add(a), a => context.Entry(a).State = EntityState.Modified);
        }

        public void Save(Company company)
        {
            Save(company, c => context.Companies.Add(c), c =>  {
                context.Entry(c).State = EntityState.Modified;
                context.Entry(c.Address).State = EntityState.Modified;
            });
        }

        public void Save(params CompanyProperty[] properties)
        {
            foreach (var item in properties)
                Save(item, p => context.CompanyProperties.Add(p), p => context.Entry(p).State = EntityState.Modified);
        }

        public void Save(Office office)
        {
            Save(office, o => context.Offices.Add(o), o =>
            {
                context.Entry(o).State = EntityState.Modified;
                context.Entry(o.Address).State = EntityState.Modified;
            });
        }

        public void Save(PropertyKey propertyKey)
        {
            Save(propertyKey, k => context.PropertyKeys.Add(k), k => context.Entry(k).State = EntityState.Modified);
        }

        public void Save(TaskState taskState)
        {
            Save(taskState, s => context.TaskStates.Add(s), s => context.Entry(s).State = EntityState.Modified);
        }

        protected void Save<T>(T entity, Action<T> add, Action<T> modify) where T : IIdentifier
        {
            if (entity.ID == 0)
                add(entity);
            else
                modify(entity);

            context.SaveChanges();
        }

        #endregion

        #region Delete

        public void Delete(Domain.Domain domain)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Task task)
        {
            foreach (TaskAttachment attachment in TaskAttachments.Where(ta => ta.Task.ID == task.ID))
                Delete(attachment);

            context.Tasks.Remove(task);
            context.SaveChanges();
        }

        public void Delete(TaskTemplate task)
        {
            foreach (TaskAttachment attachment in TaskAttachments.Where(ta => ta.Task.ID == task.ID))
                Delete(attachment);

            context.TaskTemplates.Remove(task);
            context.SaveChanges();
        }

        public void Delete(TaskAttachment attachment)
        {
            context.TaskAttachments.Remove(attachment);
            context.SaveChanges();
        }

        public void Delete(Office office)
        {
            office.Deleted = true;
            context.SaveChanges();
        }

        public void Delete(Company company)
        {
            company.Deleted = true;
            context.SaveChanges();
        }

        public void Delete(PropertyKey propertyKey)
        {
            foreach (CompanyProperty property in CompanyProperties.Where(p => p.Key.ID == propertyKey.ID))
                context.CompanyProperties.Remove(property);

            context.PropertyKeys.Remove(propertyKey);
            context.SaveChanges();
        }

        public void Delete(TaskState taskState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
