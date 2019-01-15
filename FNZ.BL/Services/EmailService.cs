using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;

namespace FNZ.BL.Services
{
     public class EmailService : IEmailService
    {
        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "fundacjafnz@gmail.com",
                    Password = "fundacja2018"
                };

                client.Credentials = credential;
                client.Host = "smtp.gmail.com";
                client.Port = int.Parse("587");
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress("fundacjafnz@gmail.com");
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}
