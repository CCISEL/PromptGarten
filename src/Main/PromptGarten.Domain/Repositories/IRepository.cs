// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="CCISEL">
//   Luís Falcão - 2011
// </copyright>
// <summary>
//   Base interface for an entity Repository. This interface includes the basic CRUD operations
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace PromptGarten.Domain.Repositories
{
    /// <summary>
    /// Base interface for an entity Repository. This interface includes the basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The TEntity type</typeparam>
    /// <typeparam name="TKey">The type of the TEntity key.</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable"/> for all Entities.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TEntity}"/> for all Entities</returns>
        IQueryable<TEntity> GetAll();


        /// <summary>
        /// Gets an entity given the specified <paramref name="entityId"/>.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The <typeparamref name="TEntity"/> if it exists, <code>null</code> otherwise </returns>
        TEntity Get(TKey entityId);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        void UpdateAll(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityId">The key of the entity to delete.</param>
        void Delete(TKey entityId);

        /// <summary>
        /// Saves permanently all changes made to the repository.
        /// </summary>
        void Save();


        /// <summary>
        /// Refreshes the specified entities with the physical repository values overwriting the instance values.
        /// </summary>
        /// <param name="entities">The entities to refresh.</param>
        void Refresh(params TEntity[] entities);
    }
}