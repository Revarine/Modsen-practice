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
            throw new ParserExceptions("Unknown expression type");
        }
    }
}
