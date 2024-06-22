using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.ParserF
{
    public abstract class Expression { }

    public class NumberExpression : Expression 
    {
        public double Value { get; }
        public NumberExpression( double value )
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class BinaryExpression : Expression //само выражение
    {
        public Expression Left { get; }//левый операнд
        public Expression Right { get; }//правый операнд
        public string Operator { get; }

        public BinaryExpression( Expression left, Expression right, string op )
        {
            Left = left; 
            Right = right;
            Operator = op;
        }

        public override string ToString()
        {
            return $"({Left} {Operator} {Right})"; // тут и строиться наше выражение прим. left= 2  right=1 operand=+ => 2+1
        }
    }
}
