using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.Interface.Service
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
