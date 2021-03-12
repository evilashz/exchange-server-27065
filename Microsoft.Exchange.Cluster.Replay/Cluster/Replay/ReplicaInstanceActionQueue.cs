using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200030D RID: 781
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceActionQueue : PrioritizedQueue<ReplicaInstanceQueuedItem>
	{
	}
}
