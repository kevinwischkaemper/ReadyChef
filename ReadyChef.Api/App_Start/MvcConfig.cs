using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Owin;

namespace ReadyChef.Api
{
	public static class MvcConfig
	{
		public static IAppBuilder RunMvc(this IAppBuilder app, IContainer container)
		{
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			return app.UseAutofacMvc();
		}
	}
}