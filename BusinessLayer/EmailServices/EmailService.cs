using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using BusinessLayer.JWTAuthentication;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.EmailServices
{
    public class EmailService
    {
        private readonly IConfiguration config;
        readonly UserAuthenticationJWT jWT;
        string HtmlBody;
        readonly SmtpClient smtp = new SmtpClient();
        readonly string Sender;
        public EmailService(IConfiguration config)
        {
            this.config = config;
            jWT = new UserAuthenticationJWT(config);
            Sender = config["IssuerEmailDetail:Email"];
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(config["IssuerEmailDetail:Email"], config["IssuerEmailDetail:Password"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void SendPasswordResetLinkEmail(ForgetPasswordModel resetLink)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(config["IssuerEmailDetail:ForgetPasswordHtmlBodyFile"], Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                var token = jWT.GenerateCustomerPasswordResetJWT(resetLink);
                HtmlBody = HtmlBody.Replace("JwtToken", token);
                MailMessage message = new MailMessage();
                
                message.From = new MailAddress(Sender);
                message.To.Add(new MailAddress(resetLink.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Send(message);
            }
            catch (Exception) {
                throw;
            }
        }

        internal void SendOrderEmail(CustomerOrder order)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(config["IssuerEmailDetail:OrderEmailHtmlBodyFile"], Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                HtmlBody = HtmlBody.Replace("{OrderID}", order.OrderID.ToString());
                HtmlBody = HtmlBody.Replace("{OrderDate}", order.OrderDate.ToString());
                HtmlBody = HtmlBody.Replace("{TotalCost}", order.TotalCost.ToString());
                MailMessage message = new MailMessage();

                message.From = new MailAddress(Sender);
                message.To.Add(new MailAddress(order.Email));
                message.Subject = "order email";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Send(message);
            }
            catch (Exception e)
            { 
                throw e;
            }
        }
    }
}
