using System.Collections.Generic;
using System.Linq;
using Aop.Advantage;
using Aop.Business.Domain;
using Aop.Business.ServicesPostSharp.Aspects;
using Aop.Caching;

namespace Aop.Business.ServicesPostSharp
{
	public class AdvantageServicePostSharp : IAdvantageServicePostSharp
	{
		private readonly IAdvantageWebCapsule _advantageWebCapsule;

		public AdvantageServicePostSharp(IAdvantageWebCapsule advantageWebCapsule)
		{
			_advantageWebCapsule = advantageWebCapsule;
		}

		[Logging(AspectPriority = 1)]
		[DefensiveProgramming(AspectPriority = 2)]
		[ExceptionHandling(AspectPriority = 3)]
		[TransactionManagement(AspectPriority = 4)]
		public void AddSubscription(Subscription subscription)
		{
			_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);
		}

		[Cache(CacheStore = CacheStore.Memory, Minutes = 60)]
		public IList<Subscription> GetSubscriptions(int customerId)
		{
			return
				_advantageWebCapsule.GetSubscriptions(customerId)
					.Select(s => new Subscription {CustomerId = customerId, PubCode = s})
					.ToList();
		}
	}
}
