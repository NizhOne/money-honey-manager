namespace API.Options
{
    public class AuthenticationOptions
    {
        public string BackendHost { get; set; }
        public string FrontendHost { get; set; }
        public string IssuerSigningKey { get; set; }
        public string TokenExpireDays { get; set; }
        public Facebook Facebook { get; set; }
    }

    public class Facebook
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
