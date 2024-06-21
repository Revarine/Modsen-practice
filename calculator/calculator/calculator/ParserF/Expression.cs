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

    public class BinaryExpression : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }
        public string Operator { get; }

        public BinaryExpression( Expression left, Expression right, string op )
        {
            Left = left;
            Right = right;
            Operator = op;
        }

        public override string ToString()
        {
            return $"({Left} {Operator} {Right})";
        }
    }

    public class VariableExpression : Expression
    {
        public string Name { get; }
        public VariableExpression( string name )
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
