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
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMediator()
            .AddEventBus()
            .AddPersistence()
            .AddAccountsInfrastructure()
            .AddDevTools()
            .AddControllers();

        return new ValueTask<WebApplicationBuilder>(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseControllers();

        return new ValueTask<WebApplication>(app);
    }
}