using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OutlookService.Service;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F5F RID: 3935
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class OutlookServiceSubscriptionItemEnumeratorBase : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x06006391 RID: 25489 RVA: 0x0013654E File Offset: 0x0013474E
		public OutlookServiceSubscriptionItemEnumeratorBase(IFolder folder) : this(folder, null, OutlookServiceSubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x0013655D File Offset: 0x0013475D
		public OutlookServiceSubscriptionItemEnumeratorBase(IFolder folder, string appId) : this(folder, appId, OutlookServiceSubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x00136584 File Offset: 0x00134784
		public OutlookServiceSubscriptionItemEnumeratorBase(IFolder folder, string appId, Unlimited<uint> resultSize)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfInvalidValue<Unlimited<uint>>("resultSize", resultSize, (Unlimited<uint> x) => x.IsUnlimited || x.Value > 0U);
			this.folder = folder;
			this.appId = appId;
			this.maximumResultSize = resultSize;
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x001369C0 File Offset: 0x00134BC0
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			using (IQueryResult query = this.folder.IItemQuery(ItemQueryType.None, null, this.GetSortByConstraint(), OutlookServiceSubscriptionItemEnumeratorBase.OutlookServiceSubscriptionItemProperties))
			{
				if (!query.SeekToCondition(SeekReference.OriginBeginning, OutlookServiceSubscriptionItemEnumeratorBase.implicitQueryFilter, SeekToConditionFlags.AllowExtendedFilters))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<IFolder>((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator: No Subscriptions found on the folder {0}.", this.folder);
					yield break;
				}
				int currentSize = 0;
				IStorePropertyBag[] messages = query.GetPropertyBags(50);
				while (messages.Length > 0)
				{
					foreach (IStorePropertyBag message in messages)
					{
						if (!this.maximumResultSize.IsUnlimited && (long)currentSize == (long)((ulong)this.maximumResultSize.Value))
						{
							ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<Unlimited<uint>>((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator:Maximum Number of elements reached {0}.", this.maximumResultSize);
							yield break;
						}
						string itemClass = message.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
						if (string.IsNullOrEmpty(itemClass))
						{
							ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceWarning<IFolder>((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator:An empty itemClass was retrieved from the folder {0}.", this.folder);
						}
						else
						{
							if (!itemClass.Equals("OutlookService.Notification.Subscription", StringComparison.OrdinalIgnoreCase))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator: Found an item that does not match the Subscription itemClass.");
								yield break;
							}
							if (!this.CorrectAppId(message))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator:  Found an item that does not match the AppId being Enumerated.");
								yield break;
							}
							if (this.ShouldStopProcessingItems(message))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator: Processing items stopped by ShouldStopProcessingItems.");
								yield break;
							}
							if (this.ShouldSkipItem(message))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<IFolder>((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.GetEnumerator:Message requested to be skipped by ShouldSkipItem {0}.", this.folder);
							}
							else
							{
								currentSize++;
								yield return message;
							}
						}
					}
					messages = query.GetPropertyBags(50);
				}
			}
			yield break;
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x001369DC File Offset: 0x00134BDC
		protected bool CorrectAppId(IStorePropertyBag item)
		{
			if (this.appId == null)
			{
				return true;
			}
			string valueOrDefault = item.GetValueOrDefault<string>(OutlookServiceSubscriptionItemSchema.AppId, null);
			if (valueOrDefault == null)
			{
				ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceError<IStorePropertyBag>((long)this.GetHashCode(), "OutlookServiceSubscriptionItemEnumeratorBase.CorrectAppId: Failed to retrieve AppId from itemProperty {0}.", item);
				return false;
			}
			return valueOrDefault == this.appId;
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x00136A2D File Offset: 0x00134C2D
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x06006397 RID: 25495
		protected abstract SortBy[] GetSortByConstraint();

		// Token: 0x06006398 RID: 25496
		protected abstract bool ShouldStopProcessingItems(IStorePropertyBag item);

		// Token: 0x06006399 RID: 25497
		protected abstract bool ShouldSkipItem(IStorePropertyBag item);

		// Token: 0x04003513 RID: 13587
		internal static readonly Unlimited<uint> EnumeratorDefaultMaximumSize = 50U;

		// Token: 0x04003514 RID: 13588
		private static readonly QueryFilter implicitQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, "OutlookService.Notification.Subscription");

		// Token: 0x04003515 RID: 13589
		public static readonly PropertyDefinition[] OutlookServiceSubscriptionItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			OutlookServiceSubscriptionItemSchema.SubscriptionId,
			OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC,
			OutlookServiceSubscriptionItemSchema.AppId,
			OutlookServiceSubscriptionItemSchema.DeviceNotificationId,
			OutlookServiceSubscriptionItemSchema.ExpirationTime,
			OutlookServiceSubscriptionItemSchema.LockScreen
		};

		// Token: 0x04003516 RID: 13590
		protected static readonly SortBy[] RefreshTimeUTCDescSortBy = new SortBy[]
		{
			new SortBy(OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC, SortOrder.Descending)
		};

		// Token: 0x04003517 RID: 13591
		protected static readonly SortBy[] RefreshTimeUTCAscSortBy = new SortBy[]
		{
			new SortBy(OutlookServiceSubscriptionItemSchema.LastUpdateTimeUTC, SortOrder.Ascending)
		};

		// Token: 0x04003518 RID: 13592
		protected static readonly SortBy[] ExpirationTimeDescSortBy = new SortBy[]
		{
			new SortBy(OutlookServiceSubscriptionItemSchema.ExpirationTime, SortOrder.Descending)
		};

		// Token: 0x04003519 RID: 13593
		protected static readonly SortBy[] ExpirationTimeAscSortBy = new SortBy[]
		{
			new SortBy(OutlookServiceSubscriptionItemSchema.ExpirationTime, SortOrder.Ascending)
		};

		// Token: 0x0400351A RID: 13594
		private readonly IFolder folder;

		// Token: 0x0400351B RID: 13595
		private readonly string appId;

		// Token: 0x0400351C RID: 13596
		private readonly Unlimited<uint> maximumResultSize;
	}
}
