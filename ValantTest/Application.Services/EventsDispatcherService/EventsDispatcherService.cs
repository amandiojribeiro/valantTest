namespace ValantTest.Application.Services.EventsDispatcher
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Core.TypedGateways;
    using Domain.Core.TypedRepositories;
    using Domain.Model;

    public class EventsDispatcherService : IEventsDispatcherService
    {
        private readonly INotificationRepository notificationRepository;

        private readonly IItemRepository itemRepository;

        private readonly ISignalRGateway gateway;

        private volatile int consecutiveErrorCount = 0;

        public EventsDispatcherService(INotificationRepository notificationRepository, IItemRepository itemRepository, ISignalRGateway gateway)
        {
            this.itemRepository = itemRepository;
            this.notificationRepository = notificationRepository;
            this.gateway = gateway;
        }

        public Task InitializeEventDispatcher(CancellationToken token)
        {
            return Task.Factory.StartNew(
                async () =>
                {
                    try
                    {
                        while (!token.IsCancellationRequested)
                        {
                            if (this.consecutiveErrorCount >= 100)
                            {
                                throw new Exception("Max Consecutive Errors exceeded!");
                            }

                            try
                            {
                                await this.Dispatch();
                                Thread.Sleep(5000);
                            }
                            catch (Exception ex)
                            {
                                Interlocked.Increment(ref this.consecutiveErrorCount);
                                Debug.Write(ex, string.Format("IEventsDispatcherService.RunEventDispatcher [Count: {0}]", this.consecutiveErrorCount));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex, "Events Dispatcher Service Stop working!!!");
                        throw;
                    }
                },
                TaskCreationOptions.LongRunning);
        }

        private async Task Dispatch()
        {
            try
            {
                var date = DateTime.UtcNow;
                var result = await this.itemRepository.GetExpiredItemsByDate(date);
                foreach (Item item in result)
                {
                    var notification = new Notification { Id = Guid.NewGuid(), Description = string.Format("Item {0} has expired!", item.Label), NotificationDate = DateTime.UtcNow, Type = 1 };
                    await this.notificationRepository.SaveNotificationAsync(notification);
                    await this.gateway.SendMessage(notification);
                }
                await this.itemRepository.RemoveExpiredItems(date);
                return;
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref this.consecutiveErrorCount);
                Debug.Write(ex, "IEventsDispatcherService.DispatchFirst");
                throw;
            }
        }
    }
}
