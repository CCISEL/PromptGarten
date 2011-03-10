using System;
using System.Linq;
using System.Linq.Expressions;

namespace PromptGarten.Domain.Services
{
    public interface IRepository
    {
        IQueryable<TEntity> Query<TEntity>();

        void Save<TEntity>(TEntity target);
        void Delete<TEntity>(TEntity target);
    }

    public static class RepositoryExtensions
    {
        public static TEntity Find<TEntity>(this IRepository repository, Expression<Func<TEntity, bool>> filter)
        {
            return repository.Query<TEntity>().SingleOrDefault(filter);
        }
    }
}
