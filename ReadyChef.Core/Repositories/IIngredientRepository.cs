using ReadyChef.Core.Models;
using System.Collections.Generic;

namespace ReadyChef.Core.Repositories
{
    public interface IIngredientRepository
    {
        void Add(Ingredient ingredient);

        IEnumerable<Ingredient> GetAll();
    }
}
