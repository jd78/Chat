using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Models;

namespace Chat.Business.Services
{
    public interface IUserService
    {
        bool Login(string username);
        void Logout(string username);
        bool IsUserRegistered(string username);
        IList<string> GetUsers();

    }
}
