using System;

namespace Aop.Caching
{
	public class CachedObject
	{
		public string Key { get; set; }
		public object Value { get; set; }
		public DateTime CachedDate { get; set; }
	}
}
