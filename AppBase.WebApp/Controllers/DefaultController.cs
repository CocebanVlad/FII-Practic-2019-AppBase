using System.Web.Mvc;

namespace AppBase.WebApp.Controllers
{
    public class DefaultController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}