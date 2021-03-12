using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PhysicalSize : LoadMetric
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00009646 File Offset: 0x00007846
		private PhysicalSize() : base("PhysicalSize", true)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009654 File Offset: 0x00007854
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return this.GetUnitsForSize(mailbox.PhysicalSize);
		}

		// Token: 0x040000D3 RID: 211
		public static readonly PhysicalSize Instance = new PhysicalSize();
	}
}
