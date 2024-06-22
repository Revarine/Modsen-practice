using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        throw new Exception("Unknown operator: " + binaryExpr.Operator);
                }

            }
            throw new Exception("Unknown expression type");
        }
    }
}
