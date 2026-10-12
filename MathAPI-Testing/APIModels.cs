namespace MathAPI_Testing.Models
{
    public class MathRequest
    {
        public string Expr { get; set; } = string.Empty;
        public int Precision { get; set; }
    }

    public class MathResponse
    {
        public double Result { get; set; }
    }
}