using System.Runtime.Serialization;

namespace Calculator.Exeptions
{
    [Serializable]
    public class ErrorInExpression : ApplicationException
    {
        public ErrorInExpression() { }

        public ErrorInExpression(string message) : base(message) { }

        public ErrorInExpression(string message, Exception inner) : base(message, inner) { }

        protected ErrorInExpression(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
