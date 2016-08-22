using Microsoft.AspNetCore.Http.Authentication;

namespace OAuth.Intuit
{
    public class RequestToken
    {
        /// <summary>
        /// Gets or sets the Twitter request token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Twitter token secret.
        /// </summary>
        public string TokenSecret { get; set; }

        public bool CallbackConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties.
        /// </summary>
        public AuthenticationProperties Properties { get; set; }
    }
}