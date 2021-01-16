using System.Collections.Generic;

namespace Estimator
{
    public static class Utilities
    {
        public static bool AddToDictionary<TKey, TValue>(TKey key, TValue value, Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, value);
            return true;
        }

        public static bool RemoveFromDictionary<TKey, TValue>(TKey key, Dictionary<TKey, TValue> dictionary)
        {
            if (!dictionary.ContainsKey(key)) return false;
            dictionary.Remove(key);
            return true;
        }
    }
}