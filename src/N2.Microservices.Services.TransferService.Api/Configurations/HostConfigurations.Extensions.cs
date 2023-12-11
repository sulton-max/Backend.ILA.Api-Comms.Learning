using System.Reflection;
using Microsoft.EntityFrameworkCore;
using N2.Microservices.EventBus.EventBus;
using N2.Microservices.Services.TransferService.Application.Transfers.Events;
using N2.Microservices.Services.TransferService.Application.Transfers.Queries;
using N2.Microservices.Services.TransferService.Application.Transfers.Services;
using N2.Microservices.Services.TransferService.Infrastructure.Transfers.Events;
using N2.Microservices.Services.TransferService.Infrastructure.Transfers.Queries;
using N2.Microservices.Services.TransferService.Persistence.DataContexts;
using N2.Microservices.Services.TransferService.Persistence.Repositories;
using N2.Microservices.Services.TransferService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.TransferService.Api.Configurations;

public static partial class HostConfigurations
{
    private static Lazy<List<Assembly>> _assemblies = new(() =>
    {
        var test = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());

        return assemblies;
    });

    private static async Task<WebApplicationBuilder> AddEventBus(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEventBusBroker, RabbitMqEventBusBroker>();
        var eventBusBroker = builder.Services.BuildServiceProvider().GetRequiredService<IEventBusBroker>();
        await eventBusBroker.SubscribeAsync<TransferEvent, TransferEventHandler>();

        return builder;
    }

    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(_assemblies.Value.ToArray()); });

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TransferDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("TransferServiceDatabase")));

        return builder;
    }

    private static WebApplicationBuilder AddAccountsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITransferRepository, TransferRepository>();
        builder.Services.AddScoped<ITransferService, Infrastructure.Transfers.Services.TransferService>();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

        return builder;
    }

    private static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }

    private static WebApplication UseControllers(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        return app;
    }
}