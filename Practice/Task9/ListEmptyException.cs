using System;
using System.Runtime.Serialization;

namespace Task9
{
    [Serializable]
    internal class ListEmptyException : Exception
    {
        public ListEmptyException()
        {
        }

        public ListEmptyException(string message) : base(message)
        {
        }

        public ListEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ListEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}