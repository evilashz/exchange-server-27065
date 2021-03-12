using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000C8 RID: 200
	internal class MovingAverage
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x0001A00D File Offset: 0x0001820D
		internal MovingAverage(int size)
		{
			this.values = new Queue<long>(size);
			this.size = size;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001A028 File Offset: 0x00018228
		internal long Value
		{
			get
			{
				long result;
				lock (this)
				{
					result = ((this.values.Count > 0) ? (this.sum / (long)this.values.Count) : 0L);
				}
				return result;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001A084 File Offset: 0x00018284
		internal long Update(long dataPoint)
		{
			long value;
			lock (this)
			{
				this.sum += dataPoint;
				this.values.Enqueue(dataPoint);
				while (this.values.Count > this.size)
				{
					this.sum -= this.values.Dequeue();
				}
				value = this.Value;
			}
			return value;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001A108 File Offset: 0x00018308
		internal long Update(double dataPoint)
		{
			return this.Update((long)dataPoint);
		}

		// Token: 0x040003E6 RID: 998
		private Queue<long> values;

		// Token: 0x040003E7 RID: 999
		private long sum;

		// Token: 0x040003E8 RID: 1000
		private int size;
	}
}
