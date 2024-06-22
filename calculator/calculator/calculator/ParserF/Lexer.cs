using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    public class Lexer
    {
        private static readonly Regex tokenPattern = new Regex(@"\d+(\.\d+)?|[+\-*/()]");

        public List<Token> Tokenize( string input ) //находит совпадения с Regex и создает список токенов
        {
            var matches = tokenPattern.Matches(input);
            var tokens = new List<Token>();

            foreach (Match match in matches)
            {
                var value = match.Value;

                if (double.TryParse(value, out _))
                {
                    tokens.Add(new Token(Token.TokenType.Number, value));
                }

                else if ("+-*/".Contains(value))
                {
                    tokens.Add(new Token(Token.TokenType.Operator, value));
                }

                else if (value == "(")
                {
                    tokens.Add(new Token(Token.TokenType.LeftParen, value));
                }

                else if (value == ")")
                {
                    tokens.Add(new Token(Token.TokenType.RightParen, value));
                }
            }

            return tokens;
        }

    }
}
