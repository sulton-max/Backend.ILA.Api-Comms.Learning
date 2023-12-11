using N2.Microservices.Services.TransferService.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();

var app = builder.Build();
await app.ConfigureAsync();
app.Run();