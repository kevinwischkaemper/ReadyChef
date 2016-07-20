using ReadyChef.Core.Models;
using System.Collections.Generic;

namespace ReadyChef.Core.Repositories
{
    public interface IIngredientRepository
    {
        int Add(string name);

        IEnumerable<Ingredient> GetAll();
    }
}
