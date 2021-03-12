using System;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200035C RID: 860
	public interface ISyncBatchResults
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001DC0 RID: 7616
		// (set) Token: 0x06001DC1 RID: 7617
		SyncBatchStatisticsBase Stats { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001DC2 RID: 7618
		// (set) Token: 0x06001DC3 RID: 7619
		string RawResponse { get; set; }

		// Token: 0x06001DC4 RID: 7620
		void CalculateStats();
	}
}
