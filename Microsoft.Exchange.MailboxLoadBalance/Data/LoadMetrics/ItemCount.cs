using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x02000051 RID: 81
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemCount : LoadMetric
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x000095A0 File Offset: 0x000077A0
		private ItemCount() : base("ItemCount", false)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000095AE File Offset: 0x000077AE
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return mailbox.ItemCount;
		}

		// Token: 0x040000D0 RID: 208
		public static readonly ItemCount Instance = new ItemCount();
	}
}
