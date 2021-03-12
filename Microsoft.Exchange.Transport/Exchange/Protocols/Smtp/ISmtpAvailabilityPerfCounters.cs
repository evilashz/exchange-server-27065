using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D3 RID: 1235
	internal interface ISmtpAvailabilityPerfCounters
	{
		// Token: 0x06003918 RID: 14616
		void UpdatePerformanceCounters(LegitimateSmtpAvailabilityCategory category);

		// Token: 0x06003919 RID: 14617
		void IncrementMessageLoopsInLastHourCounter(long incrementValue);
	}
}
