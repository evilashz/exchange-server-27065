using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000080 RID: 128
	internal enum WatchdogAction : uint
	{
		// Token: 0x0400028E RID: 654
		Disable,
		// Token: 0x0400028F RID: 655
		Log,
		// Token: 0x04000290 RID: 656
		TerminateProcess,
		// Token: 0x04000291 RID: 657
		BugCheck,
		// Token: 0x04000292 RID: 658
		Max
	}
}
