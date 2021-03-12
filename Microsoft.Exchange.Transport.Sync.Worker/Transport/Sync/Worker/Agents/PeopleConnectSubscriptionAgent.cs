using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PeopleConnectSubscriptionAgent : SubscriptionAgent
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008F69 File Offset: 0x00007169
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00008F71 File Offset: 0x00007171
		private IConnectSubscriptionCleanup SubscriptionCleanup { get; set; }

		// Token: 0x060001F1 RID: 497 RVA: 0x00008F7A File Offset: 0x0000717A
		public PeopleConnectSubscriptionAgent() : this(null)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008F83 File Offset: 0x00007183
		internal PeopleConnectSubscriptionAgent(IConnectSubscriptionCleanup cleanup) : base("People Connect Agent")
		{
			this.SubscriptionCleanup = cleanup;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008F97 File Offset: 0x00007197
		public override bool IsEventInteresting(AggregationType aggregationType, SubscriptionEvents events)
		{
			return aggregationType == AggregationType.PeopleConnection && events == SubscriptionEvents.WorkItemCompleted;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008FA4 File Offset: 0x000071A4
		public override void OnWorkItemCompleted(SubscriptionEventSource source, SubscriptionWorkItemCompletedEventArgs e)
		{
			e.SyncLogSession.LogDebugging((TSLID)1493UL, PeopleConnectSubscriptionAgent.Tracer, "{0}: OnWorkItemCompleted event received for subscription {1}.", new object[]
			{
				base.Name,
				e.SubscriptionMessageId
			});
			AggregationStatus status = e.Subscription.Status;
			if (status != AggregationStatus.Succeeded)
			{
				if (status != AggregationStatus.Disabled)
				{
					return;
				}
				if (!PeopleConnectSubscriptionAgent.VerifyAndLogMailboxSession(e.MailboxSession, e.SyncLogSession, e.SubscriptionMessageId))
				{
					return;
				}
				if (WellKnownNetworkNames.LinkedIn.Equals(e.Subscription.Name, StringComparison.OrdinalIgnoreCase) && e.Subscription.DetailedAggregationStatus == DetailedAggregationStatus.AuthenticationError)
				{
					if (this.SubscriptionCleanup == null)
					{
						this.SubscriptionCleanup = new ConnectSubscriptionCleanup(SubscriptionManager.Instance);
					}
					try
					{
						this.SubscriptionCleanup.Cleanup(e.MailboxSession, (IConnectSubscription)e.Subscription, true);
						return;
					}
					catch (LocalizedException ex)
					{
						e.SyncLogSession.LogError((TSLID)178UL, PeopleConnectSubscriptionAgent.Tracer, "Encountered exception during cleanup: {0}", new object[]
						{
							ex
						});
						return;
					}
				}
				new PeopleConnectNotifier(e.MailboxSession).NotifyNewTokenNeeded(e.Subscription.Name);
			}
			else
			{
				if (!PeopleConnectSubscriptionAgent.VerifyAndLogMailboxSession(e.MailboxSession, e.SyncLogSession, e.SubscriptionMessageId))
				{
					return;
				}
				if (e.Subscription.IsInitialSyncDone && !e.Subscription.WasInitialSyncDone)
				{
					new PeopleConnectNotifier(e.MailboxSession).NotifyInitialSyncCompleted(e.Subscription.Name);
					return;
				}
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00009128 File Offset: 0x00007328
		private static bool VerifyAndLogMailboxSession(MailboxSession mailboxSession, SyncLogSession syncLogSession, StoreObjectId subscriptionMessageId)
		{
			if (mailboxSession == null)
			{
				syncLogSession.LogError((TSLID)1371UL, PeopleConnectSubscriptionAgent.Tracer, "Mailbox is null {0}", new object[]
				{
					subscriptionMessageId
				});
				return false;
			}
			if (!mailboxSession.IsConnected)
			{
				syncLogSession.LogError((TSLID)1494UL, PeopleConnectSubscriptionAgent.Tracer, "Mailbox is not connected {0}", new object[]
				{
					subscriptionMessageId
				});
				return false;
			}
			return true;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009193 File Offset: 0x00007393
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PeopleConnectSubscriptionAgent>(this);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000919B File Offset: 0x0000739B
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x04000112 RID: 274
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionAgentManagerTracer;
	}
}
