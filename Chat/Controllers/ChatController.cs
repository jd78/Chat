using System.Web.Mvc;
using Chat.Business;
using Chat.Business.Services;
using Chat.Models;

namespace Chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserService _userService;

        public ChatController() : this(IocContainerManager.Instance.ResolveDependency<IUserService>())
        {
            
        }

        public ChatController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View(Session["User"]);
        }

        public ActionResult Logout(User model)
        {
            _userService.Logout(model.Username);
            Session.Remove("User");
            return Redirect("/Home/index");
        }
    }
}
