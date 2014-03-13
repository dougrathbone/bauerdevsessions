using System;
using System.Linq;
using System.Transactions;
using Castle.DynamicProxy;

namespace Aop.Business.ServicesDynamicProxy.Aspects
{
	public class TransactionManagement :IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionManagementAttribute), false).Any())
			{
				invocation.Proceed();
				return;
			}

			Console.WriteLine("Starting transaction");
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
						invocation.Proceed();

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
			Console.WriteLine("Transaction complete");
		}
	}

	public class TransactionManagementAttribute : Attribute { }
}