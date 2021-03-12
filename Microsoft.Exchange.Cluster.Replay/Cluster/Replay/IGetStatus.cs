using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200010C RID: 268
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IGetStatus
	{
		// Token: 0x06000A67 RID: 2663
		CopyStatusEnum GetStatus();
	}
}
