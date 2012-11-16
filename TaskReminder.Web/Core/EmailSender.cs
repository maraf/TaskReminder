using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using TaskReminder.Core.Domain;
using System.Net;
using System.Configuration;

namespace TaskReminder.Web.Core
{
    public class EmailSender
    {
        public const string SenderEmail = "neptuo@gmail.com";
        public const string SenderPassword = "11neptuo11";

        public static void SendEmail(User user, string subject, string body)
        {
            if (String.IsNullOrEmpty(user.Email))
                return;

            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"] ?? SenderEmail, "Úkolovník");
            var toAddress = new MailAddress(user.Email, user.FirstName + " " + user.LastName);
            string fromPassword = ConfigurationManager.AppSettings["SenderPassword"] ?? SenderPassword;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}