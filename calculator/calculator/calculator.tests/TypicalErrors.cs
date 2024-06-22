using calculator.ParserF;

namespace calculator.tests
{
    public class TypicalErrors
    {
        private readonly Computations computations = new Computations();


        [Fact]
        public void DevidedByZero()
        {
            var expression = new BinaryExpression(
                new NumberExpression(9.00),
                new NumberExpression(0.00),
                "/"
            );

            string expectedResult = "∞";
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult.ToString());
        }

        [Fact]
        public void NegativeMultiplication()
        {
            var expression = new BinaryExpression(
                new NumberExpression(-7.00),
                new NumberExpression(-7.00),
                "*"
            );

            double expectedResult = 49.00;
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void WithoutParameters_String()
        {
            Assert.Throws<ParserF.ParserExceptions>(() =>
            {
                var expression = new BinaryExpression(
                    new NumberExpression(-7.00),
                    new NumberExpression(-7.00),
                    "random"
                );

                double realResult = computations.Calculate(expression);
            });
        }

        [Fact]
        public void WithoutParameters_Char()
        {
            Assert.Throws<ParserF.ParserExceptions>(() =>
            {
                var expression = new BinaryExpression(
                    new NumberExpression(-7.00),
                    new NumberExpression(-7.00),
                    "#"
                );

                double realResult = computations.Calculate(expression);
            });
        }


        [Fact]
        public void WithoutParameters_OneArgument()
        {
            double number = -7.00;
            var expression = new NumberExpression(number);
            double expectedResult = number;

            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }
    }
}
