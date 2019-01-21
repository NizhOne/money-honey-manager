using System;

namespace API.Exceptions
{
    public class FacebookTokenValidationException : Exception
    {
        public FacebookTokenValidationException(string message) : base(message)
        {
        }
    }
}
