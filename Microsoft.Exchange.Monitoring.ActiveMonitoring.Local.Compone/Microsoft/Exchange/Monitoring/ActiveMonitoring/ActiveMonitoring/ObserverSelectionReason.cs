using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring
{
	// Token: 0x020001D6 RID: 470
	[Flags]
	internal enum ObserverSelectionReason
	{
		// Token: 0x040009CF RID: 2511
		None = 0,
		// Token: 0x040009D0 RID: 2512
		NoneInMaintenance = 1,
		// Token: 0x040009D1 RID: 2513
		NoSelectionTimestamp = 2,
		// Token: 0x040009D2 RID: 2514
		OldSelectionTimestamp = 4,
		// Token: 0x040009D3 RID: 2515
		NotEnoughObservers = 8,
		// Token: 0x040009D4 RID: 2516
		NoObserverTimestamp = 16,
		// Token: 0x040009D5 RID: 2517
		OldObserverTimestamp = 32
	}
}
