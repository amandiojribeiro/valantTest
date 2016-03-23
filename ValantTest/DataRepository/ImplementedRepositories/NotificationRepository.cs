namespace ValantTest.Data.Repository.ImplementedRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Core.TypedRepositories;
    using Domain.Model;

    public class NotificationRepository : GenericListRepository<Notification, Guid>, INotificationRepository
    {
        public async Task<List<Notification>> GetNotificationsByDateAsync(DateTime date)
        {
            var result = await this.GetAllAsync();
            return result.Where(x => x.NotificationDate.ToShortDateString() == date.ToShortDateString()).ToList();
        }

        public async Task<Notification> SaveNotificationAsync(Notification notification)
        {
            await this.InsertAsync(notification);
            var result = await this.GetAllAsync();
            return result.FirstOrDefault(x => x.Id == notification.Id);
        }
    }
}
