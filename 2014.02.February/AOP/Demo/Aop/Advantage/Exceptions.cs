using System;

namespace Aop.Advantage
{
	public static class Exceptions
	{
		public static bool Handle(Exception ex)
		{
			// exception handling
			if (ex.GetType() == typeof(ArithmeticException))
				return false;
			if (ex.GetType() == typeof(TimeoutException))
				return true;

			// etc...
			return false;
		}
	}
}