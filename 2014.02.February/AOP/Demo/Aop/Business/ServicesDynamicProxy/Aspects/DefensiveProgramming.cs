using System;
using System.Linq;
using Castle.DynamicProxy;

namespace Aop.Business.ServicesDynamicProxy.Aspects
{
	public class DefensiveProgramming : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(DefensiveProgrammingAttribute), false).Any())
			{
				invocation.Proceed();
				return;
			}

			foreach (var argument in invocation.Arguments)
			{
				if (argument == null)
					throw new ArgumentNullException();
				if (argument is int && (int)argument <= 0)
					throw new ArgumentException();
			}

			invocation.Proceed();
		}
	}

	public class DefensiveProgrammingAttribute : Attribute { }
}