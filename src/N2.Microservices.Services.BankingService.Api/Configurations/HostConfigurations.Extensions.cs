using System.Reflection;
using Microsoft.EntityFrameworkCore;
using N2.Microservices.EventBus.EventBus;
using N2.Microservices.Services.BankingService.Application.Accounts.Services;
using N2.Microservices.Services.BankingService.Infrastructure.Accounts.Services;
using N2.Microservices.Services.BankingService.Persistence.DataContexts;
using N2.Microservices.Services.BankingService.Persistence.Repositories;
using N2.Microservices.Services.BankingService.Persistence.Repositories.Interfaces;

namespace N2.Microservices.Services.BankingService.Api.Configurations;

public static partial class HostConfigurations
{
    private static Lazy<List<Assembly>> _assemblies = new(() =>
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());

        return assemblies;
    });

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEventBusBroker, RabbitMqEventBusBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(_assemblies.Value.ToArray()); });

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BankingDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("BankingServiceDatabase")));

        return builder;
    }

    private static WebApplicationBuilder AddAccountsInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IAccountService, AccountService>();

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