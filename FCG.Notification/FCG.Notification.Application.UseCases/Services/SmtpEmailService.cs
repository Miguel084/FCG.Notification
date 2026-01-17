using FCG.Notification.Application.Interface.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FCG.Notification.Application.UseCases.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var smtpServer = _configuration["Smtp:Server"];
            var port = int.Parse(_configuration["Smtp:Port"]);
            var user = _configuration["Smtp:User"];
            var password = _configuration["Smtp:Password"];

            var message = new MailMessage
            {
                From = new MailAddress(user, "FCG Games"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            using var client = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
