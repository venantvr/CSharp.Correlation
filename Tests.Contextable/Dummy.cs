using System;

namespace Tests.Contextable
{
    public class Dummy : IDisposable, ICloneable
    {
        public Guid Guid { get; set; }

        public object Clone()
        {
            return new Dummy {Guid = Guid};
        }

        public void Dispose()
        {
        }
    }
}