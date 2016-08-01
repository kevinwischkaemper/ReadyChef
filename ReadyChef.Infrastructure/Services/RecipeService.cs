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
            return _recipeRepository.GetAll();
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

        public IEnumerable<Recipe> GetByName(string name)
        {
            return _recipeRepository.GetByName(name);
        }

        public IEnumerable<Recipe> GetByIngredient(string ingredient)
        {
            return _recipeRepository.GetByIngredient(ingredient);
        }
    }
}
