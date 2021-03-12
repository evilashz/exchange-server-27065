using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E7 RID: 1511
	[Flags]
	internal enum ThrottlingPolicyFlags
	{
		// Token: 0x04003185 RID: 12677
		None = 0,
		// Token: 0x04003186 RID: 12678
		IsServiceAccount = 1,
		// Token: 0x04003187 RID: 12679
		OrganizationScope = 2,
		// Token: 0x04003188 RID: 12680
		GlobalScope = 4
	}
}
