using System;
using System.Data;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x0200069E RID: 1694
	internal interface IReportContextFactory
	{
		// Token: 0x06003C06 RID: 15366
		IDbConnection CreateConnection(bool createBackupConnection = false);

		// Token: 0x06003C07 RID: 15367
		IReportContext CreateReportContext(IDbConnection connection);
	}
}
