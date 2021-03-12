using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Completion;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200003F RID: 63
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionCompletionData
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00016326 File Offset: 0x00014526
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0001632E File Offset: 0x0001452E
		internal SubscriptionCompletionStatus SubscriptionCompletionStatus { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00016337 File Offset: 0x00014537
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0001633F File Offset: 0x0001453F
		internal AggregationType AggregationType { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00016348 File Offset: 0x00014548
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00016350 File Offset: 0x00014550
		internal SyncPhase SyncPhase { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00016359 File Offset: 0x00014559
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00016361 File Offset: 0x00014561
		internal Guid DatabaseGuid { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0001636A File Offset: 0x0001456A
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00016372 File Offset: 0x00014572
		internal Guid MailboxGuid { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0001637B File Offset: 0x0001457B
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00016383 File Offset: 0x00014583
		internal Guid SubscriptionGuid { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0001638C File Offset: 0x0001458C
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00016394 File Offset: 0x00014594
		internal StoreObjectId SubscriptionMessageID { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0001639D File Offset: 0x0001459D
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000163A5 File Offset: 0x000145A5
		internal bool MoreDataToDownload { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000333 RID: 819 RVA: 0x000163AE File Offset: 0x000145AE
		// (set) Token: 0x06000334 RID: 820 RVA: 0x000163B6 File Offset: 0x000145B6
		internal SerializedSubscription SerializedSubscription { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000163BF File Offset: 0x000145BF
		// (set) Token: 0x06000336 RID: 822 RVA: 0x000163C7 File Offset: 0x000145C7
		internal string SyncWatermark { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000163D0 File Offset: 0x000145D0
		// (set) Token: 0x06000338 RID: 824 RVA: 0x000163D8 File Offset: 0x000145D8
		internal ExDateTime? LastSuccessfulDispatchTime { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000163E1 File Offset: 0x000145E1
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000163E9 File Offset: 0x000145E9
		internal WorkType? DispatchedWorkType { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000163F2 File Offset: 0x000145F2
		internal bool DisableSubscription
		{
			get
			{
				return SubscriptionCompletionStatus.DisableSubscription == this.SubscriptionCompletionStatus;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000163FD File Offset: 0x000145FD
		internal bool InvalidState
		{
			get
			{
				return SubscriptionCompletionStatus.InvalidState == this.SubscriptionCompletionStatus;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00016408 File Offset: 0x00014608
		internal bool SubscriptionDeleted
		{
			get
			{
				return SubscriptionCompletionStatus.DeleteSubscription == this.SubscriptionCompletionStatus;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00016413 File Offset: 0x00014613
		internal bool SyncFailed
		{
			get
			{
				return SubscriptionCompletionStatus.SyncError == this.SubscriptionCompletionStatus;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00016420 File Offset: 0x00014620
		internal bool TryGetCurrentWorkType(SyncLogSession syncLogSession, out WorkType? workType)
		{
			workType = null;
			bool result;
			try
			{
				workType = new WorkType?(WorkTypeManager.ClassifyWorkTypeFromSubscriptionInformation(this.AggregationType, this.SyncPhase));
				result = true;
			}
			catch (NotSupportedException ex)
			{
				syncLogSession.LogError((TSLID)1289UL, "TryGetCurrentWorkType: Invalid Aggregation type ({0}) or Sync Phase ({1}): {2}", new object[]
				{
					this.AggregationType,
					this.SyncPhase,
					ex
				});
				result = false;
			}
			return result;
		}
	}
}
