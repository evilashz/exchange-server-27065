using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000679 RID: 1657
	internal interface ICachePerformanceCounters
	{
		// Token: 0x06001E13 RID: 7699
		void Accessed(AccessStatus accessStatus);

		// Token: 0x06001E14 RID: 7700
		void SizeUpdated(long cacheSize);
	}
}
