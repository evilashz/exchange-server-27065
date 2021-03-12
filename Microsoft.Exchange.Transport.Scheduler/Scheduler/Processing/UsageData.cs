using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000031 RID: 49
	internal class UsageData
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00005664 File Offset: 0x00003864
		public UsageData(int outstandingJobs, long memoryUsed, long processingTicks)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("oustrandingJobs", outstandingJobs, 0, int.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<long>("memoryUsed", memoryUsed, 0L, long.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<long>("processingTicks", processingTicks, 0L, long.MaxValue);
			this.outstandingJobs = outstandingJobs;
			this.memoryUsed = memoryUsed;
			this.processingTicks = processingTicks;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000056C9 File Offset: 0x000038C9
		public int OutstandingJobs
		{
			get
			{
				return this.outstandingJobs;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000056D1 File Offset: 0x000038D1
		public long MemoryUsed
		{
			get
			{
				return this.memoryUsed;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000056D9 File Offset: 0x000038D9
		public long ProcessingTicks
		{
			get
			{
				return this.processingTicks;
			}
		}

		// Token: 0x040000A8 RID: 168
		public static readonly UsageData EmptyUsage = new UsageData(0, 0L, 0L);

		// Token: 0x040000A9 RID: 169
		private readonly int outstandingJobs;

		// Token: 0x040000AA RID: 170
		private readonly long memoryUsed;

		// Token: 0x040000AB RID: 171
		private readonly long processingTicks;
	}
}
