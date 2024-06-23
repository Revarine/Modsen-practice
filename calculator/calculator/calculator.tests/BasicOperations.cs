using calculator.ParserF;

namespace calculator.tests
{
    public class BasicOperations
    {
        private readonly Parser parser = new Parser();
        private readonly Computations computations = new Computations();


        [Fact]
        public void AmountTest()
        {
            var expression = new BinaryExpression(
                new NumberExpression(1.00),
                new NumberExpression(3.00),
                "+"
            );

            double expectedResult = 4.00;
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void DifferenceTest()
        {
            var expression = new BinaryExpression(
                new NumberExpression(9.99),
                new NumberExpression(3.33),
                "-"
            );

            double expectedResult = 6.66;
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void MultiplicationTest()
        {
            var expression = new BinaryExpression(
                new NumberExpression(9.0007),
                new NumberExpression(9.0001),
                "*"
            );

            double expectedResult = 81.00720007;
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void DivisionTest()
        {
            var expression = new BinaryExpression(
                new NumberExpression(81.00720007),
                new NumberExpression(9.0007),
                "/"
            );

            double expectedResult = 9.0001;
            double realResult = computations.Calculate(expression);

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void PriorityTest()
        {
            double expectedResult = 6;

            double realResult = computations.Calculate(
                parser.Parse("2 + 2 * 2")
            );

            Assert.Equal(expectedResult, realResult);
        }

        [Fact]
        public void ManyOperatorsTests()
        {
            double expectedResult = 8;

            double realResult = computations.Calculate(
                parser.Parse("2 + 4 / 2 + 2 * 2")
            );

            Assert.Equal(expectedResult, realResult);
        }
    }
}