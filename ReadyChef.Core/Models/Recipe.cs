using System;
using System.Collections.Generic;

namespace ReadyChef.Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReadyTime { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}
