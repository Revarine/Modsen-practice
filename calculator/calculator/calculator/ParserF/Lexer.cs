using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace calculator.ParserF
{
    public class Lexer
    {
        private static readonly Regex tokenPattern = new Regex(@"\d+(\.\d+)?|[a-zA-Z_][a-zA-Z0-9_]*|[+\-*/()]");

        public List<Token> Tokenize( string input )
        {
            var matches = tokenPattern.Matches(input);
            var tokens = new List<Token>();

            for (int i = 0; i < matches.Count; i++)
            {
                var value = matches[i].Value;

                if (value == "-" && (i == 0 || "+-*/(".Contains(matches[i - 1].Value)))
                {
                    if (i + 1 < matches.Count && double.TryParse(matches[i + 1].Value, out _))
                    {
                        value += matches[i + 1].Value;
                        i++;
                    }
                }

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
                else
                {
                    tokens.Add(new Token(Token.TokenType.Identifier, value));
                }
            }

            return tokens;
        }
    }
}
