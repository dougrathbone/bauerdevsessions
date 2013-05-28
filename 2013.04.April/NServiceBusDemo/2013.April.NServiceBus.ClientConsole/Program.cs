using System;
using DemoUtils;
using _2013.April.NServiceBus.Messages;

namespace _2013.April.NServiceBus.ClientConsole
{
    class Program
    {
        static void Main()
        {
            var endpoint = new SelfHostedEndpoint();
            endpoint.Initialize();

            var orderer = new Orderer(endpoint.Bus);
            
            while (true)
            {
                var person = GetUserInput("Hi, i'm your friendly order taker, lets start with your name?");
                var product = GetUserInput("What would you like to order {0}?", person);

                orderer.PlaceOrder(product, person, DispalyServerResponse);

                Wait();  
            }
        }

        private static void DispalyServerResponse(OrderReceivedResult response)
        {
            if (response == OrderReceivedResult.OrderReceived)
            {
                ShowMessage("Order received. You will be notified when it has been dispatched");
            }
            else
            {
                ShowError("Bad luck, your order has been rejected.");
            }
        }
        
        private static string GetUserInput(string instruction, params object[] args)
        {
            using (new OutputFormatter(ConsoleColor.Blue))
            {
                Console.WriteLine(instruction, args);
            }
            return Console.ReadLine();
        }

        private static void ShowMessage(string message, params object[] args)
        {
            using (new OutputFormatter(ConsoleColor.Green))
            {
                Console.WriteLine(message, args);
            }
        }

        private static void ShowError(string message, params object[] args)
        {
            using (new OutputFormatter(ConsoleColor.Red))
            {
                Console.WriteLine(message, args);
            }
        }

        private static void Wait()
        {
            Console.ReadLine();
        }
    }
}
