using ReadyChef.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Repositories
{
    public interface IRecipeIngredientRepository
    {
        void Add(int recipeId, int ingredientId);

        void Delete(int recipeId, int ingredientId);

        IEnumerable<int> GetIngredientIds(int recipeId);

        IEnumerable<int> GetRecipeIds(int ingredientId);
    }
}
