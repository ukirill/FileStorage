using DBModel.Helpers;
using DBModel.Repositories;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}