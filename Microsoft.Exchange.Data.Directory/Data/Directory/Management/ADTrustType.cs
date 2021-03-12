using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006DE RID: 1758
	public enum ADTrustType
	{
		// Token: 0x0400371A RID: 14106
		[LocDescription(DirectoryStrings.IDs.NotTrust)]
		None,
		// Token: 0x0400371B RID: 14107
		[LocDescription(DirectoryStrings.IDs.ExternalTrust)]
		External,
		// Token: 0x0400371C RID: 14108
		[LocDescription(DirectoryStrings.IDs.ForestTrust)]
		Forest
	}
}
