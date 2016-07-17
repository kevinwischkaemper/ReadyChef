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
				.RunWebApi(container);

			ConfigureAuth(app);
		}
	}
}