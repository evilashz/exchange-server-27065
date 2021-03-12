using System;
using System.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E9 RID: 233
	public class AverageSlidingSequence : SlidingSequence<long>
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0001B4C2 File Offset: 0x000196C2
		public AverageSlidingSequence(TimeSpan slidingWindowLength, TimeSpan bucketLength) : base(slidingWindowLength, bucketLength, () => DateTime.UtcNow)
		{
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001B4F4 File Offset: 0x000196F4
		public long CalculateAverage()
		{
			long result;
			lock (this.syncObject)
			{
				double num = 0.0;
				foreach (object obj2 in this)
				{
					long num2 = (long)obj2;
					num += (double)num2;
				}
				result = ((this.Count<long>() != 0) ? ((long)Math.Round(num / (double)this.Count<long>())) : 0L);
			}
			return result;
		}

		// Token: 0x0400047C RID: 1148
		private object syncObject = new object();
	}
}
