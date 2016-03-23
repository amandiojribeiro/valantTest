namespace ValantTest.Data.Repository.ImplementedRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Core.TypedRepositories;
    using Domain.Model;

    public class ItemRepository : GenericListRepository<Item, string>, IItemRepository
    {
        public async Task<List<Item>> GetExpiredItemsByDate(DateTime date)
        {
            var result = await this.GetAllAsync();
            return result.Where(x => x.ExpirationDate <= date).ToList();
        }

        public async Task<Item> GetItemByLablelAsync(string label)
        {
            var result = await this.GetAllAsync();
            var item = result.FirstOrDefault(x => x.Label == label);
            return item;
        }

        public async Task RemoveExpiredItems(DateTime date)
        {
            var result = await this.GetAllAsync();
            foreach (Item item in result.Where(x => x.ExpirationDate <= date))
            {
                await this.DeleteAsync(item);
            }
        }

        public async Task<Item> SaveItemAsync(Item item)
        {
            item.Id = Guid.NewGuid();
            await this.InsertAsync(item);
            return item;
        }

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            var result = await this.GetAllAsync();
            return result.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
