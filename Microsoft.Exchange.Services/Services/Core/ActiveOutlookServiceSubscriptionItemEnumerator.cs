using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F60 RID: 3936
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ActiveOutlookServiceSubscriptionItemEnumerator : OutlookServiceSubscriptionItemEnumeratorBase
	{
		// Token: 0x0600639C RID: 25500 RVA: 0x00136B1E File Offset: 0x00134D1E
		public ActiveOutlookServiceSubscriptionItemEnumerator(IFolder folder) : this(folder, null, 72U)
		{
		}

		// Token: 0x0600639D RID: 25501 RVA: 0x00136B2A File Offset: 0x00134D2A
		public ActiveOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId) : this(folder, appId, 72U)
		{
		}

		// Token: 0x0600639E RID: 25502 RVA: 0x00136B36 File Offset: 0x00134D36
		public ActiveOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, uint deactivationInHours) : this(folder, appId, deactivationInHours, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x0600639F RID: 25503 RVA: 0x00136B46 File Offset: 0x00134D46
		public ActiveOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, uint deactivationInHours, Unlimited<uint> resultSize) : base(folder, appId, resultSize)
		{
			this.deactivationInHours = deactivationInHours;
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x00136B64 File Offset: 0x00134D64
		protected override SortBy[] GetSortByConstraint()
		{
			return OutlookServiceSubscriptionItemEnumeratorBase.RefreshTimeUTCDescSortBy;
		}

		// Token: 0x060063A1 RID: 25505 RVA: 0x00136B6C File Offset: 0x00134D6C
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC);
			ExDateTime? exDateTime = obj as ExDateTime?;
			if (obj is PropertyError || exDateTime == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Failed to retrieve SubscriptionRefreshTimeUTC from itemProperty {0}.", item);
				return true;
			}
			if (exDateTime.Value.AddHours(this.deactivationInHours) < ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "ActiveOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Found a deactivated '{0}' subscription {1}:{2} [{3}], stopping processing items..", new object[]
					{
						exDateTime,
						item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.AppId, string.Empty),
						item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.DeviceNotificationId, string.Empty),
						this.deactivationInHours
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x060063A2 RID: 25506 RVA: 0x00136C4C File Offset: 0x00134E4C
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			string valueOrDefault = item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.SubscriptionId, null);
			if (valueOrDefault == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveOutlookServiceSubscriptionItemEnumerator.ShouldSkipItem: Failed to retrieve SubscriptionId from itemProperty {0}.", item);
				return false;
			}
			if (this.ids.Contains(valueOrDefault))
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveOutlookServiceSubscriptionItemEnumerator.ShouldSkipItem: Replicated item found on ActiveOutlookServiceSubscriptionEnumerator '{0}'.", valueOrDefault);
				return true;
			}
			this.ids.Add(valueOrDefault);
			object obj = item.TryGetProperty(OutlookServiceSubscriptionItemSchema.ExpirationTime);
			ExDateTime? exDateTime = obj as ExDateTime?;
			if (obj is PropertyError || exDateTime == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldSkipItem: Failed to retrieve ExpirationTime from itemProperty {0}.", item);
				return true;
			}
			if (exDateTime.Value < ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "ActiveSubscriptionItemEnumerator.ShouldSkipItem: Found an expired '{0}' subscription {1}:{2} [{3}], skipping item.", new object[]
					{
						exDateTime,
						item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.AppId, string.Empty),
						item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.DeviceNotificationId, string.Empty),
						this.deactivationInHours
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x0400351E RID: 13598
		private readonly uint deactivationInHours;

		// Token: 0x0400351F RID: 13599
		private HashSet<string> ids = new HashSet<string>();
	}
}
