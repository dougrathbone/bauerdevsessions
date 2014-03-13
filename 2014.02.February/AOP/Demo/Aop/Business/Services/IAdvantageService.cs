using Aop.Business.Domain;

namespace Aop.Business.Services
{
	public interface IAdvantageService
	{
		void AddSubscriptionV1(Subscription subscription);
		void AddSubscriptionV2(Subscription subscription);
		void AddSubscriptionV3(Subscription subscription);
		void AddSubscriptionV4(Subscription subscription);
		void AddSubscriptionV5(Subscription subscription);
	}
}
