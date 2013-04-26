using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoUtils;
using NServiceBus;
using _2013.April.NServiceBus.Events;

namespace _2013.April.NServiceBus.ClientConsole
{
    public class OrderDispatchedEventHandler : IHandleMessages<OrderDispatchedEvent>
    {
        public void Handle(OrderDispatchedEvent message)
        {
            using (new OutputFormatter(ConsoleColor.Green))
            {
                Console.WriteLine(message.Message);
            }
        }
    }
}
