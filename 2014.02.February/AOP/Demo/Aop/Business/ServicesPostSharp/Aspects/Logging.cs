using System;
using Aop.Business.Domain;
using PostSharp.Aspects;

namespace Aop.Business.ServicesPostSharp.Aspects
{
	[Serializable]
	public class Logging : OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionArgs args)
		{
			Console.WriteLine("{0}: {1}", args.Method.Name, DateTime.Now);

			foreach (var argument in args.Arguments)
			{
				var loggable = argument as ILoggable;
				if (loggable != null)
				{
					Console.WriteLine(loggable.LogInformation());
				}
			}
		}

		public override void OnSuccess(MethodExecutionArgs args)
		{
			Console.WriteLine("{0} complete: {1}", args.Method.Name, DateTime.Now);
		}
	}
}