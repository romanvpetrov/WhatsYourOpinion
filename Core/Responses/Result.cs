using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class Result<T> : Result
    {
        public Result(bool success, string[] errorMessages, T value) : base(success, errorMessages)
        {
            Value = value;
        }
        public T Value { get; set; }
    }

    public class Result
    {
        public Result(bool success, string[] errorMessages)
        {
            Success = success;
            ErrorMessages = errorMessages;
        }
        public string[] ErrorMessages { get; set; }
        public bool Success { get; set; }
    }
}
