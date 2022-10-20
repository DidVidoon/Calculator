using System.Runtime.Serialization;

namespace Calculator.Exeptions
{
    [Serializable]
    public class DivisionByZeroException : ApplicationException
    {
        public DivisionByZeroException() { }

        public DivisionByZeroException(string message) : base(message) { }

        public DivisionByZeroException(string message, Exception inner) : base(message, inner) { }

        protected DivisionByZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
