using System;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200001C RID: 28
	internal class SmallCounterMap
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0000B6AC File Offset: 0x000098AC
		public SmallCounterMap()
		{
			this.keys = new long[20];
			this.counts = new int[20];
			this.counterNumbers = 0;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000B6D5 File Offset: 0x000098D5
		public int CounterNumbers
		{
			get
			{
				return this.counterNumbers;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public int NumberOfMajorSource(int majorityThresholdPercent)
		{
			int num = this.CounterNumbers;
			if (num == 0)
			{
				return 0;
			}
			int[] array = new int[num];
			array[0] = this.CounterValue(0);
			int num2 = array[0];
			for (int i = 1; i < num; i++)
			{
				int num3 = this.CounterValue(i);
				num2 += num3;
				int num4 = i;
				while (num4 > 0 && array[num4 - 1] < num3)
				{
					array[num4] = array[num4 - 1];
					num4--;
				}
				array[num4] = num3;
			}
			int num5 = num2 * majorityThresholdPercent / 100;
			int num6 = 0;
			int num7 = 0;
			for (int j = 0; j < num; j++)
			{
				num7 += array[j];
				if (num7 > num5)
				{
					return num6 + 1;
				}
				num6++;
			}
			return num6;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000B790 File Offset: 0x00009990
		public int CounterValue(long key)
		{
			int num = this.CounterNumbers;
			for (int i = 0; i < num; i++)
			{
				if (key == this.keys[i])
				{
					return this.CounterValue(i);
				}
			}
			return 0;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public void Increment(long key)
		{
			for (int i = 0; i < 100; i++)
			{
				if (this.InternalIncrement(key))
				{
					return;
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B7E8 File Offset: 0x000099E8
		private bool NotInUse(int i)
		{
			return this.CounterValue(i) == 0;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B7F4 File Offset: 0x000099F4
		private int CounterValue(int i)
		{
			if (i < 20)
			{
				return this.counts[i];
			}
			return 0;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B808 File Offset: 0x00009A08
		private bool InternalIncrement(long key)
		{
			int num = 0;
			while (num < 20 && !this.NotInUse(num))
			{
				if (key == this.keys[num])
				{
					Interlocked.Increment(ref this.counts[num]);
					return true;
				}
				num++;
			}
			if (num >= 20)
			{
				return true;
			}
			if (this.NotInUse(num))
			{
				long num2 = Interlocked.CompareExchange(ref this.keys[num], key, 0L);
				if (num2 == 0L)
				{
					Interlocked.Increment(ref this.counterNumbers);
					Interlocked.Increment(ref this.counts[num]);
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000B7 RID: 183
		public const int MaxCounters = 20;

		// Token: 0x040000B8 RID: 184
		private readonly long[] keys;

		// Token: 0x040000B9 RID: 185
		private readonly int[] counts;

		// Token: 0x040000BA RID: 186
		private int counterNumbers;
	}
}
