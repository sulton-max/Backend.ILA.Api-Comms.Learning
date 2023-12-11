namespace N2.Microservices.Services.TransferService.Api.Configurations;

public static partial class HostConfigurations
{
    public static async ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMediator()
            .AddPersistence()
            .AddAccountsInfrastructure()
            .AddDevTools()
            .AddControllers();
        
        await builder.AddEventBus();

        return builder;
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseControllers();

        return new ValueTask<WebApplication>(app);
    }
}