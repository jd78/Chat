using Chat.Business.Providers;
using Chat.Business.Services;
using Munq;

namespace Chat.Business
{
    public class IocContainerManager
    {
        private static IocContainerManager _instance ;
        private static IocContainer ContainerManager { get; set; }

        public static IocContainerManager Instance
        {
            get { return _instance ?? (_instance = new IocContainerManager()); }
        }

        public IocContainerManager()
        {
            ContainerManager = new IocContainer();

            ContainerManager.Register<IUserService, UserProvider>().AsCached();
            ContainerManager.Register<IChatHubService, ChatHubProvider>().AsCached();
        }

        public T ResolveDependency<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }
    }
}