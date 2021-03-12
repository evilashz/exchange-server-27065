using System;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x0200010C RID: 268
	internal interface IMeterableJetDataSource
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B82 RID: 2946
		string DatabasePath { get; }

		// Token: 0x06000B83 RID: 2947
		long GetAvailableFreeSpace();

		// Token: 0x06000B84 RID: 2948
		long GetDatabaseFileSize();

		// Token: 0x06000B85 RID: 2949
		long GetCurrentVersionBuckets();
	}
}
