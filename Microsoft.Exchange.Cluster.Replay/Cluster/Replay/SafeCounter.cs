using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002EF RID: 751
	internal class SafeCounter
	{
		// Token: 0x06001E22 RID: 7714 RVA: 0x00089F7C File Offset: 0x0008817C
		public SafeCounter(ExPerformanceCounter wrappedCounter)
		{
			this.m_perfCounter = wrappedCounter;
			this.m_perfCounter.RawValue = 0L;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00089F98 File Offset: 0x00088198
		public void Reset()
		{
			Thread.VolatileWrite(ref this.m_value, 0L);
			this.m_perfCounter.RawValue = 0L;
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001E24 RID: 7716 RVA: 0x00089FB4 File Offset: 0x000881B4
		// (set) Token: 0x06001E25 RID: 7717 RVA: 0x00089FBC File Offset: 0x000881BC
		public long Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				long num = Interlocked.Exchange(ref this.m_value, value);
				long incrementValue = value - num;
				this.m_perfCounter.IncrementBy(incrementValue);
			}
		}

		// Token: 0x04000CBC RID: 3260
		private long m_value;

		// Token: 0x04000CBD RID: 3261
		private ExPerformanceCounter m_perfCounter;
	}
}
