using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AFE RID: 2814
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SubscriptionItemEnumeratorBase : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x06006644 RID: 26180 RVA: 0x001B1E32 File Offset: 0x001B0032
		public SubscriptionItemEnumeratorBase(IFolder folder) : this(folder, SubscriptionItemEnumeratorBase.EnumeratorDefaultMaximumSize)
		{
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x001B1E58 File Offset: 0x001B0058
		public SubscriptionItemEnumeratorBase(IFolder folder, Unlimited<uint> resultSize)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfInvalidValue<Unlimited<uint>>("resultSize", resultSize, (Unlimited<uint> x) => x.IsUnlimited || x.Value > 0U);
			this.folder = folder;
			this.maximumResultSize = resultSize;
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x001B2254 File Offset: 0x001B0454
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			using (IQueryResult query = this.folder.IItemQuery(ItemQueryType.None, null, this.GetSortByConstraint(), SubscriptionItemEnumeratorBase.PushNotificationSubscriptionItemProperties))
			{
				if (!query.SeekToCondition(SeekReference.OriginBeginning, SubscriptionItemEnumeratorBase.implicitQueryFilter, SeekToConditionFlags.AllowExtendedFilters))
				{
					ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<IFolder>((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator: No Subscriptions found on the folder {0}.", this.folder);
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
							ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<Unlimited<uint>>((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator:Maximum Number of elements reached {0}.", this.maximumResultSize);
							yield break;
						}
						string itemClass = message.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
						if (string.IsNullOrEmpty(itemClass))
						{
							ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceWarning<IFolder>((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator:An empty itemClass was retrieved from the folder {0}.", this.folder);
						}
						else
						{
							if (!itemClass.Equals("Exchange.PushNotification.Subscription", StringComparison.OrdinalIgnoreCase))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator: Found an item that does not match the Subscription itemClass.");
								yield break;
							}
							if (this.ShouldStopProcessingItems(message))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator: Processing items stopped by ShouldStopProcessingItems.");
								yield break;
							}
							if (this.ShouldSkipItem(message))
							{
								ExTraceGlobals.StorageNotificationSubscriptionTracer.TraceDebug<IFolder>((long)this.GetHashCode(), "SubscriptionItemEnumeratorBase.GetEnumerator:Message requested to be skipped by ShouldSkipItem {0}.", this.folder);
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

		// Token: 0x06006647 RID: 26183 RVA: 0x001B2270 File Offset: 0x001B0470
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x06006648 RID: 26184
		protected abstract SortBy[] GetSortByConstraint();

		// Token: 0x06006649 RID: 26185
		protected abstract bool ShouldStopProcessingItems(IStorePropertyBag item);

		// Token: 0x0600664A RID: 26186
		protected abstract bool ShouldSkipItem(IStorePropertyBag item);

		// Token: 0x04003A15 RID: 14869
		internal static readonly Unlimited<uint> EnumeratorDefaultMaximumSize = 50U;

		// Token: 0x04003A16 RID: 14870
		private static readonly QueryFilter implicitQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, "Exchange.PushNotification.Subscription");

		// Token: 0x04003A17 RID: 14871
		public static readonly PropertyDefinition[] PushNotificationSubscriptionItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			PushNotificationSubscriptionItemSchema.SubscriptionId,
			PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC,
			PushNotificationSubscriptionItemSchema.SerializedNotificationSubscription
		};

		// Token: 0x04003A18 RID: 14872
		protected static readonly SortBy[] RefreshTimeUTCDescSortBy = new SortBy[]
		{
			new SortBy(PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC, SortOrder.Descending)
		};

		// Token: 0x04003A19 RID: 14873
		protected static readonly SortBy[] RefreshTimeUTCAscSortBy = new SortBy[]
		{
			new SortBy(PushNotificationSubscriptionItemSchema.LastUpdateTimeUTC, SortOrder.Ascending)
		};

		// Token: 0x04003A1A RID: 14874
		private readonly IFolder folder;

		// Token: 0x04003A1B RID: 14875
		private readonly Unlimited<uint> maximumResultSize;
	}
}
