using System;
using Contextable;
using MassTransit;

namespace MassTransitClient
{
    public class MySubscriber
    {
        private readonly IBusControl _bus;

        public MySubscriber()
        {
            MessageHandler<YourMessage1> p = context => NewMethod1(context);
            MessageHandler<YourMessage2> q = context => NewMethod2(context);

            _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
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

            _bus.Start();
        }

        private static System.Threading.Tasks.Task NewMethod2(ConsumeContext<YourMessage2> context)
        {
            using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
                return Console.Out.WriteLineAsync($"Received YourMessage2: {context.Message.Text}");
        }

        private static System.Threading.Tasks.Task NewMethod1(ConsumeContext<YourMessage1> context)
        {
            using (new Context<Correlation>(new Correlation(context.Message.CorrelationId)))
                return Console.Out.WriteLineAsync($"Received YourMessage1: {context.Message.Text} {context.Message.Date} {context.Message.CorrelationId}");
        }
    }
}