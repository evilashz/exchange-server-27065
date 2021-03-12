using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200030F RID: 783
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplicaInstanceAmQueuedItem : ReplicaInstanceQueuedItem
	{
		// Token: 0x06002055 RID: 8277 RVA: 0x0009698E File Offset: 0x00094B8E
		protected ReplicaInstanceAmQueuedItem(ReplicaInstance replicaInstance) : base(replicaInstance)
		{
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00096997 File Offset: 0x00094B97
		protected override Exception GetOperationCancelledException()
		{
			return new AmDbActionCancelledException(base.DbName, this.Name);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000969AA File Offset: 0x00094BAA
		protected override Exception GetOperationTimedoutException(TimeSpan timeout)
		{
			return new AmDbOperationTimedoutException(base.DbName, this.Name, timeout);
		}
	}
}
