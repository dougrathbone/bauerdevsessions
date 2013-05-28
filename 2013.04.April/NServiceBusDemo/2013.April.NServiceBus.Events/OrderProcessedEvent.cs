using NServiceBus;

namespace _2013.April.NServiceBus.Events
{
    public class OrderProcessedEvent : IEvent
    {
        public string Product { get; set; }
        public string OrderPlacedBy { get; set; }
    }
}
