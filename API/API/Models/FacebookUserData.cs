﻿using Newtonsoft.Json;

namespace API.Models
{
    public class FacebookUserData
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Locale { get; set; }
    }

    public class FacebookUserAccessTokenData
    {
        [JsonProperty("app_id")]
        public long AppId { get; set; }

        public string Type { get; set; }

        public string Application { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class FacebookUserAccessTokenValidation
    {
        public FacebookUserAccessTokenData Data { get; set; }
    }

    public class FacebookAppAccessToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

    public class FacebookAuthViewModel
    {
        public string AccessToken { get; set; }
    }
}
