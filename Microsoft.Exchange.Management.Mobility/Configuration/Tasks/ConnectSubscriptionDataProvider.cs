using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConnectSubscriptionDataProvider : AggregationSubscriptionDataProvider
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		public ConnectSubscriptionDataProvider(AggregationTaskType taskType, IRecipientSession session, ADUser adUser) : base(taskType, session, adUser)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public override void Delete(IConfigurable instance)
		{
			ConnectSubscriptionProxy connectSubscriptionProxy = instance as ConnectSubscriptionProxy;
			ConnectSubscription connectSubscription = (ConnectSubscription)connectSubscriptionProxy.Subscription;
			AggregationSubscriptionIdentity subscriptionIdentity = connectSubscription.SubscriptionIdentity;
			try
			{
				using (MailboxSession mailboxSession = base.OpenMailboxSession(subscriptionIdentity))
				{
					ConnectSubscriptionCleanup connectSubscriptionCleanup = new ConnectSubscriptionCleanup(SubscriptionManager.Instance);
					connectSubscriptionCleanup.Cleanup(mailboxSession, connectSubscription, true);
					new PeopleConnectNotifier(mailboxSession).NotifyDisconnected(connectSubscription.Name);
				}
			}
			catch (LocalizedException ex)
			{
				CommonLoggingHelper.SyncLogSession.LogError((TSLID)1507UL, AggregationTaskUtils.Tracer, "ConnectSubscriptionDataProvider.Delete: {0} hit exception: {1}.", new object[]
				{
					connectSubscription.Name,
					ex
				});
				Exception ex2 = ex;
				if (!(ex is FailedDeletePeopleConnectSubscriptionException))
				{
					ex2 = new FailedDeletePeopleConnectSubscriptionException(connectSubscription.SubscriptionType, ex);
				}
				throw ex2;
			}
		}
	}
}
