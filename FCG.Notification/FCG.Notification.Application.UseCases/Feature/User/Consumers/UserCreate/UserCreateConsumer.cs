using MassTransit;
using FCG.Shared.Contracts;
using MediatR;
using FCG.Notification.Application.UseCases.Feature.Email.Command.SendEmail;

namespace FCG.Notification.Application.UseCases.Feature.User.Consumers.UserCreate
{
    public class UserCreateConsumer : IConsumer<FCG.Shared.Contracts.UserCreatedEvent>
    {
        private readonly IMediator _mediator;
        public UserCreateConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<FCG.Shared.Contracts.UserCreatedEvent> context)
        {
            return _mediator.Send(
                 new SendEmailGreetingCommand
                 {
                     Email = context.Message.Email,
                     Name= context.Message.Name
                 }
            );
        }
    }
}
