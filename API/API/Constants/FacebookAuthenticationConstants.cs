namespace API.Constants
{
    public static class FacebookAuthenticationConstants
    {
        public static string AccessTokenEndpoint = "https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&grant_type=client_credentials";
        public static string ValidateTokenEndpoint = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}";
        public static string UserInfoEndpoint = "https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={0}";
    }
}
