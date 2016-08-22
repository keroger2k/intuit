using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace OAuth.Intuit
{
    public class BaseIntuitContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="BaseTwitterContext"/>
        /// </summary>
        /// <param name="context">The HTTP environment</param>
        /// <param name="options">The options for Twitter</param>
        public BaseIntuitContext(HttpContext context, IntuitOptions options)
            : base(context)
        {
            Options = options;
        }

        public IntuitOptions Options { get; }
    }
}
