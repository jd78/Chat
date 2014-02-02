using Chat.Business.Services;
using Microsoft.AspNet.SignalR;

namespace Chat.Business.Providers
{
    public class ChatHubProvider : IChatHubService
    {
        public void AddUser(string name)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.broadcastUserLogIn(name);
        }

        public void RemoveUser(string name)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.broadcastUserLogOut(name);
        }
    }
}