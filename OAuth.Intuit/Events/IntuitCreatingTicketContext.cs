﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace OAuth.Intuit
{
    public class IntuitCreatingTicketContext : BaseIntuitContext
    {
        /// <summary>
        /// Initializes a <see cref="TwitterCreatingTicketContext"/>
        /// </summary>
        /// <param name="context">The HTTP environment</param>
        /// <param name="options">The options for Twitter</param>
        /// <param name="userId">Twitter user ID</param>
        /// <param name="screenName">Twitter screen name</param>
        /// <param name="accessToken">Twitter access token</param>
        /// <param name="accessTokenSecret">Twitter access token secret</param>
        /// <param name="user">User details</param>
        public IntuitCreatingTicketContext(
            HttpContext context,
            IntuitOptions options,
            string userId,
            string screenName,
            string accessToken,
            string accessTokenSecret,
            JObject user)
            : base(context, options)
        {
            UserId = userId;
            ScreenName = screenName;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            User = user ?? new JObject();
        }

        /// <summary>
        /// Gets the Twitter user ID
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Gets the Twitter screen name
        /// </summary>
        public string ScreenName { get; }

        /// <summary>
        /// Gets the Twitter access token
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// Gets the Twitter access token secret
        /// </summary>
        public string AccessTokenSecret { get; }

        /// <summary>
        /// Gets the JSON-serialized user or an empty
        /// <see cref="JObject"/> if it is not available.
        /// </summary>
        public JObject User { get; }

        /// <summary>
        /// Gets the <see cref="ClaimsPrincipal"/> representing the user
        /// </summary>
        public ClaimsPrincipal Principal { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }
    }
}

