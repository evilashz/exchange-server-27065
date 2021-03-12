using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000300 RID: 768
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplaySystemQueuedItem : ReplayQueuedItemBase
	{
		// Token: 0x06001F39 RID: 7993 RVA: 0x0008DBA0 File Offset: 0x0008BDA0
		protected ReplaySystemQueuedItem(ReplicaInstanceManager riManager)
		{
			this.ReplicaInstanceManager = riManager;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0008DBAF File Offset: 0x0008BDAF
		// (set) Token: 0x06001F3B RID: 7995 RVA: 0x0008DBB7 File Offset: 0x0008BDB7
		protected ReplicaInstanceManager ReplicaInstanceManager { get; set; }

		// Token: 0x06001F3C RID: 7996 RVA: 0x0008DBC0 File Offset: 0x0008BDC0
		protected override Exception GetOperationCancelledException()
		{
			return new ReplaySystemOperationCancelledException(base.GetType().Name);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0008DBD2 File Offset: 0x0008BDD2
		protected override Exception GetOperationTimedoutException(TimeSpan timeout)
		{
			return new ReplaySystemOperationTimedoutException(base.GetType().Name, timeout);
		}
	}
}
