using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PromptGarten.Common;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Test
{
    public class InMemoryRepository : IRepository, IEnumerable
    {
        private readonly IDictionary<Type, List<object>> _db = new Dictionary<Type, List<object>>();

        public void Add<TAggregate>(TAggregate aggregate)
        {
            _db.FindOrCreate(typeof(TAggregate), () => new List<object>()).Add(aggregate);
        }

        public IQueryable<TAggregate> Query<TAggregate>()
        {
            return _db[typeof(TAggregate)].Cast<TAggregate>().AsQueryable();
        }

        public void Save<TAggregate>(TAggregate target)
        {
            Add(target);
        }

        public void Delete<TAggregate>(TAggregate target)
        {
            if (_db[typeof(TAggregate)].Remove(target) == false)
            {
                throw new InvalidOperationException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _db.GetEnumerator();
        }
    }
}