using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D4 RID: 1492
	public enum RelocationStatusDetails : byte
	{
		// Token: 0x04002F28 RID: 12072
		NotStarted,
		// Token: 0x04002F29 RID: 12073
		InitializationStarted = 5,
		// Token: 0x04002F2A RID: 12074
		InitializationFinished = 10,
		// Token: 0x04002F2B RID: 12075
		SynchronizationStartedFullSync = 15,
		// Token: 0x04002F2C RID: 12076
		SynchronizationFinishedFullSync = 25,
		// Token: 0x04002F2D RID: 12077
		SynchronizationStartedDeltaSync = 30,
		// Token: 0x04002F2E RID: 12078
		SynchronizationFinishedDeltaSync = 40,
		// Token: 0x04002F2F RID: 12079
		LockdownStarted = 45,
		// Token: 0x04002F30 RID: 12080
		LockdownStartedFinalDeltaSync = 50,
		// Token: 0x04002F31 RID: 12081
		LockdownFinishedFinalDeltaSync = 55,
		// Token: 0x04002F32 RID: 12082
		LockdownSwitchedGLS = 60,
		// Token: 0x04002F33 RID: 12083
		RetiredUpdatedSourceForest = 65,
		// Token: 0x04002F34 RID: 12084
		RetiredUpdatedTargetForest = 70,
		// Token: 0x04002F35 RID: 12085
		Arriving = 75,
		// Token: 0x04002F36 RID: 12086
		Active = 80,
		// Token: 0x04002F37 RID: 12087
		Cleanup = 90
	}
}
