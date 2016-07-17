using System.Collections.Generic;
using ReadyChef.Core.Models;

namespace ReadyChef.Core.Services
{
    public interface IRecipeService
    {
        Recipe GetRecipe(string name);
        void AddRecipe (Recipe recipe);
        IEnumerable<Recipe> GetAllRecipes();
        IEnumerable<Recipe> GetAllRecipes(string ingredient);
    }
}
