using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F3 RID: 755
	internal class SlidingWindowResultCounter
	{
		// Token: 0x06001638 RID: 5688 RVA: 0x0006785E File Offset: 0x00065A5E
		public SlidingWindowResultCounter(int slidingWindowSize)
		{
			if (slidingWindowSize <= 0)
			{
				throw new ArgumentException("Sliding window size must be greater than zero.");
			}
			this.queue = new Queue<bool>(slidingWindowSize);
			this.slidingWindowSize = slidingWindowSize;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00067888 File Offset: 0x00065A88
		public void Clear()
		{
			lock (SlidingWindowResultCounter.lockObject)
			{
				this.queue.Clear();
				this.numberOfFailures = 0;
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000678D4 File Offset: 0x00065AD4
		public void AddSuccess()
		{
			this.AddValue(true);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x000678DD File Offset: 0x00065ADD
		public void AddFailure()
		{
			this.AddValue(false);
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000678E8 File Offset: 0x00065AE8
		public double FailurePercentage
		{
			get
			{
				double result;
				lock (SlidingWindowResultCounter.lockObject)
				{
					if (this.queue.Count > 0)
					{
						result = (double)(this.numberOfFailures * 100) / (double)this.queue.Count;
					}
					else
					{
						result = 0.0;
					}
				}
				return result;
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00067954 File Offset: 0x00065B54
		private void AddValue(bool newValue)
		{
			lock (SlidingWindowResultCounter.lockObject)
			{
				if (this.queue.Count < this.slidingWindowSize)
				{
					this.queue.Enqueue(newValue);
					if (!newValue)
					{
						this.numberOfFailures++;
					}
				}
				else
				{
					bool flag2 = this.queue.Dequeue();
					this.queue.Enqueue(newValue);
					if (flag2 != newValue)
					{
						if (!newValue)
						{
							this.numberOfFailures++;
						}
						else
						{
							this.numberOfFailures--;
						}
					}
				}
			}
		}

		// Token: 0x04000E6C RID: 3692
		private Queue<bool> queue;

		// Token: 0x04000E6D RID: 3693
		private int slidingWindowSize;

		// Token: 0x04000E6E RID: 3694
		private int numberOfFailures;

		// Token: 0x04000E6F RID: 3695
		private static object lockObject = new object();
	}
}
