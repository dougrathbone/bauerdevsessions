using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostSharp.Aspects;

namespace Aop.PostSharpTestsNoIoC
{
	public static class LoggingService
	{
		public static List<string> _messages = new List<string>();

		public static List<string> Messages
		{
			get { return _messages; }
		}

		public static void Write(string message)
		{
			_messages.Add(message);
		}
	}

	[Serializable]
	public class LoggingAspect : OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionArgs args)
		{
			LoggingService.Write("Before: " + args.Method.Name);
		}
		public override void OnSuccess(MethodExecutionArgs args)
		{
			LoggingService.Write("After: " + args.Method.Name);
		}
	}

	[TestClass]
	public class LoggingAspectTests
	{
		[TestMethod]
		public void LoggingAspectTest()
		{
			// Arrange
			var args = new MethodExecutionArgs(null, Arguments.Empty)
				           {
					           Method = new DynamicMethod("AddSubscription", null, null)
				           };

			var aspect = new LoggingAspect();

			// Act
			aspect.OnEntry(args);
			aspect.OnSuccess(args);

			// Assert
			Assert.IsTrue(LoggingService.Messages.Contains("Before: " + args.Method.Name));
			Assert.IsTrue(LoggingService.Messages.Contains("After: " + args.Method.Name));
		}
	}
}
