using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000E0 RID: 224
	internal class PercentageBooleanSlidingCounter
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001D01E File Offset: 0x0001B21E
		private PercentageBooleanSlidingCounter(int maxSize, TimeSpan entryMaxTime, bool invert)
		{
			this.maxSize = maxSize;
			this.invert = invert;
			this.entryMaxTime = entryMaxTime;
			this.queue = new Queue<PercentageBooleanSlidingCounter.SlidingData>(this.maxSize);
			this.syncLock = new object();
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001D057 File Offset: 0x0001B257
		internal static PercentageBooleanSlidingCounter CreateSuccessCounter(int maxSize, TimeSpan entryMaxTime)
		{
			return new PercentageBooleanSlidingCounter(maxSize, entryMaxTime, false);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001D061 File Offset: 0x0001B261
		internal static PercentageBooleanSlidingCounter CreateFailureCounter(int maxSize, TimeSpan entryMaxTime)
		{
			return new PercentageBooleanSlidingCounter(maxSize, entryMaxTime, true);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001D06C File Offset: 0x0001B26C
		internal int Update(bool operationSucceeded)
		{
			int percentage;
			lock (this.syncLock)
			{
				this.PurgeOldEntries();
				this.AddEntry(operationSucceeded);
				percentage = this.GetPercentage();
			}
			return percentage;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		private void AddEntry(bool operationSucceeded)
		{
			this.queue.Enqueue(new PercentageBooleanSlidingCounter.SlidingData(operationSucceeded));
			if (operationSucceeded)
			{
				this.successCounter++;
				return;
			}
			this.failureCounter++;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		private void PurgeOldEntries()
		{
			while (this.queue.Count >= this.maxSize || (this.queue.Count > 0 && ExDateTime.UtcNow.Subtract(this.queue.Peek().CreationTime) >= this.entryMaxTime))
			{
				PercentageBooleanSlidingCounter.SlidingData slidingData = this.queue.Dequeue();
				if (slidingData.Success)
				{
					this.successCounter--;
				}
				else
				{
					this.failureCounter--;
				}
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001D17C File Offset: 0x0001B37C
		private int GetPercentage()
		{
			int num = this.failureCounter + this.successCounter;
			if (this.invert)
			{
				return this.failureCounter * 100 / num;
			}
			return this.successCounter * 100 / num;
		}

		// Token: 0x04000436 RID: 1078
		private Queue<PercentageBooleanSlidingCounter.SlidingData> queue;

		// Token: 0x04000437 RID: 1079
		private int maxSize;

		// Token: 0x04000438 RID: 1080
		private TimeSpan entryMaxTime;

		// Token: 0x04000439 RID: 1081
		private object syncLock;

		// Token: 0x0400043A RID: 1082
		private bool invert;

		// Token: 0x0400043B RID: 1083
		private int successCounter;

		// Token: 0x0400043C RID: 1084
		private int failureCounter;

		// Token: 0x020000E1 RID: 225
		private class SlidingData
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x0001D1B6 File Offset: 0x0001B3B6
			internal SlidingData(bool success)
			{
				this.CreationTime = ExDateTime.UtcNow;
				this.Success = success;
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
			// (set) Token: 0x06000765 RID: 1893 RVA: 0x0001D1D8 File Offset: 0x0001B3D8
			internal ExDateTime CreationTime { get; private set; }

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001D1E1 File Offset: 0x0001B3E1
			// (set) Token: 0x06000767 RID: 1895 RVA: 0x0001D1E9 File Offset: 0x0001B3E9
			internal bool Success { get; private set; }
		}
	}
}
