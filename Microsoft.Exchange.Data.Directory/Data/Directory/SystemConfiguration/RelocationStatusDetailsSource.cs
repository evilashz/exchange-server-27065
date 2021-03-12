using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D5 RID: 1493
	public enum RelocationStatusDetailsSource : byte
	{
		// Token: 0x04002F39 RID: 12089
		NotStarted,
		// Token: 0x04002F3A RID: 12090
		InitializationStarted = 5,
		// Token: 0x04002F3B RID: 12091
		InitializationFinished = 10,
		// Token: 0x04002F3C RID: 12092
		SynchronizationStartedFullSync = 15,
		// Token: 0x04002F3D RID: 12093
		SynchronizationFinishedFullSync = 25,
		// Token: 0x04002F3E RID: 12094
		SynchronizationStartedDeltaSync = 30,
		// Token: 0x04002F3F RID: 12095
		SynchronizationFinishedDeltaSync = 40,
		// Token: 0x04002F40 RID: 12096
		LockdownStarted = 45,
		// Token: 0x04002F41 RID: 12097
		LockdownStartedFinalDeltaSync = 50,
		// Token: 0x04002F42 RID: 12098
		LockdownFinishedFinalDeltaSync = 55,
		// Token: 0x04002F43 RID: 12099
		LockdownSwitchedGLS = 60,
		// Token: 0x04002F44 RID: 12100
		RetiredUpdatedSourceForest = 65,
		// Token: 0x04002F45 RID: 12101
		RetiredUpdatedTargetForest = 70
	}
}
