using System;
using NServiceBus;
using _2013.April.NServiceBus.Messages;

namespace _2013.April.NServiceBus.ClientConsole
{
    public class Orderer
    {
        private readonly IBus _bus;

        public Orderer(IBus bus)
        {
            _bus = bus;
        }

        public void PlaceOrder(string product, string theOrderPlacer, Action<OrderReceivedResult> callback)
        {
            //send to the bus, for it to route to the approapriate handler
            _bus.Send<PlaceOrderCommand>(m =>
            {
                m.Product = product;
                m.OrderMadeBy = theOrderPlacer;
            }).Register(callback);
        }
    }
}
