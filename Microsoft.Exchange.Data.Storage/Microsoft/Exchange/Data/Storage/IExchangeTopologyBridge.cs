using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D4C RID: 3404
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IExchangeTopologyBridge
	{
		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x0600760B RID: 30219
		TimeSpan CacheTimerRefreshTimeout { get; }

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x0600760C RID: 30220
		TimeSpan CacheExpirationTimeout { get; }

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x0600760D RID: 30221
		TimeSpan GetServiceTopologyDefaultTimeout { get; }

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x0600760E RID: 30222
		TimeSpan NotificationDelayTimeout { get; }

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x0600760F RID: 30223
		TimeSpan MinExpirationTimeForCacheDueToCacheMiss { get; }

		// Token: 0x06007610 RID: 30224
		ExchangeTopology ReadExchangeTopology(DateTime timestamp, ExchangeTopologyScope topologyScope, bool forceRefresh);

		// Token: 0x06007611 RID: 30225
		IRegisteredExchangeTopologyNotification RegisterExchangeTopologyNotification(ADNotificationCallback callback, ExchangeTopologyScope scope);
	}
}
