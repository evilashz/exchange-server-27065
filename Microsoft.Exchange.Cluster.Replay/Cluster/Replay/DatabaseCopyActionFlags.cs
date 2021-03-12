using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000304 RID: 772
	[Flags]
	internal enum DatabaseCopyActionFlags : uint
	{
		// Token: 0x04000CFC RID: 3324
		Replication = 1U,
		// Token: 0x04000CFD RID: 3325
		Activation = 2U,
		// Token: 0x04000CFE RID: 3326
		ActiveCopy = 4U,
		// Token: 0x04000CFF RID: 3327
		SyncSuspendResume = 8U,
		// Token: 0x04000D00 RID: 3328
		SkipSettingResumeAutoReseedState = 16U
	}
}
