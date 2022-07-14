using BackEndProjectJuan.Interfaces;
using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;

        public EmailService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }
        public async Task SendEmailAsync(string mail, string link)
        {
            var emailConfig = _config.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            AppUser appUser = await _userManager.FindByEmailAsync(mail);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailConfig.Title, emailConfig.From));
            message.To.Add(new MailboxAddress(appUser.Name, appUser.Email));
            message.Subject = emailConfig.Subject;
            string emailBody = link;
            message.Body = new TextPart { Text = emailBody };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(emailConfig.SmtpServer, emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(emailConfig.Username, emailConfig.Password);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
