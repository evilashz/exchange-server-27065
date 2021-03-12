using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DispatchWorkChecker
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x00017B4C File Offset: 0x00015D4C
		internal DispatchWorkChecker(SyncLogSession syncLogSession, IDispatchEntryManager dispatchEntryManager, ISyncManagerConfiguration configuration, IHealthLogDispatchEntryReporter healthLogDispatchEntryReporter)
		{
			this.syncLogSession = syncLogSession;
			this.dispatchEntryManager = dispatchEntryManager;
			this.healthLogDispatchEntryReporter = healthLogDispatchEntryReporter;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00017B6A File Offset: 0x00015D6A
		protected IHealthLogDispatchEntryReporter HealthLogDispatchEntryReporter
		{
			get
			{
				return this.healthLogDispatchEntryReporter;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00017B74 File Offset: 0x00015D74
		internal bool CanAttemptDispatchForWorkType(DispatchTrigger dispatchTrigger, WorkType workTypeAttempting, WorkType? completedWorkType)
		{
			if (completedWorkType != null)
			{
				WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(completedWorkType.Value);
				WorkTypeDefinition workTypeDefinition2 = WorkTypeManager.Instance.GetWorkTypeDefinition(workTypeAttempting);
				if (workTypeDefinition.IsLightLoad && !workTypeDefinition2.IsLightLoad)
				{
					this.syncLogSession.LogDebugging((TSLID)349UL, "DWC.CanAttemptDispatchForWorkType: Previous sync {0} was light load, new job {1} is heavy load, not doing to trigger dispatch", new object[]
					{
						completedWorkType.Value,
						workTypeAttempting
					});
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00017BF8 File Offset: 0x00015DF8
		internal bool CanAttemptDispatchForSubscription(DispatchEntry dispatchEntry, out DispatchResult? dispatchResult)
		{
			SyncUtilities.ThrowIfArgumentNull("dispatchEntry", dispatchEntry);
			dispatchResult = null;
			if (this.dispatchEntryManager.ContainsSubscription(dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid))
			{
				this.syncLogSession.LogDebugging((TSLID)351UL, "DWC.CanAttemptDispatchForSubscription: Subscription already being dispatched ID:{0}", new object[]
				{
					dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid
				});
				dispatchResult = new DispatchResult?(DispatchResult.SubscriptionAlreadyDispatched);
				return false;
			}
			if (!ContentAggregationConfig.OwaMailboxPolicyConstraintEnabled)
			{
				this.syncLogSession.LogDebugging((TSLID)115UL, "DWC.CanAttemptDispatchForSubscription: policy checks are disabled.", new object[0]);
			}
			else if (MailboxPolicyConstraint.Instance.WantsDispositionChangedToDeletion(dispatchEntry, this.syncLogSession))
			{
				this.syncLogSession.LogDebugging((TSLID)1111UL, "DWC.CanAttemptDispatchForSubscription: Subscription is targeted for deletion ID:{0}", new object[]
				{
					dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid
				});
				dispatchResult = new DispatchResult?(DispatchResult.PolicyInducedDeletion);
				return false;
			}
			return true;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00017CF7 File Offset: 0x00015EF7
		protected virtual ExDateTime GetCurrentTime()
		{
			return ExDateTime.UtcNow;
		}

		// Token: 0x04000217 RID: 535
		private readonly IDispatchEntryManager dispatchEntryManager;

		// Token: 0x04000218 RID: 536
		private readonly IHealthLogDispatchEntryReporter healthLogDispatchEntryReporter;

		// Token: 0x04000219 RID: 537
		private readonly SyncLogSession syncLogSession;
	}
}
