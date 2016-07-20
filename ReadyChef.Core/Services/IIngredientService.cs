using ReadyChef.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Services
{
    public interface IIngredientService
    {
        int AddIngredient(string name);
    }
}
