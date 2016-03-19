namespace Application.Services.Tests.Inventory
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ValantTest.Application.Services.InventoryService;
    using ValantTest.Domain.Core.TypedGateways;
    using ValantTest.Domain.Core.TypedRepositories;
    using ValantTest.Domain.Model;
    using ValantTest.Infrastructure.CrossCutting.Adapters;
    using ValantTest.Infrastructure.CrossCutting.Adapters.Automapper;
    using ValantTest.Application.DTO;

    [TestClass]
    public class InventoryServiceTests
    {
        private Mock<IItemRepository> itemRepository;
        private Mock<INotificationRepository> notificationRepository;
        private Mock<ISignalRGateway> gateway;

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

        private ItemDto FakeValidItemDto
        {
            get
            {
                return new ItemDto
                {
                    Description = "Test description",
                    ExpirationDate = DateTime.UtcNow,
                    Label = "Test",
                };
            }
        }

        private Item FakeValidItemWithOne
        {
            get
            {
                return new Item
                {
                    Count = 1,
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

            var dummy = new Mock<InventoryService>();

            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }

        [TestMethod]
        [TestCategory("Inventory")]
        public async Task Take_Item_By_Label_Not_Found_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(null));
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>()));
            this.gateway.Setup(x => x.SendMessage(It.IsAny<Notification>()));

            var notification = new Notification { Id = Guid.NewGuid(), Description = string.Format("Item {0} has been taken!<br/>There are {1} left;", "Teste", "1"), NotificationDate = DateTime.UtcNow, Type = 2 };

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.TakeItemByLabel(null);

            ///Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Inventory")]
        public async Task Take_Item_By_Label_Found_Count_More_Than_Zero_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(this.FakeValidItem));
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>())).Returns(Task.FromResult<Notification>(this.FakeNotification));
            this.gateway.Setup(x => x.SendMessage(It.IsAny<Notification>())).Returns(Task.FromResult(0));
            this.itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>())).Returns(Task.FromResult(0));

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.TakeItemByLabel("Test");

            ///Assert
            Assert.AreEqual(this.FakeValidItem.Label, result.Label);
            Assert.AreEqual(this.FakeValidItem.Description, result.Description);
            Assert.IsTrue(this.FakeValidItem.Count > result.Count);

            this.itemRepository.Verify(
               x => x.UpdateAsync(It.IsAny<Item>()),
               Times.Once);

            this.notificationRepository.Verify(
                x => x.SaveNotificationAsync(It.IsAny<Notification>()),
                Times.Once);

            this.gateway.Verify(
              x => x.SendMessage(It.IsAny<Notification>()),
              Times.Once);
        }

        [TestMethod]
        [TestCategory("Inventory")]
        public async Task Take_Item_By_Label_Found_Count_Is_Zero_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(this.FakeValidItemWithOne));
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>())).Returns(Task.FromResult<Notification>(this.FakeNotification));
            this.gateway.Setup(x => x.SendMessage(It.IsAny<Notification>())).Returns(Task.FromResult(0));
            this.itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>())).Returns(Task.FromResult(0));

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.TakeItemByLabel("Test");

            ///Assert
            Assert.AreEqual(this.FakeValidItem.Label, result.Label);
            Assert.AreEqual(this.FakeValidItem.Description, result.Description);
            Assert.IsTrue(this.FakeValidItem.Count > result.Count);

            this.itemRepository.Verify(
               x => x.DeleteAsync(It.IsAny<Item>()),
               Times.Once);

            this.notificationRepository.Verify(
                x => x.SaveNotificationAsync(It.IsAny<Notification>()),
                Times.Once);

            this.gateway.Verify(
              x => x.SendMessage(It.IsAny<Notification>()),
              Times.Once);
        }

        [TestMethod]
        [TestCategory("Inventory")]
        [ExpectedException(typeof(System.NullReferenceException))]
        public async Task Save_Item_Pass_Null_ItemDto_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(null));
            this.itemRepository.Setup(x => x.SaveItemAsync(It.IsAny<Item>())).Returns(Task.FromResult<Item>(this.FakeValidItemWithOne));
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>())).Returns(Task.FromResult<Notification>(this.FakeNotification));
            this.gateway.Setup(x => x.SendMessage(It.IsAny<Notification>())).Returns(Task.FromResult(0));
            this.itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>())).Returns(Task.FromResult(0));

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.SaveItem(null);
        }

        [TestMethod]
        [TestCategory("Inventory")]
        public async Task Save_Item_Pass_Valid_ItemDto_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(null));
            this.itemRepository.Setup(x => x.SaveItemAsync(It.IsAny<Item>())).Returns(Task.FromResult<Item>(this.FakeValidItemWithOne));
            this.itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>())).Returns(Task.FromResult(0));

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.SaveItem(this.FakeValidItemDto);

            ///Assert
            Assert.AreEqual(this.FakeValidItemDto.Label, result.Label);
            Assert.AreEqual(this.FakeValidItemDto.Description, result.Description);
            Assert.AreEqual(result.Count, 1);

            this.itemRepository.Verify(
               x => x.SaveItemAsync(It.IsAny<Item>()),
               Times.Once);
        }

        [TestMethod]
        [TestCategory("Inventory")]
        public async Task Save_Item_Pass_Valid_ItemDto_Already_Exists_Test()
        {
            /// Setup
            this.itemRepository.Setup(x => x.GetItemByLablelAsync(It.IsAny<string>())).Returns(Task.FromResult<Item>(this.FakeValidItem));
            this.itemRepository.Setup(x => x.UpdateAsync(It.IsAny<Item>())).Returns(Task.FromResult(0));

            var inventoryService = new InventoryService(this.notificationRepository.Object, this.itemRepository.Object, this.gateway.Object);

            /// Act
            var result = await inventoryService.SaveItem(this.FakeValidItemDto);

            ///Assert
            Assert.AreEqual(this.FakeValidItem.Label, result.Label);
            Assert.AreEqual(this.FakeValidItem.Description, result.Description);
            Assert.IsTrue(this.FakeValidItem.Count < result.Count);

            this.itemRepository.Verify(
               x => x.UpdateAsync(It.IsAny<Item>()),
               Times.Once);
        }
    }
}
