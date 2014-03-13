using System.Collections.Generic;
using Aop.Business.Domain;

namespace Aop.Business.ServicesDynamicProxy
{
	public interface IAdvantageServiceDynamicProxy
	{
		void AddSubscription(Subscription subscription);
		IList<Subscription> GetSubscriptions(int customerId);
	}
}
