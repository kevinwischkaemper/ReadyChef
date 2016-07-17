using System.Collections.Generic;
using System.Web.Http;

namespace ReadyChef.Api.Controllers
{
	/// <summary>
	/// Interact with ingredients
	/// </summary>
	public class IngredientController : ApiController
	{
		/// <summary>
		/// Gets all ingredients
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetAll()
		{
			return new List<string>();
		}
	}
}