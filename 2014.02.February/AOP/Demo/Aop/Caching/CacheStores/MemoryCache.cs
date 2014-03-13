using System;
using System.Runtime.Caching;

namespace Aop.Caching.CacheStores
{
	public class MemoryCache : ICache
	{
		#region Private Variables

		private readonly System.Runtime.Caching.MemoryCache _cache;

		#endregion

		#region Constructor

		public MemoryCache()
		{
			_cache = System.Runtime.Caching.MemoryCache.Default;
		}

		#endregion

		#region Interface Members

		public object Get(string key, Type type)
		{
			return _cache[key];
		}

		public void Set(string key, object data, TimeSpan lifespan)
		{
			if (data == null)
			{
				return;
			}

			var expiresAt = DateTime.UtcNow.Add(lifespan);
			Set(key, data, expiresAt);
		}

		public void Set(string key, object data, DateTime expiresAt)
		{
			if (data == null)
			{
				return;
			}

			var policy = new CacheItemPolicy { AbsoluteExpiration = expiresAt };
			Set(key, data, policy);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		public void Clear()
		{
			foreach (var item in _cache)
			{
				Remove(item.Key);
			}
		}

		#endregion

		#region Private Methods

		private void Set(string key, object value, CacheItemPolicy policy)
		{
			_cache.Set(key, value, policy);
		}

		#endregion
	}
}
