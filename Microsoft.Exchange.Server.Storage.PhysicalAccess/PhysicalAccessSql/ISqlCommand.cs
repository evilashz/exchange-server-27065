using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D3 RID: 211
	public interface ISqlCommand : IDisposable
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600092C RID: 2348
		// (set) Token: 0x0600092D RID: 2349
		string CommandText { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600092E RID: 2350
		// (set) Token: 0x0600092F RID: 2351
		int CommandTimeout { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000930 RID: 2352
		// (set) Token: 0x06000931 RID: 2353
		CommandType CommandType { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000932 RID: 2354
		// (set) Token: 0x06000933 RID: 2355
		ISqlConnection Connection { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000934 RID: 2356
		SqlParameterCollection Parameters { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000935 RID: 2357
		// (set) Token: 0x06000936 RID: 2358
		ISqlTransaction Transaction { get; set; }

		// Token: 0x06000937 RID: 2359
		int ExecuteNonQuery();

		// Token: 0x06000938 RID: 2360
		ISqlDataReader ExecuteReader();

		// Token: 0x06000939 RID: 2361
		ISqlDataReader ExecuteReader(CommandBehavior behavior);

		// Token: 0x0600093A RID: 2362
		object ExecuteScalar();
	}
}
