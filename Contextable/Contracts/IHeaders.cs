using System;

namespace Contextable.Contracts
{
    public interface IHeaders
    {
        DateTime Date { get; set; }
        string CorrelationId { get; }
    }
}