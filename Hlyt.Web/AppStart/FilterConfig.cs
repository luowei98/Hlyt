using System.Web.Mvc;
using Hlyt.Web.Core.ActionFilters;


namespace Hlyt.Web.AppStart
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserFilter());
        }
    }
}
