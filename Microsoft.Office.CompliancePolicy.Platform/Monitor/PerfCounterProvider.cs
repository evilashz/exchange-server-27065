using System;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x0200007E RID: 126
	public abstract class PerfCounterProvider
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000B608 File Offset: 0x00009808
		public PerfCounterProvider(string categoryName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("categoryName", categoryName);
			this.CategoryName = categoryName;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B622 File Offset: 0x00009822
		public PerfCounterProvider()
		{
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000B62A File Offset: 0x0000982A
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000B632 File Offset: 0x00009832
		private protected string CategoryName { protected get; private set; }

		// Token: 0x06000342 RID: 834
		public abstract void Increment(string counterName);

		// Token: 0x06000343 RID: 835
		public abstract void Increment(string counterName, string baseCounterName);

		// Token: 0x06000344 RID: 836
		public abstract void IncrementBy(string counterName, long incrementValue);

		// Token: 0x06000345 RID: 837
		public abstract void IncrementBy(string counterName, long incrementValue, string baseCounterName);
	}
}
