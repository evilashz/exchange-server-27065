using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D2 RID: 1490
	public enum RelocationStateRequested : byte
	{
		// Token: 0x04002F15 RID: 12053
		None,
		// Token: 0x04002F16 RID: 12054
		InitializationFinished = 10,
		// Token: 0x04002F17 RID: 12055
		SynchronizationFinishedFullSync = 25,
		// Token: 0x04002F18 RID: 12056
		SynchronizationFinishedDeltaSync = 40,
		// Token: 0x04002F19 RID: 12057
		LockdownFinishedFinalDeltaSync = 55,
		// Token: 0x04002F1A RID: 12058
		LockdownSwitchedGLS = 60,
		// Token: 0x04002F1B RID: 12059
		RetiredUpdatedSourceForest = 65,
		// Token: 0x04002F1C RID: 12060
		RetiredUpdatedTargetForest = 70,
		// Token: 0x04002F1D RID: 12061
		Cleanup = 90
	}
}
