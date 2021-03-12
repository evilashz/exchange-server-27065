using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000216 RID: 534
	[Flags]
	public enum ProvisioningFlags
	{
		// Token: 0x04000B1C RID: 2844
		Default = 0,
		// Token: 0x04000B1D RID: 2845
		DisableMxRecordProvisioning = 1,
		// Token: 0x04000B1E RID: 2846
		ExchangeOnlineProtection = 2,
		// Token: 0x04000B1F RID: 2847
		SyncOnly = 4,
		// Token: 0x04000B20 RID: 2848
		SynchronousProvisioningEnabled = 8
	}
}
