namespace ValantTest.Data.Repository.ImplementedRepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Core.RepositoryInterfaces;
    using System.Collections.Concurrent;

    public abstract class GenericListRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static ConcurrentQueue<TEntity> entitiesList;

        public GenericListRepository()
        {
            if (entitiesList == null)
            {
                entitiesList = new ConcurrentQueue<TEntity>();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                entitiesList.TryDequeue(out entity);
            });
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<TEntity>>(entitiesList);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                lock (entitiesList)
                {
                    entitiesList.Enqueue(entity);
                }
            });
        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            await this.DeleteAsync(entityToUpdate);
            await this.InsertAsync(entityToUpdate);
        }
    }
}
