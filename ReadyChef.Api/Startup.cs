using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Microsoft.Owin;
using Owin;
using ReadyChef.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace ReadyChef.Api
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			

			var builder = AutofacConfig.Configure(app);
			IContainer container = builder.Build();

			app.UseAutofacMiddleware(container)
				.RunWebApi(container)
				.RunMvc(container);

			AreaRegistration.RegisterAllAreas();
			MvcFilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			MvcRouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			ConfigureAuth(app);
		}
	}
}