using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000206 RID: 518
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISubscriptionInformationLoader
	{
		// Token: 0x06001176 RID: 4470
		bool TryLoadStateStorage(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, out IStateStorage stateStorage, out ISyncException exception);

		// Token: 0x06001177 RID: 4471
		bool TryReloadStateStorage(AggregationWorkItem workItem, IStateStorage stateStorage, out ISyncException exception);

		// Token: 0x06001178 RID: 4472
		bool TryLoadSubscription(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, out ISyncWorkerData subscription, out ISyncException exception, out bool invalidState);

		// Token: 0x06001179 RID: 4473
		bool TryLoadMailboxSession(AggregationWorkItem workItem, SyncMailboxSession syncMailboxSession, out OrganizationId organizationId, out bool invalidState, out ISyncException exception);

		// Token: 0x0600117A RID: 4474
		bool TrySaveSubscription(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete, out Exception exception);

		// Token: 0x0600117B RID: 4475
		bool TryDeleteSubscription(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, EventHandler<RoundtripCompleteEventArgs> roundtripComplete);

		// Token: 0x0600117C RID: 4476
		bool IsMailboxOverQuota(SyncMailboxSession syncMailboxSession, SyncLogSession syncLogSession, ulong requiredFreeBytes);

		// Token: 0x0600117D RID: 4477
		bool TrySendSubscriptionNotificationEmail(SyncMailboxSession syncMailboxSession, ISyncWorkerData subscription, SyncLogSession syncLogSession, out bool retry);
	}
}
