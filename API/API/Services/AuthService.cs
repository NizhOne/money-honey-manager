using API.Constants;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using API.Models.Domain;
using API.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationOptions authConfig;
        private static readonly HttpClient Client = new HttpClient();

        public AuthService(IOptions<AuthenticationOptions> authOptions)
        {
            this.authConfig = authOptions.Value;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.authConfig.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.authConfig.TokenExpireDays));

            var token = new JwtSecurityToken(
                this.authConfig.BackendHost,
                this.authConfig.BackendHost,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<FacebookUserData> GetFacebookUserInfoAsync(string accessToken)
        {
            var appAccessTokenResponse = await Client.GetStringAsync(String.Format(FacebookAuthenticationConstants.AccessTokenEndpoint, authConfig.Facebook.ClientId, authConfig.Facebook.ClientSecret));
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

            var userAccessTokenValidationResponse = await Client.GetStringAsync(String.Format(FacebookAuthenticationConstants.ValidateTokenEndpoint, accessToken, appAccessToken.AccessToken));
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            var userInfoResponse = await Client.GetStringAsync(String.Format(FacebookAuthenticationConstants.UserInfoEndpoint,accessToken));
            return JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
        }
    }
}
