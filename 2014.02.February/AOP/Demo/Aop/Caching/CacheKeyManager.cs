using System.Text;
using PostSharp.Aspects;

namespace Aop.Caching
{
	public static class CacheKeyBuilder
	{
		public static string BuildCacheKey(Arguments arguments, string methodName)
		{
			var sb = new StringBuilder();
			sb.Append(methodName);
			foreach (var argument in arguments.ToArray())
			{
				sb.Append(argument == null ? "_" : argument.ToString());
			}
			return sb.ToString();
		}
	}
}
