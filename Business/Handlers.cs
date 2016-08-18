using System;
using System.Threading.Tasks;
using Contextable;
using Contracts;
using MassTransit;

namespace Business
{
    public static class Handlers
    {
        public static Task HandleMessage(ConsumeContext<YourMessage2> context)
        {
            using (new Context<Correlation>(new Correlation(Guid.NewGuid().ToString())))
                return Console.Out.WriteLineAsync($"Received YourMessage2: {context.Message.Text}");
        }

        public static Task HandleMessage(ConsumeContext<YourMessage1> context)
        {
            using (new Context<Correlation>(new Correlation(context.Message.CorrelationId)))
                return
                    Console.Out.WriteLineAsync(
                        $"Received YourMessage1: {context.Message.Text} {context.Message.Date} {context.Message.CorrelationId}")
                    ;
        }
    }
}