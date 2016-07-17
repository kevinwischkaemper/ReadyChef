using ReadyChef.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyChef.Core;
using ReadyChef.Infrastructure.Repositories;
using ReadyChef.Core.Repositories;

namespace ReadyChef.Infrastructure.Services
{
    public class RecipeService : IRecipeService
    {
        IRecipeRepository _RecipeRepository;

        public RecipeService(IRecipeRepository RecipeRepository)
        {
            _RecipeRepository = RecipeRepository;
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
            throw new NotImplementedException();
        }

        public void AddRecipe(Recipe recipe)
        {
            _RecipeRepository.Add(recipe);
        }
    }
}
