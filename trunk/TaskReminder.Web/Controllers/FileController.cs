using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskReminder.Core.Domain;
using System.IO;

namespace TaskReminder.Web.Controllers
{
    public class FileController : TaskReminder.Web.Mvc.Controller
    {
        public ActionResult Attachment(string fileName)
        {
            TaskAttachment attachment = Repository.TaskAttachments.FirstOrDefault(a => a.FileName == fileName);
            if (attachment != null)
            {
                return File(Server.MapPath(Path.Combine("~/attachments", fileName)), attachment.ContentType, attachment.Name + Path.GetExtension(attachment.FileName));
            }

            return new HttpNotFoundResult();
        }
    }
}
