using System.Net;
using System.Net.Mail;

namespace CoreIdentityProject.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link)
        {
            

            var smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("oktc95@gmail.com", "19691991aq")
                
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("oktc95@gmail.com"),
                Subject = $"Author:OguzhanKutucu ::: Reset Password",
                Body = "<h2>Şifrenizi yenilemek için aşağıdaki linke tıklayınız..</h2><hr/><a href='{link}'>Şifre yenileme linki</a>",
                IsBodyHtml = true

            };

            mailMessage.To.Add("kkutucu@gmail.com");

            smtpClient.Send(mailMessage);

        }
    }
}
