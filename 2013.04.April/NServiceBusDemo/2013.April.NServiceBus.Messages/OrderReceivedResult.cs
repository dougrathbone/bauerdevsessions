using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace _2013.April.NServiceBus.Messages
{
    public enum OrderReceivedResult
    {
        OrderReceived,
        OrderRejected
    }
}
