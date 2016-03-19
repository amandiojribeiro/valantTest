namespace ValantTest.Presentation.Api
{
    using System.Web.Http;
    using Infrastructure.CrossCutting.Adapters;
    using Infrastructure.CrossCutting.Adapters.Automapper;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Services.EventsDispatcher;
    using System.Diagnostics;
    using System;

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly CancellationTokenSource ApplicationCancelationToken = new CancellationTokenSource();
        private static Task eventDispatcherTask = null;

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());

            this.InitializeEventsDispatcher();
        }

        protected void Application_End()
        {
            try
            {
                ApplicationCancelationToken.Cancel();
                eventDispatcherTask.Wait();
            }
            catch (Exception ex)
            {
                Debug.Write(ex, "EventDispatcherTask cancel");
            }
        }

        private void InitializeEventsDispatcher()
        {
            var eventsDispatcher = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IEventsDispatcherService)) as IEventsDispatcherService;
            if (eventsDispatcher != null)
            {
                eventDispatcherTask = eventsDispatcher.InitializeEventDispatcher(ApplicationCancelationToken.Token);
            }
            else
            {
                Debug.Write("IEventsDispatcherService could not be resolved!");
            }
        }
    }
}
