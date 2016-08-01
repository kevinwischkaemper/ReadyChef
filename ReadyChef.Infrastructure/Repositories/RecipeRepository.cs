using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using ReadyChef.Core.DataAccess;
using ReadyChef.Core.Models;
using System.Data;
using ReadyChef.Core.Exceptions;

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
                INSERT INTO dbo.Recipe ([Name],ReadyTime,Details) VALUES (@name, @readyTime, @details);
                SELECT SCOPE_IDENTITY();";

            const string addRecipeIngredientsQuery = @"INSERT INTO dbo.RecipeIngredient (RecipeId,IngredientId,Amount,AmountUnits) VALUES (@RecipeId,@IngredientId,@Amount,@AmountUnits)";

            

            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
               
                var newId = connection.ExecuteScalar<int>(addRecipeQuery, new
                {
                    name = recipe.Name,
                    readyTime = recipe.ReadyTime,
                    details = recipe.Details
                });

                if (newId == 0) throw new Exception();

                var ingredientParams = recipe.RecipeIngredients.Select(p =>
                {
                    return new
                    {
                        RecipeId = newId,
                        IngredientId = p.Ingredient.Id,
                        Amount = p.Amount,
                        AmountUnits = p.AmountUnits
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

            string recipeQuery = @"SELECT R.[Name], R.Id, R.ReadyTime, R.Details, RI.Amount AS IngredientAmount, RI.AmountUnits AS IngredientAmountUnits, I.Name AS IngredientName, I.Id AS IngredientId
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
            if (recipeDataRows.Count() == 0)
            {
                return null;
            }
            var firstRow = recipeDataRows.First();
            recipe.Id = firstRow.Id;
            recipe.Name = firstRow.Name;
            recipe.ReadyTime = firstRow.ReadyTime;
            recipe.Details = firstRow.Details;
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
                    AmountUnits = row.IngredientAmountUnits
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetAll()
        {
            IEnumerable<ConsolidatedRecipeDataRow> recipeDataRows;
            List<Recipe> recipeList = new List<Recipe>();
            string recipeQuery = @"SELECT R.[Name], R.Id, R.ReadyTime, R.Details, RI.Amount AS IngredientAmount, RI.AmountUnits AS IngredientAmountUnits, I.Name AS IngredientName, I.Id AS IngredientId
                                    FROM dbo.Recipe R
                                    INNER JOIN dbo.RecipeIngredient RI
	                                    ON R.Id = RI.RecipeId
                                    INNER JOIN dbo.Ingredient I
	                                    ON I.Id = RI.IngredientId";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                recipeDataRows = connection.Query<ConsolidatedRecipeDataRow>(recipeQuery);
            }
            var firstRow = recipeDataRows.First();
            var recipe = new Recipe();
            recipe.Id = firstRow.Id;
            recipe.Name = firstRow.Name;
            recipe.ReadyTime = firstRow.ReadyTime;
            recipe.Details = firstRow.Details;
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var row in recipeDataRows)
            {
                if (row.Name != recipe.Name)
                {
                    recipeList.Add(recipe);
                    recipe = new Recipe();
                    recipe.Id = row.Id;
                    recipe.Name = row.Name;
                    recipe.ReadyTime = row.ReadyTime;
                    recipe.Details = row.Details;
                    recipe.RecipeIngredients = new List<RecipeIngredient>();
                }
                var recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient()
                    {
                        Name = row.IngredientName,
                        Id = row.IngredientId
                    },
                    Amount = row.IngredientAmount,
                    AmountUnits = row.IngredientAmountUnits
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            recipeList.Add(recipe);
            return recipeList;
        }

        public IEnumerable<Recipe> GetAll(string ingredient)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Recipe> GetByIngredient(string ingredient)
        {
            IEnumerable<ConsolidatedRecipeDataRow> recipeDataRows;
            List<Recipe> recipeList = new List<Recipe>();
            string recipeQuery = @"SELECT R.[Name], R.Id, R.ReadyTime, R.Details, RI.Amount AS IngredientAmount, RI.AmountUnits AS IngredientAmountUnits, I.Name AS IngredientName, I.Id AS IngredientId
                                    FROM dbo.Recipe R
                                    INNER JOIN dbo.RecipeIngredient RI
                                     ON R.Id = RI.RecipeId
                                    INNER JOIN dbo.Ingredient I
                                     ON I.Id = RI.IngredientId
                                    WHERE I.Name LIKE '%' + @ingredient + '%'";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                recipeDataRows = connection.Query<ConsolidatedRecipeDataRow>(recipeQuery, new { ingredient = ingredient });
            }
            if (recipeDataRows.Count() <= 0) throw new RecipeNotFoundException();
            var firstRow = recipeDataRows.First();
            var recipe = new Recipe();
            recipe.Id = firstRow.Id;
            recipe.Name = firstRow.Name;
            recipe.ReadyTime = firstRow.ReadyTime;
            recipe.Details = firstRow.Details;
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var row in recipeDataRows)
            {
                if (row.Name != recipe.Name)
                {
                    recipeList.Add(recipe);
                    recipe = new Recipe();
                    recipe.Id = row.Id;
                    recipe.Name = row.Name;
                    recipe.ReadyTime = row.ReadyTime;
                    recipe.Details = row.Details;
                    recipe.RecipeIngredients = new List<RecipeIngredient>();
                }
                var recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient()
                    {
                        Name = row.IngredientName,
                        Id = row.IngredientId
                    },
                    Amount = row.IngredientAmount,
                    AmountUnits = row.IngredientAmountUnits
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            recipeList.Add(recipe);
            return recipeList;
        }

        public IEnumerable<Recipe> GetByName(string name)
        {
            IEnumerable<ConsolidatedRecipeDataRow> recipeDataRows;
            List<Recipe> recipeList = new List<Recipe>();
            string recipeQuery = @"SELECT R.[Name], R.Id, R.ReadyTime, R.Details, RI.Amount AS IngredientAmount, RI.AmountUnits AS IngredientAmountUnits, I.Name AS IngredientName, I.Id AS IngredientId
                                    FROM dbo.Recipe R
                                    INNER JOIN dbo.RecipeIngredient RI
	                                    ON R.Id = RI.RecipeId
                                    INNER JOIN dbo.Ingredient I
	                                    ON I.Id = RI.IngredientId
                                    WHERE R.[Name] LIKE '%' + @name + '%'";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                recipeDataRows = connection.Query<ConsolidatedRecipeDataRow>(recipeQuery, new { name = name});
            }
            if (recipeDataRows.Count() <= 0) throw new RecipeNotFoundException();
            var firstRow = recipeDataRows.First();
            var recipe = new Recipe();
            recipe.Id = firstRow.Id;
            recipe.Name = firstRow.Name;
            recipe.ReadyTime = firstRow.ReadyTime;
            recipe.Details = firstRow.Details;
            recipe.RecipeIngredients = new List<RecipeIngredient>();
            foreach (var row in recipeDataRows)
            {
                if (row.Name != recipe.Name)
                {
                    recipeList.Add(recipe);
                    recipe = new Recipe();
                    recipe.Id = row.Id;
                    recipe.Name = row.Name;
                    recipe.ReadyTime = row.ReadyTime;
                    recipe.Details = row.Details;
                    recipe.RecipeIngredients = new List<RecipeIngredient>();
                }
                var recipeIngredient = new RecipeIngredient()
                {
                    Ingredient = new Ingredient()
                    {
                        Name = row.IngredientName,
                        Id = row.IngredientId
                    },
                    Amount = row.IngredientAmount,
                    AmountUnits = row.IngredientAmountUnits
                };
                recipe.RecipeIngredients.Add(recipeIngredient);
            }
            recipeList.Add(recipe);
            return recipeList;
        }
    }
}
