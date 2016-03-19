namespace ValantTest.Client.Sdk.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO;

    public interface INotificationsClient
    {
        Task<List<NotificationDto>> GetNotificationsByDate(DateTime date);
    }
}
