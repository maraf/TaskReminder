using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public class EmailHelper
    {
        public const string TaskEditUrlFormat = "http://ukolovnik.apps.neptuo.com/task-{0}/edit";

        public const string TaskEmailBodyFormat = @"
            <html>
                <body>
                    Byl vám přiřazen nový úkol<br />
                    Název: <strong><a href='{0}'>{1}</a></strong><br />
                    Zákazník: {2}
                    Stav: {3}<br />
                    Požadovaného dokončení: {4}<br />
                    Vytvořil: {5}<br />
                </body>
            </html>";

        public static void SendTaskAssigned(Task task)
        {
            string subject = String.Format("Úkolovník: Byl vám přiřazen nový úkol");
            string message = String.Format(TaskEmailBodyFormat, 
                String.Format(TaskEditUrlFormat, task.ID), 
                task.Name, 
                task.Office.Company.Name, 
                task.TaskState.Name, 
                task.ToComplete, 
                task.CreatedBy.FirstName + " " + task.CreatedBy.LastName
            );

            EmailSender.SendEmail(task.AssignedTo, subject, message);
        }
    }
}