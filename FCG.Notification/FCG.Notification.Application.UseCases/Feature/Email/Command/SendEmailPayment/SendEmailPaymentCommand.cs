using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmailPayment
{
    public class SendEmailPaymentCommand : IRequest<bool>
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public decimal? Price { get; set; }
        public string StatusMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Game { get; set; }
    }
}
