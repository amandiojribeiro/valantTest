namespace ValantTest.Domain.Core.TypedRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Model;
    using RepositoryInterfaces;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public interface IItemRepository : IRepository<Item, string>
    {
        Task<Item> GetItemByLablelAsync(string label);

        Task<Item> GetItemByIdAsync(Guid id);

        Task<List<Item>> GetExpiredItemsByDate(DateTime date);

        Task<Item> SaveItemAsync(Item item);

        Task RemoveExpiredItems(DateTime date);
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public interface INotificationRepository : IRepository<Notification, Guid>
    {
        Task<List<Notification>> GetNotificationsByDateAsync(DateTime date);

        Task<Notification> SaveNotificationAsync(Notification notification);
    }
}
