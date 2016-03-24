namespace ValantTest.Domain.Core.RepositoryInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Model;

    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all the entities of this repository.
        /// </summary>
        /// <returns>A list with all entities in repository.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Add entity to the repository.
        /// </summary>
        /// <param name="entity">the entity to add.</param>
        /// <returns>The added entity.</returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Deleted entity in repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Updates the passed entity
        /// </summary>
        /// <param name="entityToUpdate">Entity to update.</param>
        /// <returns>Task</returns>
        Task UpdateAsync(TEntity entityToUpdate);
    }
}
