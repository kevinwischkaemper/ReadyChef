using System.Web.Http;
using ReadyChef.Core.Models;
using ReadyChef.Core.Services;
using System.Collections.Generic;
using ReadyChef.Core.Exceptions;
using System.Linq;

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
        public IEnumerable<Recipe> GetAll()
        {
            return _recipeService.GetAllRecipes();
        }
        /// <summary>
        /// Get a single recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/recipe/getbyname/{name}")]
        [HttpGet]
        public IEnumerable<Recipe> GetByName(string name)
        {
            try
            {
                return _recipeService.GetByName(name).Take(50);

            }
            catch (RecipeNotFoundException)
            {
                return null;
            }
        }

        [Route("api/recipe/getbyingredient/{ingredient}")]
        [HttpGet]
        public IEnumerable<Recipe> GetByIngredient(string ingredient)
        {
            try
            {
                return _recipeService.GetByIngredient(ingredient).Take(50);
            }
            catch (RecipeNotFoundException)
            {
                return null;
            }
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
