using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ReadyChef.Core.DataAccess
{
	public interface IDbConnectionFactory
	{
		IDbConnection GetReadyChefConnection();
		Task<IDbConnection> GetReadyChefConnectionAsync(CancellationToken ct = default(CancellationToken));
	}
}
