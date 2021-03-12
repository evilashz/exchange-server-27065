using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200009E RID: 158
	[Flags]
	internal enum AmBcsServerChecks
	{
		// Token: 0x040002B8 RID: 696
		None = 0,
		// Token: 0x040002B9 RID: 697
		DebugOptionDisabled = 1,
		// Token: 0x040002BA RID: 698
		ClusterNodeUp = 2,
		// Token: 0x040002BB RID: 699
		DatacenterActivationModeStarted = 4,
		// Token: 0x040002BC RID: 700
		AutoActivationAllowed = 8
	}
}
