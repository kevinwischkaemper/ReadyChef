using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyChef.Core;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using ReadyChef.Core.Models;

namespace ReadyChef.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;
        private readonly IIngredientRepository _ingredientRepository;
        public RecipeRepository(string connectionString, IIngredientRepository ingredientRepository)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ReadyChef"].ConnectionString;
            _ingredientRepository = ingredientRepository;
        }

        public void Add(Recipe recipe)
        {
            const string addRecipeQuery = @"
                INSERT INTO dbo.Recipe ([Name],ReadyTime) VALUES (@name, @readyTime);
                SELECT SCOPE_IDENTITY();";

            const string addRecipeIngredientsQuery = @"
                INSERT INTO dbo.RecipeIngredient (RecipeId,IngredientId) VALUES @recipeIngredients";

            

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var newId = connection.Query<int>(addRecipeQuery, new
                {
                    name = recipe.Name,
                    readyTime = recipe.ReadyTime
                }).FirstOrDefault();

                if (newId == 0) throw new Exception();

                var ingredientParams = recipe.Ingredients.Select(p =>
                {
                    return new
                    {
                        RecipeId = newId,
                        IngredientId = p.Id
                    };
                });

                connection.Execute(addRecipeIngredientsQuery, new
                {
                    recipeIngredients = ingredientParams
                });
            }
        }

        public void Delete(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(string name)
        {
            string query = $"SELECT * FROM dbo.Recipe WHERE Name = {name}";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var recipe = connection.QuerySingle<Recipe>(query);
            }
            return new Recipe();
        }

        public IEnumerable<Recipe> GetAll()
        {
            string query = "SELECT * FROM dbo.Recipe";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Recipe>(query);
            }
        }

        public IEnumerable<Recipe> GetAll(string ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
