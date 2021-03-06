using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200029D RID: 669
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SingleInstanceItemHandler<T> where T : class, IItem
	{
		// Token: 0x06001BB4 RID: 7092 RVA: 0x000800D9 File Offset: 0x0007E2D9
		protected SingleInstanceItemHandler(ItemQueryType itemQueryType, string queryItemClass)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("queryItemClass", queryItemClass);
			this.itemQueryType = itemQueryType;
			this.queryItemClass = queryItemClass;
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001BB5 RID: 7093
		protected abstract Trace Tracer { get; }

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000800FC File Offset: 0x0007E2FC
		public T GetItem(IStoreSession session, IFolder folder, ICollection<PropertyDefinition> propsToReturn)
		{
			IEnumerable<IStorePropertyBag> itemEnumerator = this.GetItemEnumerator(session, folder);
			IStorePropertyBag storePropertyBag = this.ClearStaleItems(session, itemEnumerator);
			T t = default(T);
			if (storePropertyBag != null)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				try
				{
					this.Tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItem - Attempting to bind to item with ID {0}", valueOrDefault);
					t = this.BindToItem(session, valueOrDefault, propsToReturn);
				}
				catch (ObjectNotFoundException arg)
				{
					this.Tracer.TraceWarning<VersionedId, ObjectNotFoundException>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItem - Failed to bind to item with ID {0} as object was not found. A new item will be created. Exception: {1}", valueOrDefault, arg);
				}
			}
			if (t == null)
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					this.Tracer.TraceDebug<VersionedId>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItem - Couldn't find item, creating a new one. Folder ID = {0}", folder.Id);
					t = this.CreateItem(session, folder);
					disposeGuard.Add<T>(t);
					try
					{
						this.InitializeNewItemData(session, folder, t);
					}
					catch (LocalizedException arg2)
					{
						this.Tracer.TraceWarning<LocalizedException>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItem - Couldn't initialize data for the newly created single instance item. Exception: {0}", arg2);
					}
					disposeGuard.Success();
				}
			}
			return t;
		}

		// Token: 0x06001BB7 RID: 7095
		protected abstract T BindToItem(IStoreSession session, StoreId itemId, ICollection<PropertyDefinition> propsToReturn);

		// Token: 0x06001BB8 RID: 7096
		protected abstract T CreateItem(IStoreSession session, IFolder folder);

		// Token: 0x06001BB9 RID: 7097
		protected abstract void InitializeNewItemData(IStoreSession session, IFolder folder, T item);

		// Token: 0x06001BBA RID: 7098 RVA: 0x0008021C File Offset: 0x0007E41C
		private IStorePropertyBag ClearStaleItems(IStoreSession session, IEnumerable<IStorePropertyBag> propertyBagsFound)
		{
			IStorePropertyBag storePropertyBag = null;
			ExDateTime exDateTime = ExDateTime.MinValue;
			foreach (IStorePropertyBag storePropertyBag2 in propertyBagsFound)
			{
				ExDateTime valueOrDefault = storePropertyBag2.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
				this.Tracer.TraceDebug<ExDateTime, ExDateTime>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:ClearStaleItems - Most recent modified time = {0}. Current item modified time = {1}", exDateTime, valueOrDefault);
				if (valueOrDefault >= exDateTime)
				{
					if (storePropertyBag != null)
					{
						this.RemoveStaleItem(session, storePropertyBag);
					}
					exDateTime = valueOrDefault;
					storePropertyBag = storePropertyBag2;
				}
				else
				{
					this.RemoveStaleItem(session, storePropertyBag2);
				}
			}
			return storePropertyBag;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000802B8 File Offset: 0x0007E4B8
		private void RemoveStaleItem(IStoreSession session, IStorePropertyBag staleItem)
		{
			VersionedId valueOrDefault = staleItem.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			ExDateTime valueOrDefault2 = staleItem.GetValueOrDefault<ExDateTime>(StoreObjectSchema.LastModifiedTime, ExDateTime.MinValue);
			this.Tracer.TraceDebug<VersionedId, ExDateTime>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:RemoveStaleItem - Removing stale metadata item with ID={0} and ModifiedTime={1}", valueOrDefault, valueOrDefault2);
			try
			{
				session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					valueOrDefault
				});
			}
			catch (ObjectNotFoundException arg)
			{
				this.Tracer.TraceWarning<VersionedId, ObjectNotFoundException>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:RemoveStaleItem - Failed to remove stale metadata item with ID {0} as object was not found. Exception: {1}", valueOrDefault, arg);
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0008079C File Offset: 0x0007E99C
		private IEnumerable<IStorePropertyBag> GetItemEnumerator(IStoreSession session, IFolder folder)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("folder", folder);
			this.Tracer.TraceDebug<ItemQueryType, string>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Querying folder for {0} items with '{1}' Item Class", this.itemQueryType, this.queryItemClass);
			using (IQueryResult query = folder.IItemQuery(this.itemQueryType, null, SingleInstanceItemHandler<T>.ItemQuerySortOrder, SingleInstanceItemHandler<T>.ItemQueryColumns))
			{
				ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.ItemClass, this.queryItemClass);
				this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Seeking to items with Item class = {0}", this.queryItemClass);
				if (query.SeekToCondition(SeekReference.OriginBeginning, filter))
				{
					IStorePropertyBag[] items = query.GetPropertyBags(100);
					this.Tracer.TraceDebug<int>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Retrieved {0} property bags from store", (items != null) ? items.Length : 0);
					while (items != null && items.Length > 0)
					{
						foreach (IStorePropertyBag item in items)
						{
							string itemClass = item.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
							if (string.IsNullOrEmpty(itemClass))
							{
								this.Tracer.TraceDebug((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Skipping item with blank item class");
							}
							else
							{
								if (!itemClass.Equals(this.queryItemClass, StringComparison.OrdinalIgnoreCase))
								{
									this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Done enumerating items, found different ItemClass value. Item class found = {0}.", itemClass);
									yield break;
								}
								this.Tracer.TraceDebug((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Returning item");
								yield return item;
							}
						}
						items = query.GetPropertyBags(100);
						this.Tracer.TraceDebug<int>((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - Consumed previous set of property bags. Retrieved {0} additional rows", (items != null) ? items.Length : 0);
					}
				}
				this.Tracer.TraceDebug((long)this.GetHashCode(), "SingleInstanceItemHandler<T>:GetItemEnumerator - No items found");
			}
			yield break;
		}

		// Token: 0x0400133D RID: 4925
		private const int ItemQueryBatchSize = 100;

		// Token: 0x0400133E RID: 4926
		private static readonly PropertyDefinition[] ItemQueryColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x0400133F RID: 4927
		private static readonly SortBy[] ItemQuerySortOrder = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04001340 RID: 4928
		private readonly ItemQueryType itemQueryType;

		// Token: 0x04001341 RID: 4929
		private readonly string queryItemClass;
	}
}
