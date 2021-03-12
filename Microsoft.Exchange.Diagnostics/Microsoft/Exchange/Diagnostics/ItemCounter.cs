using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000134 RID: 308
	public class ItemCounter
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0002246C File Offset: 0x0002066C
		public ItemCounter(ExPerformanceCounter current, ExPerformanceCounter peak, ExPerformanceCounter total)
		{
			if (current == null)
			{
				throw new ArgumentNullException("current");
			}
			if (peak == null)
			{
				throw new ArgumentNullException("peak");
			}
			if (total == null)
			{
				throw new ArgumentNullException("total");
			}
			this.current = current;
			this.peak = peak;
			this.total = total;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000224C0 File Offset: 0x000206C0
		public void Increment()
		{
			this.total.Increment();
			lock (this.current)
			{
				long val = this.current.Increment();
				this.peak.RawValue = Math.Max(this.peak.RawValue, val);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00022530 File Offset: 0x00020730
		public void Decrement()
		{
			lock (this.current)
			{
				this.current.Decrement();
			}
		}

		// Token: 0x040005F3 RID: 1523
		private ExPerformanceCounter current;

		// Token: 0x040005F4 RID: 1524
		private ExPerformanceCounter peak;

		// Token: 0x040005F5 RID: 1525
		private ExPerformanceCounter total;
	}
}
