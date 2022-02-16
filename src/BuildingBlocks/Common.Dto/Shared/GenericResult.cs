using Common.Dto.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Common.Dto.Shared
{
    public class GenericResult
    {
        private readonly DateTime _executionStartTime;
        public GenericResult()
        {
            _executionStartTime = DateTime.Now;
        }
        public string Version { get; set; }
        public bool Success
        {
            get { return Data != null; }
        }
        private int _statusCode;
        public int StatusCode
        {
            get { return _statusCode; }
            set
            {
                _statusCode = value;
                switch (_statusCode)
                {
                    case (int)HttpStatusCode.Unauthorized:
                    case (int)HttpStatusCode.NotFound:
                    case (int)HttpStatusCode.BadRequest:
                        MessageType = EnumResponseMessageType.Warn.GetHashCode();
                        break;
                    case (int)HttpStatusCode.InternalServerError:
                        MessageType = EnumResponseMessageType.Fatal.GetHashCode();
                        break;
                    default:
                        MessageType = EnumResponseMessageType.Info.GetHashCode();
                        break;
                }
            }
        }

        public int MessageType { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                ExecutionTime = (int)(DateTime.Now - _executionStartTime).TotalMilliseconds;
            }
        }
        public Object Data { get; set; }
        public string ServerTime
        {
            get { return DateTime.Now.ToString("dd.MM.yyyy - HH:mm"); }
        }
        public int ExecutionTime { get; private set; }

        public class WithValidationErrorMessage : GenericResult
        {
            public List<ValidationErrorModel> ValidationErrors { get; set; }

            public WithValidationErrorMessage()
            {
                ValidationErrors = new List<ValidationErrorModel>();
            }

            public class ValidationErrorModel
            {
                public string FieldName { get; set; }
                public string Message { get; set; }
            }
        }

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
