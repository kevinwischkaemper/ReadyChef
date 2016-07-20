using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Models
{
    public class RecipeIngredient
    {
        public Ingredient Ingredient { get; set; }
        public int Amount { get; set; }
        public string AmountUnits { get; set; }
    }
}
