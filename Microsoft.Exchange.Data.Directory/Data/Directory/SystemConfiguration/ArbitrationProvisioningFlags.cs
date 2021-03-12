using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005B2 RID: 1458
	[Flags]
	public enum ArbitrationProvisioningFlags
	{
		// Token: 0x04002D96 RID: 11670
		None = 0,
		// Token: 0x04002D97 RID: 11671
		Enabled = 1,
		// Token: 0x04002D98 RID: 11672
		Halted = 2,
		// Token: 0x04002D99 RID: 11673
		HaltRecoveryDisabled = 4
	}
}
