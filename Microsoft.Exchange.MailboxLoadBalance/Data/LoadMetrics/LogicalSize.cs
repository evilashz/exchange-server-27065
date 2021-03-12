using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class LogicalSize : LoadMetric
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000961E File Offset: 0x0000781E
		private LogicalSize() : base("LogicalSize", true)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000962C File Offset: 0x0000782C
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return this.GetUnitsForSize(mailbox.LogicalSize);
		}

		// Token: 0x040000D2 RID: 210
		public static readonly LogicalSize Instance = new LogicalSize();
	}
}
