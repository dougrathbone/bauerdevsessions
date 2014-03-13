using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Aop.Caching.CacheStores
{
	public class MemcachedCache : ICache
	{
		#region Private Variables

		private readonly IMemcachedClient _cache;

		#endregion

		#region Constructor

		public MemcachedCache(IMemcachedClient cache)
		{
			if (cache == null) throw new ArgumentNullException("cache");

			_cache = cache;
		}

		#endregion

		#region Interface Members

		public void Set(string key, object value, DateTime expiresAt)
		{
			_cache.Store(StoreMode.Set, key, value, expiresAt);
		}

		public void Set(string key, object value, TimeSpan validFor)
		{
			_cache.Store(StoreMode.Set, key, value, validFor);
		}

		public object Get(string key, Type type)
		{
			return _cache.Get(key);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}

		public void Clear()
		{
			_cache.FlushAll();
		}

		#endregion
	}
}
