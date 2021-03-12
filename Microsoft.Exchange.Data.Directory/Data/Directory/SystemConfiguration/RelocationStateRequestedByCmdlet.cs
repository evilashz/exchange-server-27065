using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D3 RID: 1491
	public enum RelocationStateRequestedByCmdlet : byte
	{
		// Token: 0x04002F1F RID: 12063
		InitializationFinished = 10,
		// Token: 0x04002F20 RID: 12064
		SynchronizationFinishedFullSync = 25,
		// Token: 0x04002F21 RID: 12065
		SynchronizationFinishedDeltaSync = 40,
		// Token: 0x04002F22 RID: 12066
		LockdownFinishedFinalDeltaSync = 55,
		// Token: 0x04002F23 RID: 12067
		LockdownSwitchedGLS = 60,
		// Token: 0x04002F24 RID: 12068
		RetiredUpdatedSourceForest = 65,
		// Token: 0x04002F25 RID: 12069
		RetiredUpdatedTargetForest = 70,
		// Token: 0x04002F26 RID: 12070
		Cleanup = 90
	}
}
