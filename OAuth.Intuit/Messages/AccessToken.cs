namespace OAuth.Intuit
{
    public class AccessToken : RequestToken
    {
        /// <summary>
        /// Gets or sets the Twitter User ID.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the Twitter screen name.
        /// </summary>
        public string ScreenName { get; set; }
    }
}