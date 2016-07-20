using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using ReadyChef.Core.DataAccess;
using ReadyChef.Core.Models;
using System.Data;

namespace ReadyChef.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
	    private readonly IDbConnectionFactory _dbConnectionFactory;

        public RecipeRepository(IDbConnectionFactory dboConnectionFactory)
        {
			_dbConnectionFactory = dboConnectionFactory;
        }

	    public void Add(Recipe recipe)
        {
            const string addRecipeQuery = @"
                INSERT INTO dbo.Recipe ([Name],ReadyTime) VALUES (@name, @readyTime);
                SELECT SCOPE_IDENTITY();";

            const string addRecipeIngredientsQuery = @"INSERT INTO dbo.RecipeIngredient (RecipeId,IngredientId,Amount) VALUES (@RecipeId,@IngredientId,@Amount)";

            

            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
               
                var newId = connection.ExecuteScalar<int>(addRecipeQuery, new
                {
                    name = recipe.Name,
                    readyTime = recipe.ReadyTime
                });

                if (newId == 0) throw new Exception();

                var ingredientParams = recipe.RecipeIngredients.Select(p =>
                {
                    return new
                    {
                        RecipeId = newId,
                        IngredientId = p.Ingredient.Id,
                        Amount = p.Amount
                    };
                });
                IDbTransaction trans = connection.BeginTransaction();
                connection.Execute(addRecipeIngredientsQuery, ingredientParams, transaction: trans);
                trans.Commit();
            }
        }

        public void Delete(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(string name)
        {
            Recipe recipe = new Recipe();
            IEnumerable<ConsolidatedRecipeDataRow> recipeDataRows;

            string recipeQuery = @"SELECT R.[Name], R.Id, R.ReadyTime, RI.Amount AS IngredientAmount, I.Name AS IngredientName, I.Id AS IngredientId
                                    FROM dbo.Recipe R
                                    INNER JOIN dbo.RecipeIngredient RI
	                                    ON R.Id = RI.RecipeId
                                    INNER JOIN dbo.Ingredient I
	                                    ON I.Id = RI.IngredientId 
                                    WHERE R.[Name] = @name";

            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                 recipeDataRows = connection.Query<ConsolidatedRecipeDataRow>(recipeQuery, new { name = name });
            }
            recipe.Id = recipeDataRows.First().Id;
            recipe.Name = recipeDataRows.First().Name;
            recipe.ReadyTime = recipeDataRows.First().ReadyTime;
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var row in recipeDataRows)
            {
                var recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient()
                    {
                        Name = row.IngredientName,
                        Id = row.IngredientId
                    },
                    Amount = row.IngredientAmount,
                    AmountUnits = "units"
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetAll()
        {
            string query = "SELECT * FROM dbo.Recipe";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                return connection.Query<Recipe>(query);
            }
        }

        public IEnumerable<Recipe> GetAll(string ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
