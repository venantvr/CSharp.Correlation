using System;
using System.Threading.Tasks;
using Contextable;
using MassTransit;

namespace CustomMassTransit
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

        private static Task NewMethod2(ConsumeContext<YourMessage2> context)
        {
            using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
                return Console.Out.WriteLineAsync(string.Format("Received YourMessage2: {0}", context.Message.Text));
        }

        private static Task NewMethod1(ConsumeContext<YourMessage1> context)
        {
            using (new Context<Correlation>(new Correlation(context.Message.CorrelationId)))
                return Console.Out.WriteLineAsync(string.Format("Received YourMessage1: {0} {1} {2}", context.Message.Text, context.Message.Date, context.Message.CorrelationId))
                    ;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = new MyPublisher();
            var subs = new MySubscriber();

            bus.Publish(new YourMessage1 { Text = "Hi1", Date = DateTime.Now });
            bus.Publish(new YourMessage2 { Text = "Hi2" });

            //bus.Stop();
            Console.ReadKey();
        }
    }

    public interface IHeaders
    {
        DateTime Date { get; set; }
        string CorrelationId { get; }
    }

    public class YourMessage1 : IHeaders
    {
        public string Text { get; set; }
        public string CorrelationId => "blahblah";
        public DateTime Date { get; set; }
    }

    public class YourMessage2
    {
        //public DateTime Date { get; set; }

        public string Text { get; set; }
    }
}