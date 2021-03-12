using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000229 RID: 553
	internal interface IMonitoringADConfigProvider
	{
		// Token: 0x06001514 RID: 5396
		IMonitoringADConfig GetConfig(bool waitForInit = true);

		// Token: 0x06001515 RID: 5397
		IMonitoringADConfig GetRecentConfig(bool waitForInit = true);

		// Token: 0x06001516 RID: 5398
		IMonitoringADConfig GetConfigIgnoringStaleness(bool waitForInit = true);
	}
}
