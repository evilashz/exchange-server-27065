using System;
using System.Data.SqlClient;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000DF RID: 223
	public interface ISqlClientFactory
	{
		// Token: 0x06000988 RID: 2440
		ISqlCommand CreateSqlCommand();

		// Token: 0x06000989 RID: 2441
		ISqlCommand CreateSqlCommand(SqlCommand command);

		// Token: 0x0600098A RID: 2442
		ISqlCommand CreateSqlCommand(string commandText, ISqlConnection connection, ISqlTransaction transaction);

		// Token: 0x0600098B RID: 2443
		ISqlConnection CreateSqlConnection(SqlConnection connection);

		// Token: 0x0600098C RID: 2444
		ISqlConnection CreateSqlConnection(string connectionString);

		// Token: 0x0600098D RID: 2445
		ISqlDataReader CreateSqlDataReader(SqlDataReader reader);

		// Token: 0x0600098E RID: 2446
		ISqlTransaction CreateSqlTransaction(SqlTransaction transaction);
	}
}
