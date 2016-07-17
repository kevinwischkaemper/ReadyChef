using ReadyChef.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadyChef.Core.Models;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace ReadyChef.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly string _connectionString;
        public IngredientRepository(string connectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ReadyChef"].ConnectionString;
        }

        public void Add(Ingredient ingredient)
        {
            string query = $"INSERT dbo.Ingredient (Name) VALUES ('{ingredient.Name}')";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query);
            }
        }

        public IEnumerable<Ingredient> GetAll()
        {
            string query = $"SELECT * FROM dbo.Ingredient";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Ingredient>(query);
            }
        }
    }
}
