using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyChef.Core.Models;
using ReadyChef.Core.Services;

namespace ReadyChef.Infrastructure.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public int AddIngredient(string name)
        {
            int resultId = _ingredientRepository.Add(name);
            return resultId;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
