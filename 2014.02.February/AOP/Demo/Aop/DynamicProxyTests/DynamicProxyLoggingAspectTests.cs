using System;
using Castle.DynamicProxy;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aop.DynamicProxyTests
{
	#region Logging Service

	public interface ILoggingService
	{
		void Write(string message);
	}

	public class LoggingService : ILoggingService
	{
		public void Write(string message)
		{
			Console.WriteLine("Logging: " + message);
		}
	}

	#endregion Logging Service

	#region Logging Aspect

	public class LoggingAspect : IInterceptor
	{
		readonly ILoggingService _loggingService;

		public LoggingAspect(ILoggingService loggingService)
		{
			_loggingService = loggingService;
		}

		public void Intercept(IInvocation invocation)
		{
			_loggingService.Write("Log start");
			invocation.Proceed();
			var returnValue = (int)invocation.ReturnValue;
			_loggingService.Write(string.Format("Log {0} end", returnValue));
		}
	}

	#endregion Logging Aspect

	#region Unit Test

	[TestClass]
	public class LoggingAspectTests
	{
		[TestMethod]
		public void LoggingAspectTest()
		{
			// Arrange
			var mockLoggingService = new Mock<ILoggingService>();
			var loggingAspect = new LoggingAspect(mockLoggingService.Object);
			var mockInvocation = new Mock<IInvocation>();
			mockInvocation.Setup(x => x.ReturnValue).Returns(3);

			// Act
			loggingAspect.Intercept(mockInvocation.Object);

			// Assert
			mockLoggingService.Verify(x => x.Write("Log start"));
			mockLoggingService.Verify(x => x.Write(string.Format("Log 3 end")));
		}
	}

	#endregion Unit Test
}
