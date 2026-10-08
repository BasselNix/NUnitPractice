namespace MathAPI_Testing
{
    internal class Util
    {
        // builds request body of POST request from maths expression string and precision integer 
        public static string RequestBody(string expression, int precision)
        {
            return $"{{\"expr\": \"{expression}\", \"precision\": {precision}}}";
        }

        public static double ResponseResult(Dictionary<string, string>? response)
        {
            if (response != null && response.TryGetValue("result", out string? resultString))
            {
                if (double.TryParse(resultString, out double result))
                    return result;

                throw new Exception("Failed to convert response's result string");
            }

            throw new Exception("Failed to parse response's result string");
        }

        public static double OperationResult(double x, double y, Func<double, double, double> op)
        {
            return op(x, y);
        }
    }
}
