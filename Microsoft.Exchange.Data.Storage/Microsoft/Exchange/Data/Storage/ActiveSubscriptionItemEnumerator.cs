using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFF RID: 2815
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ActiveSubscriptionItemEnumerator : SubscriptionItemEnumeratorBase
	{
		// Token: 0x0600664D RID: 26189 RVA: 0x001B230D File Offset: 0x001B050D
		public ActiveSubscriptionItemEnumerator(IFolder folder) : this(folder, 72U)
		{
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x001B2318 File Offset: 0x001B0518
		public ActiveSubscriptionItemEnumerator(IFolder folder, uint expirationInHours) : this(folder, expirationInHours, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x001B2327 File Offset: 0x001B0527
		public ActiveSubscriptionItemEnumerator(IFolder folder, uint expirationInHours, Unlimited<uint> resultSize) : base(folder, resultSize)
		{
			this.expirationInHours = expirationInHours;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x001B2343 File Offset: 0x001B0543
		protected override SortBy[] GetSortByConstraint()
		{
			return SubscriptionItemEnumeratorBase.RefreshTimeUTCDescSortBy;
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x001B234C File Offset: 0x001B054C
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC);
			ExDateTime? arg = obj as ExDateTime?;
			if (obj is PropertyError || arg == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldStopProcessingItems: Failed to retrieve SubscriptionRefreshTimeUTC from itemProperty {0}.", item);
				return true;
			}
			if (arg.Value.AddHours(this.expirationInHours) < ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<ExDateTime?, string, uint>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldStopProcessingItems: Found an expired '{0}' subscription {1} [{2}], stopping processing items.", arg, item.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription, string.Empty), this.expirationInHours);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x001B23FC File Offset: 0x001B05FC
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			string valueOrDefault = item.GetValueOrDefault<string>(PushNotificationSubscriptionItemSchema.SubscriptionId, null);
			if (valueOrDefault == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldSkipItem: Failed to retrieve SubscriptionId from itemProperty {0}.", item);
				return false;
			}
			if (this.ids.Contains(valueOrDefault))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldSkipItem: Replicated item found on ActiveSubscriptionEnumerator '{0}'.", valueOrDefault);
				return true;
			}
			this.ids.Add(valueOrDefault);
			return false;
		}

		// Token: 0x04003A1D RID: 14877
		private readonly uint expirationInHours;

		// Token: 0x04003A1E RID: 14878
		private HashSet<string> ids = new HashSet<string>();
	}
}
