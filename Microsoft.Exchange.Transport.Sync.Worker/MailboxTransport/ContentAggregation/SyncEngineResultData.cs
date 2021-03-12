using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000213 RID: 531
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncEngineResultData
	{
		// Token: 0x060012B2 RID: 4786 RVA: 0x00040718 File Offset: 0x0003E918
		private SyncEngineResultData(ExDateTime startSyncTime, int cloudItemsSynced, bool cloudMoreItemsAvailable, bool disableSubscription, bool invalidState, string updatedSyncWatermark, ISyncWorkerData updatedSubscription, SyncPhase syncPhaseBeforeSync, bool deleteSubscription)
		{
			this.startSyncTime = startSyncTime;
			this.cloudItemsSynced = cloudItemsSynced;
			this.cloudMoreItemsAvailable = cloudMoreItemsAvailable;
			this.disableSubscription = disableSubscription;
			this.invalidState = invalidState;
			this.updatedSyncWatermark = updatedSyncWatermark;
			this.updatedSubscription = updatedSubscription;
			this.syncPhaseBeforeSync = syncPhaseBeforeSync;
			this.deleteSubscription = deleteSubscription;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00040770 File Offset: 0x0003E970
		internal SyncEngineResultData(ExDateTime startSyncTime, int cloudItemsSynced, bool cloudMoreItemsAvailable, bool disableSubscription, bool invalidState, string updatedSyncWatermark, ISyncWorkerData updatedSubscription, SyncPhase syncPhaseBeforeSync) : this(startSyncTime, cloudItemsSynced, cloudMoreItemsAvailable, disableSubscription, invalidState, updatedSyncWatermark, updatedSubscription, syncPhaseBeforeSync, false)
		{
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00040794 File Offset: 0x0003E994
		internal SyncEngineResultData(ExDateTime startSyncTime, bool deleteSubscription) : this(startSyncTime, 0, false, false, false, null, null, SyncPhase.Initial, deleteSubscription)
		{
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x000407B0 File Offset: 0x0003E9B0
		internal int CloudItemsSynced
		{
			get
			{
				return this.cloudItemsSynced;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000407B8 File Offset: 0x0003E9B8
		internal bool CloudMoreItemsAvailable
		{
			get
			{
				return this.cloudMoreItemsAvailable;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x000407C0 File Offset: 0x0003E9C0
		internal ExDateTime StartSyncTime
		{
			get
			{
				return this.startSyncTime;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000407C8 File Offset: 0x0003E9C8
		public bool DisableSubscription
		{
			get
			{
				return this.disableSubscription;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000407D0 File Offset: 0x0003E9D0
		public bool InvalidState
		{
			get
			{
				return this.invalidState;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000407D8 File Offset: 0x0003E9D8
		internal string UpdatedSyncWatermark
		{
			get
			{
				return this.updatedSyncWatermark;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x000407E0 File Offset: 0x0003E9E0
		internal ISyncWorkerData UpdatedSubscription
		{
			get
			{
				return this.updatedSubscription;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x000407E8 File Offset: 0x0003E9E8
		internal SyncPhase SyncPhaseBeforeSync
		{
			get
			{
				return this.syncPhaseBeforeSync;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x000407F0 File Offset: 0x0003E9F0
		internal bool DeleteSubscription
		{
			get
			{
				return this.deleteSubscription;
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000407F8 File Offset: 0x0003E9F8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "CloudItemsSynced:{0},CloudMoreItemsAvailable:{1},StartSyncTime:{2},disableSubscription:{3},invalidState:{4},updatedSyncWatermark:{5},updatedSubscription:{6},syncPhaseBeforeSync:{7},deleteSubscription:{8}.", new object[]
			{
				this.cloudItemsSynced,
				this.cloudMoreItemsAvailable,
				this.startSyncTime,
				this.disableSubscription,
				this.invalidState,
				this.updatedSyncWatermark ?? "<null>",
				this.updatedSubscription,
				this.syncPhaseBeforeSync,
				this.deleteSubscription
			});
		}

		// Token: 0x040009DD RID: 2525
		private readonly int cloudItemsSynced;

		// Token: 0x040009DE RID: 2526
		private readonly bool cloudMoreItemsAvailable;

		// Token: 0x040009DF RID: 2527
		private readonly ExDateTime startSyncTime;

		// Token: 0x040009E0 RID: 2528
		private readonly bool disableSubscription;

		// Token: 0x040009E1 RID: 2529
		private readonly bool invalidState;

		// Token: 0x040009E2 RID: 2530
		private readonly string updatedSyncWatermark;

		// Token: 0x040009E3 RID: 2531
		private readonly ISyncWorkerData updatedSubscription;

		// Token: 0x040009E4 RID: 2532
		private readonly SyncPhase syncPhaseBeforeSync;

		// Token: 0x040009E5 RID: 2533
		private readonly bool deleteSubscription;
	}
}
