namespace calculator.ParserF
{
    public class Token
    {
        public TokenTypesEnum.TokenType Type { get; }
        public string Value { get; }

        public Token( TokenTypesEnum.TokenType type, string value )
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
