using ECommerceAPI.Domain.ResponseModels;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.EmailService
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string file)
        {
            int check = email.IndexOf("@");
            if (email.Substring(check + 1).ToLower() == "code.edu.az")
            {
                //mail content
                MailMessage msg = new MailMessage();
                //Attachment
                msg.Attachments.Add(new Attachment(file.ToString()));
                msg.IsBodyHtml = true;
                msg.From = new MailAddress("Khanim.pg@code.edu.az", "");
                msg.Subject = "Download Export file";
                msg.To.Add(email);
                msg.IsBodyHtml = true;
                msg.Body = "Your report in the appropriate category for the date range you requested";

                //Server details
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("aktivemailunvanidaxiledin", "aktivpassworddaxiledin");
                client.EnableSsl = true;
                client.Send(msg);
            }
        }
    }
}
