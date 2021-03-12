using System;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001C2 RID: 450
	internal interface IDatabaseHealthTracker
	{
		// Token: 0x06001194 RID: 4500
		void UpdateHealthInfo(HealthInfoPersisted healthInfo);

		// Token: 0x06001195 RID: 4501
		DateTime GetDagHealthInfoUpdateTimeUtc();

		// Token: 0x06001196 RID: 4502
		HealthInfoPersisted GetDagHealthInfo();
	}
}
