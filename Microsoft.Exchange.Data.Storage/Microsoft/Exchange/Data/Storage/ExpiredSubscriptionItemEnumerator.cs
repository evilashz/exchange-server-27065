using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B00 RID: 2816
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ExpiredSubscriptionItemEnumerator : SubscriptionItemEnumeratorBase
	{
		// Token: 0x06006653 RID: 26195 RVA: 0x001B2467 File Offset: 0x001B0667
		internal ExpiredSubscriptionItemEnumerator(IFolder folder) : this(folder, 72U)
		{
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x001B2472 File Offset: 0x001B0672
		internal ExpiredSubscriptionItemEnumerator(IFolder folder, uint expirationInHours) : this(folder, expirationInHours, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x001B2481 File Offset: 0x001B0681
		internal ExpiredSubscriptionItemEnumerator(IFolder folder, uint expirationInHours, Unlimited<uint> resultSize) : base(folder, resultSize)
		{
			this.expirationInHours = expirationInHours;
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x001B2492 File Offset: 0x001B0692
		protected override SortBy[] GetSortByConstraint()
		{
			return SubscriptionItemEnumeratorBase.RefreshTimeUTCAscSortBy;
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x001B249C File Offset: 0x001B069C
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC);
			ExDateTime? arg = obj as ExDateTime?;
			if (obj is PropertyError || arg == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldStopProcessingItems: Failed to retrieve SubscriptionRefreshTimeUTC from itemProperty {0}.", item);
				return false;
			}
			if (arg.Value.AddHours(this.expirationInHours) >= ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<ExDateTime?, string, uint>((long)this.GetHashCode(), "ExpiredSubscriptionItemEnumerator.ShouldStopProcessingItems: Found an active '{0}' subscription {1} [{2}], stopping processing items.", arg, item.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription, string.Empty), this.expirationInHours);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x001B254B File Offset: 0x001B074B
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			return false;
		}

		// Token: 0x04003A1F RID: 14879
		private readonly uint expirationInHours;
	}
}
