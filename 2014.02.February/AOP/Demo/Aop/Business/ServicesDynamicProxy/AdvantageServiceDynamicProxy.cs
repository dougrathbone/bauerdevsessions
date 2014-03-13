using System.Collections.Generic;
using System.Linq;
using Aop.Advantage;
using Aop.Business.Domain;
using Aop.Business.ServicesDynamicProxy.Aspects;

namespace Aop.Business.ServicesDynamicProxy
{
	public class AdvantageServiceDynamicProxy : IAdvantageServiceDynamicProxy
	{
		private readonly IAdvantageWebCapsule _advantageWebCapsule;

		public AdvantageServiceDynamicProxy(IAdvantageWebCapsule advantageWebCapsule)
		{
			_advantageWebCapsule = advantageWebCapsule;
		}

		[DefensiveProgramming]
		[Logging]
		[TransactionManagement]
		[ExceptionHandling]
		public virtual void AddSubscription(Subscription subscription)
		{
			_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);
		}

		public virtual IList<Subscription> GetSubscriptions(int customerId)
		{
			return
				_advantageWebCapsule.GetSubscriptions(customerId)
					.Select(s => new Subscription {CustomerId = customerId, PubCode = s})
					.ToList();
		}
	}
}
