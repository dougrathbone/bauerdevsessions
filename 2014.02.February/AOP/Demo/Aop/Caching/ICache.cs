using System;

namespace Aop.Caching
{
	public interface ICache
	{
		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="type">Type of returned value.</param>
		/// <returns>The value associated with the specified key.</returns>
		object Get(string key, Type type);

		/// <summary>
		/// Adds the specified key and object to the cache.
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="data">Data</param>
		/// <param name="lifespan">Cache time</param>
		void Set(string key, object data, TimeSpan lifespan);

		/// <summary>
		/// Adds the specified key and object to the cache.
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="data">Data</param>
		/// <param name="expiresAt">Cache expiry date and time</param>
		void Set(string key, object data, DateTime expiresAt);

		/// <summary>
		/// Removes the value with the specified key from the cache
		/// </summary>
		/// <param name="key">/key</param>
		void Remove(string key);

		/// <summary>
		/// Clear all cache data
		/// </summary>
		void Clear();
	}
}
