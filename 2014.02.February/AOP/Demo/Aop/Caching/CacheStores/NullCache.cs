using System;

namespace Aop.Caching.CacheStores
{
	/// <summary>
	/// <see cref="ICache"/> implementation which does nothing
	/// </summary>
	/// <remarks>
	/// Used when real caches are unavailable or disabled
	/// </remarks>
	public class NullCache : ICache
	{
		public void InitialiseInternal()
		{
		}

		public void Set(string key, object value, DateTime expiresAt)
		{
		}

		public void Set(string key, object value, TimeSpan validFor)
		{
		}

		public object Get(string key, Type type)
		{
			return null;
		}

		public void Remove(string key)
		{
		}

		public bool IsSet(string key)
		{
			return false;
		}

		public void Clear()
		{
		}
	}
}
