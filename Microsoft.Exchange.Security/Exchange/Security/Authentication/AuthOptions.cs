using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200005E RID: 94
	[Flags]
	public enum AuthOptions
	{
		// Token: 0x040002AE RID: 686
		None = 0,
		// Token: 0x040002AF RID: 687
		SyncAD = 1,
		// Token: 0x040002B0 RID: 688
		BypassCache = 2,
		// Token: 0x040002B1 RID: 689
		SyncADBackEndOnly = 4,
		// Token: 0x040002B2 RID: 690
		PasswordAndHRDSync = 8,
		// Token: 0x040002B3 RID: 691
		CompactToken = 16,
		// Token: 0x040002B4 RID: 692
		ReturnWindowsIdentity = 32,
		// Token: 0x040002B5 RID: 693
		BypassPositiveCache = 64,
		// Token: 0x040002B6 RID: 694
		SyncUPN = 128,
		// Token: 0x040002B7 RID: 695
		AllowOfflineOrgIdAsPrimeAuth = 256,
		// Token: 0x040002B8 RID: 696
		LiveIdXmlAuth = 512
	}
}
