using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000029 RID: 41
	internal class QueueLogInfo
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004676 File Offset: 0x00002876
		public QueueLogInfo(string display, DateTime timeStamp)
		{
			ArgumentValidator.ThrowIfNull("display", display);
			this.display = display;
			this.timeStamp = timeStamp;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000046AD File Offset: 0x000028AD
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000046B5 File Offset: 0x000028B5
		public long Enqueues
		{
			get
			{
				return this.enqueues;
			}
			set
			{
				this.enqueues = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000046BE File Offset: 0x000028BE
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000046C6 File Offset: 0x000028C6
		public long Dequeues
		{
			get
			{
				return this.dequeues;
			}
			set
			{
				this.dequeues = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000046CF File Offset: 0x000028CF
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000046D7 File Offset: 0x000028D7
		public long Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000046E0 File Offset: 0x000028E0
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000046E8 File Offset: 0x000028E8
		public UsageData UsageData
		{
			get
			{
				return this.usageData;
			}
			set
			{
				this.usageData = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000046F1 File Offset: 0x000028F1
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000046F9 File Offset: 0x000028F9
		public string Display
		{
			get
			{
				return this.display;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004701 File Offset: 0x00002901
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004709 File Offset: 0x00002909
		public TimeSpan TotalLockTime
		{
			get
			{
				return this.totalLockTime;
			}
			set
			{
				this.totalLockTime = value;
			}
		}

		// Token: 0x04000071 RID: 113
		private readonly DateTime timeStamp;

		// Token: 0x04000072 RID: 114
		private readonly string display;

		// Token: 0x04000073 RID: 115
		private UsageData usageData = UsageData.EmptyUsage;

		// Token: 0x04000074 RID: 116
		private long enqueues;

		// Token: 0x04000075 RID: 117
		private long dequeues;

		// Token: 0x04000076 RID: 118
		private long count;

		// Token: 0x04000077 RID: 119
		private TimeSpan totalLockTime = TimeSpan.Zero;
	}
}
