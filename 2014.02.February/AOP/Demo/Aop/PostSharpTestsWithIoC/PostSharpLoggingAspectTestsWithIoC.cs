using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostSharp.Aspects;

namespace Aop.PostSharpTestsWithIoC
{
	public class StringHelpers
	{
		[LoggingAspect]
		public string Reverse(string str)
		{
			return new string(str.Reverse().ToArray());
		}
	}

	[Serializable]
	public class LoggingAspect : OnMethodBoundaryAspect
	{
		ILoggingService _loggingService;

		public static Func<ILifetimeScope> GetScope { get; set; }

		public override void RuntimeInitialize(MethodBase method)
		{
			if (!AspectSettings.On) return;
			_loggingService = GetScope().Resolve<ILoggingService>();
		}

		public override void OnEntry(MethodExecutionArgs args)
		{
			if (!AspectSettings.On) return;
			_loggingService.Log("before");
		}

		public override void OnSuccess(MethodExecutionArgs args)
		{
			if (!AspectSettings.On) return;
			_loggingService.Log("after");
		}
	}

	public interface ILoggingService
	{
		void Log(string logMessage);
	}

	public static class AspectSettings
	{
		public static bool On = true;
	}

	[TestClass]
	public class MyNormalCodeTest
	{
		public void SetupIoC(ILoggingService loggingService)
		{
			var builder = new ContainerBuilder();
			builder.RegisterInstance(loggingService).As<ILoggingService>();
			var container = builder.Build();
			LoggingAspect.GetScope = container.Resolve<ILifetimeScope>;
		}

		[TestMethod]
		public void ReverseTest()
		{
			// Arrange
			var loggingService = new Mock<ILoggingService>();
			SetupIoC(loggingService.Object);
			var stringHelpers = new StringHelpers();

			// Act
			var result = stringHelpers.Reverse("hello");

			// Assert
			Assert.AreEqual("olleh", result);
			loggingService.Verify(x => x.Log("before"));
			loggingService.Verify(x => x.Log("after"));
		}

		[TestMethod]
		public void ReverseTestAlternative()
		{
			// Arrange
			AspectSettings.On = false;
			var myCode = new StringHelpers();

			// Act
			var result = myCode.Reverse("hello");

			// Assert
			Assert.AreEqual("olleh", result);
		}
	}
}
