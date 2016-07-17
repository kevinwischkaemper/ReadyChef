using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace ReadyChef.Api.DependencyInjection
{
	public class ReadyChefModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(
				Assembly.Load("ReadyChef.Core"),
				Assembly.Load("ReadyChef.Infrastructure"),
				Assembly.GetExecutingAssembly()
				)
				.AsImplementedInterfaces();
		}
	}
}