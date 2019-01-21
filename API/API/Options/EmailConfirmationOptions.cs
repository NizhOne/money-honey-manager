namespace API.Options
{
    public class EmailConfirmationOptions
    {
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }
}
