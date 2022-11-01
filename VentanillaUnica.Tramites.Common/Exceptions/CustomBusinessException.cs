using System.Runtime.Serialization;

namespace VentanillaUnica.Tramites.Common.Exceptions
{
    public class CustomBusinessException : Exception
    {
        public CustomBusinessException(string message) : base(message)
        {
        }

        public CustomBusinessException(string message, Exception exception) : base(message, exception)
        {
        }

        public CustomBusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
