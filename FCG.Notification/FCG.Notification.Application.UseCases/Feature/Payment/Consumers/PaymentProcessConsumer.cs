using FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmailPayment;
using FCG.Notification.Application.UseCases.Services;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.Application.UseCases.Feature.Payment.Consumers
{
    public class PaymentProcessConsumer : IConsumer<FCG.Shared.Contracts.PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentProcessConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.PaymentProcessedEvent> context)
        {
            return _mediator.Send(
                 new SendEmailPaymentCommand
                 {
                     UserId = context.Message.UserId,
                     GameId = context.Message.GameId,
                     Email = context.Message.Email,
                     Name = context.Message.Name,
                     Game = context.Message.Game,
                     Price = context.Message.Price
                 }
            );
        }
    }
}
