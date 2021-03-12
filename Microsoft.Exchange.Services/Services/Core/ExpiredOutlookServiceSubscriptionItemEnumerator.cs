using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F62 RID: 3938
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ExpiredOutlookServiceSubscriptionItemEnumerator : OutlookServiceSubscriptionItemEnumeratorBase
	{
		// Token: 0x060063AA RID: 25514 RVA: 0x00136F65 File Offset: 0x00135165
		internal ExpiredOutlookServiceSubscriptionItemEnumerator(IFolder folder) : this(folder, null)
		{
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x00136F6F File Offset: 0x0013516F
		internal ExpiredOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId) : this(folder, appId, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x00136F7E File Offset: 0x0013517E
		internal ExpiredOutlookServiceSubscriptionItemEnumerator(IFolder folder, string appId, Unlimited<uint> resultSize) : base(folder, appId, resultSize)
		{
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x00136F89 File Offset: 0x00135189
		protected override SortBy[] GetSortByConstraint()
		{
			return OutlookServiceSubscriptionItemEnumeratorBase.ExpirationTimeAscSortBy;
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x00136F90 File Offset: 0x00135190
		protected override bool ShouldStopProcessingItems(IStorePropertyBag item)
		{
			object obj = item.TryGetProperty(OutlookServiceSubscriptionItemSchema.ExpirationTime);
			ExDateTime? arg = obj as ExDateTime?;
			if (obj is PropertyError || arg == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "ExpiredOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Failed to retrieve ExpirationTime from itemProperty {0}.", item);
				return false;
			}
			if (arg.Value >= ExDateTime.UtcNow)
			{
				if (ExTraceGlobals.StorageNotificationSubscriptionTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<ExDateTime?, string, string>((long)this.GetHashCode(), "ExpiredOutlookServiceSubscriptionItemEnumerator.ShouldStopProcessingItems: Found an expired '{0}' subscription {1}:{2}, stopping processing items..", arg, item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.AppId, string.Empty), item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.DeviceNotificationId, string.Empty));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x00137039 File Offset: 0x00135239
		protected override bool ShouldSkipItem(IStorePropertyBag item)
		{
			return false;
		}
	}
}
