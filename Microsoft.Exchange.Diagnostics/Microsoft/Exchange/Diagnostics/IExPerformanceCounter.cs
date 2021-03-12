using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000039 RID: 57
	internal interface IExPerformanceCounter
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600013E RID: 318
		// (set) Token: 0x0600013F RID: 319
		long RawValue { get; set; }

		// Token: 0x06000140 RID: 320
		long Increment();

		// Token: 0x06000141 RID: 321
		long Decrement();

		// Token: 0x06000142 RID: 322
		void Reset();

		// Token: 0x06000143 RID: 323
		long IncrementBy(long incrementValue);
	}
}
