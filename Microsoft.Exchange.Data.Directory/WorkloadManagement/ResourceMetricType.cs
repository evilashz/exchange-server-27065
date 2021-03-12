using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A05 RID: 2565
	public enum ResourceMetricType
	{
		// Token: 0x04004BFC RID: 19452
		None,
		// Token: 0x04004BFD RID: 19453
		ActiveDirectoryReplicationLatency,
		// Token: 0x04004BFE RID: 19454
		MdbLatency,
		// Token: 0x04004BFF RID: 19455
		Processor,
		// Token: 0x04004C00 RID: 19456
		MdbReplication,
		// Token: 0x04004C01 RID: 19457
		CiAgeOfLastNotification,
		// Token: 0x04004C02 RID: 19458
		MdbAvailability = 7,
		// Token: 0x04004C03 RID: 19459
		DiskLatency,
		// Token: 0x04004C04 RID: 19460
		Remote = 1000
	}
}
