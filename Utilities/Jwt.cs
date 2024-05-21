namespace TS.Bootcamp.ECommerce.WebAPI.Utilities
{
    public class Jwt
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}
