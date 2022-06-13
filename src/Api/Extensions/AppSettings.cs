namespace Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public string Issuer { get; set; }
        public string ValidAt { get; set; }
    }
}
