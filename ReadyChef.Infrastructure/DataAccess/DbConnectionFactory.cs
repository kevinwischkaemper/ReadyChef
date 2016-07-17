using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ReadyChef.Core.DataAccess;

namespace ReadyChef.Infrastructure.DataAccess
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _readyChefConnectionString;

		public DbConnectionFactory(string readyChefConnectionString)
		{
			if (string.IsNullOrWhiteSpace(readyChefConnectionString))
				throw new ArgumentNullException(nameof(readyChefConnectionString));

			_readyChefConnectionString = readyChefConnectionString;
		}

		public IDbConnection GetReadyChefConnection()
		{
			var connection = new SqlConnection(_readyChefConnectionString);
			connection.Open();
			return connection;			
		}

		public async Task<IDbConnection> GetReadyChefConnectionAsync(CancellationToken ct = new CancellationToken())
		{
			var connection = new SqlConnection(_readyChefConnectionString);
			await connection.OpenAsync(ct);
			return connection;
		}
	}
}
