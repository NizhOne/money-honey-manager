namespace API.Constants
{
    public static class EmailConfirmationConstants
    {
        public static string Endpoint = "{0}/api/Auth/ConfirmEmail?userId={1}&code={2}";
        public static string EmailBody = "Please confirm your account by<a href={0}> clicking here</a>.";
        public static string EmailSubject = "Confirm your email";

    }
}
