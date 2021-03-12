using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Probes
{
	// Token: 0x020001D2 RID: 466
	[Flags]
	internal enum ObserverHeartbeatResult
	{
		// Token: 0x040009BE RID: 2494
		None = 0,
		// Token: 0x040009BF RID: 2495
		Success = 1,
		// Token: 0x040009C0 RID: 2496
		OldResponderResult = 2,
		// Token: 0x040009C1 RID: 2497
		NoResponderResult = 4,
		// Token: 0x040009C2 RID: 2498
		ServiceNotResponsive = 8,
		// Token: 0x040009C3 RID: 2499
		MachineNotResponsive = 16,
		// Token: 0x040009C4 RID: 2500
		MonitoringOffline = 32,
		// Token: 0x040009C5 RID: 2501
		NoLongerObserver = 64
	}
}
