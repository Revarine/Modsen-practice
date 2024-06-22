using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    public class Parser
    {
        private List<Token> tokens;
        private int position;

        public Expression Parse( string input )
        {
            var lexer = new Lexer();

            tokens = lexer.Tokenize(input);// Получение списка токенов из входной строки
            position = 0;

            Console.WriteLine("Tokens:");

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            return ParseExpression();
        }

        private Expression ParseExpression() // парсит сложение и вычитание
        {
            var left = ParseTerm();

            while (position < tokens.Count && (tokens[position].Value == "+" || tokens[position].Value == "-"))
            {
                var op = tokens[position++].Value;
                var right = ParseTerm();
                left = new BinaryExpression(left, right, op);

                Console.WriteLine($"Parsed Expression: {left}");
            }

            return left;
        }

        private Expression ParseTerm() // для умножения и деления
        {
            var left = ParseOperand();

            while (position < tokens.Count && (tokens[position].Value == "*" || tokens[position].Value == "/"))
            {
                var op = tokens[position++].Value;
                var right = ParseOperand();
                left = new BinaryExpression(left, right, op);

                Console.WriteLine($"Parsed Term: {left}");
            }

            return left;
        }

        private Expression ParseOperand()
        {
            if (tokens[position].Type == Token.TokenType.Number)
            {
                var numberExpr = new NumberExpression(double.Parse(tokens[position++].Value));
                Console.WriteLine($"Parsed Factor: {numberExpr}");
                return numberExpr;
            }

            if (tokens[position].Type == Token.TokenType.LeftParen)
            {
                position++;
                var expr = ParseExpression();

                if (tokens[position].Type != Token.TokenType.RightParen)
                {
                    throw new Exception("Missing closing parenthesis");
                }

                position++;
                return expr;

            }

            throw new Exception("Unexpected token: " + tokens[position].Value);
        }
    }
}
