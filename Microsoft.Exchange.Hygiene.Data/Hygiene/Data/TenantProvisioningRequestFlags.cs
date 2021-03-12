using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000107 RID: 263
	[Flags]
	public enum TenantProvisioningRequestFlags
	{
		// Token: 0x0400054B RID: 1355
		Default = 0,
		// Token: 0x0400054C RID: 1356
		Reporting = 1,
		// Token: 0x0400054D RID: 1357
		ServiceUpgrade = 2,
		// Token: 0x0400054E RID: 1358
		GlobalLocator = 4,
		// Token: 0x0400054F RID: 1359
		MicrosoftOnline = 8,
		// Token: 0x04000550 RID: 1360
		PremigrationCheck = 16,
		// Token: 0x04000551 RID: 1361
		Relocation = 32,
		// Token: 0x04000552 RID: 1362
		ForceServiceUpgrade = 64
	}
}
