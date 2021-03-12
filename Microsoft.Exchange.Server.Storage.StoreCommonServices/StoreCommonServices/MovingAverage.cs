using System;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000D6 RID: 214
	public class MovingAverage
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000294E0 File Offset: 0x000276E0
		public int Average
		{
			get
			{
				int num = this.currentIndex;
				if (this.windowIsFull || num > this.windowValues.Length)
				{
					return this.windowSum >> 10;
				}
				if (num != 0)
				{
					return this.windowSum / num;
				}
				return 0;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00029520 File Offset: 0x00027720
		public void AddValue(int value)
		{
			int num = Interlocked.Increment(ref this.currentIndex) - 1 & 1023;
			int num2 = Interlocked.Exchange(ref this.windowValues[num], value);
			Interlocked.Add(ref this.windowSum, value - num2);
			if (num >= this.windowValues.Length - 1)
			{
				this.windowIsFull = true;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00029578 File Offset: 0x00027778
		public void Reset()
		{
			for (int i = 0; i < this.windowValues.Length; i++)
			{
				this.windowValues[i] = 0;
			}
			this.currentIndex = 0;
			this.windowSum = 0;
			this.windowIsFull = false;
		}

		// Token: 0x040004E8 RID: 1256
		private const int BitsFactor = 10;

		// Token: 0x040004E9 RID: 1257
		private readonly int[] windowValues = new int[1024];

		// Token: 0x040004EA RID: 1258
		private int currentIndex;

		// Token: 0x040004EB RID: 1259
		private int windowSum;

		// Token: 0x040004EC RID: 1260
		private bool windowIsFull;
	}
}
