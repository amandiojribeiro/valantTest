namespace Application.Services.Tests.EventDispatcher
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ValantTest.Domain.Core.TypedRepositories;
    using ValantTest.Domain.Core.TypedGateways;
    using ValantTest.Application.Services.EventsDispatcher;
    using ValantTest.Infrastructure.CrossCutting.Adapters;
    using ValantTest.Infrastructure.CrossCutting.Adapters.Automapper;
    using ValantTest.Domain.Model;
    using System.Threading;
    using System.Collections.Generic;

    [TestClass]
    public class EventDispatcherServicesTests
    {
        private Mock<IItemRepository> itemRepository;
        private Mock<INotificationRepository> notificationRepository;
        private Mock<ISignalRGateway> gateway;
        private static readonly CancellationTokenSource ApplicationCancelationToken = new CancellationTokenSource();
        private static Task eventDispatcherTask = null;

        private Item FakeValidItem
        {
            get
            {
                return new Item
                {
                    Count = 2,
                    Description = "Test description",
                    ExpirationDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Label = "Test",
                    Type = "1"
                };
            }
        }

        private Notification FakeNotification
        {
            get
            {
                return new Notification
                {
                    Description = "Test description",
                    NotificationDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                };
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            this.itemRepository = new Mock<IItemRepository>();
            this.notificationRepository = new Mock<INotificationRepository>();
            this.gateway = new Mock<ISignalRGateway>();

            var dummy = new Mock<EventsDispatcherService>();

            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }

        [TestMethod]
        [TestCategory("Dispatcher")]
        public void Event_Dispatcher_Initialize_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetExpiredItemsByDate(It.IsAny<DateTime>())).Returns(Task.FromResult<List<Item>>(new List<Item>{ FakeValidItem }));
            this.itemRepository.Setup(x => x.RemoveExpiredItems(It.IsAny<DateTime>())).Returns(Task.FromResult(0));
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>())).Returns(Task.FromResult<Notification>(FakeNotification));
            this.gateway.Setup(x => x.SendMessage(It.IsAny<Notification>())).Returns(Task.FromResult(0)); 

            var eventsDispatcherService = new EventsDispatcherService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            eventDispatcherTask = eventsDispatcherService.InitializeEventDispatcher(ApplicationCancelationToken.Token);

            Thread.Sleep(50);

            ApplicationCancelationToken.Cancel();
            eventDispatcherTask.Wait();

            this.gateway.Verify(
              x => x.SendMessage(It.IsAny<Notification>()));

            this.itemRepository.Verify(
               x => x.RemoveExpiredItems(It.IsAny<DateTime>()));

            this.itemRepository.Verify(
               x => x.GetExpiredItemsByDate(It.IsAny<DateTime>()));

            this.notificationRepository.Verify(
                x => x.SaveNotificationAsync(It.IsAny<Notification>()));
        }
    }
}
