using FCG.Notification.Application.UseCases.Feature.Payment.Consumers;
using FCG.Notification.Application.UseCases.Feature.User.Consumers.UserCreate;
using FCG.Notification.Application.UseCases.Services;
using FCG.Shared.Contracts;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using static MassTransit.Logging.OperationName;
using static System.Net.WebRequestMethods;

namespace FCG.Notification.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreateConsumer>();
                x.AddConsumer<PaymentProcessConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["Rabbitmq:Url"], "/", h =>
                    {
                        h.Username(configuration["Rabbitmq:Username"]);
                        h.Password(configuration["Rabbitmq:Password"]);
                    });

                    cfg.ReceiveEndpoint("user-create-queue", e =>
                    {
                        e.ConfigureConsumer<UserCreateConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("payment-process-notification-queue", e =>
                    {
                        e.ConfigureConsumer<PaymentProcessConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
