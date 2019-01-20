namespace API.Options
{
    public class AuthenticationOptions
    {
        public string BackendHost { get; set; }
        public string FrontendHost { get; set; }
        public string IssuerSigningKey { get; set; }
        public string TokenExpireDays { get; set; }
    }
}
