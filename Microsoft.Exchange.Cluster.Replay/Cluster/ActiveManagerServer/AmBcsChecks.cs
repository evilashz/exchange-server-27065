using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200009D RID: 157
	[Flags]
	internal enum AmBcsChecks
	{
		// Token: 0x040002A6 RID: 678
		None = 0,
		// Token: 0x040002A7 RID: 679
		IsHealthyOrDisconnected = 1,
		// Token: 0x040002A8 RID: 680
		IsCatalogStatusHealthy = 2,
		// Token: 0x040002A9 RID: 681
		CopyQueueLength = 4,
		// Token: 0x040002AA RID: 682
		ReplayQueueLength = 8,
		// Token: 0x040002AB RID: 683
		IsCatalogStatusCrawling = 16,
		// Token: 0x040002AC RID: 684
		IsPassiveCopy = 32,
		// Token: 0x040002AD RID: 685
		IsSeedingSource = 64,
		// Token: 0x040002AE RID: 686
		TotalQueueLengthMaxAllowed = 128,
		// Token: 0x040002AF RID: 687
		ManagedAvailabilityInitiatorBetterThanSource = 256,
		// Token: 0x040002B0 RID: 688
		ManagedAvailabilityAllHealthy = 512,
		// Token: 0x040002B1 RID: 689
		ManagedAvailabilityUptoNormalHealthy = 1024,
		// Token: 0x040002B2 RID: 690
		ManagedAvailabilityAllBetterThanSource = 2048,
		// Token: 0x040002B3 RID: 691
		ManagedAvailabilitySameAsSource = 4096,
		// Token: 0x040002B4 RID: 692
		ActivationEnabled = 8192,
		// Token: 0x040002B5 RID: 693
		MaxActivesUnderHighestLimit = 16384,
		// Token: 0x040002B6 RID: 694
		MaxActivesUnderPreferredLimit = 32768
	}
}
