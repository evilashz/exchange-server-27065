using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200009C RID: 156
	[Flags]
	internal enum AmBcsSkipFlags
	{
		// Token: 0x0400029D RID: 669
		None = 0,
		// Token: 0x0400029E RID: 670
		LegacySkipAllChecks = 1,
		// Token: 0x0400029F RID: 671
		SkipClientExperienceChecks = 2,
		// Token: 0x040002A0 RID: 672
		SkipHealthChecks = 4,
		// Token: 0x040002A1 RID: 673
		SkipLagChecks = 8,
		// Token: 0x040002A2 RID: 674
		SkipMaximumActiveDatabasesChecks = 16,
		// Token: 0x040002A3 RID: 675
		SkipActiveCopyChecks = 32,
		// Token: 0x040002A4 RID: 676
		SkipAll = 62
	}
}
