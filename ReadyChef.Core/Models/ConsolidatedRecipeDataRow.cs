using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadyChef.Core.Models
{
    public class ConsolidatedRecipeDataRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReadyTime { get; set; }
        public string Details { get; set; }
        public int IngredientAmount { get; set; }
        public string IngredientName { get; set; }
        public int IngredientId { get; set; }
        public string IngredientAmountUnits { get; set; }
    }
}
