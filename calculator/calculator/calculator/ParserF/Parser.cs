using System;
using System.Collections.Generic;
using System.Linq;

namespace calculator.ParserF
{
    public class Parser
    {
        private List<Token> tokens;
        private int position;

        public Expression Parse( string input )
        {
            var lexer = new Lexer();
            tokens = lexer.Tokenize(input);// geting token list
            position = 0;
            return ParseExpression();
        }

        private Expression ParseExpression() 
        {
            var left = ParseTerm();

            while (position < tokens.Count && (tokens[position].Value == "+" || tokens[position].Value == "-"))
            {
                var op = tokens[position++].Value;
                var right = ParseTerm();
                left = new BinaryExpression(left, right, op);
            }

            return left;
        }

        private Expression ParseTerm()
        {
            var left = ParseOperand();

            while (position < tokens.Count && (tokens[position].Value == "*" || tokens[position].Value == "/"))
            {
                var op = tokens[position++].Value;
                var right = ParseOperand();
                left = new BinaryExpression(left, right, op);
            }

            return left;
        }

        private Expression ParseOperand()
        {
            if (tokens[position].Type == TokenTypesEnum.TokenType.Number)
            {
                var numberExpr = new NumberExpression(double.Parse(tokens[position++].Value));
                return numberExpr;
            }

            if (tokens[position].Type == TokenTypesEnum.TokenType.Identifier)
            {
                var identifierExpr = new VariableExpression(tokens[position++].Value);
                return identifierExpr;
            }

            if (tokens[position].Type == TokenTypesEnum.TokenType.LeftParen)
            {
                position++;
                var expr = ParseExpression();

                if (tokens[position].Type != TokenTypesEnum.TokenType.RightParen)
                {
                    throw new ParserExceptions("Missing closing parenthesis");
                }

                position++;
                return expr;
            }

            throw new ParserExceptions("Unexpected token: " + tokens[position].Value);
        }
    }
}
