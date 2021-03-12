using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x0200004F RID: 79
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConsumerMailboxSize : LoadMetric
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00009535 File Offset: 0x00007735
		private ConsumerMailboxSize() : base("ConsumerMailboxSize", true)
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009544 File Offset: 0x00007744
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			if (mailbox.MailboxType != DirectoryMailboxType.Consumer)
			{
				return 0L;
			}
			return (long)mailbox.PhysicalSize.ToBytes();
		}

		// Token: 0x040000CE RID: 206
		public static readonly ConsumerMailboxSize Instance = new ConsumerMailboxSize();
	}
}
