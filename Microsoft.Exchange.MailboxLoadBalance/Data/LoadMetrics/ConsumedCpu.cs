using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class ConsumedCpu : LoadMetric
	{
		// Token: 0x060002CC RID: 716 RVA: 0x000094D3 File Offset: 0x000076D3
		static ConsumedCpu()
		{
			ConsumedCpu.Instance = new ConsumedCpu();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000094E9 File Offset: 0x000076E9
		private ConsumedCpu() : base("CPU", false)
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000094F7 File Offset: 0x000076F7
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			return mailbox.TotalCpu;
		}

		// Token: 0x040000CC RID: 204
		public static readonly ConsumedCpu Instance = new ConsumedCpu();
	}
}
