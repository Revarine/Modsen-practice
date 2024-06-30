namespace Shop.Exceptions;

public class RepeatingNameException : Exception
{
    public RepeatingNameException(string message) : base(message)
    {
        
    }
}