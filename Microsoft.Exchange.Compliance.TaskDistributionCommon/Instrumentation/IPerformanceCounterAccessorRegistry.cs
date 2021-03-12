using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x02000022 RID: 34
	internal interface IPerformanceCounterAccessorRegistry
	{
		// Token: 0x0600007F RID: 127
		IPerformanceCounterAccessor GetOrAddPerformanceCounterAccessor(string type);
	}
}
