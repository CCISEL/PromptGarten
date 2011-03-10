using System;
using System.Linq;
using System.Linq.Expressions;

namespace PromptGarten.Domain.Services
{
    public interface IRepository
    {
        IQueryable<TAggregate> Query<TAggregate>();

        void Save<TAggregate>(TAggregate target);
        void Delete<TAggregate>(TAggregate target);
    }

    public static class RepositoryExtensions
    {
        public static TAggregate Find<TAggregate>(this IRepository repository, Expression<Func<TAggregate, bool>> filter)
        {
            return repository.Query<TAggregate>().SingleOrDefault(filter);
        }
    }
}
