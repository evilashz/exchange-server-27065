using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E2 RID: 738
	internal enum LogReplayPlayDownReason
	{
		// Token: 0x04000C4F RID: 3151
		None,
		// Token: 0x04000C50 RID: 3152
		LagDisabled,
		// Token: 0x04000C51 RID: 3153
		NotEnoughFreeSpace,
		// Token: 0x04000C52 RID: 3154
		InRequiredRange,
		// Token: 0x04000C53 RID: 3155
		NormalLogReplay
	}
}
