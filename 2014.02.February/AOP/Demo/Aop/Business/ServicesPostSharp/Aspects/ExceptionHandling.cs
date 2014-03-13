using System;
using Aop.Advantage;
using PostSharp.Aspects;

namespace Aop.Business.ServicesPostSharp.Aspects
{
	[Serializable]
	public class ExceptionHandling : OnExceptionAspect
	{
		public override void OnException(MethodExecutionArgs args)
		{
			if (Exceptions.Handle(args.Exception))
			{
				args.FlowBehavior = FlowBehavior.Continue;
			}
		}
	}
}