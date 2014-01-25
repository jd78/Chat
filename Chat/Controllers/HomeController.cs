using System.Web.Mvc;
using Chat.Business;
using Chat.Business.Services;
using Chat.Models;

namespace Chat.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IUserService _userService;

        public HomeController() : this(IocContainerManager.Instance.ResolveDependency<IUserService>())
        {
            
        }

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoToChat(User model)
        {
            if(!ModelState.IsValid)
                return View("Index");

            if (_userService.Login(model.Username))
            {
                Session.Add("User", model);
                return Redirect("/Chat/index");
            }

            ViewBag.Error = "Username already registered, please choose a different username";
            return View("Index");

        }
    }
}
