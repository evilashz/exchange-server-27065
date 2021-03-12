using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Reporting
{
	// Token: 0x020002B1 RID: 689
	internal interface ITenantThrottlingSession
	{
		// Token: 0x060018D2 RID: 6354
		void SetThrottleState(TenantThrottleInfo throttleInfo);

		// Token: 0x060018D3 RID: 6355
		TenantThrottleInfo GetThrottleState(Guid tenantId);

		// Token: 0x060018D4 RID: 6356
		void SaveTenantThrottleInfo(List<TenantThrottleInfo> throttleInfoList, int partitionId = 0);

		// Token: 0x060018D5 RID: 6357
		List<TenantThrottleInfo> GetTenantThrottlingDigest(int partitionId = 0, Guid? tenantId = null, bool overriddenOnly = false, int tenantCount = 5000, bool throttledOnly = true);

		// Token: 0x060018D6 RID: 6358
		int GetPhysicalPartitionCount();

		// Token: 0x060018D7 RID: 6359
		TransportProcessingQuotaConfig GetTransportThrottlingConfig();

		// Token: 0x060018D8 RID: 6360
		void SetTransportThrottlingConfig(TransportProcessingQuotaConfig config);
	}
}
