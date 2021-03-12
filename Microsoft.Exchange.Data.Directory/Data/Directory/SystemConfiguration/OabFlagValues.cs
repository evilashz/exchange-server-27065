using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000514 RID: 1300
	[Flags]
	internal enum OabFlagValues
	{
		// Token: 0x0400275B RID: 10075
		None = 0,
		// Token: 0x0400275C RID: 10076
		PublicFolderDistributionEnabled = 1,
		// Token: 0x0400275D RID: 10077
		GlobalWebDistributionEnabled = 2,
		// Token: 0x0400275E RID: 10078
		ShadowMailboxDistributionEnabled = 4
	}
}
