namespace ValantTest.Client.Sdk.Services
{
    using System.Threading.Tasks;
    using Application.DTO;

    public interface IInventoryClient
    {
        Task<ItemDto> GetByItemLablel(string label);

        Task<ItemDto> AddItem(ItemDto request);
    }
}
