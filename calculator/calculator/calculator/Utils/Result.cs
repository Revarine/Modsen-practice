using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Utils
{
    internal class Result<T>
    {
        private T Value { get; set; }
        private string? Error { get; set; }

        public Result(T Value) 
        {
            this.Value = Value;
        }

        public Result(string Error)
        {
            this.Error = Error;
        }

        public bool HasValue => Error == null;

        public static implicit operator bool(Result<T> result)
        {
            return result.HasValue;
        }
    }
}
