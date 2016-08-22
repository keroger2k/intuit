using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;

using System.Globalization;
using System.Text.Encodings.Web;
using OAuth.Intuit;

namespace AuthWithLinkedIn.Controllers
{
    public class HomeController : Controller
    {
        private const string SandboxAccountingUrl = "https://sandbox-quickbooks.api.intuit.com";
        private const string UriFormatted = "{0}/v3/company/{1}/{2}";

        private readonly HttpClient _httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public async Task<IActionResult> FirstQuery()
        {
            //var requestToken = User.Claims.FirstOrDefault(c => c.Type.Equals("urn:intuit:accessToken")).Value;
            //var requestTokenSecret = User.Claims.FirstOrDefault(c => c.Type.Equals("urn:intuit:accessTokenSecret")).Value;
            var accessToken = User.Claims.FirstOrDefault(c => c.Type.Equals("urn:intuit:accessToken")).Value;
            var accessTokenSecret = User.Claims.FirstOrDefault(c => c.Type.Equals("urn:intuit:accessTokenSecret")).Value;

            var queryHandler = new IntuitQueryHandler(new AccessToken
            {
                Token = accessToken,
                TokenSecret = accessTokenSecret
            });

            var response = await queryHandler.QueryCompanyInfo();

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
