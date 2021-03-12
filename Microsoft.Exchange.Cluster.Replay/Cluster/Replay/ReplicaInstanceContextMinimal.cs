using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstanceContextMinimal
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00037515 File Offset: 0x00035715
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0003751D File Offset: 0x0003571D
		public bool Suspended { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00037526 File Offset: 0x00035726
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0003752E File Offset: 0x0003572E
		public CopyStatusEnum LastCopyStatus { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00037537 File Offset: 0x00035737
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x0003753F File Offset: 0x0003573F
		public FailureInfo FailureInfo { get; private set; }

		// Token: 0x06000C91 RID: 3217 RVA: 0x00037548 File Offset: 0x00035748
		public ReplicaInstanceContextMinimal(ReplicaInstanceContext previousContext)
		{
			this.Suspended = previousContext.Suspended;
			this.LastCopyStatus = previousContext.GetStatus();
			this.FailureInfo = new FailureInfo(previousContext.FailureInfo);
		}
	}
}
