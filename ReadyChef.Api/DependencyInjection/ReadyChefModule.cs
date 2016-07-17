using System.Configuration;
using System.Reflection;
using Autofac;
using ReadyChef.Core.DataAccess;
using ReadyChef.Infrastructure.DataAccess;
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
				.Where(p => p.IsClass && !p.IsAbstract)
				.AsImplementedInterfaces();

			var readyChefConnectionString = ConfigurationManager.ConnectionStrings["ReadyChef"].ConnectionString;

			builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>()
				.WithParameter("readyChefConnectionString", readyChefConnectionString);
		}
	}
}