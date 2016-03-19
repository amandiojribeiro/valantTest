namespace ValantTest.Client.Sdk.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.DTO;
    using Helper;

    public class NotificationsClient : ApiClient, INotificationsClient
    {
        public async Task<List<NotificationDto>> GetNotificationsByDate(DateTime date)
        {
            return await this.Get<List<NotificationDto>>("notifications/" + date);
        }
    }
}
