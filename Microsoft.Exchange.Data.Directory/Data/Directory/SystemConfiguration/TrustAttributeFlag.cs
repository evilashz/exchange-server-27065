using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000334 RID: 820
	[Flags]
	public enum TrustAttributeFlag
	{
		// Token: 0x04001727 RID: 5927
		None = 0,
		// Token: 0x04001728 RID: 5928
		NonTransitive = 1,
		// Token: 0x04001729 RID: 5929
		UpLevelOnly = 2,
		// Token: 0x0400172A RID: 5930
		QuarantinedDomain = 4,
		// Token: 0x0400172B RID: 5931
		ForestTransitive = 8,
		// Token: 0x0400172C RID: 5932
		CrossOrganization = 16,
		// Token: 0x0400172D RID: 5933
		WithinForest = 32
	}
}
