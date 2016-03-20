namespace ValantTest.Application.Services.InventoryService
{
    using System.Threading.Tasks;
    using Domain.Core.TypedRepositories;
    using Domain.Model;
    using DTO;
    using Infrastructure.CrossCutting.Adapters;
    using Domain.Core.TypedGateways;
    using System;

    public class InventoryService : IInventoryService
    {
        private readonly IItemRepository itemRepository;

        private readonly ISignalRGateway gateway;

        private readonly INotificationRepository notificationRepository;

        public InventoryService(INotificationRepository notificationRepository, IItemRepository itemRepository, ISignalRGateway gateway)
        {
            this.itemRepository = itemRepository;
            this.gateway = gateway;
            this.notificationRepository = notificationRepository;
        }

        public async Task<ItemDto> SaveItem(ItemDto request)
        {
            var resultItem = await this.itemRepository.GetItemByLablelAsync(request.Label);
            if (resultItem == null)
            {
                request.Count = 0;
                var itemToInsert = TypeAdapterHelper.Adapt<Item>(request);
                itemToInsert.Count = 1;
                var result = await this.itemRepository.SaveItemAsync(itemToInsert);
                return TypeAdapterHelper.Adapt<ItemDto>(result);
            }
            else
            {
                resultItem.Count += 1;
                await this.itemRepository.UpdateAsync(resultItem);
                return TypeAdapterHelper.Adapt<ItemDto>(resultItem);
            }
        }

        public async Task<ItemDto> TakeItemByLabel(string label)
        {
            var result = await this.itemRepository.GetItemByLablelAsync(label);
            if (result != null)
            {
                result.Count -= 1;
                if (result.Count > 0)
                {
                    await this.itemRepository.UpdateAsync(result);
                }
                else
                {
                    await this.itemRepository.DeleteAsync(result);
                }
                var notification = new Notification { Id = Guid.NewGuid(), Description = string.Format("Item {0} has been taken!<br/>There are {1} left;", label, result.Count), NotificationDate = DateTime.UtcNow, Type = 2 };
                await this.notificationRepository.SaveNotificationAsync(notification);
                await this.gateway.SendMessage(notification);
            }
            return TypeAdapterHelper.Adapt<ItemDto>(result);
        }
    }
}
