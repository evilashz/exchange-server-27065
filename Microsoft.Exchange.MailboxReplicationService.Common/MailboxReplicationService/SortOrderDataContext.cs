using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000FB RID: 251
	internal class SortOrderDataContext : DataContext
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x00012752 File Offset: 0x00010952
		public SortOrderDataContext(SortOrderData sortOrder)
		{
			this.sortOrder = sortOrder;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00012761 File Offset: 0x00010961
		public override string ToString()
		{
			return string.Format("SortOrder: {0}", (this.sortOrder != null) ? this.sortOrder.ToString() : "(null)");
		}

		// Token: 0x04000561 RID: 1377
		private SortOrderData sortOrder;
	}
}
