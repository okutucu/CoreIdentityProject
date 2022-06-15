﻿using System.Net;
using System.Net.Mail;

namespace CoreIdentityProject.Helper
{
    public static class PasswordReset
    {
        public static void PasswordResetSendEmail(string link,string email,string userName)
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
                Subject = $"Author:OguzhanKutucu ::: Reset Password",
                Body = $"<h2>Şifrenizi yenilemek için aşağıdaki linke tıklayınız..</h2><hr/><a href='{link}'>Şifre yenileme linki</a>",
                IsBodyHtml = true

            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);

        }
    }
}
