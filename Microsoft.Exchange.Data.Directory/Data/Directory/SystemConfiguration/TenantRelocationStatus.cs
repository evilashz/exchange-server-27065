using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005D1 RID: 1489
	public enum TenantRelocationStatus : byte
	{
		// Token: 0x04002F0E RID: 12046
		NotStarted = 200,
		// Token: 0x04002F0F RID: 12047
		Synchronization,
		// Token: 0x04002F10 RID: 12048
		Lockdown,
		// Token: 0x04002F11 RID: 12049
		Retired,
		// Token: 0x04002F12 RID: 12050
		Arriving,
		// Token: 0x04002F13 RID: 12051
		Active
	}
}
