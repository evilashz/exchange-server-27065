using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000018 RID: 24
	internal class AssistantEndWorkCycleCheckpointStatistics
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000043D3 File Offset: 0x000025D3
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000043DB File Offset: 0x000025DB
		public string DatabaseName { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000043E4 File Offset: 0x000025E4
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000043EC File Offset: 0x000025EC
		public DateTime StartTime { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000043F5 File Offset: 0x000025F5
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000043FD File Offset: 0x000025FD
		public DateTime EndTime { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004406 File Offset: 0x00002606
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000440E File Offset: 0x0000260E
		public int TotalMailboxCount { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004417 File Offset: 0x00002617
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000441F File Offset: 0x0000261F
		public int ProcessedMailboxCount { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004428 File Offset: 0x00002628
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004430 File Offset: 0x00002630
		public int MailboxErrorCount { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004439 File Offset: 0x00002639
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004441 File Offset: 0x00002641
		public int FailedToOpenStoreSessionCount { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000444A File Offset: 0x0000264A
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004452 File Offset: 0x00002652
		public int RetriedMailboxCount { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004463 File Offset: 0x00002663
		public int MailboxesProcessedSeparatelyCount { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000446C File Offset: 0x0000266C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004474 File Offset: 0x00002674
		public int MailboxRemainingCount { get; set; }

		// Token: 0x06000088 RID: 136 RVA: 0x00004480 File Offset: 0x00002680
		public List<KeyValuePair<string, object>> FormatCustomData()
		{
			return new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("DatabaseName", string.Format("{0}", this.DatabaseName)),
				new KeyValuePair<string, object>("StartTime", string.Format("{0}", this.StartTime)),
				new KeyValuePair<string, object>("EndTime", string.Format("{0}", this.EndTime)),
				new KeyValuePair<string, object>("TotalMailboxCount", string.Format("{0}", this.TotalMailboxCount)),
				new KeyValuePair<string, object>("ProcessedMailboxCount", string.Format("{0}", this.ProcessedMailboxCount)),
				new KeyValuePair<string, object>("MailboxErrorCount", string.Format("{0}", this.MailboxErrorCount)),
				new KeyValuePair<string, object>("FailedToOpenStoreSessionCount", string.Format("{0}", this.FailedToOpenStoreSessionCount)),
				new KeyValuePair<string, object>("RetriedMailboxCount", string.Format("{0}", this.RetriedMailboxCount)),
				new KeyValuePair<string, object>("MailboxesProcessedSeparatelyCount", string.Format("{0}", this.MailboxesProcessedSeparatelyCount)),
				new KeyValuePair<string, object>("MailboxRemainingCount", string.Format("{0}", this.MailboxRemainingCount))
			};
		}
	}
}
