using ReadyChef.Core.Repositories;
using System.Collections.Generic;
using ReadyChef.Core.Models;
using Dapper;
using ReadyChef.Core.DataAccess;

namespace ReadyChef.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
	    private readonly IDbConnectionFactory _dbConnectionFactory;

        public IngredientRepository(IDbConnectionFactory dbConnectionFactory)
        {
	        _dbConnectionFactory = dbConnectionFactory;
        }

	    public void Add(Ingredient ingredient)
        {
            string query = $"INSERT dbo.Ingredient (Name) VALUES ('{ingredient.Name}')";
            using (var connection = _dbConnectionFactory.GetReadyChefConnection())
            {
                connection.Execute(query);
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
