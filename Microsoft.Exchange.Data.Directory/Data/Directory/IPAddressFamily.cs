using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000106 RID: 262
	public enum IPAddressFamily
	{
		// Token: 0x040005A7 RID: 1447
		[LocDescription(DirectoryStrings.IDs.IPv4Only)]
		IPv4Only,
		// Token: 0x040005A8 RID: 1448
		[LocDescription(DirectoryStrings.IDs.IPv6Only)]
		IPv6Only,
		// Token: 0x040005A9 RID: 1449
		[LocDescription(DirectoryStrings.IDs.Any)]
		Any
	}
}
