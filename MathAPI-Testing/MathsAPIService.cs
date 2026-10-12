using MathAPI_Testing.Models;
using RestSharp;

namespace MathAPI_Testing
{
    public class MathsAPIService
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
            request.AddJsonBody(new MathRequest { Expr = expression, Precision = precision });

            return _client.Post<MathResponse>(request);
        }

        public double GetExpression(string expression)
        {
            var request = new RestRequest($"?expr={Uri.EscapeDataString(expression)}");
            double.TryParse(_client.Get(request).Content, out double result);

            return result;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}