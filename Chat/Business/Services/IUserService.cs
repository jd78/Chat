using System.Collections.Generic;

namespace Chat.Business.Services
{
    public interface IUserService
    {
        bool Login(string username);
        void Logout(string username);
        bool IsUserRegistered(string username);
        IList<string> GetUsers();
        void KeepAlive(string username);
    }
}
