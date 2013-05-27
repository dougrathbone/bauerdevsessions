using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace _2013.April.NServiceBus.Messages
{
    public class PlaceOrderCommand : ICommand
    {
        public string Product { get; set; }
        public string OrderMadeBy { get; set; }

        public bool HasProduct()
        {
            return !string.IsNullOrEmpty(Product);
        }
    }
}
