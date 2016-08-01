using ReadyChef.Core.Models;
using ReadyChef.Core.Services;
using ReadyChef.Infrastructure.DataAccess;
using ReadyChef.Infrastructure.Repositories;
using ReadyChef.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        private string _filePath;
        private string _text;
        public List<Recipe> RecipeList = new List<Recipe>();
        private static DbConnectionFactory dbFactory = new DbConnectionFactory(@"User ID=sa;Initial Catalog=ReadyChef;Data Source=DESKTOP-CTHM6BB\SQLEXPRESS; Password=turkster9435");
        private static IngredientRepository ingredientRepository = new IngredientRepository(dbFactory);
        private static IngredientService _ingredientService = new IngredientService(ingredientRepository);
        private static RecipeRepository recipeRepository = new RecipeRepository(dbFactory);
        private static RecipeService _recipeService = new RecipeService(recipeRepository);

        public Form1()
        {
            InitializeComponent();
        }
        

        private void SelectFileToParse(object sender, EventArgs e)
        {
            var result = openFilePathDialog.ShowDialog();
            if (result == DialogResult.OK) {
                _filePath = openFilePathDialog.FileName;
                _text = File.ReadAllText(_filePath).Replace('"','#');
                btnParse.Enabled = true;
            }
        }

        private void Parse(object sender, EventArgs e)
        {
            Regex recipeStart = new Regex("<recipe");
            Regex recipeEnd = new Regex("</recipe>");
            Regex recipeDescription = new Regex(@"(?<=\bdescription=#)[^#]*");
            Regex ingredientList = new Regex(@"<RecipeItem [^/>]+/>");
            Regex ingredientName = new Regex(@"(?<=\bItemName=#)[^#]*");
            Regex ingredientQuantity = new Regex(@"(?<=\bitemQuantity=#)[^#]*");
            Regex minutes = new Regex(@"\b\d+\b(?= minutes)");
            Regex details = new Regex(@"(?<=\bMETHOD:)[^]]*");

            var recipeStartMatches = recipeStart.Matches(_text).Cast<Match>();
            var recipeEndMatches = recipeEnd.Matches(_text).Cast<Match>();              
            IEnumerable<Tuple<int,int>> recipeMatchRanges = recipeStartMatches.Zip(recipeEndMatches,
                (startMatch, endMatch) => new Tuple<int, int>(startMatch.Index, endMatch.Index));
            int length = _text.Length;
            foreach (var range in recipeMatchRanges) {
                string recipeBody = _text.Substring(range.Item1, range.Item2 - range.Item1);

                Recipe recipe = new Recipe();
                recipe.RecipeIngredients = new List<RecipeIngredient>();
                recipe.Name = recipeDescription.Match(recipeBody).Value;

                foreach (Match ingredientRange in ingredientList.Matches(recipeBody)) {
                    var recipeIngredient = new RecipeIngredient();
                    var ingredient = new Ingredient();
                    ingredient.Name = ingredientName.Match(ingredientRange.Value).Value;

                    recipeIngredient.Ingredient = ingredient;
                    recipeIngredient.Amount = (int) Math.Floor(Convert.ToDouble(ingredientQuantity.Match(ingredientRange.Value).Value));
                    recipeIngredient.AmountUnits = "units";
                    recipe.RecipeIngredients.Add(recipeIngredient);
                }

                var unformattedDetails = details.Match(recipeBody).Value.Replace("\r", "").Replace("\n", "");
                recipe.Details = Regex.Replace(unformattedDetails, @"(^\d(\.)?)|((?<=\.)\d(\.)?)", "").Replace('#','"');
                string temp = minutes.Match(recipe.Details).Value;
                try { recipe.ReadyTime = Convert.ToInt32(temp); }
                catch (System.FormatException) { recipe.ReadyTime = 20; }

                RecipeList.Add(recipe);                
            }
            dgvRecipeList.DataSource = RecipeList;
            dgvRecipeList.Update();
        }

        private void CommitRecipes(object sender, EventArgs e)
        {
            var populatedList = RecipeList.Select(recipe =>
            {
                var newRecipe = new Recipe();
                newRecipe.Details = recipe.Details;
                newRecipe.Name = recipe.Name;
                newRecipe.ReadyTime = recipe.ReadyTime;
                newRecipe.RecipeIngredients = recipe.RecipeIngredients
                .Select(recipeIngredient =>
                {
                    var id = _ingredientService.AddIngredient(recipeIngredient.Ingredient.Name);
                    var newIngredient = new Ingredient();
                    newIngredient.Id = id;
                    newIngredient.Name = recipeIngredient.Ingredient.Name;
                    var newRecipeIngredient = new RecipeIngredient();
                    newRecipeIngredient.Ingredient = newIngredient;
                    newRecipeIngredient.Amount = recipeIngredient.Amount;
                    newRecipeIngredient.AmountUnits = recipeIngredient.AmountUnits;
                    return newRecipeIngredient;
                }).ToList();
                return newRecipe;
            });

            foreach (var recipe in populatedList) {
                _recipeService.AddRecipe(recipe);
            }
        }
    }
}
