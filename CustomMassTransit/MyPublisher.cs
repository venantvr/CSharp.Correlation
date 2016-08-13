using System;
using MassTransit;

namespace MassTransitClient
{
    public class MyPublisher
    {
        private readonly IBusControl _bus;

        public MyPublisher()
        {
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
                                                   });

            _bus.Start();
        }

        public void Publish<T>(T message) where T : class, new()
        {
            //using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
            //{
            _bus.Publish(message);
            //}
        }
    }
}