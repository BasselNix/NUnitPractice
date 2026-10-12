using RestSharp;

namespace MathAPI_Testing
{
    [TestFixture]
    public class MathjsTests
    {
        public required MathsAPIService client;
        private readonly int _precision = TestContext.Parameters.Get<int>("RoundingIndex", 100);

        [OneTimeSetUp]
        public void Setup()
        {
            client = new MathsAPIService();
        }

        [Category("Addition")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0,-12)]
        [TestCase(1235.55,4.45)]
        public void TestAddition(double x, double y)
        {
            var response = client.PostExpression($"{x} + {y}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x + y;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        

        [Category("Subtraction")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestSubtraction(double x, double y)
        {
            var response = client.PostExpression($"{x} - {y}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x - y;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Category("Multiplication")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestMultiplication(double x, double y)
        {
            var response = client.PostExpression($"{x} * {y}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x * y;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Category("Division")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestDivision(double x, double y)
        {
            var response = client.PostExpression($"{x} / {y}", _precision);
            Assert.That(response, Is.Not.Null);

            double actualResult = response.Result;
            double expectedResult = x / y;

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(Math.Pow(10, -_precision)));
        }

        [Category("Square Root")]
        [TestCase(16)]
        [TestCase(32)]
        [TestCase(1235.55)]
        public void TestSquareRoot(double x)
        {
            double actualResult = client.GetExpression($"sqrt({x})");
            double expectedResult = Math.Sqrt(x);

            Assert.That(actualResult, Is.EqualTo(expectedResult).Within(Math.Pow(10, -_precision)));
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
