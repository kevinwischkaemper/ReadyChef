using System;
using System.Collections.Generic;

namespace ReadyChef.Core.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ReadyTime { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
