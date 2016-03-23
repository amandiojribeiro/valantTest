using System.Threading.Tasks;

namespace ValantTest.Data.Repository.ImplementedRepositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Core.RepositoryInterfaces;
    using System.Linq;
    using Domain.Model;

    public abstract class GenericListRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private static ConcurrentDictionary<TId, TEntity> entitiesList;

        public GenericListRepository()
        {
            if (entitiesList == null)
            {
                entitiesList = new ConcurrentDictionary<TId, TEntity>();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                entitiesList.TryRemove(entity.Key, out entity);
            });
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<TEntity>>(entitiesList.Select(x => x.Value).ToList());
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                entitiesList[entity.Key] = entity;

            });
        }

        public Task UpdateAsync(TEntity entity)
        {
            return this.InsertAsync(entity);
        }
    }
}
