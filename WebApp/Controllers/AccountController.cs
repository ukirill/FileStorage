using DBModel.Models;
using DBModel.Repositories;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository UserRepository { get; set; }

        public AccountController()
        {
            UserRepository = new NHUserRepository();
        }


        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                model.Email = model.Email.ToLower();             
                if (UserRepository.Validate(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                model.Email = model.Email.ToLower();
                User user = UserRepository.GetUserByEmail(model.Email);
                if (user == null)
                {
                    user = new User()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                    };
                    UserRepository.Create(user);

                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
