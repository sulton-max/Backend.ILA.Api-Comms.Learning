namespace N2.Microservices.Common.Events;

public abstract class Event
{
    public DateTimeOffset TimeStamp { get; protected set; }

    protected Event()
    {
        TimeStamp = DateTimeOffset.Now;
    }
}