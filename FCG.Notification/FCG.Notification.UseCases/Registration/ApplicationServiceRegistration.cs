using FCG.Notification.UseCases.Feature.User.Consumer.UserCreate;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FCG.Notification.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<UserCreateConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("amqp://localhost"), h =>
                    {
                        h.Username("admin");
                        h.Password("admin123");
                    });

                    cfg.ReceiveEndpoint("user-created-event", e => // Nome da fila
                    {
                        e.ConfigureConsumer<UserCreateConsumer>(context);
                    });

                    // Configura automaticamente as filas com base nos consumidores registrados
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
