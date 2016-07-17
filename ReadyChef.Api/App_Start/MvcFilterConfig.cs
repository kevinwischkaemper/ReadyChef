using System.Web.Mvc;

namespace ReadyChef.Api
{
	public class MvcFilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}