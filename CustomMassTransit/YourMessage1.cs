using System;

namespace MassTransitClient
{
    public class YourMessage1 : IHeaders
    {
        public string CorrelationId => "blahblah";

        public DateTime Date { get; set; }

        public string Text { get; set; }
    }
}