using System;
using Contracts;

namespace CustomMassTransit
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var bus = new MyPublisher();
            var subs = new MySubscriber();

            bus.Publish(new YourMessage1 {Text = "Hi1", Date = DateTime.Now});
            bus.Publish(new YourMessage2 {Text = "Hi2"});

            //bus.Stop();
            Console.ReadKey();
        }
    }
}