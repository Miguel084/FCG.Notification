using FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmail;
using FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmailSaudacao;
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
        private readonly GameApiService _gameApiService;
        private readonly UserApiService _userApiService;
        public PaymentProcessConsumer(IMediator mediator, GameApiService gameApiService, UserApiService userApiService)
        {
            _gameApiService = gameApiService;
            _userApiService = userApiService;
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.PaymentProcessedEvent> context)
        {
            return _mediator.Send(
                 new SendEmailPaymentCommand
                 {
                     UserId = context.Message.UserId,
                     GameId = context.Message.GameId
                 }
            );
        }
    }
}
