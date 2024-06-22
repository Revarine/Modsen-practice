using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    internal class ParserExceptions : Exception
    {
        public ParserExceptions( string message ) : base(message) { }
    }

}
