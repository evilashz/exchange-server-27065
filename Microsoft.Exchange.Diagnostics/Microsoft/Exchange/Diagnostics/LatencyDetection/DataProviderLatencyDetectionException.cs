using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000168 RID: 360
	internal class DataProviderLatencyDetectionException : LatencyDetectionException
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x0002694B File Offset: 0x00024B4B
		internal DataProviderLatencyDetectionException(LatencyDetectionContext trigger, IPerformanceDataProvider provider) : base(trigger, provider)
		{
		}
	}
}
