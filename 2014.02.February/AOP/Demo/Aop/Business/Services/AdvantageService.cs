using System;
using System.Transactions;
using Aop.Advantage;
using Aop.Business.Domain;

namespace Aop.Business.Services
{
	public class AdvantageService : IAdvantageService
	{
		private readonly IAdvantageWebCapsule _advantageWebCapsule;

		public AdvantageService(IAdvantageWebCapsule advantageWebCapsule)
		{
			_advantageWebCapsule = advantageWebCapsule;
		}

		#region V1 No Cross Cutting

		public void AddSubscriptionV1(Subscription subscription)
		{
			_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);
		}

		#endregion V1

		#region V2 With Logging

		public void AddSubscriptionV2(Subscription subscription)
		{
			// logging
			Console.WriteLine("AddSubscription: {0}", DateTime.Now);
			Console.WriteLine("Customer Id: {0}", subscription.CustomerId);
			Console.WriteLine("Pub Code: {0}", subscription.PubCode);

			_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);

			// logging
			Console.WriteLine("AddSubscription complete: {0}", DateTime.Now);
		}

		#endregion V2 With Logging

		#region V3 With Logging, Defensive Programming

		public void AddSubscriptionV3(Subscription subscription)
		{
			// defensive programming
			if (subscription == null) throw new ArgumentNullException("subscription");

			// logging
			Console.WriteLine("AddSubscription: {0}", DateTime.Now);
			Console.WriteLine("Customer Id: {0}", subscription.CustomerId);
			Console.WriteLine("Pub Code: {0}", subscription.PubCode);

			_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);

			// logging
			Console.WriteLine("AddSubscription complete: {0}", DateTime.Now);
		}

		#endregion V3 With Logging, Defensive Programming

		#region V4 With Logging, Defensive Programming, Transactions

		public void AddSubscriptionV4(Subscription subscription)
		{
			// defensive programming
			if (subscription == null) throw new ArgumentNullException("subscription");

			// logging
			Console.WriteLine("AddSubscription: {0}", DateTime.Now);
			Console.WriteLine("Customer Id: {0}", subscription.CustomerId);
			Console.WriteLine("Pub Code: {0}", subscription.PubCode);

			// start new transaction
			using (var scope = new TransactionScope())
			{
				try
				{
					_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);

					// complete transaction
					scope.Complete();
				}
				catch
				{
					throw;
				}
			}

			// logging
			Console.WriteLine("AddSubscription complete: {0}", DateTime.Now);
		}

		#endregion V4 With Logging, Defensive Programming, Transactions

		#region V5 With Logging, Defensive Programming, Transactions, Retries And Exception Handling

		public void AddSubscriptionV5(Subscription subscription)
		{
			// defensive programming
			if (subscription == null) throw new ArgumentNullException("subscription");

			// logging
			Console.WriteLine("AddSubscription: {0}", DateTime.Now);
			Console.WriteLine("Customer Id: {0}", subscription.CustomerId);
			Console.WriteLine("Pub Code: {0}", subscription.PubCode);

			// exception handling
			try
			{
				// start new transaction
				using (var scope = new TransactionScope())
				{
					// retry up to three times
					var retries = 3;
					var succeeded = false;
					while (!succeeded)
					{
						try
						{
							_advantageWebCapsule.AddSubscription(subscription.CustomerId, subscription.PubCode);

							// complete transaction
							scope.Complete();
							succeeded = true;
						}
						catch
						{
							// don't re-throw until the
							// retry limit is reached
							if (retries >= 0)
								retries--;
							else
								throw;
						}
					}
				}
			}
			catch (Exception ex)
			{
				// exception handling
				if (!Exceptions.Handle(ex))
				{
					throw;
				}
			}

			// logging
			Console.WriteLine("AddSubscription complete: {0}", DateTime.Now);
		}

		#endregion V5 With Logging, Defensive Programming, Transactions, Retries And Exception Handling
	}
}
