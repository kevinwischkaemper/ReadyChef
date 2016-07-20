using ReadyChef.Core.Models;
using ReadyChef.Core.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace ReadyChef.Api.Controllers
{
	/// <summary>
	/// Interact with ingredients
	/// </summary>
	public class IngredientController : ApiController
	{
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

		/// <summary>
		/// Gets all ingredients
		/// </summary>
		/// <returns></returns>
		public IEnumerable<string> GetAll()
		{
			return new List<string>();
		}

        /// <summary>
        /// Adds an ingredient and returns its ID
        /// </summary>
        /// <param name="ingredient"></param>
        /// <returns></returns>
        
        [HttpPost]
        public IHttpActionResult Add([FromBody]string name)
        {
            var ingredientId = _ingredientService.AddIngredient(name);
            return Ok(ingredientId);
        }
	}
}