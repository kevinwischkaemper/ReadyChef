using System.Web.Http;

namespace ReadyChef.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			// needed to make the help page area work
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
    }
}
