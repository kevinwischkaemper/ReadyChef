using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Repositories
{
    public interface IRecipeRepository
    {
        void Add(Recipe recipe);

        void Delete(Recipe recipe);

        Recipe Get(string name);

        IEnumerable<Recipe> GetAll();

        IEnumerable<Recipe> GetAll(string ingredient);
    }
}
