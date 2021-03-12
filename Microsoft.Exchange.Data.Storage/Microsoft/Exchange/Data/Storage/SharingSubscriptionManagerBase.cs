using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DB5 RID: 3509
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SharingSubscriptionManagerBase<TKey, TData> : SharingItemManagerBase<TData>, IDisposable where TData : class, ISharingSubscriptionData<TKey>
	{
		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06007898 RID: 30872 RVA: 0x00214351 File Offset: 0x00212551
		protected MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x06007899 RID: 30873 RVA: 0x0021435C File Offset: 0x0021255C
		protected SharingSubscriptionManagerBase(MailboxSession mailboxSession, string itemClass, PropertyDefinition[] additionalItemProperties)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullOrEmptyArgument(itemClass, "itemClass");
			Util.ThrowOnNullArgument(additionalItemProperties, "additionalItemProperties");
			this.mailboxSession = mailboxSession;
			this.folder = Folder.Bind(mailboxSession, DefaultFolderType.Sharing);
			this.itemClass = itemClass;
			this.itemClassFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, itemClass);
			this.itemProperties = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
			{
				SharingSubscriptionManagerBase<TKey, TData>.ItemPropertiesBase,
				additionalItemProperties
			});
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x002143DD File Offset: 0x002125DD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x002143E8 File Offset: 0x002125E8
		public TData[] GetAll()
		{
			this.CheckDisposed("GetAll");
			object[][] array = this.FindAll();
			List<TData> list = new List<TData>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				TData tdata = this.CreateDataObjectFromItem(array[i]);
				if (tdata != null)
				{
					list.Add(tdata);
				}
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "{0}: All {1} subscription(s) are returned.", this.mailboxSession.MailboxOwner, list.Count);
			return list.ToArray();
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x00214464 File Offset: 0x00212664
		public TData GetExisting(TKey subscriptionKey)
		{
			this.CheckDisposed("GetExisting");
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TKey>((long)this.GetHashCode(), "{0}: looking for subscription {1}", this.mailboxSession.MailboxOwner, subscriptionKey);
			object[] array = this.FindFirstByKey(subscriptionKey);
			if (array != null)
			{
				TData tdata = this.CreateDataObjectFromItem(array);
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: found subscription {1}", this.mailboxSession.MailboxOwner, tdata);
				return tdata;
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TKey>((long)this.GetHashCode(), "{0}: No subscription was found {1}", this.mailboxSession.MailboxOwner, subscriptionKey);
			return default(TData);
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x00214524 File Offset: 0x00212724
		public TData GetByLocalFolderId(StoreObjectId localFolderId)
		{
			this.CheckDisposed("GetByLocalFolderId");
			Util.ThrowOnNullArgument(localFolderId, "localFolderId");
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: looking for subscription by folderId: {1}", this.mailboxSession.MailboxOwner, localFolderId);
			byte[] localFolderIdBytes = localFolderId.GetBytes();
			object[] array = this.FindFirst((object[] properties) => ArrayComparer<byte>.Comparer.Equals(localFolderIdBytes, SharingItemManagerBase<TData>.TryGetPropertyRef<byte[]>(properties, 2)));
			if (array != null)
			{
				TData tdata = this.CreateDataObjectFromItem(array);
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: found subscription {1}", this.mailboxSession.MailboxOwner, tdata);
				return tdata;
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId>((long)this.GetHashCode(), "{0}: No subscription was found for folder: {1}.", this.mailboxSession.MailboxOwner, localFolderId);
			return default(TData);
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x002145E8 File Offset: 0x002127E8
		public TData CreateOrUpdate(TData subscriptionData, bool throwIfConflict)
		{
			this.CheckDisposed("CreateOrUpdate");
			Util.ThrowOnNullArgument(subscriptionData, "subscriptionData");
			TData tdata = default(TData);
			try
			{
				if (subscriptionData.Id == null)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TKey>((long)this.GetHashCode(), "{0}: creating subscription {1}", this.mailboxSession.MailboxOwner, subscriptionData.Key);
					using (MessageItem messageItem = MessageItem.Create(this.mailboxSession, this.folder.Id))
					{
						messageItem.ClassName = this.itemClass;
						this.SaveSubscriptionData(messageItem, subscriptionData);
						messageItem.Load();
						this.ResolveConflictAfterCreation(subscriptionData.Key, messageItem.Id);
						goto IL_11B;
					}
				}
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, VersionedId, TData>((long)this.GetHashCode(), "{0}: updating subscription ItemId={1}, Data={2}", this.mailboxSession.MailboxOwner, subscriptionData.Id, subscriptionData);
				using (Item item = MessageItem.Bind(this.mailboxSession, subscriptionData.Id, this.itemProperties))
				{
					this.SaveSubscriptionData(item, subscriptionData);
				}
				IL_11B:;
			}
			catch (SharingConflictException)
			{
				tdata = this.GetExisting(subscriptionData.Key);
				if (tdata == null || !tdata.LocalFolderId.Equals(subscriptionData.LocalFolderId))
				{
					this.Rollback(subscriptionData);
				}
				if (throwIfConflict)
				{
					throw;
				}
			}
			TData result;
			if ((result = tdata) == null)
			{
				result = subscriptionData;
			}
			return result;
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x00214794 File Offset: 0x00212994
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x002147B1 File Offset: 0x002129B1
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (disposing)
				{
					this.folder.Dispose();
				}
			}
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x002147E0 File Offset: 0x002129E0
		private void Rollback(TData subscription)
		{
			if (subscription.LocalFolderId != null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: Delete the referring folder due to failure of saving subscription {1}", this.mailboxSession.MailboxOwner, subscription);
				this.mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					subscription.LocalFolderId
				});
			}
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x00214844 File Offset: 0x00212A44
		private void SaveSubscriptionData(Item item, TData subscriptionData)
		{
			this.StampItemFromDataObject(item, subscriptionData);
			ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: A race condition occurred when saving subscription {1}", this.mailboxSession.MailboxOwner, subscriptionData);
				throw new SharingConflictException(conflictResolutionResult);
			}
			ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, TData>((long)this.GetHashCode(), "{0}: saved subscription {1}", this.mailboxSession.MailboxOwner, subscriptionData);
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x002148B8 File Offset: 0x00212AB8
		private void ResolveConflictAfterCreation(TKey subscriptionKey, VersionedId subscriptionId)
		{
			object[] array = this.FindFirstByKey(subscriptionKey);
			VersionedId versionedId = (VersionedId)array[1];
			if (!subscriptionId.ObjectId.Equals(versionedId.ObjectId))
			{
				ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, VersionedId>((long)this.GetHashCode(), "{0}: A race condition occurred. This subscription is about to be deleted due to duplication: {0}", this.mailboxSession.MailboxOwner, subscriptionId);
				AggregateOperationResult aggregateOperationResult = this.mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					subscriptionId
				});
				if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
				{
					ExTraceGlobals.SharingTracer.TraceError<IExchangePrincipal, VersionedId, AggregateOperationResult>((long)this.GetHashCode(), "{0}: Failed to delete this subscription {1}: {2}", this.mailboxSession.MailboxOwner, subscriptionId, aggregateOperationResult);
				}
				throw new SharingConflictException();
			}
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x00214958 File Offset: 0x00212B58
		private object[][] FindAll()
		{
			List<object[]> list = new List<object[]>();
			using (QueryResult queryResult = this.folder.ItemQuery(ItemQueryType.None, null, SharingSubscriptionManagerBase<TKey, TData>.ItemQuerySorts, this.itemProperties))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, this.itemClassFilter))
				{
					object[][] rows;
					int num;
					for (;;)
					{
						rows = queryResult.GetRows(10000);
						if (rows.Length == 0)
						{
							goto IL_A2;
						}
						num = 0;
						foreach (object[] properties in rows)
						{
							string y = SharingItemManagerBase<TData>.TryGetPropertyRef<string>(properties, 0);
							if (!StringComparer.OrdinalIgnoreCase.Equals(this.itemClass, y))
							{
								break;
							}
							num++;
						}
						if (num != rows.Length)
						{
							break;
						}
						list.AddRange(rows);
					}
					if (num > 0)
					{
						Array.Resize<object[]>(ref rows, num);
						list.AddRange(rows);
					}
				}
				IL_A2:;
			}
			return list.ToArray();
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x00214A2C File Offset: 0x00212C2C
		protected object[] FindFirst(Predicate<object[]> match)
		{
			using (QueryResult queryResult = this.folder.ItemQuery(ItemQueryType.None, null, SharingSubscriptionManagerBase<TKey, TData>.ItemQuerySorts, this.itemProperties))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, this.itemClassFilter))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(10000);
						if (rows.Length == 0)
						{
							goto IL_89;
						}
						bool flag = false;
						foreach (object[] array2 in rows)
						{
							string y = SharingItemManagerBase<TData>.TryGetPropertyRef<string>(array2, 0);
							if (!StringComparer.OrdinalIgnoreCase.Equals(this.itemClass, y))
							{
								flag = true;
								break;
							}
							if (match(array2))
							{
								goto Block_5;
							}
						}
						if (flag)
						{
							goto IL_89;
						}
					}
					Block_5:
					object[] array2;
					return array2;
				}
				IL_89:;
			}
			return null;
		}

		// Token: 0x060078A6 RID: 30886
		protected abstract object[] FindFirstByKey(TKey subscriptionKey);

		// Token: 0x04005356 RID: 21334
		private readonly MailboxSession mailboxSession;

		// Token: 0x04005357 RID: 21335
		private readonly Folder folder;

		// Token: 0x04005358 RID: 21336
		private readonly string itemClass;

		// Token: 0x04005359 RID: 21337
		private readonly ComparisonFilter itemClassFilter;

		// Token: 0x0400535A RID: 21338
		private readonly PropertyDefinition[] itemProperties;

		// Token: 0x0400535B RID: 21339
		private static readonly PropertyDefinition[] ItemPropertiesBase = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			SharingSchema.ExternalSharingLocalFolderId
		};

		// Token: 0x0400535C RID: 21340
		private static readonly SortBy[] ItemQuerySorts = new SortBy[]
		{
			new SortBy(InternalSchema.ItemClass, SortOrder.Ascending),
			new SortBy(InternalSchema.LastModifiedTime, SortOrder.Ascending),
			new SortBy(InternalSchema.CreationTime, SortOrder.Ascending),
			new SortBy(InternalSchema.MID, SortOrder.Ascending)
		};

		// Token: 0x0400535D RID: 21341
		private bool isDisposed;

		// Token: 0x02000DB6 RID: 3510
		protected enum ItemPropertiesIndexBase
		{
			// Token: 0x0400535F RID: 21343
			ItemClass,
			// Token: 0x04005360 RID: 21344
			Id,
			// Token: 0x04005361 RID: 21345
			ExternalSharingLocalFolderId,
			// Token: 0x04005362 RID: 21346
			Next
		}
	}
}
