using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200001B RID: 27
	internal class FixedSizeQueueBool
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000751F File Offset: 0x0000571F
		public double Mean
		{
			get
			{
				if (this.queue.Count > 0)
				{
					return (double)this.trueCount / (double)this.queue.Count;
				}
				return 0.0;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000754D File Offset: 0x0000574D
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000755A File Offset: 0x0000575A
		public int TrueCount
		{
			get
			{
				return this.trueCount;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007562 File Offset: 0x00005762
		public FixedSizeQueueBool(int windowSize)
		{
			if (windowSize <= 0)
			{
				throw new ArgumentException("Input windowSize cannot be <= 0");
			}
			this.queue = new Queue<bool>(windowSize);
			this.windowSize = windowSize;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000758C File Offset: 0x0000578C
		public void Clear()
		{
			this.queue.Clear();
			this.trueCount = 0;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000075A0 File Offset: 0x000057A0
		public void AddSample(bool sample)
		{
			if (this.queue.Count == this.windowSize && this.queue.Dequeue())
			{
				this.trueCount--;
			}
			this.queue.Enqueue(sample);
			if (sample)
			{
				this.trueCount++;
			}
		}

		// Token: 0x0400024A RID: 586
		private readonly int windowSize;

		// Token: 0x0400024B RID: 587
		private Queue<bool> queue;

		// Token: 0x0400024C RID: 588
		private int trueCount;
	}
}
