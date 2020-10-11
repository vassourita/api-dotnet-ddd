namespace Api.Domain.Security
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }

        public string Issues { get; set; }

        public int Seconds { get; set; }
    }
}