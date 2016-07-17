using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Module = Autofac.Module;

namespace ReadyChef.Api.DependencyInjection
{
	public class MvcModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterModule(new AutofacWebTypesModule());
			builder.RegisterSource(new ViewRegistrationSource());
			builder.RegisterFilterProvider();
			builder.RegisterControllers(typeof(ReadyChef.Api.WebApiApplication).Assembly)
				.InstancePerRequest();
		}
	}
}