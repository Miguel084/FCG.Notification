using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmail
{
    public class SendEmailGreetingCommand  : IRequest<bool>
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
