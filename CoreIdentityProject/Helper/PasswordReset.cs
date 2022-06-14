using System.Net;
using System.Net.Mail;

namespace CoreIdentityProject.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link,string email,string userName)
        {
            //var fromAddress = new MailAddress("oktc95@gmail.com");
            //var toAddress = new MailAddress(email, userName);
            //const string fromPassword = "19691991aq";
            //const string subject = "www.hashtag.com::Reset Password";
            //string body = $"<a href='{link}'>Reset Password Link</a>";

            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true
            //})
            //{
            //    smtp.Send(message);
            //}






            var smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("oktc95@gmail.com", "viitckxmwjzptqxm")

            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("oktc95@gmail.com"),
                Subject = $"Author:OguzhanKutucu ::: Reset Password",
                Body = $"<h2>Şifrenizi yenilemek için aşağıdaki linke tıklayınız..</h2><hr/><a href='{link}'>Şifre yenileme linki</a>",
                IsBodyHtml = true

            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);

        }
    }
}
