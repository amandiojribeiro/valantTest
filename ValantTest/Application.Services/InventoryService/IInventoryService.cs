namespace ValantTest.Application.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface IInventoryService
    {
        Task<ItemDto> SaveItem(ItemDto request);

        Task<ItemDto> TakeItemByLabel(string label);
    }
}
