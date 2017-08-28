using System;
using System.Runtime.Serialization;

namespace Task10
{
    [Serializable]
    internal class LoopedListEmptyException : Exception
    {
        public LoopedListEmptyException()
        {
        }

        public LoopedListEmptyException(string message) : base(message)
        {
        }

        public LoopedListEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoopedListEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}