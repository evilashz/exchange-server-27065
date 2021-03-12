using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010E RID: 270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISetPassiveSeeding : IGetStatus
	{
		// Token: 0x06000A6C RID: 2668
		void BeginPassiveSeeding(PassiveSeedingSourceContextEnum PassiveSeedingSourceContext, bool invokedForRestart);

		// Token: 0x06000A6D RID: 2669
		void EndPassiveSeeding();

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A6E RID: 2670
		PassiveSeedingSourceContextEnum PassiveSeedingSourceContext { get; }
	}
}
