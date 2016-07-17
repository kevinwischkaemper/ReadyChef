using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyChef.Core;
using ReadyChef.Core.Models;
using System.Data.SqlClient;
using Dapper;

namespace ReadyChef.Infrastructure.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly string _connectionString;
        public RecipeIngredientRepository(string connectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ReadyChef"].ConnectionString;
        }

        public void Add(int recipeId, int ingredientId)
        {
            string query = $"INSERT dbo.RecipeIngredient (RecipeId,IngredientId) VALUES ('{recipeId}','{ingredientId}')";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query);
            }
        }

        public void Delete(int recipeId, int ingredientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetIngredientIds(int recipeId)
        {
            string query = $"SELECT IngredientId FROM dbo.RecipeIngredient WHERE RecipeId = {recipeId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<int>(query);
            }
        }

        public IEnumerable<int> GetRecipeIds(int ingredientId)
        {
            string query = $"SELECT RecipeId FROM dbo.RecipeIngredient WHERE IngredientId = {ingredientId}";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<int>(query);
            }
        }
    }
}
