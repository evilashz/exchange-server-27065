using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Worker.Agents
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionWorkItemCompletedEventResult : SubscriptionWorkItemEventResult
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00008EF1 File Offset: 0x000070F1
		public DetailedAggregationStatus DetailedAggregationStatus
		{
			get
			{
				return this.detailedAggregationStatus;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008EF9 File Offset: 0x000070F9
		public bool DisableSubscription
		{
			get
			{
				return this.disableSubscription;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008F01 File Offset: 0x00007101
		public bool KeepSubscriptionEnabled
		{
			get
			{
				return this.keepSubscriptionEnabled;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008F09 File Offset: 0x00007109
		public bool SyncPhaseCompleted
		{
			get
			{
				return this.syncPhaseCompleted;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008F11 File Offset: 0x00007111
		public void SetDisableSubscription(DetailedAggregationStatus detailedAggregationStatus)
		{
			this.disableSubscription = true;
			this.detailedAggregationStatus = detailedAggregationStatus;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008F21 File Offset: 0x00007121
		public void SetKeepEnabledSubscription()
		{
			this.keepSubscriptionEnabled = true;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008F2A File Offset: 0x0000712A
		public void SetSyncPhaseCompleted()
		{
			this.syncPhaseCompleted = true;
		}

		// Token: 0x0400010E RID: 270
		private bool disableSubscription;

		// Token: 0x0400010F RID: 271
		private bool keepSubscriptionEnabled;

		// Token: 0x04000110 RID: 272
		private DetailedAggregationStatus detailedAggregationStatus;

		// Token: 0x04000111 RID: 273
		private bool syncPhaseCompleted;
	}
}
