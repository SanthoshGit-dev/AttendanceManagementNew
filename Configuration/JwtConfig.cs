namespace AttendanceManagement.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; } = null!;
        public int ExpiryInHours { get; set; }
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
