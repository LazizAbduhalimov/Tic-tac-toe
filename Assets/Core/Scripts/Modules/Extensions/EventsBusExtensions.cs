using SevenBoldPencil.EasyEvents;

public static class EventsBusExtension
{
    public static T NewEventSingleton<T>(this EventsBus bus, T @event) where T : struct, IEventSingleton
    {
        return bus.NewEventSingleton<T>() = @event;
    }

    public static T NewEvent<T>(this EventsBus bus, T @event) where T : struct, IEventReplicant
    {
        return bus.NewEvent<T>() = @event;
    }
}