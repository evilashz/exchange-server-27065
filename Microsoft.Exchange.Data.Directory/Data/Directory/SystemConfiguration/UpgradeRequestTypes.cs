using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002CC RID: 716
	public enum UpgradeRequestTypes
	{
		// Token: 0x040013CC RID: 5068
		None,
		// Token: 0x040013CD RID: 5069
		TenantUpgrade,
		// Token: 0x040013CE RID: 5070
		PrestageUpgrade,
		// Token: 0x040013CF RID: 5071
		CancelPrestageUpgrade,
		// Token: 0x040013D0 RID: 5072
		PilotUpgrade,
		// Token: 0x040013D1 RID: 5073
		TenantUpgradeDryRun
	}
}
