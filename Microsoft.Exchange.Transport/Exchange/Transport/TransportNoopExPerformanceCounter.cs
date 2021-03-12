using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001AA RID: 426
	internal class TransportNoopExPerformanceCounter : IExPerformanceCounter
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004D891 File Offset: 0x0004BA91
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004D895 File Offset: 0x0004BA95
		public long RawValue
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0004D897 File Offset: 0x0004BA97
		public long Increment()
		{
			return 0L;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004D89B File Offset: 0x0004BA9B
		public long Decrement()
		{
			return 0L;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004D89F File Offset: 0x0004BA9F
		public void Reset()
		{
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004D8A1 File Offset: 0x0004BAA1
		public long IncrementBy(long incrementValue)
		{
			return 0L;
		}

		// Token: 0x04000A09 RID: 2569
		public static readonly TransportNoopExPerformanceCounter Instance = new TransportNoopExPerformanceCounter();
	}
}
