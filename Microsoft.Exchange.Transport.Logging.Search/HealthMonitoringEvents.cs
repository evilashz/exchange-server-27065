using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000003 RID: 3
	internal enum HealthMonitoringEvents
	{
		// Token: 0x04000006 RID: 6
		DeliveryReports_Search_Latencies,
		// Token: 0x04000007 RID: 7
		DeliveryReports_Get_Latencies,
		// Token: 0x04000008 RID: 8
		DeliveryReports_Search_Errors,
		// Token: 0x04000009 RID: 9
		DeliveryReports_Get_Errors,
		// Token: 0x0400000A RID: 10
		TransportSync_DispatchStats,
		// Token: 0x0400000B RID: 11
		TransportSync_LatencyStats,
		// Token: 0x0400000C RID: 12
		TransportSync_ManualSubscriptionStats,
		// Token: 0x0400000D RID: 13
		TransportSync_ProvisionedSubscriptionStats,
		// Token: 0x0400000E RID: 14
		TransportSync_PoisonSubscriptionStats,
		// Token: 0x0400000F RID: 15
		TransportSync_SyncStats,
		// Token: 0x04000010 RID: 16
		TransportSync_SubscriptionStats,
		// Token: 0x04000011 RID: 17
		TransportSync_TenantStats
	}
}
