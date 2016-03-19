namespace ValantTest.Client.Sdk.Services
{
    using System.Threading.Tasks;
    using Application.DTO;
    using Helper;

    public class InventoryClient : ApiClient, IInventoryClient 
    {
        public InventoryClient()
        {
        }

        public async Task<ItemDto> AddItem(ItemDto request)
        {
            return await this.Post<ItemDto>("items", request);
        }

        public async Task<ItemDto> GetByItemLablel(string label)
        {
           return await this.Get<ItemDto>("items/" + label);
        }
    }
}
