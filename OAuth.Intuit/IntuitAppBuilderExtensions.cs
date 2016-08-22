using OAuth.Intuit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace OAuth.Intuit
{
    public static class IntuitAppBuilderExtensions
    {

        /// <summary>
        /// Adds the <see cref="IntuitMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables Twitter authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseIntuitAuthentication(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<IntuitMiddleware>();
        }

        /// <summary>
        /// Adds the <see cref="IntuitMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables Twitter authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">An action delegate to configure the provided <see cref="IntuitOptions"/>.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseIntuitAuthentication(this IApplicationBuilder app, IntuitOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<IntuitMiddleware>(Options.Create(options));
        }
    }
}
