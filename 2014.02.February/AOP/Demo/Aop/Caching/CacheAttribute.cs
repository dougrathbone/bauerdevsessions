using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aop.PostSharpTestsWithIoC;
using Autofac;
using PostSharp.Aspects;

namespace Aop.Caching
{
	[Serializable]
	public class CacheAttribute : MethodInterceptionAspect
	{
		[NonSerialized]
		private ICache _cache;

		#region Properties

		/// <summary>
		/// Type of cache store to use
		/// </summary>
		public CacheStore CacheStore { get; set; }

		/// <summary>
		/// Days the element to be cached should live in the cache
		/// </summary>
		public int Days { get; set; }

		/// <summary>
		/// Hours the element to be cached should live in the cache
		/// </summary>
		public int Hours { get; set; }

		/// <summary>
		/// Minutes the element to be cached should live in the cache
		/// </summary>
		public int Minutes { get; set; }

		/// <summary>
		/// Seconds the items should live in the cache
		/// </summary>
		public int Seconds { get; set; }

		/// <summary>
		/// Lifespan of the response in the cache
		/// </summary>
		public TimeSpan LifeSpan
		{
			get
			{
				// Default 1 hour
				if (Days == 0 && Hours == 0 && Minutes == 0 && Seconds == 0)
				{
					return new TimeSpan(1, 0, 0);
				}
				return new TimeSpan(Days, Hours, Minutes, Seconds);
			}
		}

		public static Func<ILifetimeScope> GetInstanceStore { get; set; }

		#endregion

		public override void OnInvoke(MethodInterceptionArgs args)
		{
			if (!AspectSettings.On)
			{
				args.ReturnValue = args.Invoke(args.Arguments);
				return;
			}

			var key = CacheKeyBuilder.BuildCacheKey(args.Arguments, _methodName);

			var container = new CacheContainer();
			_cache = container.GetCache(CacheStore);

			var value = _cache.Get(key, ((MethodInfo) args.Method).ReturnType);
			if (value == null)
			{
				lock (_syncRoot)
				{
					value = _cache.Get(key, ((MethodInfo) args.Method).ReturnType);
					if (value == null)
					{
						value = args.Invoke(args.Arguments);
						_cache.Set(key, value, LifeSpan);
					}
				}
			}

			args.ReturnValue = value;
		}

		#region Initialization Logic

		[NonSerialized]
		private object _syncRoot;
		private string _methodName;

		public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
		{
			_methodName = method.Name;
		}

		public override void RuntimeInitialize(MethodBase method)
		{
			_syncRoot = new object();
		}

		#endregion

		#region Type Validation Logic

		public override bool CompileTimeValidate(MethodBase method)
		{
			var methodInfo = method as MethodInfo;
			if (methodInfo != null)
			{
				var returnType = methodInfo.ReturnType;
				if (IsDisallowedCacheReturnType(returnType))
				{
					return false;
				}
			}
			return true;
		}

		private static readonly IList<Type> DisallowedTypes = new List<Type>
		{
			typeof (Stream),
			typeof (IEnumerable),
			typeof (IQueryable)
		};

		private static bool IsDisallowedCacheReturnType(Type returnType)
		{
			return DisallowedTypes.Any(t => t == returnType);
		}

		#endregion
	}
}
