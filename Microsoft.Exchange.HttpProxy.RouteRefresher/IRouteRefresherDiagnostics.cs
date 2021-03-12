using System;

namespace Microsoft.Exchange.HttpProxy.RouteRefresher
{
	// Token: 0x02000003 RID: 3
	internal interface IRouteRefresherDiagnostics
	{
		// Token: 0x06000003 RID: 3
		void AddErrorInfo(object value);

		// Token: 0x06000004 RID: 4
		void AddGenericInfo(object value);

		// Token: 0x06000005 RID: 5
		void IncrementSuccessfulMailboxServerCacheUpdates();

		// Token: 0x06000006 RID: 6
		void IncrementTotalMailboxServerCacheUpdateAttempts();

		// Token: 0x06000007 RID: 7
		void IncrementSuccessfulAnchorMailboxCacheUpdates();

		// Token: 0x06000008 RID: 8
		void IncrementTotalAnchorMailboxCacheUpdateAttempts();

		// Token: 0x06000009 RID: 9
		void LogRouteRefresherLatency(Action operationToTrack);
	}
}
