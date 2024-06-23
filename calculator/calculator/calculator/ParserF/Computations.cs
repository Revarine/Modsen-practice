using System;
using System.Linq;

namespace calculator.ParserF
{
    public class Computations
    {
        public double Calculate( Expression expression )
        {
            if (expression is NumberExpression numberExpr)
            {
                return numberExpr.Value;
            }

            if (expression is VariableExpression variableExpr)
            {
                var variable = DataVault.DataVault.GetVariables().FirstOrDefault(v => v.Name == variableExpr.Name);
                
                if (variable == null)
                {
                    throw new ParserExceptions($"Variable not found: {variableExpr.Name}");
                }
                return variable.Value;
            }

            if (expression is BinaryExpression binaryExpr)
            {
                var left = Calculate(binaryExpr.Left);
                var right = Calculate(binaryExpr.Right);

                switch (binaryExpr.Operator)
                {
                    case "+":
                        return left + right;
                    case "-":
                        return left - right;
                    case "*":
                        return left * right;
                    case "/":
                        return left / right;
                    default:
                        throw new ParserExceptions("Unknown operator: " + binaryExpr.Operator);
                }
            }

            if (expression is FunctionCallExpression functionCallExpr)
            {
                var function = DataVault.DataVault.GetFunctions().FirstOrDefault(f => f.Name == functionCallExpr.Name);

                if (function == null)
                {
                    throw new ParserExceptions($"Function not found: {functionCallExpr.Name}");
                }

                if (function.Parameters.Count != functionCallExpr.Arguments.Count)
                {
                    throw new ParserExceptions($"Argument count mismatch for function: {functionCallExpr.Name}");
                }

                var localVariables = new Dictionary<string, double>();

                for (int i = 0; i < function.Parameters.Count; i++)
                {
                    var paramName = function.Parameters[i].Name;
                    var argValue = Calculate(functionCallExpr.Arguments[i]);
                    localVariables[paramName] = argValue;
                }

                var expressionParser = new Parser();
                var parsedExpression = expressionParser.Parse(function.Expression);

                return CalculateWithVariables(parsedExpression, localVariables);
            }

            throw new ParserExceptions("Unknown expression type");
        }

        private double CalculateWithVariables( Expression expression, Dictionary<string, double> variables )
        {
            if (expression is NumberExpression numberExpr)
            {
                return numberExpr.Value;
            }

            if (expression is VariableExpression variableExpr)
            {

                if (!variables.TryGetValue(variableExpr.Name, out var value))
                {
                    throw new ParserExceptions($"Variable not found: {variableExpr.Name}");
                }
                return value;
            }
            if (expression is BinaryExpression binaryExpr)
            {
                var left = CalculateWithVariables(binaryExpr.Left, variables);
                var right = CalculateWithVariables(binaryExpr.Right, variables);

                switch (binaryExpr.Operator)
                {
                    case "+":
                        return left + right;
                    case "-":
                        return left - right;
                    case "*":
                        return left * right;
                    case "/":
                        return left / right;
                    default:
                        throw new ParserExceptions("Unknown operator: " + binaryExpr.Operator);
                }
            }
            if (expression is FunctionCallExpression functionCallExpr)
            {
                throw new ParserExceptions("Nested functions are not supported.");
            }

            throw new ParserExceptions("Unknown expression type");
        }
    }
}
