using NServiceBus;

namespace _2013.April.NServiceBus.ClientConsole
{
    public class SelfHostedEndpoint
    {
        public IBus Bus { get; set; }

        public void Initialize()
        {
            Bus = Configure.With()
                    .Log4Net()
                    .DefaultBuilder()
                    .XmlSerializer()
                    .MsmqTransport()
                    .UnicastBus()
                    .Log4Net()
                    .CreateBus()
                    .Start(() => Configure.Instance.ForInstallationOn<global::NServiceBus.Installation.Environments.Windows>().Install());
        }
    }
}
