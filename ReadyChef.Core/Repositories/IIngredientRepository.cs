using ReadyChef.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Repositories
{
    public interface IIngredientRepository
    {
        void Add(Ingredient ingredient);

        IEnumerable<Ingredient> GetAll();
    }
}
