using System;
using System.Web.Mvc;


namespace Hlyt.Web.Core.ActionFilters
{
    public class HlytAuthorize : AuthorizeAttribute
    {
        public HlytAuthorize(params string[] roles)
        {
            Roles = String.Join(", ", roles);
        }
    }
}
