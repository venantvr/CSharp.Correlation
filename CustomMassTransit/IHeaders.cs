using System;

namespace CustomMassTransit
{
    public interface IHeaders
    {
        DateTime Date { get; set; }
        string CorrelationId { get; }
    }
}