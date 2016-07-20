using ReadyChef.Core.Services;
using System;
using System.Collections.Generic;
using ReadyChef.Core.Models;
using ReadyChef.Core.Repositories;

namespace ReadyChef.Infrastructure.Services
{
    public class RecipeService : IRecipeService
    {
	    private readonly IRecipeRepository _recipeRepository;

	    public RecipeService(IRecipeRepository recipeRepository)
	    {
		    _recipeRepository = recipeRepository;
	    }


	    public IEnumerable<Recipe> GetAllRecipes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetAllRecipes(string ingredient)
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipe(string name)
        {
            return _recipeRepository.Get(name);
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipeRepository.Add(recipe);
        }
    }
}
