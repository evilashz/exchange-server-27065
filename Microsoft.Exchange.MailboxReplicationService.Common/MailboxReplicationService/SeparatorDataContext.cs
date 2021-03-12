using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000104 RID: 260
	internal class SeparatorDataContext : DataContext
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x000129F2 File Offset: 0x00010BF2
		private SeparatorDataContext()
		{
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000129FA File Offset: 0x00010BFA
		public override string ToString()
		{
			return "--------";
		}

		// Token: 0x0400056D RID: 1389
		public static readonly SeparatorDataContext Separator = new SeparatorDataContext();
	}
}
