using System;
using System.Collections.Generic;

namespace PromptGarten.Common
{
    public static class DictionaryExtensions
    {
        public static TValue FindOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> items, TKey key, Func<TValue> value)
        {
            TValue res;
            if (!items.TryGetValue(key, out res))
            {
                res = value();
                items.Add(key, res);
            }
            return res;
        }
    }
}