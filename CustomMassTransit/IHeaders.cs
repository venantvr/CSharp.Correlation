using System;

namespace MassTransitClient
{
    public interface IHeaders
    {
        DateTime Date { get; set; }
        string CorrelationId { get; }
    }
}