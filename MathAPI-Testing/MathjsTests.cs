using System.Globalization;
using NUnit.Framework;

namespace MathAPI_Testing
{
    [TestFixture]
    public class MathjsTests
    {
        public required MathsAPIService client;
        private readonly int _precision = TestContext.Parameters.Get<int>("RoundingIndex", 100);
        private double _tolerance;

        [OneTimeSetUp]
        public void Setup()
        {
            client = new MathsAPIService();
            _tolerance = Math.Pow(10, -_precision);
        }

        [Category("Addition")]
        // Positive + Positive
        [TestCase("2", "3")]
        // Positive + Negative
        [TestCase("137", "-7")]
        // Negative + Positive
        [TestCase("-137", "7")]
        // Negative + Negative
        [TestCase("-15.5", "-4.5")]
        // Zero boundaries
        [TestCase("0", "-12")]
        [TestCase("0", "0")]
        // Decimals
        [TestCase("1235.55", "4.45")]
        // Scientific notation
        [TestCase("1e3", "2.5e2")]
        [TestCase("-1e3", "5e-1")]
        public void TestAddition(string xStr, string yStr)
        {
            double x = double.Parse(xStr, CultureInfo.InvariantCulture);
            double y = double.Parse(yStr, CultureInfo.InvariantCulture);

            var response = client.PostExpression($"{xStr} + {yStr}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x + y;

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(_tolerance));
        }

        [Category("Subtraction")]
        // Positive - Positive
        [TestCase("2", "3")]
        // Positive - Negative
        [TestCase("137", "-7")]
        // Negative - Positive
        [TestCase("-137", "7")]
        // Negative - Negative
        [TestCase("-15.5", "-4.5")]
        // Zero boundaries
        [TestCase("0", "-12")]
        [TestCase("12", "0")]
        // Decimals
        [TestCase("1235.55", "4.45")]
        // Scientific notation
        [TestCase("1e3", "2.5e2")]
        [TestCase("5e2", "-1e2")]
        public void TestSubtraction(string xStr, string yStr)
        {
            double x = double.Parse(xStr, CultureInfo.InvariantCulture);
            double y = double.Parse(yStr, CultureInfo.InvariantCulture);

            var response = client.PostExpression($"{xStr} - {yStr}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x - y;

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(_tolerance));
        }

        [Category("Multiplication")]
        // Positive * Positive
        [TestCase("2", "3")]
        // Positive * Negative
        [TestCase("137", "-7")]
        // Negative * Positive
        [TestCase("-137", "7")]
        // Negative * Negative
        [TestCase("-12.5", "-4")]
        // Zero boundary
        [TestCase("0", "-12")]
        [TestCase("0", "0")]
        // Decimals
        [TestCase("1235.55", "4.45")]
        // Scientific notation
        [TestCase("1e3", "2e-2")]
        [TestCase("-2e2", "3e3")]
        public void TestMultiplication(string xStr, string yStr)
        {
            double x = double.Parse(xStr, CultureInfo.InvariantCulture);
            double y = double.Parse(yStr, CultureInfo.InvariantCulture);

            var response = client.PostExpression($"{xStr} * {yStr}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x * y;

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(_tolerance));
        }

        [Category("Division")]
        // Positive / Positive
        [TestCase("2", "3")]
        // Positive / Negative
        [TestCase("137", "-7")]
        // Negative / Positive
        [TestCase("-137", "7")]
        // Negative / Negative
        [TestCase("-100", "-4")]
        // Zero numerator
        [TestCase("0", "-12")]
        // Decimals
        [TestCase("1235.55", "4.45")]
        // Scientific notation
        [TestCase("1e4", "2e2")]
        [TestCase("5e-1", "2e-2")]
        public void TestDivision(string xStr, string yStr)
        {
            double x = double.Parse(xStr, CultureInfo.InvariantCulture);
            double y = double.Parse(yStr, CultureInfo.InvariantCulture);

            var response = client.PostExpression($"{xStr} / {yStr}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x / y;

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(_tolerance));
        }

        [Category("Square Root")]
        // Perfect square
        [TestCase("16")]
        // Non-perfect square / Decimal
        [TestCase("32")]
        [TestCase("1235.55")]
        // Zero boundary
        [TestCase("0")]
        // Scientific notation
        [TestCase("1e4")]
        [TestCase("2.25e2")]
        public void TestSquareRoot(string xStr)
        {
            double x = double.Parse(xStr, CultureInfo.InvariantCulture);

            double actualResult = client.GetExpression($"sqrt({xStr})");
            double expectedResult = Math.Sqrt(x);

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(_tolerance));
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
