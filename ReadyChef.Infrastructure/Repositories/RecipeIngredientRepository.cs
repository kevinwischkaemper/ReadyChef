using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using Dapper;
using ReadyChef.Core.DataAccess;

namespace ReadyChef.Infrastructure.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
	    private readonly IDbConnectionFactory _dbConnectionFactory;
		public RecipeIngredientRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
		}

	    public void Add(int recipeId, int ingredientId, int amount)
        {
            string query = $"INSERT dbo.RecipeIngredient (RecipeId,IngredientId,Amount) VALUES ('{recipeId}','{ingredientId}','{amount}')";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
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
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                return connection.Query<int>(query);
            }
        }

        public IEnumerable<int> GetRecipeIds(int ingredientId)
        {
            string query = $"SELECT RecipeId FROM dbo.RecipeIngredient WHERE IngredientId = {ingredientId}";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                return connection.Query<int>(query);
            }
        }
    }
}
