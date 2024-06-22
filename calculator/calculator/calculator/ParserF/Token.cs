using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    public class Token
    {
        public enum TokenType
        { 
            Number,
            Operator,
            LeftParen,
            RightParen
        }

        public TokenType Type{ get; set; }
        public string Value { get; set; }

        public Token( TokenType type, string value )
        { 
            Type = type; 
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}('{Value}')";
        }
    }
}
