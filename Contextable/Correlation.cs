using System;

namespace Contextable
{
    public class Correlation : IDisposable, ICloneable
    {
        public Correlation()
        {
        }

        public Correlation(string id)
        {
            Value = id;
        }

        public string Value { get; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}