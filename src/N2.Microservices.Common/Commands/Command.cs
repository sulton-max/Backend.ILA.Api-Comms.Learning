using N2.Microservices.Common.Events;

namespace N2.Microservices.Common.Commands;

public abstract class Command : Message
{
    public DateTimeOffset TimeStamp { get; protected set; }

    protected Command()
    {
        TimeStamp = DateTimeOffset.Now;
    }
}