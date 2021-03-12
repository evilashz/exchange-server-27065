using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F61 RID: 3937
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeactivatedOutlookServiceSubscriptionItemEnumerator : OutlookServiceSubscriptionItemEnumeratorBase
	{
		// Token: 0x060063A3 RID: 25507 RVA: 0x00136D76 File Offset: 0x00134F76
		internal DeactivatedOutlookServiceSubscriptionItemEnumerator(IFolder folder) : this(folder, null, 72U)
		{
		}

		// Token: 0x060063A4 RID: 25508 RVA: 0x00136D82 File Offset: 0x00134F82
		internal DeactivatedOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId) : this(folder, appId, 72U)
		{
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x00136D8E File Offset: 0x00134F8E
		internal DeactivatedOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, uint deactivationInHours) : this(folder, appId, deactivationInHours, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x00136D9E File Offset: 0x00134F9E
		internal DeactivatedOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, uint deactivationInHours, Unlimited<uint> resultSize) : base(folder, appId, resultSize)
		{
			this.deactivationInHours = deactivationInHours;
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x00136DB1 File Offset: 0x00134FB1
		protected override SortBy[] GetSortByConstraint()
		{
			return OutlookServiceSubscriptionItemEnumeratorBase.RefreshTimeUTCAscSortBy;
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x00136DB8 File Offset: 0x00134FB8
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC);
			ExDateTime? exDateTime = obj as ExDateTime?;
			if (obj is PropertyError || exDateTime == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "DeactivatedOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Failed to retrieve SubscriptionRefreshTimeUTC from itemProperty {0}.", item);
				return false;
			}
			if (exDateTime.Value.AddHours(this.deactivationInHours) >= ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "DeactivatedOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Found an deactivated '{0}' subscription {1}:{2} [{3}], stopping processing items.", new object[]
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

		// Token: 0x060063A9 RID: 25513 RVA: 0x00136E98 File Offset: 0x00135098
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(OutlookServiceSubscriptionItemSchema.ExpirationTime);
			ExDateTime? exDateTime = obj as ExDateTime?;
			if (obj is PropertyError || exDateTime == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "DeactivatedOutlookServiceSubscriptionItemEnumerator.ShouldSkipItem: Failed to retrieve ExpirationTime from itemProperty {0}.", item);
				return false;
			}
			if (exDateTime.Value < ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "DeactivatedOutlookServiceSubscriptionItemEnumerator.ShouldSkipItem: Found an expired '{0}' subscription {1}:{2} [{3}], skipping item.", new object[]
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

		// Token: 0x04003520 RID: 13600
		private readonly uint deactivationInHours;
	}
}
