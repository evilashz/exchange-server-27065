using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010F RID: 271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetActiveSeeding : IGetStatus
	{
		// Token: 0x06000A6F RID: 2671
		void BeginActiveSeeding(SeedType seedType);

		// Token: 0x06000A70 RID: 2672
		void EndActiveSeeding();

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A71 RID: 2673
		bool ActiveSeedingSource { get; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A72 RID: 2674
		SeedType SeedType { get; }
	}
}
