namespace ValantTest.Presentation.Api
{
    using System.Web.Http;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer().LoadConfiguration();
            return container;
        }
    }
}