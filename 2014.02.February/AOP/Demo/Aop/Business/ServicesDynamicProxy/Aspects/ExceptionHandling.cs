using System;
using Aop.Advantage;
using Castle.DynamicProxy;

namespace Aop.Business.ServicesDynamicProxy.Aspects
{
	public class ExceptionHandling : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			try
			{
				invocation.Proceed();
			}
			catch (Exception ex)
			{
				if (!Exceptions.Handle(ex))
				{
					throw;
				}
			}
		}
	}

	public class ExceptionHandlingAttribute : Attribute { }
}