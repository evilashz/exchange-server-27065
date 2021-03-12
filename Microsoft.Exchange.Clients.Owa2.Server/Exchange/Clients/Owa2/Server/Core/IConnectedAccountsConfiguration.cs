using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000CE RID: 206
	internal interface IConnectedAccountsConfiguration
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000827 RID: 2087
		bool NotificationsEnabled { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000828 RID: 2088
		bool LogonTriggeredSyncNowEnabled { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000829 RID: 2089
		bool RefreshButtonTriggeredSyncNowEnabled { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600082A RID: 2090
		TimeSpan RefreshButtonTriggeredSyncNowSuppressThreshold { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600082B RID: 2091
		bool PeriodicSyncNowEnabled { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600082C RID: 2092
		TimeSpan PeriodicSyncNowInterval { get; }
	}
}
