using Route.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Route.BLL.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("abdelrhmanelhshamy@gmail.com", "bfyhvujdxpporprg");
            client.Send("abdelrhmanelhshamy@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
