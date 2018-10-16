using System;
using System.Net;
using System.Net.Mail;

namespace FindieServer.Services
{
    public static class MailService
    {
        public static void SendRegistrationMail(string userMail, string username)
        {
            var fromMail = new MailAddress("findienoreply@gmail.com", "From Findie Team");
            var toMail = new MailAddress(userMail, "To Name");
            const string fromPassword = "49fKD#(LSdnvkslLI*)#RJKHFNV<SKJDOO#*jsdhfw__ru4hfbskh3^*^()%_DNFGJWFKDMMX*@W(&";
            string subject = $"Welcome, {username} !";
            string body = $"We're glad that You're with us {username}! For the best experience, please report each bug to us! Stay tuned for the new Debug versions" +
                          $" and enjoy Findie application!  " +
                          $"This message has been generated automatically, please do not respond to this Email Adress.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromMail.Address, fromPassword)
            };

            using (var message = new MailMessage(fromMail, toMail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public static void SendMailToTester(string testerMail)
        {
            var fromMail = new MailAddress("findienoreply@gmail.com", "From Name");
            try
            {
                var toMail = new MailAddress(testerMail, "From Findie team");
                const string fromPassword = "49fKD#(LSdnvkslLI*)#RJKHFNV<SKJDOO#*jsdhfw__ru4hfbskh3^*^()%_DNFGJWFKDMMX*@W(&";
                string subject = $"Welcome aboard!";
                string body =
                    $"Hello! We're glad that you joined out testers team! To start please follow this link to get access for a new debug version of findie " +
                    "http://ujeb.se/Vguqv";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMail.Address, fromPassword)
                };

                using (var message = new MailMessage(fromMail, toMail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (ArgumentException ex)
            {
                return;
            }
            catch (FormatException)
            {

            }
        }
    }
}
