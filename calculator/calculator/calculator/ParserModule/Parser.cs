using System;
using System.Collections.Generic;
using System.Linq;

namespace calculator.ParserF;

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
        if (tokens[position].Type == TokenTypesEnum.TokenType.Operator && tokens[position].Value == "-")
        {
            position++;
            var factor = ParseOperand();
            return new BinaryExpression(new NumberExpression(0), factor, "-");
        }

        if (tokens[position].Type == TokenTypesEnum.TokenType.Number)
        {
            return new NumberExpression(double.Parse(tokens[position++].Value));
        }

        if (tokens[position].Type == TokenTypesEnum.TokenType.Identifier)
        {
            var identifier = tokens[position++].Value;

            if (position < tokens.Count && tokens[position].Value == "(")
            {
                position++; // scip (
                var args = new List<Expression>();
                while (tokens[position].Value != ")")
                {
                    args.Add(ParseExpression());
                    if (tokens[position].Value == ",")
                    {
                        position++;
                    }
                }
                position++; // skip )
                return new FunctionCallExpression(identifier, args);
            }
            return new VariableExpression(identifier);
        }

        if (tokens[position].Type == TokenTypesEnum.TokenType.LeftParen)
        {
            position++;
            var expr = ParseExpression();

            if (tokens[position].Type != TokenTypesEnum.TokenType.RightParen)
            {
                throw new Exception("Missing closing parenthesis");
            }

            position++;
            return expr;
        }

        throw new Exception("Unexpected token: " + tokens[position].Value);
    }
}