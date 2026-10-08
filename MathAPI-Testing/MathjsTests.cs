using RestSharp;

namespace MathAPI_Testing
{
    [TestFixture]
    public class MathjsTests
    {
        private readonly RestClient _client = new RestClient("http://api.mathjs.org/v4/");
        private readonly int _precision = TestContext.Parameters.Get<int>("RoundingIndex", 100);

        [OneTimeSetUp]
        public void Setup()
        {
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        [Category("Addition")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0,-12)]
        [TestCase(1235.55,4.45)]
        public void TestAddition(double x, double y)
        {
            try
            {
                var request = new RestRequest("", Method.Post);

                string body = Util.RequestBody($"{x} + {y}", _precision);
                request.AddBody(body);

                var response = _client.Post<Dictionary<string, string>>(request);

                double actualResult = Util.ResponseResult(response);
                double expectedResult = Util.OperationResult(x, y, (x, y) => x + y);

                Assert.That(actualResult, Is.EqualTo(expectedResult));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        

        [Category("Subtraction")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestSubtraction(double x, double y)
        {
            try
            {
                var request = new RestRequest("", Method.Post);

                string body = Util.RequestBody($"{x} - {y}", _precision);
                request.AddBody(body);

                var response = _client.Post<Dictionary<string, string>>(request);

                double actualResult = Util.ResponseResult(response);
                double expectedResult = Util.OperationResult(x, y, (x, y) => x - y);

                Assert.That(actualResult, Is.EqualTo(expectedResult));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Category("Multiplication")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestMultiplication(double x, double y)
        {
            try
            {
                var request = new RestRequest("", Method.Post);

                string body = Util.RequestBody($"{x} * {y}", _precision);
                request.AddBody(body);

                var response = _client.Post<Dictionary<string, string>>(request);

                double actualResult = Util.ResponseResult(response);
                double expectedResult = Util.OperationResult(x, y, (x, y) => x * y);

                Assert.That(actualResult, Is.EqualTo(expectedResult));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Category("Division")]
        [TestCase(2, 3)]
        [TestCase(-137, 7)]
        [TestCase(0, -12)]
        [TestCase(1235.55, 4.45)]
        public void TestDivision(double x, double y)
        {
            try
            {
                var request = new RestRequest("", Method.Post);

                string body = Util.RequestBody($"{x} / {y}", _precision);
                request.AddBody(body);

                var response = _client.Post<Dictionary<string, string>>(request);

                double actualResult = Util.ResponseResult(response);
                double expectedResult = Util.OperationResult(x, y, (x, y) => x / y);

                Assert.That(actualResult, Is.EqualTo(expectedResult).Within(Math.Pow(10, -_precision)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Category("Square Root")]
        [TestCase(16)]
        [TestCase(32)]
        [TestCase(1235.55)]
        public void TestSquareRoot(double x)
        {
            try
            {
                var request = new RestRequest($"?expr=sqrt({x})");

                var response = _client.Get(request);

                if (double.TryParse(response.Content, out double actualResult) == false)
                    throw new Exception("Failed to convert _client response to double");

                double expectedResult = Math.Sqrt(x);

                Assert.That(actualResult, Is.EqualTo(expectedResult).Within(Math.Pow(10, -_precision)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
