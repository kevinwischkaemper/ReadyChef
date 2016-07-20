using System.Web.Http;
using ReadyChef.Core.Models;
using ReadyChef.Core.Services;

namespace ReadyChef.Api.Controllers
{
    /// <summary>
    /// Interact with recipes
    /// </summary>
    public class RecipeController : ApiController
	{
		private readonly IRecipeService _recipeService;

		public RecipeController(IRecipeService recipeService)
		{
			_recipeService = recipeService;
		}

		/// <summary>
		/// Get all recipes
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public IHttpActionResult GetAll()
        {
            //asdf
            return Ok(true);
        }
        /// <summary>
        /// Get a single recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Recipe Get(string id)
        {
            return _recipeService.GetRecipe(id);
        }

        /// <summary>
        /// Add a new recipe
        /// </summary>
        /// <param name="recipe"></param>
        [HttpPost]
        public void Add([FromBody] Recipe recipe)
        {
            _recipeService.AddRecipe(recipe);
        }		
    }
}
