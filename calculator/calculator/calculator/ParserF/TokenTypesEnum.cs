using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    public class TokenTypesEnum
    {
        public enum TokenType
        {
            Number,
            Operator,
            LeftParen,
            RightParen,
            Identifier
        }


    }
}
