using System;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x020001C9 RID: 457
	internal interface IsMemberOfResolverConfig
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001293 RID: 4755
		bool Enabled { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001294 RID: 4756
		long ResolvedGroupsMaxSize { get; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001295 RID: 4757
		TimeSpan ResolvedGroupsExpirationInterval { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001296 RID: 4758
		TimeSpan ResolvedGroupsCleanupInterval { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001297 RID: 4759
		TimeSpan ResolvedGroupsPurgeInterval { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001298 RID: 4760
		TimeSpan ResolvedGroupsRefreshInterval { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001299 RID: 4761
		long ExpandedGroupsMaxSize { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600129A RID: 4762
		TimeSpan ExpandedGroupsExpirationInterval { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600129B RID: 4763
		TimeSpan ExpandedGroupsCleanupInterval { get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600129C RID: 4764
		TimeSpan ExpandedGroupsPurgeInterval { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600129D RID: 4765
		TimeSpan ExpandedGroupsRefreshInterval { get; }
	}
}
