using System;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000204 RID: 516
	internal class NonIndexableItemStatisticsInfo
	{
		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003C6A6 File Offset: 0x0003A8A6
		public NonIndexableItemStatisticsInfo(string mailbox, int itemCount, string errorMessage)
		{
			this.Mailbox = mailbox;
			this.ItemCount = itemCount;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003C6C3 File Offset: 0x0003A8C3
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0003C6CB File Offset: 0x0003A8CB
		public string Mailbox { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003C6D4 File Offset: 0x0003A8D4
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public int ItemCount { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003C6E5 File Offset: 0x0003A8E5
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0003C6ED File Offset: 0x0003A8ED
		public string ErrorMessage { get; set; }
	}
}
