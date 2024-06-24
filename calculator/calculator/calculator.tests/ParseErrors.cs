using calculator.ParserF;

namespace calculator.tests;

public class ParseErrors
{
    private readonly Parser parser = new Parser();
    private readonly Computations computations = new Computations();


    [Fact]
    public void ParseNormal()
    {
        double expectedResult = 4;
            
        double realResult = computations.Calculate(
            parser.Parse("1 + 3")
        );

        Assert.Equal(expectedResult, realResult);
    }
        
    [Fact]
    public void ParseBrake()
    {
        Assert.Throws<ParserF.ParserExceptions>(() =>
        {
            double realResult = computations.Calculate(
                parser.Parse("--1 + 3")
            );
        });
    }

    [Fact]
    public void ParseNormal_WithBrackets_TwoArguments()
    {
        double expectedResult = 5;

        double realResult = computations.Calculate(
            parser.Parse("(2 + 3)")
        );

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void ParseNormal_WithBrackets_ManyArguments()
    {
        double expectedResult = 25;

        double realResult = computations.Calculate(
            parser.Parse("(-3 + 8) * (3 + 2)")
        );

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void ParseBrake_WithBrackets_TwoArguments()
    {
        Assert.Throws<ParserF.ParserExceptions>(() => 
        {
            double realResult = computations.Calculate(
                parser.Parse("())())()()()()() 7 ))) +2)")
            );
        });
    }

    [Fact]
    public void ParseBrake_WithBrackets_ManyArguments()
    {
        Assert.Throws<ParserF.ParserExceptions>(() => 
        {
            double realResult = computations.Calculate(
                parser.Parse("(((-3 + 8()) * ))(3.999 + 2.001)")
            );
        });
    }

    [Fact]
    public void ParseBrake_NoArguments()
    {
        Assert.Throws<ParserF.ParserExceptions>(() => 
        {
            double realResult = computations.Calculate(
                parser.Parse("") // fix
            );
        });
    }
        
    [Fact]
    public void ParseBrake_EmptyArguments()
    {
        Assert.Throws<ParserF.ParserExceptions>(() => 
        {
            double realResult = computations.Calculate(
                parser.Parse(" + ")
            );
        });
    }
        
    [Fact]
    public void ParseBrake_ManyOperators()
    {
        Assert.Throws<ParserF.ParserExceptions>(() => 
        {
            double realResult = computations.Calculate(
                parser.Parse("1 + 1 +") // fix
            );
        });
    }

}