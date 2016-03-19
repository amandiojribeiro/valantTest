namespace ValantTest.Application.Services.EventsDispatcher
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEventsDispatcherService
    {
        Task InitializeEventDispatcher(CancellationToken token);
    }
}
