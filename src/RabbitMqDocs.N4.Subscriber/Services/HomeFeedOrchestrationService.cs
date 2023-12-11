namespace RabbitMqDocs.N4.Subscriber.Services;

public class HomeFeedOrchestrationService
{
    public async ValueTask<bool> CreateHomeFeedAsync(Guid userId)
    {
        await Task.Delay(1000);

        Console.WriteLine($"Home feed created for user {userId}");

        return true;
    }
}