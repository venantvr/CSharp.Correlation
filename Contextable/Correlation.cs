namespace Contextable
{
    public class Correlation
    {
        public Correlation()
        {
        }

        public Correlation(string id)
        {
            Value = id;
        }

        public string Value { get; }
    }
}