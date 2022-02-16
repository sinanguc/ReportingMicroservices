using Common.Messages;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Common.Helpers.ErrorHandling.CustomErrors
{
    [Serializable]
    public class DatabaseException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.InternalServerError;

        public object Value { get; set; }
        public DatabaseException() : base(GenericMessages.Error_Occurred_During_Database_Registration)
        { }

        public DatabaseException(String message) : base(message)
        { }

        public DatabaseException(String message, Exception innerException) : base(message, innerException)
        { }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
