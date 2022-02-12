using Common.Messages;
using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;

namespace Common.Helpers.ErrorHandling.CustomErrors
{
    [Serializable]
    public class ValidationException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.BadRequest;

        public object Value { get; set; }
        public ValidationException() : base(GenericMessages.Please_Fill_In_All_Required_Fields)
        { }

        public ValidationException(String message) : base(message)
        { }

        public ValidationException(String message, Exception innerException) : base(message, innerException)
        { }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class RecordExistException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.OK;


        public object Value { get; set; }

        public RecordExistException() : base(GenericMessages.Already_Registered)
        { }

        public RecordExistException(String message) : base(message)
        { }

        public RecordExistException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        { }

        public RecordExistException(String message, Exception innerException) : base(message, innerException)
        { }

        protected RecordExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public int Status { get; set; } = (int)HttpStatusCode.NotFound;

        public object Value { get; set; }
        public RecordNotFoundException() : base(GenericMessages.No_Records_Found)
        { }

        public RecordNotFoundException(String message) : base(message)
        { }

        public RecordNotFoundException(String message, Exception innerException) : base(message, innerException)
        { }

        protected RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
