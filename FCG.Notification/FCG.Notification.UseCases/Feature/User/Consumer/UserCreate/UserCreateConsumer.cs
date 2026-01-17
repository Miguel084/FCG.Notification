using FCG.Notification.Dto.User;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Notification.UseCases.Feature.User.Consumer.UserCreate
{
    public class UserCreateConsumer : IConsumer<UserCreateDto>
    {
        public async Task Consume(ConsumeContext<UserCreateDto> context)
        {
            var dados = context.Message;
            // Lógica de negócio aqui
            await Task.CompletedTask;
        }
    }
}
