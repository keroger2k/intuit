using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Text.Encodings.Web;
using System.Diagnostics;

namespace OAuth.Intuit
{
    public class IntuitQueryHandler 
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //private const string RequestTokenEndpoint = "https://sandbox-quickbooks.api.intuit.com/v3/company/123145846055972/companyinfo/123145846055972?minorversion=4";
        //private const string RequestTokenEndpoint = "https://appcenter.intuit.com/api/v1/Account/AppMenu";
        private const string RequestURI = "https://sandbox-quickbooks.api.intuit.com/v3/company/123145842388867/companyinfo/123145842388867?minorversion=4";
        private readonly HttpClient _httpClient;
        private readonly IntuitOptions _options;
        private readonly AccessToken _accessToken;

        public IntuitQueryHandler(AccessToken accessToken)
        {
            _httpClient = new HttpClient();
            _accessToken = accessToken;
            _options = new IntuitOptions {
                ConsumerKey = "qyprdo8dCoBnClt1hxLoCEbdGnZRuK",
                ConsumerSecret = "QR8OmIvGH3FwEvRjqzmj9jKuxKzlux9SAPaNBJxE",
            };

        }

        public async Task<FormCollection> QueryCompanyInfo()
        {

            var nonce = Guid.NewGuid().ToString("N");

            var authorizationParts = new Dictionary<string, string>
            {
                { "oauth_token", _accessToken.Token },
                { "oauth_nonce", nonce },
                { "oauth_consumer_key", _options.ConsumerKey },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_timestamp", GenerateTimeStamp() },
                { "oauth_version", "1.0" }
            };

            var parameterBuilder = new StringBuilder();
            foreach (var authorizationKey in authorizationParts)
            {
                parameterBuilder.AppendFormat("{0}={1}&", UrlEncoder.Default.Encode(authorizationKey.Key), UrlEncoder.Default.Encode(authorizationKey.Value));
            }
            parameterBuilder.Length--;
            var parameterString = parameterBuilder.ToString();

            var canonicalizedRequestBuilder = new StringBuilder();
            canonicalizedRequestBuilder.Append(HttpMethod.Get.Method);
            canonicalizedRequestBuilder.Append("&");
            canonicalizedRequestBuilder.Append(UrlEncoder.Default.Encode(RequestURI));
            canonicalizedRequestBuilder.Append("&");
            canonicalizedRequestBuilder.Append(UrlEncoder.Default.Encode(parameterString));

            var signature = ComputeSignature(_options.ConsumerSecret, _accessToken.TokenSecret, canonicalizedRequestBuilder.ToString());
            authorizationParts.Add("oauth_signature", signature);

            var authorizationHeaderBuilder = new StringBuilder();
            authorizationHeaderBuilder.Append("OAuth ");
            foreach (var authorizationPart in authorizationParts)
            {
                authorizationHeaderBuilder.AppendFormat(
                    "{0}=\"{1}\",", authorizationPart.Key, UrlEncoder.Default.Encode(authorizationPart.Value));
            }
            authorizationHeaderBuilder.Length = authorizationHeaderBuilder.Length - 1;

            var request = new HttpRequestMessage(HttpMethod.Get, RequestURI);
            request.Headers.Add("Authorization", authorizationHeaderBuilder.ToString());
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("intuit_tid", "idg-e42phkc0eqia5v54h2dwnfon-50201957");
            request.Headers.Add("User-Agent", "APIExplorer");

            var response = await _httpClient.SendAsync(request);
            Debug.Write(response.RequestMessage.Headers.ToString());
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();

            var responseParameters = new FormCollection(new FormReader(responseText).ReadForm());
            
            return responseParameters;
        }

        private static string GenerateTimeStamp()
        {
            var secondsSinceUnixEpocStart = DateTime.UtcNow - Epoch;
            return Convert.ToInt64(secondsSinceUnixEpocStart.TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }

        private string ComputeSignature(string consumerSecret, string tokenSecret, string signatureData)
        {
            using (var algorithm = new HMACSHA1())
            {
                algorithm.Key = Encoding.ASCII.GetBytes(
                    string.Format(CultureInfo.InvariantCulture,
                        "{0}&{1}",
                        UrlEncoder.Default.Encode(consumerSecret),
                        string.IsNullOrEmpty(tokenSecret) ? string.Empty : UrlEncoder.Default.Encode(tokenSecret)));
                var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(signatureData));
                return Convert.ToBase64String(hash);
            }
        }
    }
}