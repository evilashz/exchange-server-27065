using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class ConsumerMailboxCount : LoadMetric
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000950B File Offset: 0x0000770B
		private ConsumerMailboxCount() : base("ConsumerMailboxCount", false)
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00009519 File Offset: 0x00007719
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return (mailbox.MailboxType == DirectoryMailboxType.Consumer) ? 1L : 0L;
		}

		// Token: 0x040000CD RID: 205
		public static readonly ConsumerMailboxCount Instance = new ConsumerMailboxCount();
	}
}
