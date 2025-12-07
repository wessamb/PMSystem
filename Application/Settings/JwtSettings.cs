namespace PMSystem.Application.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiresInMinutes { get; set; } = 60;
    }
}
