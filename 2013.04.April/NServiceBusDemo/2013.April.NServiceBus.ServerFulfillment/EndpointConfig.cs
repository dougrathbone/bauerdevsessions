using NServiceBus;

namespace _2013.April.NServiceBus.ServerFulfillment 
{
    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}