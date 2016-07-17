using System.Web.Http;
using ReadyChef.Core.Models;
using ReadyChef.Core.Services;

namespace ReadyChef.Api.Controllers
{
    public class RecipeController : ApiController
	{
		private readonly IRecipeService _recipeService;

		public RecipeController(IRecipeService recipeService)
		{
			_recipeService = recipeService;
		}

		[HttpGet]
        public IHttpActionResult GetAll()
        {
            //asdf
            return Ok(true);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id == 1)
                return Ok("returned 1");
            else
                return Ok("not 1");
        }

        [HttpPost]
        public void Add([FromBody] Recipe recipe)
        {

        }
    }
}
