using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005FE RID: 1534
	public enum UMGlobalCallRoutingScheme
	{
		// Token: 0x04003287 RID: 12935
		[LocDescription(DirectoryStrings.IDs.None)]
		None,
		// Token: 0x04003288 RID: 12936
		[LocDescription(DirectoryStrings.IDs.E164)]
		E164,
		// Token: 0x04003289 RID: 12937
		[LocDescription(DirectoryStrings.IDs.GatewayGuid)]
		GatewayGuid,
		// Token: 0x0400328A RID: 12938
		[LocDescription(DirectoryStrings.IDs.Reserved1)]
		Reserved1,
		// Token: 0x0400328B RID: 12939
		[LocDescription(DirectoryStrings.IDs.Reserved2)]
		Reserved2,
		// Token: 0x0400328C RID: 12940
		[LocDescription(DirectoryStrings.IDs.Reserved3)]
		Reserved3
	}
}
