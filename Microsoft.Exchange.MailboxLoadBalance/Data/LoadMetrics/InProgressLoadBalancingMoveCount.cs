using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class InProgressLoadBalancingMoveCount : LoadMetric
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00009577 File Offset: 0x00007777
		private InProgressLoadBalancingMoveCount() : base("InProgressLoadBalancingMoveCount", false)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009585 File Offset: 0x00007785
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return mailbox.IsBeingLoadBalanced ? 1L : 0L;
		}

		// Token: 0x040000CF RID: 207
		public static readonly InProgressLoadBalancingMoveCount Instance = new InProgressLoadBalancingMoveCount();
	}
}
