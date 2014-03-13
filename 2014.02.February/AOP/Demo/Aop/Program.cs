using Aop.Advantage;
using Aop.Business.Domain;
using Aop.Business.Services;
using Aop.Business.ServicesDynamicProxy;
using Aop.Business.ServicesDynamicProxy.Aspects;
using Aop.Business.ServicesPostSharp;
using Aop.Caching.CacheStores;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Enyim.Caching;
using Aop.Caching;

namespace Aop
{
	class Program
	{
		private static IContainer Container { get; set; }

		static void Main()
		{
			SetupIoC();

			var subscription = new Subscription
			{
				CustomerId = 1,
				PubCode = "ABC"
			};

			using (var scope = Container.BeginLifetimeScope())
			{
				//#region Add Subscription No AOP

				//var advantageService = scope.Resolve<IAdvantageService>();

				//advantageService.AddSubscriptionV5(subscription);

				//#endregion Add Subscription No AOP

				#region Add Subscription PostSharp AOP

				var advantageServicePostSharp = scope.Resolve<IAdvantageServicePostSharp>();

				//advantageServicePostSharp.AddSubscription(subscription);

				#endregion Add Subscription PostSharp AOP

				//#region Add Subscription DynamicProxy AOP

				//var advantageServiceDynamicProxy = scope.Resolve<IAdvantageServiceDynamicProxy>();

				//advantageServiceDynamicProxy.AddSubscription(subscription);

				//#endregion

				#region Get Subscriptions PostSharp AOP

				CacheContainer.GetScope = () => scope;

				advantageServicePostSharp.GetSubscriptions(1);

				advantageServicePostSharp.GetSubscriptions(1);

				#endregion Get Subscriptions PostSharp AOP

				//#region Get Subscriptions DynamicProxy AOP

				//advantageServiceDynamicProxy.GetSubscriptions(1);

				//#endregion
			}
		}

		private static void SetupIoC()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<AdvantageService>().As<IAdvantageService>();
			builder.RegisterType<FakeAdvantageWebCapsule>().As<IAdvantageWebCapsule>();

			#region PostSharp

			builder.RegisterType<AdvantageServicePostSharp>().As<IAdvantageServicePostSharp>();

			#endregion PostSharp

			#region Dynamic Proxy

			builder.Register(c => new DefensiveProgramming());
			builder.Register(c => new Logging());
			builder.Register(c => new TransactionManagement());
			builder.Register(c => new ExceptionHandling());

			builder.RegisterType<AdvantageServiceDynamicProxy>()
				.As<IAdvantageServiceDynamicProxy>()
				.EnableInterfaceInterceptors()
				.InterceptedBy(typeof (Logging))
				.InterceptedBy(typeof (DefensiveProgramming))
				.InterceptedBy(typeof (ExceptionHandling))
				.InterceptedBy(typeof (TransactionManagement));

			#endregion Dynamic Proxy

			#region Caching

			builder.RegisterType<MemcachedClient>().As<IMemcachedClient>().SingleInstance();
			builder.RegisterType<MemcachedCache>().SingleInstance();
			builder.RegisterType<MemoryCache>().SingleInstance();
			builder.RegisterType<NullCache>().SingleInstance();

			#endregion Caching

			Container = builder.Build();
		}
	}
}
