namespace Aop.Business.Domain
{
	public class Subscription : ILoggable
	{
		public int CustomerId { get; set; }
		public string PubCode { get; set; }

		public string LogInformation()
		{
			return "Customer Id: " + CustomerId
			       + "\n" +
			       "Pub Code: " + PubCode;
		}
	}
}
