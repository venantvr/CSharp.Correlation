using System;
using Business;
using Contracts;
using MassTransit;

namespace CustomMassTransit
{
    public class MySubscriber
    {
        public MySubscriber()
        {
            MessageHandler<YourMessage1> p = Handlers.HandleMessage;
            MessageHandler<YourMessage2> q = Handlers.HandleMessage;

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host =
                    sbc.Host(
                        new Uri(
                            @"amqp://tlcswhrf:HTaEhnLDKWizDJNlWao9eLG9QDNXbp9r@white-swan.rmq.cloudamqp.com/tlcswhrf"),
                        h =>
                        {
                            h.Username("tlcswhrf");
                            h.Password("HTaEhnLDKWizDJNlWao9eLG9QDNXbp9r");
                        });

                sbc.ReceiveEndpoint(host, "sandbox",
                    ep =>
                    {
                        ep.Handler(p);
                        ep.Handler(q);
                    });
            });

            bus.Start();
        }
    }
}