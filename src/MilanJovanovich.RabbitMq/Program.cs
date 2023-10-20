using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Options;
using MilanJovanovich.RabbitMq.Messaging;
using MilanJovanovich.RabbitMq.Models.Settings;
using MilanJovanovich.RabbitMq.Products.Events;
using MilanJovanovich.RabbitMq.Services;
using MilanJovanovich.RabbitMq.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(nameof(MessageBrokerSettings)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddControllers();
builder.Services.AddRouting(configs => configs.LowercaseUrls = true);

builder.Services.AddMassTransit(bussConfigurator =>
{
    bussConfigurator.SetKebabCaseEndpointNameFormatter();
    bussConfigurator.AddConsumer<ProductCreatedEventConsumer>();
    bussConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var settings = context.GetRequiredService<IOptions<MessageBrokerSettings>>().Value;
        configurator.Host(settings.Host,
            host =>
            {
                host.Username(settings.Username);
                host.Password(settings.Password);
            });
    });

    bussConfigurator.REceiveEnd
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEventBus, EventBus>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();