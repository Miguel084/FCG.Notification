using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmailSaudacao
{
    public class SendEmailPaymentCommand : IRequest<bool>
    {
        public int GameId { get; set; }
        public int UserId { get; set; }

    }
}
