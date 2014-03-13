using System.Collections.Generic;

namespace Aop.Advantage
{
	public interface IAdvantageWebCapsule
	{
		void AddSubscription(int customerId, string pubCode);
		IList<string> GetSubscriptions(int customerId);
	}
}
