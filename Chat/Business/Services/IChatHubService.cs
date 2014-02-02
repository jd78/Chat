namespace Chat.Business.Services
{
    public interface IChatHubService
    {
        void AddUser(string name);
        void RemoveUser(string name);
    }
}
