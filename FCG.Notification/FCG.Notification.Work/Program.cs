using FCG.Notification.Application.Dto;
using FCG.Notification.Application.UseCases.Feature.User.Consumers.UserCreate;
using FCG.Notification.Work;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// 🔹 Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// 🔹 MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreateConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin123");
        });

        // Define o exchange manualmente
        cfg.Message<UserCreateDto>(e =>
        {
            e.SetEntityName("usuario-criado");
        });

        // Fila manual
        cfg.ReceiveEndpoint("usuario-criado-queue", e =>
        {
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<UserCreateConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();