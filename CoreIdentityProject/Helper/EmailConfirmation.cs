using System.Net;
using System.Net.Mail;

namespace CoreIdentityProject.Helper
{
    public static class EmailConfirmation
    {
        public static void SendEmail(string link, string email)
        {

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("oktc95@gmail.com", "viitckxmwjzptqxm")

            };
              
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("oktc95@gmail.com"),
                Subject = $"Author:OguzhanKutucu ::: Email Doğrulama",
                Body = $"<h2>Email adresinizi doğrulamak için lütfen aşağıdaki linke tıklayınız..</h2><hr/><a href='{link}'>Email doğrulama linki</a>",
                IsBodyHtml = true

            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);

        }


    }
}
