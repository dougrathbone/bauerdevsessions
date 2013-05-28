using NServiceBus;

namespace _2013.April.NServiceBus.Events
{
    public class OrderDispatchedEvent : IEvent
    {
        public string Product { get; set; }
        public string DispatchedTo { get; set; }
        public string Message { get; set; }
    }
}
