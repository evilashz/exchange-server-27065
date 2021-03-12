using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009AA RID: 2474
	internal class BudgetTypeSetting
	{
		// Token: 0x1700284B RID: 10315
		// (get) Token: 0x0600722F RID: 29231 RVA: 0x0017A318 File Offset: 0x00178518
		// (set) Token: 0x06007230 RID: 29232 RVA: 0x0017A320 File Offset: 0x00178520
		public TimeSpan MaxDelay { get; private set; }

		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x06007231 RID: 29233 RVA: 0x0017A329 File Offset: 0x00178529
		// (set) Token: 0x06007232 RID: 29234 RVA: 0x0017A331 File Offset: 0x00178531
		public int MaxMicroDelayMultiplier { get; private set; }

		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x06007233 RID: 29235 RVA: 0x0017A33A File Offset: 0x0017853A
		// (set) Token: 0x06007234 RID: 29236 RVA: 0x0017A342 File Offset: 0x00178542
		public int MaxDelayedThreads { get; private set; }

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x06007235 RID: 29237 RVA: 0x0017A34B File Offset: 0x0017854B
		// (set) Token: 0x06007236 RID: 29238 RVA: 0x0017A353 File Offset: 0x00178553
		public int MaxDelayedThreadPerProcessor { get; private set; }

		// Token: 0x06007237 RID: 29239 RVA: 0x0017A35C File Offset: 0x0017855C
		public BudgetTypeSetting(TimeSpan maxDelay, int maxMicroDelayMultiplier, int maxDelayedThreadsPerProcessor)
		{
			this.MaxDelay = maxDelay;
			this.MaxMicroDelayMultiplier = maxMicroDelayMultiplier;
			this.MaxDelayedThreadPerProcessor = maxDelayedThreadsPerProcessor;
			this.MaxDelayedThreads = maxDelayedThreadsPerProcessor * BudgetTypeSetting.ProcessorCount;
		}

		// Token: 0x040049EE RID: 18926
		private static readonly int ProcessorCount = Environment.ProcessorCount;

		// Token: 0x040049EF RID: 18927
		public static readonly BudgetTypeSetting OneMinuteSetting = new BudgetTypeSetting(TimeSpan.FromMinutes(1.0), 10, 50);
	}
}
