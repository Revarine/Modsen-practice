namespace calculator.ParserF
{
    public class Token
    {
        public enum TokenType
        {
            Number,
            Operator,
            LeftParen,
            RightParen,
            Identifier
        }

        public TokenType Type { get; }
        public string Value { get; }

        public Token( TokenType type, string value )
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
    }
}
