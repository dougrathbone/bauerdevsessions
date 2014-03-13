using System;
using System.Collections.Generic;

namespace Aop.Advantage
{
	public class FakeAdvantageWebCapsule : IAdvantageWebCapsule
	{
		public void AddSubscription(int customerId, string pubCode)
		{
			throw new Exception();
			Console.WriteLine("Adding '{0}' subscription for customer with id: '{1}'", pubCode, customerId);
		}

		public IList<string> GetSubscriptions(int customerId)
		{
			return new List<string>
				       {
					       "ABC",
					       "DEF",
					       "GHI"
				       };
		}
	}
}
