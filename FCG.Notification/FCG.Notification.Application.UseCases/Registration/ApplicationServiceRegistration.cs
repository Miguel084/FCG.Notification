using FCG.Notification.Application.UseCases.Feature.Payment.Consumers;
using FCG.Notification.Application.UseCases.Feature.User.Consumers.UserCreate;
using FCG.Notification.Application.UseCases.Handler;
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

            services.AddHttpContextAccessor();
            services.AddTransient<AuthenticationHandler>();

            // HttpClient configurado
            services.AddHttpClient<UserApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7116/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<AuthenticationHandler>();

            services.AddHttpClient<GameApiService>(client => 
            {
                client.BaseAddress = new Uri("https://localhost:7030/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<AuthenticationHandler>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserCreateConsumer>();
                x.AddConsumer<PaymentProcessConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("admin123");
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
