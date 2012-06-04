using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using TaskReminder.Core.Domain.Repository;
using TaskReminder.Core.EntityFramework;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public class TaskScheduleJob : TaskScheduleCore, IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            if (DateTime.Now.Day == 1)
                Execute();
        }
    }

    public class TaskScheduleCore
    {
        private IRepository repository = new Repository();

        public void Execute()
        {
            List<int> tasks = new List<int>();
            IEnumerable<TaskTemplate> templates = repository.TaskTemplates.Where(t => t.AutoRepeat == true).ToList();
            foreach (TaskTemplate template in templates)
            {
                if (!repository.Tasks.Where(t => t.TaskTemplateID == template.ID && t.ToComplete != null && t.ToComplete.Value.Month == DateTime.Now.Month).Any())
                {
                    if (IsForSchedule(template.Period, template.CompleteInMonth))
                    {
                        Task task = template.AsTask();
                        task.Created = DateTime.Now;
                        task.Assigned = DateTime.Now;
                        task.ToComplete = NextCompleteDateTime(template);
                        repository.Save(task);
                        tasks.Add(task.ID);
                    }
                }
            }

            SendEmails(tasks);
        }

        private void SendEmails(IEnumerable<int> tasks)
        {
            foreach (int taskID in tasks)
            {
                Task task = repository.Tasks.FirstOrDefault(t => t.ID == taskID);
                if(task != null)
                    EmailHelper.SendTaskAssigned(task);
            }
        }

        private DateTime NextCompleteDateTime(TaskTemplate template)
        {
            int year = template.CompleteInMonth + template.Period > 12 ? DateTime.Now.Year + 1 : DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = template.CompleteInDay;
            return new DateTime(year, month, day);
        }

        private bool IsForSchedule(int period, int month)
        {
            if (period == 1)
                return true;

            int i = 1;
            while (month + (i * period) % 12 > month)
            {
                if (DateTime.Now.Month == month + (i * period))
                    return true;
            }

            return false;
        }
    }
}