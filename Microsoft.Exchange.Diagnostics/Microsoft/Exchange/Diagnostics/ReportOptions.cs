using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000CA RID: 202
	[Flags]
	public enum ReportOptions
	{
		// Token: 0x04000412 RID: 1042
		None = 0,
		// Token: 0x04000413 RID: 1043
		ReportTerminateAfterSend = 1,
		// Token: 0x04000414 RID: 1044
		DoNotCollectDumps = 2,
		// Token: 0x04000415 RID: 1045
		DeepStackTraceHash = 4,
		// Token: 0x04000416 RID: 1046
		DoNotLogProcessAndThreadIds = 8,
		// Token: 0x04000417 RID: 1047
		DoNotFreezeThreads = 16
	}
}
