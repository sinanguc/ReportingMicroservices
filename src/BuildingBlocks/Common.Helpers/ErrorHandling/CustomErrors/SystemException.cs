using System;
using System.Net;
using System.Runtime.Serialization;

namespace Common.Helpers.ErrorHandling.CustomErrors
{
    [Serializable]
    public class TokenException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.Unauthorized;

        public object Value { get; set; }
        public TokenException() : base()
        { }

        public TokenException(String message) : base(message)
        { }

        public TokenException(String message, Exception innerException) : base(message, innerException)
        { }

        protected TokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class LoginIncorrectException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.Unauthorized;

        public object Value { get; set; }
        public LoginIncorrectException() : base()
        { }

        public LoginIncorrectException(String message) : base(message)
        { }

        public LoginIncorrectException(String message, Exception innerException) : base(message, innerException)
        { }

        protected LoginIncorrectException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class NotAuthorizedException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.Unauthorized;

        public object Value { get; set; }
        public NotAuthorizedException() : base()
        { }

        public NotAuthorizedException(String message) : base(message)
        { }

        public NotAuthorizedException(String message, Exception innerException) : base(message, innerException)
        { }

        protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
