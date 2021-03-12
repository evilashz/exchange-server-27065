using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000301 RID: 769
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplaySystemDatabaseQueuedItem : ReplaySystemQueuedItem
	{
		// Token: 0x06001F3E RID: 7998 RVA: 0x0008DBE5 File Offset: 0x0008BDE5
		protected ReplaySystemDatabaseQueuedItem(ReplicaInstanceManager riManager, Guid dbGuid) : base(riManager)
		{
			this.DbGuid = dbGuid;
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0008DBF5 File Offset: 0x0008BDF5
		// (set) Token: 0x06001F40 RID: 8000 RVA: 0x0008DBFD File Offset: 0x0008BDFD
		protected Guid DbGuid { get; set; }

		// Token: 0x06001F41 RID: 8001 RVA: 0x0008DC08 File Offset: 0x0008BE08
		protected override Exception GetOperationCancelledException()
		{
			return new ReplayDatabaseOperationCancelledException(base.GetType().Name, this.DbGuid.ToString());
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0008DC3C File Offset: 0x0008BE3C
		protected override Exception GetOperationTimedoutException(TimeSpan timeout)
		{
			return new ReplayDatabaseOperationTimedoutException(base.GetType().Name, this.DbGuid.ToString(), timeout);
		}
	}
}
