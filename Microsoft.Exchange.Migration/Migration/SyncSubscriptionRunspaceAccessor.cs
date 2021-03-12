using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000145 RID: 325
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncSubscriptionRunspaceAccessor : RunspaceAccessorBase
	{
		// Token: 0x0600106D RID: 4205 RVA: 0x00045284 File Offset: 0x00043484
		public SyncSubscriptionRunspaceAccessor(IMigrationDataProvider dataProvider) : base(dataProvider)
		{
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004528D File Offset: 0x0004348D
		public override SubscriptionSnapshot CreateSubscription(MigrationJobItem jobItem)
		{
			throw new InvalidOperationException("still use rpc");
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00045299 File Offset: 0x00043499
		public override SubscriptionSnapshot TestCreateSubscription(MigrationJobItem jobItem)
		{
			throw new InvalidOperationException("still use rpc");
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000452A5 File Offset: 0x000434A5
		public override SnapshotStatus RetrieveSubscriptionStatus(ISubscriptionId subscriptionId)
		{
			throw new InvalidOperationException("still use rpc");
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x000452B4 File Offset: 0x000434B4
		public override SubscriptionSnapshot RetrieveSubscriptionSnapshot(ISubscriptionId subscriptionId)
		{
			MigrationUtil.ThrowOnNullArgument(subscriptionId, "subscriptionId");
			SyncSubscriptionId syncSubscriptionId = subscriptionId as SyncSubscriptionId;
			MigrationUtil.AssertOrThrow(syncSubscriptionId != null, "subscription id type not valid txsync:" + subscriptionId.GetType(), new object[0]);
			MigrationUtil.AssertOrThrow(syncSubscriptionId.Id != null, "subscription id set, but guid is missing:" + syncSubscriptionId, new object[0]);
			PSCommand pscommand = new PSCommand().AddCommand("Get-Subscription");
			pscommand.AddParameter("AggregationType", "Migration");
			pscommand.AddParameter("IncludeReport", true);
			pscommand.AddParameter("Identity", syncSubscriptionId.Id.ToString());
			pscommand.AddParameter("Mailbox", syncSubscriptionId.MailboxData.GetIdParameter<MailboxIdParameter>());
			PimSubscriptionProxy pimSubscriptionProxy = base.RunCommand<PimSubscriptionProxy>(pscommand, null, null);
			if (pimSubscriptionProxy == null)
			{
				MigrationLogger.Log(MigrationEventType.Warning, "subscription id stored {0} but no subscription found", new object[]
				{
					syncSubscriptionId
				});
				return null;
			}
			SnapshotStatus status = SyncSubscriptionRunspaceAccessor.SubscriptionStatusFromSubscription(pimSubscriptionProxy);
			SubscriptionSnapshot subscriptionSnapshot = new SubscriptionSnapshot(syncSubscriptionId, status, pimSubscriptionProxy.SyncPhase != SyncPhase.Initial, (ExDateTime)pimSubscriptionProxy.CreationTime, new ExDateTime?((ExDateTime)pimSubscriptionProxy.LastModifiedTime), (ExDateTime?)pimSubscriptionProxy.LastSyncTime, new LocalizedString?(Strings.DetailedAggregationStatus(pimSubscriptionProxy.DetailedStatus)), null);
			subscriptionSnapshot.SetStatistics(pimSubscriptionProxy.TotalItemsSynced, pimSubscriptionProxy.TotalItemsSkipped, pimSubscriptionProxy.TotalItemsInSourceMailbox);
			ByteQuantifiedSize value;
			if (!string.IsNullOrEmpty(pimSubscriptionProxy.TotalSizeOfSourceMailbox) && ByteQuantifiedSize.TryParse(pimSubscriptionProxy.TotalSizeOfSourceMailbox, out value))
			{
				subscriptionSnapshot.EstimatedTotalTransferSize = new ByteQuantifiedSize?(value);
			}
			if (pimSubscriptionProxy.TotalItemsInSourceMailbox != null)
			{
				subscriptionSnapshot.EstimatedTotalTransferCount = new ulong?((ulong)pimSubscriptionProxy.TotalItemsInSourceMailbox.Value);
			}
			subscriptionSnapshot.Report = pimSubscriptionProxy.Report;
			return subscriptionSnapshot;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004547F File Offset: 0x0004367F
		public override bool UpdateSubscription(ISubscriptionId subscriptionId, MigrationEndpointBase endpoint, MigrationJobItem jobItem, bool adoptingSubscription)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00045486 File Offset: 0x00043686
		public override bool ResumeSubscription(ISubscriptionId subscriptionId, bool finalize = false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0004548D File Offset: 0x0004368D
		public override bool SuspendSubscription(ISubscriptionId subscriptionId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00045494 File Offset: 0x00043694
		public override bool RemoveSubscription(ISubscriptionId subscriptionId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0004549B File Offset: 0x0004369B
		protected override T HandleException<T>(string commandString, Exception ex, ICollection<Type> transientExceptions)
		{
			MigrationUtil.ThrowOnNullArgument(ex, "ex");
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, ex.Message), ex);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000454BC File Offset: 0x000436BC
		private static SnapshotStatus SubscriptionStatusFromSubscription(PimSubscriptionProxy subscription)
		{
			switch (subscription.Status)
			{
			case AggregationStatus.Succeeded:
			case AggregationStatus.InProgress:
			case AggregationStatus.Delayed:
				return SnapshotStatus.InProgress;
			case AggregationStatus.Disabled:
				if (subscription.SyncPhase != SyncPhase.Initial)
				{
					return SnapshotStatus.Suspended;
				}
				return SnapshotStatus.Failed;
			case AggregationStatus.Poisonous:
			case AggregationStatus.InvalidVersion:
				return SnapshotStatus.Corrupted;
			default:
				MigrationLogger.Log(MigrationEventType.Error, "Unknown status for subscription: {0}", new object[]
				{
					subscription.Status
				});
				return SnapshotStatus.Corrupted;
			}
		}

		// Token: 0x040005BC RID: 1468
		private const string GetSubscriptionCmdletName = "Get-Subscription";

		// Token: 0x040005BD RID: 1469
		private const string AggregationTypeParameter = "AggregationType";

		// Token: 0x040005BE RID: 1470
		private const string AggregationTypeValue = "Migration";

		// Token: 0x040005BF RID: 1471
		private const string IdentityParameter = "Identity";

		// Token: 0x040005C0 RID: 1472
		private const string MailboxParameter = "Mailbox";

		// Token: 0x040005C1 RID: 1473
		private const string IncludeReportParameter = "IncludeReport";
	}
}
