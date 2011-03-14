using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PromptGarten.Common;
using PromptGarten.Domain.Exceptions;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Test
{
    public class InMemoryRepository : IRepository, IEnumerable
    {
        private readonly IDictionary<Type, List<object>> _db = new Dictionary<Type, List<object>>();
        private readonly IDictionary<Type, Func<object, IComparable>> _keyMappers = new Dictionary<Type, Func<object, IComparable>>();

        public InMemoryRepository WithKey<TAggregate>(Func<TAggregate, IComparable> keyMapper)
        {
            _keyMappers[typeof(TAggregate)] = x => keyMapper((TAggregate)x);
            return this;
        }

        public void Add<TAggregate>(TAggregate aggregate)
        {
            Table<TAggregate>().Add(aggregate);
        }

        public IQueryable<TAggregate> Query<TAggregate>()
        {
            return Table<TAggregate>().Cast<TAggregate>().AsQueryable();
        }

        public void Insert<TAggregate>(TAggregate target)
        {
            Add(target);
        }

        public void Save<TAggregate>(TAggregate target)
        {
            var km = _keyMappers[typeof(TAggregate)];
            if (Table<TAggregate>().Any(x => km(x).CompareTo(km(target)) == 0))
            {
                throw new DuplicateAggregateException();
            }

            Add(target);
        }

        public void Delete<TAggregate>(TAggregate target)
        {
            if (Table<TAggregate>().Remove(target) == false)
            {
                throw new InvalidOperationException();
            }
        }

        public List<object> Table<TAggregate>()
        {
            return _db.FindOrCreate(typeof(TAggregate), () => new List<object>());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _db.GetEnumerator();
        }
    }
}