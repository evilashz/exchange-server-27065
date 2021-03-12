using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200000B RID: 11
	internal class PerfCounterGroup
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000286C File Offset: 0x00000A6C
		public PerfCounterGroup(ExPerformanceCounter current, ExPerformanceCounter peak)
		{
			if (current == null)
			{
				throw new ArgumentNullException("current");
			}
			if (peak == null)
			{
				throw new ArgumentNullException("peak");
			}
			this.current = current;
			this.peak = peak;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000289E File Offset: 0x00000A9E
		public PerfCounterGroup(ExPerformanceCounter current, ExPerformanceCounter peak, ExPerformanceCounter total) : this(current, peak)
		{
			if (total == null)
			{
				throw new ArgumentNullException("total");
			}
			this.total = total;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028C0 File Offset: 0x00000AC0
		public void Increment()
		{
			lock (this)
			{
				if (this.total != null)
				{
					this.total.Increment();
				}
				long val = this.current.Increment();
				this.peak.RawValue = Math.Max(this.peak.RawValue, val);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002934 File Offset: 0x00000B34
		public void Decrement()
		{
			lock (this)
			{
				this.current.Decrement();
			}
		}

		// Token: 0x0400002E RID: 46
		private ExPerformanceCounter current;

		// Token: 0x0400002F RID: 47
		private ExPerformanceCounter peak;

		// Token: 0x04000030 RID: 48
		private ExPerformanceCounter total;
	}
}
