using System.Collections.Generic;

namespace ReadyChef.Core.Repositories
{
    public interface IRecipeIngredientRepository
    {
        void Add(int recipeId, int ingredientId, int amount);

        void Delete(int recipeId, int ingredientId);

        IEnumerable<int> GetIngredientIds(int recipeId);

        IEnumerable<int> GetRecipeIds(int ingredientId);
    }
}
