using System.Reflection;
using Autofac;
using Owin;

namespace ReadyChef.Api
{
	public static class AutofacConfig
	{
		public static ContainerBuilder Configure(IAppBuilder app)
		{
			var builder = new ContainerBuilder();

			builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

			return builder;
		}
	}
}