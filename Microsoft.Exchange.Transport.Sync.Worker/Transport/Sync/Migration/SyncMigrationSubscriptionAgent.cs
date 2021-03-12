using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;
using Microsoft.Exchange.Transport.Sync.Worker;
using Microsoft.Exchange.Transport.Sync.Worker.Agents;

namespace Microsoft.Exchange.Transport.Sync.Migration
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SyncMigrationSubscriptionAgent : SubscriptionAgent
	{
		// Token: 0x060001FB RID: 507 RVA: 0x000091B8 File Offset: 0x000073B8
		public SyncMigrationSubscriptionAgent() : base("Migration Agent")
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000091C8 File Offset: 0x000073C8
		public override bool IsEventInteresting(AggregationType aggregationType, SubscriptionEvents events)
		{
			if (aggregationType != AggregationType.Migration)
			{
				return false;
			}
			switch (events)
			{
			case SubscriptionEvents.WorkItemCompleted:
			case SubscriptionEvents.WorkItemFailedLoadSubscription:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000091F4 File Offset: 0x000073F4
		public override void OnWorkItemCompleted(SubscriptionEventSource source, SubscriptionWorkItemCompletedEventArgs e)
		{
			e.SyncLogSession.LogDebugging((TSLID)533UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: OnWorkItemCompleted event received for subscription {1}.", new object[]
			{
				base.Name,
				e.SubscriptionMessageId
			});
			if (!e.Subscription.IsMigration)
			{
				e.SyncLogSession.LogDebugging((TSLID)534UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: Ignoring OnWorkItemCompleted event for subscription {1} since it is not a migration subscription.", new object[]
				{
					base.Name,
					e.SubscriptionMessageId
				});
				return;
			}
			bool flag = !e.Subscription.IsInitialSyncDone;
			SyncPhase syncPhase = e.Subscription.SyncPhase;
			bool flag2 = e.Subscription.IsInitialSyncDone && !e.Subscription.WasInitialSyncDone;
			switch (e.Subscription.Status)
			{
			case AggregationStatus.Succeeded:
				if (flag2)
				{
					this.FireEventAndProcessResult(e, null);
					return;
				}
				if (SyncPhase.Finalization == e.Subscription.SyncPhase)
				{
					this.FireEvent(e, null);
					e.Result.SetSyncPhaseCompleted();
					e.Result.SetDisableSubscription(DetailedAggregationStatus.Finalized);
					return;
				}
				break;
			case AggregationStatus.InProgress:
				if (flag)
				{
					if (this.IsAuthenticationError(e) && e.Subscription.LastSuccessfulSyncTime == null)
					{
						e.SyncLogSession.LogDebugging((TSLID)535UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: Firing event for subscription {1} since an authentication error is detected.", new object[]
						{
							base.Name,
							e.SubscriptionMessageId
						});
						MigrationStatusOverride statusOverrides = new MigrationStatusOverride(DetailedAggregationStatus.AuthenticationError, MigrationSubscriptionStatus.None);
						this.FireEventAndProcessResult(e, statusOverrides);
						return;
					}
					if (this.IsIMAPPathPrefixError(e))
					{
						e.SyncLogSession.LogDebugging((TSLID)536UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: Firing event for subscription {1} since a path prefix error is detected.", new object[]
						{
							base.Name,
							e.SubscriptionMessageId
						});
						MigrationStatusOverride statusOverrides2 = new MigrationStatusOverride(DetailedAggregationStatus.CommunicationError, MigrationSubscriptionStatus.InvalidPathPrefix);
						this.FireEventAndProcessResult(e, statusOverrides2);
						return;
					}
				}
				break;
			case AggregationStatus.Delayed:
			case AggregationStatus.Disabled:
				this.FireEventAndProcessResult(e, null);
				return;
			case AggregationStatus.Poisonous:
			case AggregationStatus.InvalidVersion:
				this.FireEvent(e, null);
				break;
			default:
				return;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009410 File Offset: 0x00007610
		public override void OnWorkItemFailedLoadSubscription(SubscriptionEventSource source, SubscriptionWorkItemFailedLoadSubscriptionEventArgs e)
		{
			e.SyncLogSession.LogDebugging((TSLID)537UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: OnWorkItemFailedLoadSubscription event received for subscription {1}.", new object[]
			{
				base.Name,
				e.SubscriptionMessageId
			});
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009457 File Offset: 0x00007657
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009459 File Offset: 0x00007659
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncMigrationSubscriptionAgent>(this);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009464 File Offset: 0x00007664
		protected virtual void FireEventAndProcessResult(SubscriptionWorkItemCompletedEventArgs e, MigrationStatusOverride statusOverrides)
		{
			UpdateMigrationRequestResult updateMigrationRequestResult = this.FireEvent(e, statusOverrides);
			if (updateMigrationRequestResult != null)
			{
				this.ProcessResult(e, updateMigrationRequestResult, statusOverrides);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009488 File Offset: 0x00007688
		protected virtual void ProcessResult(SubscriptionWorkItemCompletedEventArgs e, UpdateMigrationRequestResult result, MigrationStatusOverride statusOverrides)
		{
			SyncUtilities.ThrowIfArgumentNull("e", e);
			SyncUtilities.ThrowIfArgumentNull("result", result);
			if (result.IsActionRequired())
			{
				if (result.IsDisableRequested())
				{
					DetailedAggregationStatus disableSubscription = (statusOverrides != null) ? statusOverrides.DetailedAggregationStatus : e.Subscription.DetailedAggregationStatus;
					e.Result.SetDisableSubscription(disableSubscription);
				}
				if (result.IsDeleteRequested())
				{
					e.Result.SetDeleteSubscription();
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000094F4 File Offset: 0x000076F4
		protected virtual UpdateMigrationRequestResult FireEvent(SubscriptionWorkItemCompletedEventArgs e, MigrationStatusOverride statusOverrides)
		{
			ISyncWorkerData subscription = e.Subscription;
			OrganizationId organizationId = e.OrganizationId;
			if (organizationId == null || organizationId == OrganizationId.ForestWideOrgId)
			{
				e.SyncLogSession.LogError((TSLID)538UL, SyncMigrationSubscriptionAgent.Tracer, "{0} Will not fire event because not enough information is available to resolve tenantOrgId or tenantOrgId is ForestWideOrgId. TenantOrgId: [{1}]", new object[]
				{
					base.Name,
					organizationId
				});
				return null;
			}
			UpdateMigrationRequestResult result = null;
			try
			{
				TenantPartitionHint tenantPartitionHint = TenantPartitionHint.FromOrganizationId(organizationId);
				string text;
				string text2;
				MigrationHelperBase.GetManagementMailboxData(tenantPartitionHint, out text, out text2);
				DetailedAggregationStatus detailedAggregationStatus;
				MigrationSubscriptionStatus migrationSubscriptionStatus;
				if (statusOverrides != null)
				{
					detailedAggregationStatus = statusOverrides.DetailedAggregationStatus;
					migrationSubscriptionStatus = statusOverrides.MigrationSubscriptionStatus;
				}
				else
				{
					detailedAggregationStatus = subscription.DetailedAggregationStatus;
					migrationSubscriptionStatus = MigrationSubscriptionStatus.None;
				}
				UpdateMigrationRequestArgs args = new UpdateMigrationRequestArgs(subscription.UserExchangeMailboxSmtpAddress, subscription.UserLegacyDN, text, organizationId.OrganizationalUnit, e.SubscriptionMessageId, subscription.Status, detailedAggregationStatus, migrationSubscriptionStatus, subscription.IsInitialSyncDone, subscription.LastSyncTime, subscription.LastSuccessfulSyncTime, new long?(subscription.ItemsSynced), new long?(subscription.ItemsSkipped), subscription.LastSyncNowRequestTime);
				e.SyncLogSession.LogVerbose((TSLID)539UL, SyncMigrationSubscriptionAgent.Tracer, "{0} Invoking updatemigration request on notification listener on {1} for migration mailbox: {2} in organization {3}", new object[]
				{
					base.Name,
					text2,
					text,
					organizationId.OrganizationalUnit
				});
				MigrationNotificationRpcStub migrationNotificationRpcStub = new MigrationNotificationRpcStub(text2);
				result = migrationNotificationRpcStub.UpdateMigrationRequest(args);
			}
			catch (LocalizedException ex)
			{
				e.SyncLogSession.LogError((TSLID)540UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: Exception during resolving of MigrationMailboxData to update MigrationJobItem corresponding to {1}. Exception is {2}", new object[]
				{
					base.Name,
					e.UserLegacyDn,
					ex.StackTrace
				});
			}
			catch (RpcException ex2)
			{
				e.SyncLogSession.LogError((TSLID)541UL, SyncMigrationSubscriptionAgent.Tracer, "{0}: Exception during rpc call to update MigrationJobItem corresponding to {1}. Exception is {2}", new object[]
				{
					base.Name,
					e.UserLegacyDn,
					ex2.StackTrace
				});
			}
			return result;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009708 File Offset: 0x00007908
		private bool IsAuthenticationError(SubscriptionWorkItemCompletedEventArgs e)
		{
			Exception workItemResultException = e.WorkItemResultException;
			if (workItemResultException == null)
			{
				return false;
			}
			SyncTransientException ex = workItemResultException as SyncTransientException;
			if (ex != null)
			{
				return ex.DetailedAggregationStatus == DetailedAggregationStatus.AuthenticationError;
			}
			SyncPermanentException ex2 = workItemResultException as SyncPermanentException;
			return ex2 != null && ex2.DetailedAggregationStatus == DetailedAggregationStatus.AuthenticationError;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000974C File Offset: 0x0000794C
		private bool IsIMAPPathPrefixError(SubscriptionWorkItemCompletedEventArgs e)
		{
			SyncTransientException ex = e.WorkItemResultException as SyncTransientException;
			return ex != null && ex.InnerException is IMAPInvalidPathPrefixException;
		}

		// Token: 0x04000114 RID: 276
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionAgentManagerTracer;
	}
}
