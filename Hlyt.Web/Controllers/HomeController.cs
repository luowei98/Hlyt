using System.Web.Mvc;


namespace Hlyt.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to YeHi!";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
