using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200027D RID: 637
	internal class PerfCounterGroup
	{
		// Token: 0x060029D1 RID: 10705 RVA: 0x0008364F File Offset: 0x0008184F
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

		// Token: 0x060029D2 RID: 10706 RVA: 0x00083681 File Offset: 0x00081881
		public PerfCounterGroup(ExPerformanceCounter current, ExPerformanceCounter peak, ExPerformanceCounter total) : this(current, peak)
		{
			if (total == null)
			{
				throw new ArgumentNullException("total");
			}
			this.total = total;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000836A0 File Offset: 0x000818A0
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

		// Token: 0x060029D4 RID: 10708 RVA: 0x00083714 File Offset: 0x00081914
		public void Decrement()
		{
			lock (this)
			{
				this.current.Decrement();
			}
		}

		// Token: 0x040020F3 RID: 8435
		private ExPerformanceCounter current;

		// Token: 0x040020F4 RID: 8436
		private ExPerformanceCounter peak;

		// Token: 0x040020F5 RID: 8437
		private ExPerformanceCounter total;
	}
}
