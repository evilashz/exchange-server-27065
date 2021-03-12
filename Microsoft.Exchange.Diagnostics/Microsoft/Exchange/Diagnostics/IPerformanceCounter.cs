using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000041 RID: 65
	public interface IPerformanceCounter : IDisposable
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016D RID: 365
		// (set) Token: 0x0600016E RID: 366
		string CategoryName { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		string CounterName { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000171 RID: 369
		string CounterHelp { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000172 RID: 370
		// (set) Token: 0x06000173 RID: 371
		string InstanceName { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000174 RID: 372
		// (set) Token: 0x06000175 RID: 373
		bool ReadOnly { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000176 RID: 374
		// (set) Token: 0x06000177 RID: 375
		PerformanceCounterInstanceLifetime InstanceLifetime { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376
		PerformanceCounterType CounterType { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		long RawValue { get; set; }

		// Token: 0x0600017B RID: 379
		void Close();

		// Token: 0x0600017C RID: 380
		long IncrementBy(long incrementValue);

		// Token: 0x0600017D RID: 381
		float NextValue();

		// Token: 0x0600017E RID: 382
		void RemoveInstance();
	}
}
