using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Utils
{
    internal class Result<T>
    {
        public readonly T Value;
        public readonly string? Error;

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
