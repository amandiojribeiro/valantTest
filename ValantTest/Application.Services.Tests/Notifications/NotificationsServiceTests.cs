﻿namespace Application.Services.Tests.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ValantTest.Application.DTO;
    using ValantTest.Application.Services.NotificationService;
    using ValantTest.Domain.Core.TypedRepositories;
    using ValantTest.Domain.Model;
    using ValantTest.Infrastructure.CrossCutting.Adapters;
    using ValantTest.Infrastructure.CrossCutting.Adapters.Automapper;

    [TestClass]
    public class NotificationsServiceTests
    {
        private Mock<INotificationRepository> notificationRepository;

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

        private NotificationDto FakeNotificationDto
        {
            get
            {
                return new NotificationDto
                {
                    Description = "Test description",
                };
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            this.notificationRepository = new Mock<INotificationRepository>();
            var dummy = new Mock<NotificationService>();
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }

        [TestMethod]
        [TestCategory("Notifications")]
        [ExpectedException(typeof(System.NullReferenceException))]
        public async Task Save_Notification_Pass_Null_Value_Test()
        {
            // Setup 
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>()));
            var notificationService = new NotificationService(this.notificationRepository.Object);

            // Act
            var result = await notificationService.SaveNotification(null);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Notifications")]
        public async Task Save_Notification_Pass_Valid_NotificationDto_Test()
        {
            // Setup
            this.notificationRepository.Setup(x => x.SaveNotificationAsync(It.IsAny<Notification>())).Returns(Task.FromResult<Notification>(this.FakeNotification));
            var notificationService = new NotificationService(this.notificationRepository.Object);

            // Act
            var result = await notificationService.SaveNotification(this.FakeNotificationDto);

            // Assert
            Assert.AreEqual(this.FakeNotification.Description, this.FakeNotificationDto.Description);
        }

        [TestMethod]
        [TestCategory("Notifications")]
        public async Task Get_Notification_By_Date_Pass_Valid_Date_Test()
        {
            // Setup
            this.notificationRepository.Setup(x => x.GetNotificationsByDateAsync(It.IsAny<DateTime>())).Returns(Task.FromResult<List<Notification>>(new List<Notification> { this.FakeNotification }));
            var notificationService = new NotificationService(this.notificationRepository.Object);

            // Act
            var result = await notificationService.GetNotificationsByDate(DateTime.UtcNow);
             
            // Assert
            Assert.AreEqual(result[0].Description, this.FakeNotification.Description);
        }
    }
}
