using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D4 RID: 212
	public interface ISqlConnection : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600093B RID: 2363
		// (remove) Token: 0x0600093C RID: 2364
		event SqlInfoMessageEventHandler InfoMessage;

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600093D RID: 2365
		ConnectionState State { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600093E RID: 2366
		// (set) Token: 0x0600093F RID: 2367
		SqlConnection WrappedConnection { get; set; }

		// Token: 0x06000940 RID: 2368
		ISqlTransaction BeginTransaction(IsolationLevel iso);

		// Token: 0x06000941 RID: 2369
		void ClearPool();

		// Token: 0x06000942 RID: 2370
		void Close();

		// Token: 0x06000943 RID: 2371
		ISqlCommand CreateCommand();

		// Token: 0x06000944 RID: 2372
		void Open();
	}
}
