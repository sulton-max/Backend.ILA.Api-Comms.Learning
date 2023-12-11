namespace RabbitMqDocs.N4.Subscriber.Services;

public class AccountOrchestrationService
{
    public async ValueTask<bool> VerifyIdentityAsync(Guid userId)
    {
        await Task.Delay(1000);

        Console.WriteLine($"Identity verified for user {userId}");
        
        return true;
    }
}