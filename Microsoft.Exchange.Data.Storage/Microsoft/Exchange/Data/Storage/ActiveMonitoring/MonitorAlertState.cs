using System;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000324 RID: 804
	public enum MonitorAlertState
	{
		// Token: 0x04001568 RID: 5480
		Unknown,
		// Token: 0x04001569 RID: 5481
		Healthy,
		// Token: 0x0400156A RID: 5482
		Degraded,
		// Token: 0x0400156B RID: 5483
		Unhealthy,
		// Token: 0x0400156C RID: 5484
		Repairing,
		// Token: 0x0400156D RID: 5485
		Disabled
	}
}
