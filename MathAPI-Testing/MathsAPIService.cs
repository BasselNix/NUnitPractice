using MathAPI_Testing.Models;
using RestSharp;
using System.Globalization;

namespace MathAPI_Testing
{
    public class MathsAPIService : IDisposable
    {
        public RestClient _client;

        public MathsAPIService(string baseUrl = "http://api.mathjs.org/v4/")
        {
            _client = new RestClient(baseUrl);
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        public MathResponse? PostExpression(string expression, int precision)
        {
            var request = new RestRequest("", Method.Post);
            request.AddJsonBody(new { Expr = expression, Precision = precision });

            return _client.Post<MathResponse>(request);
        }

        public double GetExpression(string expression)
        {
            var request = new RestRequest($"?expr={Uri.EscapeDataString(expression)}");
            
            if (double.TryParse(_client.Get(request).Content, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                return result;

            throw new Exception("GET request returned response that could not be parsed to double");
        }

        public void Dispose()
        {
            _client?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}