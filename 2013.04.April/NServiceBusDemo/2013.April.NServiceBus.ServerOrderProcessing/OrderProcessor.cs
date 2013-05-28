using System;
using DemoUtils;
using _2013.April.NServiceBus.Events;
using _2013.April.NServiceBus.Messages;
using NServiceBus;

namespace _2013.April.NServiceBus.ServerOrderProcessing
{
    public class OrderProcessor : IHandleMessages<PlaceOrderCommand>
    {
        private readonly IBus _bus;

        public OrderProcessor(IBus bus) //IBus automatically constructor injected
        {
            _bus = bus;
        }
        
        public void Handle(PlaceOrderCommand message)
        {
            if (message.HasProduct())
            {
                ProcessOrder(message);

                //publish an event to all subscribed to the OrderProcessedEvent event
                _bus.Publish<OrderProcessedEvent>(e =>
                {
                    e.Product = message.Product;
                    e.OrderPlacedBy = message.OrderMadeBy;
                });

                // return a success code to the caller
                _bus.Return(OrderReceivedResult.OrderReceived);

            }
            else
            {
                RejectOrder(message);

                // return a success code to the caller
                _bus.Return(OrderReceivedResult.OrderRejected);
            }
        }

        private static void ProcessOrder(PlaceOrderCommand message)
        {
            Console.Out.WriteLine("Order for '{0}' made by '{1}'", message.Product, message.OrderMadeBy);
        }

        private static void RejectOrder(PlaceOrderCommand message)
        {
            using(new OutputFormatter(ConsoleColor.Red))
            {
                Console.Out.WriteLine("An empty order has been made by '{0}', rejecting this empty order", message.OrderMadeBy);
            }
        }
    }
}
