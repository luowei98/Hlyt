using System.Linq;
using System.Security.Principal;
using Hlyt.Web.Core.Models;


namespace Hlyt.Web.Core.Extensions
{
    public static class SecurityExtensions
    {
        public static string Name(this IPrincipal user)
        {
            return user.Identity.Name;
        }

        public static bool InAnyRole(this IPrincipal user, params string[] roles)
        {
            return roles.Any(user.IsInRole);
        }

        public static HlytUser GetHlytUser(this IPrincipal principal)
        {
            if (principal.Identity is HlytUser)
                return (HlytUser) principal.Identity;

            return new HlytUser(string.Empty, new UserInfo());
        }
    }
}
