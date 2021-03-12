using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200000B RID: 11
	public class ExPerformanceCounter
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000221C File Offset: 0x0000041C
		internal ExPerformanceCounter(string categoryName, string counterName, string instanceName, ExPerformanceCounter totalInstanceCounter, params ExPerformanceCounter[] autoUpdateCounters)
		{
			this.exPerfCounterImpl = new ExPerformanceCounter(categoryName, counterName, instanceName, (totalInstanceCounter == null) ? null : totalInstanceCounter.exPerfCounterImpl, (from c in autoUpdateCounters
			select c.exPerfCounterImpl).ToArray<ExPerformanceCounter>());
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002274 File Offset: 0x00000474
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002281 File Offset: 0x00000481
		public long RawValue
		{
			get
			{
				return this.exPerfCounterImpl.RawValue;
			}
			set
			{
				this.exPerfCounterImpl.RawValue = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000228F File Offset: 0x0000048F
		public long Decrement()
		{
			return this.exPerfCounterImpl.Decrement();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000229C File Offset: 0x0000049C
		public long Increment()
		{
			return this.exPerfCounterImpl.Increment();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A9 File Offset: 0x000004A9
		public long IncrementBy(long incrementValue)
		{
			return this.exPerfCounterImpl.IncrementBy(incrementValue);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B7 File Offset: 0x000004B7
		public void Close()
		{
			this.exPerfCounterImpl.Close();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C4 File Offset: 0x000004C4
		public virtual void Reset()
		{
			this.exPerfCounterImpl.Reset();
		}

		// Token: 0x04000013 RID: 19
		private readonly ExPerformanceCounter exPerfCounterImpl;
	}
}
