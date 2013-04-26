using System.Web;
using System.Web.Mvc;

namespace _2013.April.NServiceBus.ClientWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}