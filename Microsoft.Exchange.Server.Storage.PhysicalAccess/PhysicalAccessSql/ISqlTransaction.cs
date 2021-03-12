using System;
using System.Data.SqlClient;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D8 RID: 216
	public interface ISqlTransaction : IDisposable
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		SqlTransaction WrappedTransaction { get; set; }

		// Token: 0x06000961 RID: 2401
		void Commit();

		// Token: 0x06000962 RID: 2402
		void Rollback();
	}
}
