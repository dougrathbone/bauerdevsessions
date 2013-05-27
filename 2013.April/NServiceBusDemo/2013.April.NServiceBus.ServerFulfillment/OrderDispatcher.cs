using System;
using System.Threading;
using NServiceBus;
using _2013.April.NServiceBus.Events;

namespace _2013.April.NServiceBus.ServerFulfillment
{
    public class OrderDispatcher : IHandleMessages<OrderProcessedEvent>
    {
        public IBus Bus { get; set; } //IBus automatically property injected
    
        public void Handle(OrderProcessedEvent message)
        {
            DispatchOrder(message.Product, message.OrderPlacedBy);

            //publish the event to any subscribers
            Bus.Publish<OrderDispatchedEvent>(e => 
            { 
                e.Product = message.Product;
                e.DispatchedTo = message.OrderPlacedBy;
                e.Message = string.Format
                (
                    "Hi {0}, the order that you placed for '{1}' has now been dispatched. It should be with you shortly.",
                    message.OrderPlacedBy, message.Product
                );
            });
        }

        private static void DispatchOrder(string product, string dispatchTo)
        {
            Console.Out.WriteLine("Heading out to the warehouse to find some '{0}' stock...", product);
            Thread.Sleep(2000);
            Console.Out.WriteLine("Found some, great! Now looking for a box...");
            Thread.Sleep(2000);
            Console.Out.WriteLine("Found one, now loading it into a truck...");
            Thread.Sleep(2000);
            Console.Out.WriteLine("Done. Order for '{0}' dispatched to '{1}'", product, dispatchTo);
        }
    }
}
