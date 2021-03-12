using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x02000018 RID: 24
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionAgentManager
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x0000882F File Offset: 0x00006A2F
		private SubscriptionAgentManager()
		{
			this.agents = new List<SubscriptionAgent>(1);
			this.agentFactories = new List<SubscriptionAgentFactory>(1);
			this.subscriptionEventSource = new SubscriptionEventSource();
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000885C File Offset: 0x00006A5C
		public static SubscriptionAgentManager Instance
		{
			get
			{
				if (SubscriptionAgentManager.instance == null)
				{
					lock (SubscriptionAgentManager.syncRoot)
					{
						if (SubscriptionAgentManager.instance == null)
						{
							SubscriptionAgentManager.instance = new SubscriptionAgentManager();
						}
					}
				}
				return SubscriptionAgentManager.instance;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000088B4 File Offset: 0x00006AB4
		public void Start(SyncLogSession syncLogSession, ICollection<string> disabledSubscriptionAgents)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			lock (SubscriptionAgentManager.syncRoot)
			{
				if (!SubscriptionAgentManager.started)
				{
					foreach (SubscriptionAgentFactory subscriptionAgentFactory in this.agentFactories)
					{
						SubscriptionAgent subscriptionAgent = subscriptionAgentFactory.CreateAgent();
						if (disabledSubscriptionAgents != null && disabledSubscriptionAgents.Contains(subscriptionAgent.Name))
						{
							syncLogSession.LogInformation((TSLID)528UL, SubscriptionAgentManager.Tracer, "Subscription Agent is disabled and will not be executed: {0}.", new object[]
							{
								subscriptionAgent.Name
							});
							subscriptionAgent.Dispose();
						}
						else
						{
							this.agents.Add(subscriptionAgent);
						}
					}
					SubscriptionAgentManager.started = true;
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000089A4 File Offset: 0x00006BA4
		public void Shutdown()
		{
			lock (SubscriptionAgentManager.syncRoot)
			{
				if (SubscriptionAgentManager.started)
				{
					foreach (SubscriptionAgentFactory subscriptionAgentFactory in this.agentFactories)
					{
						subscriptionAgentFactory.Close();
					}
					this.agentFactories.Clear();
					foreach (SubscriptionAgent subscriptionAgent in this.agents)
					{
						subscriptionAgent.Dispose();
					}
					this.agents.Clear();
					SubscriptionAgentManager.started = false;
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008A8C File Offset: 0x00006C8C
		public SubscriptionWorkItemCompletedEventResult ProcessOnWorkItemCompletedEvent(SyncLogSession syncLogSession, object poisonContext, Guid subscriptionId, AggregationSubscription subscription, bool isSyncNow, Exception workItemResultException, StoreObjectId subscriptionMessageId, Guid mailboxGuid, string userLegacyDn, Guid tenantGuid, OrganizationId organizationId, MailboxSession mailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("workItem", syncLogSession);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDn", userLegacyDn);
			SubscriptionWorkItemCompletedEventArgs eventArgs = new SubscriptionWorkItemCompletedEventArgs(syncLogSession, subscriptionId, subscription, isSyncNow, workItemResultException, subscriptionMessageId, mailboxGuid, userLegacyDn, tenantGuid, organizationId, mailboxSession);
			return this.ProcessEvent<SubscriptionWorkItemCompletedEventResult>(subscription.AggregationType, subscription.SubscriptionEvents, poisonContext, SubscriptionEvents.WorkItemCompleted, eventArgs);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008B10 File Offset: 0x00006D10
		public SubscriptionWorkItemFailedLoadSubscriptionEventResult ProcessOnWorkItemFailedLoadSubscriptionEvent(SyncLogSession syncLogSession, object poisonContext, Guid subscriptionId, AggregationType aggregationType, Exception workItemResultException, StoreObjectId subscriptionMessageId, Guid mailboxGuid, string userLegacyDn, Guid tenantGuid, OrganizationId organizationId)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionId", subscriptionId);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDn", userLegacyDn);
			SubscriptionWorkItemFailedLoadSubscriptionEventArgs eventArgs = new SubscriptionWorkItemFailedLoadSubscriptionEventArgs(syncLogSession, subscriptionId, workItemResultException, subscriptionMessageId, mailboxGuid, userLegacyDn, tenantGuid, organizationId);
			return this.ProcessEvent<SubscriptionWorkItemFailedLoadSubscriptionEventResult>(aggregationType, SubscriptionEvents.WorkItemFailedLoadSubscription, poisonContext, SubscriptionEvents.WorkItemFailedLoadSubscription, eventArgs);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008B78 File Offset: 0x00006D78
		internal void RegisterAgentFactory(SubscriptionAgentFactory agentFactory)
		{
			lock (SubscriptionAgentManager.syncRoot)
			{
				this.agentFactories.Add(agentFactory);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008BC0 File Offset: 0x00006DC0
		private R ProcessEvent<R>(AggregationType aggregationType, SubscriptionEvents acceptableSubscriptionEvents, object poisonContext, SubscriptionEvents subscriptionEvent, SubscriptionEventArgs<R> eventArgs) where R : SubscriptionEventResult
		{
			SyncUtilities.ThrowIfArgumentNull("eventArgs", eventArgs);
			SyncPoisonHandler.SetSyncPoisonContextOnCurrentThread(poisonContext);
			try
			{
				eventArgs.SyncLogSession.LogDebugging((TSLID)529UL, SubscriptionAgentManager.Tracer, "Considering event: {0}", new object[]
				{
					subscriptionEvent
				});
				foreach (SubscriptionAgent subscriptionAgent in this.agents)
				{
					if ((acceptableSubscriptionEvents & subscriptionEvent) == SubscriptionEvents.None)
					{
						eventArgs.SyncLogSession.LogDebugging((TSLID)530UL, SubscriptionAgentManager.Tracer, "Skipping event as the acceptable events do not include it: {0}.", new object[]
						{
							acceptableSubscriptionEvents
						});
					}
					else if (!subscriptionAgent.IsEventInteresting(aggregationType, subscriptionEvent))
					{
						eventArgs.SyncLogSession.LogDebugging((TSLID)531UL, SubscriptionAgentManager.Tracer, "Skipping event as agent {0} is not interested in it.", new object[]
						{
							subscriptionAgent.Name
						});
					}
					else
					{
						eventArgs.SyncLogSession.LogDebugging((TSLID)532UL, SubscriptionAgentManager.Tracer, "Invoking agent {0} for event.", new object[]
						{
							subscriptionAgent.Name
						});
						subscriptionAgent.Invoke(subscriptionEvent, this.subscriptionEventSource, eventArgs);
					}
				}
			}
			finally
			{
				SyncPoisonHandler.ClearSyncPoisonContextFromCurrentThread();
			}
			return eventArgs.Result;
		}

		// Token: 0x040000F9 RID: 249
		private const int AgentsCapacityEstimate = 1;

		// Token: 0x040000FA RID: 250
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionAgentManagerTracer;

		// Token: 0x040000FB RID: 251
		private static SubscriptionAgentManager instance;

		// Token: 0x040000FC RID: 252
		private static object syncRoot = new object();

		// Token: 0x040000FD RID: 253
		private static bool started;

		// Token: 0x040000FE RID: 254
		private List<SubscriptionAgent> agents;

		// Token: 0x040000FF RID: 255
		private List<SubscriptionAgentFactory> agentFactories;

		// Token: 0x04000100 RID: 256
		private SubscriptionEventSource subscriptionEventSource;
	}
}
