using System;
using Aop.Caching.CacheStores;
using Autofac;
using Enyim.Caching;

namespace Aop.Caching
{
	public class CacheContainer
	{
		public static Func<ILifetimeScope> GetScope { get; set; }

		public ICache GetCache(CacheStore type)
		{
			ICache cache;
			switch (type)
			{
				case CacheStore.Memcached:
				{
					cache = GetScope().Resolve<MemcachedCache>(new NamedParameter("cache", GetScope().Resolve<IMemcachedClient>()));
					break;
				}
				case CacheStore.Null:
				{
					cache = GetScope().Resolve<NullCache>();
					break;
				}
				default:
				{
					cache = GetScope().Resolve<MemoryCache>();
					break;
				}
			}

			return cache;
		}
	}
}
