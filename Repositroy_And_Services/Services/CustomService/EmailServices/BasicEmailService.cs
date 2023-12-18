using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.EmailServices
{
    public class BasicEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
            // Implement email sending logic here
            Console.WriteLine($"Sending email to: {to}, Subject: {subject}, Body: {body}");

            // Simulate email sending (replace with your email sending code)
            return Task.CompletedTask;
        }
    }
}
