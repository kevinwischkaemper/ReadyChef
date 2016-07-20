using ReadyChef.Core.Repositories;
using System.Collections.Generic;
using ReadyChef.Core.Models;
using Dapper;
using ReadyChef.Core.DataAccess;
using System;

namespace ReadyChef.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
	    private readonly IDbConnectionFactory _dbConnectionFactory;
        public IngredientRepository(IDbConnectionFactory dbConnectionFactory)
        {
	        _dbConnectionFactory = dbConnectionFactory;
        }

	    public int Add(string name)
        {

            string query = @"
                             INSERT dbo.Ingredient (Name) VALUES (@name);
                             SELECT SCOPE_IDENTITY();";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                var ingredientId = connection.ExecuteScalar<int>(query,new
                {
                    name = name
                });
                return ingredientId;
            }
        }

        public IEnumerable<Ingredient> GetAll()
        {
            string query = $"SELECT * FROM dbo.Ingredient";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                return connection.Query<Ingredient>(query);
            }
        }

      
    }
}
