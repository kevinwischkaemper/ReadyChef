using System.Linq;
using System.Net.Http.Formatting;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;

namespace ReadyChef.Api
{
    public static class WebApiConfig
    {
		public static IAppBuilder RunWebApi(this IAppBuilder app, IContainer container)
		{
			var config = new HttpConfiguration();

			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

			// use Web API routes
			config.MapHttpAttributeRoutes();

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

			// Web API should only use bearer token authentication
			config.SuppressDefaultHostAuthentication();

			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });

			return app.UseAutofacWebApi(config)
				.UseWebApi(config);
		}
    }
}
