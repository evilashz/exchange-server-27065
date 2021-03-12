using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x0200082F RID: 2095
	[Serializable]
	public class QueueAggregationInfo
	{
		// Token: 0x06002C69 RID: 11369 RVA: 0x00064D05 File Offset: 0x00062F05
		public QueueAggregationInfo()
		{
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x00064D18 File Offset: 0x00062F18
		public QueueAggregationInfo(List<LocalQueueInfo> queueInfo, DateTime time)
		{
			this.queueInfo = queueInfo;
			this.Time = time;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x00064D39 File Offset: 0x00062F39
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x00064D41 File Offset: 0x00062F41
		public List<LocalQueueInfo> QueueInfo
		{
			get
			{
				return this.queueInfo;
			}
			set
			{
				this.queueInfo = value;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x00064D4A File Offset: 0x00062F4A
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x00064D52 File Offset: 0x00062F52
		public DateTime Time { get; set; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x00064D5B File Offset: 0x00062F5B
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x00064D63 File Offset: 0x00062F63
		public int TotalMessageCount { get; set; }

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x00064D6C File Offset: 0x00062F6C
		// (set) Token: 0x06002C72 RID: 11378 RVA: 0x00064D74 File Offset: 0x00062F74
		public int PoisonMessageCount { get; set; }

		// Token: 0x040026D8 RID: 9944
		private List<LocalQueueInfo> queueInfo = new List<LocalQueueInfo>();
	}
}
