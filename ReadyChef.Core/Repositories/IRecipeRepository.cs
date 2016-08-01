using System.Collections.Generic;
using ReadyChef.Core.Models;

namespace ReadyChef.Core.Repositories
{
    public interface IRecipeRepository
    {
        void Add(Recipe recipe);

        void Delete(Recipe recipe);

        Recipe Get(string name);
        IEnumerable<Recipe> GetByName(string name);
        IEnumerable<Recipe> GetByIngredient(string ingredient);

        IEnumerable<Recipe> GetAll();

    }
}
