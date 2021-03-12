using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D0 RID: 1488
	public enum TenantRelocationTransition : byte
	{
		// Token: 0x04002F03 RID: 12035
		NotStartedToSync,
		// Token: 0x04002F04 RID: 12036
		SyncToLockdown,
		// Token: 0x04002F05 RID: 12037
		LockdownToLockdownFinalSync,
		// Token: 0x04002F06 RID: 12038
		LockdownToGLSSwitch,
		// Token: 0x04002F07 RID: 12039
		LockdownToLockdownSwitchedGLS,
		// Token: 0x04002F08 RID: 12040
		LockdownToSync,
		// Token: 0x04002F09 RID: 12041
		LockdownToRetired,
		// Token: 0x04002F0A RID: 12042
		RetiredToSync,
		// Token: 0x04002F0B RID: 12043
		SyncToDeltaSync,
		// Token: 0x04002F0C RID: 12044
		DeltaSyncToSync
	}
}
