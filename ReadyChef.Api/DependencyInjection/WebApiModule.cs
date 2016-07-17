using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace ReadyChef.Api.DependencyInjection
{
	public class WebApiModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
				.InstancePerRequest();
		}
	}
}