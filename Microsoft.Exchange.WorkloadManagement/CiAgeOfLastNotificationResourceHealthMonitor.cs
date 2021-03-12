using System;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000015 RID: 21
	internal class CiAgeOfLastNotificationResourceHealthMonitor : CiResourceHealthMonitorBase
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004724 File Offset: 0x00002924
		internal CiAgeOfLastNotificationResourceHealthMonitor(CiAgeOfLastNotificationResourceKey key) : base(key)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004730 File Offset: 0x00002930
		protected override int GetMetricFromStatusInternal(RpcDatabaseCopyStatus2 status)
		{
			int? contentIndexBacklog = status.ContentIndexBacklog;
			if (contentIndexBacklog == null)
			{
				return -1;
			}
			return contentIndexBacklog.GetValueOrDefault();
		}
	}
}
