using System.Collections.Generic;
using Aop.Business.Domain;

namespace Aop.Business.ServicesPostSharp
{
	public interface IAdvantageServicePostSharp
	{
		void AddSubscription(Subscription subscription);
		IList<Subscription> GetSubscriptions(int customerId);
	}
}
