namespace ValantTest.Application.Services.NotificationService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Core.TypedRepositories;
    using Domain.Model;
    using DTO;
    using Infrastructure.CrossCutting.Adapters;

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationDto>> GetNotificationsByDate(DateTime date)
        {
            var result = await this.notificationRepository.GetNotificationsByDateAsync(date);
            return TypeAdapterHelper.Adapt<List<NotificationDto>>(result);
        }

        public async Task<NotificationDto> SaveNotification(NotificationDto request)
        {
            var notificationToInsert = TypeAdapterHelper.Adapt<Notification>(request);
            var result = await this.notificationRepository.SaveNotificationAsync(notificationToInsert);
            return TypeAdapterHelper.Adapt<NotificationDto>(result);
        }
    }
}
