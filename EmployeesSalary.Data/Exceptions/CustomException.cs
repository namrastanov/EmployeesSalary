using System;
using System.Runtime.Serialization;

namespace EmployeesSalary.Data.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(ExceptionCode code, string message)
            : this(message)
        {
            SetExceptionCode(code);
        }

        public CustomException()
        {
            SetExceptionCode(ExceptionCode.Default);
        }

        public CustomException(string message) : base(message)
        {
            SetExceptionCode(ExceptionCode.Default);
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
            SetExceptionCode(ExceptionCode.Default);
        }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            SetExceptionCode(ExceptionCode.Default);
        }

        public ExceptionCode GetExceptionCode()
        {
            return (ExceptionCode)Data["Code"];
        }

        private void SetExceptionCode(ExceptionCode code)
        {
            if (Data.Contains("Code"))
            {
                Data["Code"] = code;
            }
            else
            {
                Data.Add("Code", code);
            }
        }
    }
}
