using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Business;
using Chat.Business.Services;

namespace Chat.Controllers
{
    public class ChatApiController : ApiController
    {
        //
        // GET: /Home/
        private readonly IUserService _userService;

        public ChatApiController() : this(IocContainerManager.Instance.ResolveDependency<IUserService>())
        {
            
        }

        public ChatApiController(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable GetAllUsers()
        {
            return _userService.GetUsers();
        }
    }
}
