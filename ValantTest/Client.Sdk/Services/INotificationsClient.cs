namespace ValantTest.Client.Sdk.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO;
    using System;

    public interface INotificationsClient
    {
        Task<List<NotificationDto>> GetNotificationsByDate(DateTime date);
    }
}
