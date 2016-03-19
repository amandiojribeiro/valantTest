namespace ValantTest.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface INotificationService
    {
        Task<NotificationDto> SaveNotification(NotificationDto request);

        Task<List<NotificationDto>> GetNotificationsByDate(DateTime date);
    }
}
