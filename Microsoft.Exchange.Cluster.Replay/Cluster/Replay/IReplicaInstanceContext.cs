using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000112 RID: 274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReplicaInstanceContext
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A7A RID: 2682
		bool Initializing { get; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A7B RID: 2683
		bool Resynchronizing { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A7C RID: 2684
		bool Running { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A7D RID: 2685
		bool Seeding { get; }
	}
}
