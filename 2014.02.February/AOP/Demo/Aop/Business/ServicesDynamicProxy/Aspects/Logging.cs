using System;
using System.Linq;
using Aop.Business.Domain;
using Castle.DynamicProxy;

namespace Aop.Business.ServicesDynamicProxy.Aspects
{
	public class Logging : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			if (!invocation.MethodInvocationTarget.GetCustomAttributes(typeof(LoggingAttribute), false).Any())
			{
				invocation.Proceed();
				return;
			}

			Console.WriteLine("{0}: {1}", invocation.Method.Name, DateTime.Now);

			foreach (var argument in invocation.Arguments)
			{
				var loggable = argument as ILoggable;
				if (loggable != null)
				{
					Console.WriteLine(loggable.LogInformation());
				}
			}

			invocation.Proceed();

			Console.WriteLine("{0} complete: {1}", invocation.Method.Name, DateTime.Now);
		}
	}

	public class LoggingAttribute : Attribute { }
}
