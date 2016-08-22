using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace OAuth.Intuit
{
    public interface IIntuitEvents : IRemoteAuthenticationEvents
    {
        /// <summary>
        /// Invoked whenever Twitter succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        Task CreatingTicket(IntuitCreatingTicketContext context);

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the Twitter middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="Http.Authentication.AuthenticationProperties"/> of the challenge </param>
        Task RedirectToAuthorizationEndpoint(IntuitRedirectToAuthorizationEndpointContext context);
    }
}
