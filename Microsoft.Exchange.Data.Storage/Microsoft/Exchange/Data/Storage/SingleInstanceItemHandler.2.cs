using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.QuickLogStrings;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B0B RID: 2827
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SingleInstanceItemHandler
	{
		// Token: 0x060066A6 RID: 26278 RVA: 0x001B343D File Offset: 0x001B163D
		public SingleInstanceItemHandler(string messageClass, DefaultFolderType defaultFolder)
		{
			if (messageClass == null)
			{
				throw new ArgumentNullException("messageClass");
			}
			EnumValidator.ThrowIfInvalid<DefaultFolderType>(defaultFolder, "defaultFolder");
			this.messageClass = messageClass;
			this.defaultFolder = defaultFolder;
		}

		// Token: 0x060066A7 RID: 26279 RVA: 0x001B346C File Offset: 0x001B166C
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Single instance item for MessageClass=" + this.messageClass + ", Folder=" + this.defaultFolder.ToString();
			}
			return this.toString;
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x001B34A8 File Offset: 0x001B16A8
		public string GetItemContent(MailboxSession itemStore)
		{
			List<object[]> results = this.QueryItems(itemStore);
			VersionedId mostRecentItem = this.GetMostRecentItem(results);
			return this.GetItemContent(itemStore, mostRecentItem);
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x001B34D0 File Offset: 0x001B16D0
		public string GetItemContent(MailboxSession itemStore, StoreId itemId)
		{
			if (itemId == null)
			{
				SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Item not found in {1}", this, itemStore.MailboxOwner);
				return null;
			}
			SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Item found in '{1}'", this, itemStore.MailboxOwner);
			string result;
			try
			{
				using (Item item = MessageItem.Bind(itemStore, itemId))
				{
					using (TextReader textReader = item.Body.OpenTextReader(BodyFormat.TextPlain))
					{
						result = textReader.ReadToEnd();
					}
				}
			}
			catch (ObjectNotFoundException)
			{
				SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Item no longer found in {1}", this, itemStore.MailboxOwner);
				result = null;
			}
			return result;
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x001B35AC File Offset: 0x001B17AC
		public VersionedId SetItemContent(MailboxSession itemStore, string body)
		{
			return this.Update(itemStore, false, (string existingContent) => body);
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x001B35DC File Offset: 0x001B17DC
		public void Delete(MailboxSession itemStore)
		{
			List<object[]> results = this.QueryItems(itemStore);
			VersionedId mostRecentItem = this.GetMostRecentItem(results);
			if (mostRecentItem != null)
			{
				try
				{
					itemStore.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						mostRecentItem
					});
				}
				catch (ObjectNotFoundException)
				{
					SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, VersionedId, IExchangePrincipal>((long)this.GetHashCode(), "{0}: ObjectNotFoundException encountred while trying to delete object, itemId='{1}' on mailbox {2}", this, mostRecentItem, itemStore.MailboxOwner);
				}
			}
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x001B3644 File Offset: 0x001B1844
		public void UpdateItemContent(MailboxSession itemStore, SingleInstanceItemHandler.ContentUpdater updater)
		{
			this.Update(itemStore, true, updater);
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x001B3650 File Offset: 0x001B1850
		private VersionedId Update(MailboxSession itemStore, bool getExisting, SingleInstanceItemHandler.ContentUpdater updater)
		{
			int i = 0;
			while (i < 5)
			{
				i++;
				try
				{
					return this.UpdateRetryable(itemStore, getExisting, updater);
				}
				catch (MapiExceptionObjectChanged arg)
				{
					SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal, MapiExceptionObjectChanged>((long)this.GetHashCode(), "{0}: Exception updating item: Mailbox = {1}, exception = {2}, retrying", this, itemStore.MailboxOwner, arg);
					if (i == 5)
					{
						throw;
					}
				}
				catch (ObjectNotFoundException arg2)
				{
					SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Exception updating item: Mailbox = {1}, exception = {2}, retrying", this, itemStore.MailboxOwner, arg2);
					if (i == 5)
					{
						throw;
					}
				}
				catch (SaveConflictException arg3)
				{
					SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal, SaveConflictException>((long)this.GetHashCode(), "{0}: Exception updating item: Mailbox = {1}, exception = {2}, retrying", this, itemStore.MailboxOwner, arg3);
					if (i == 5)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x001B3724 File Offset: 0x001B1924
		private VersionedId UpdateRetryable(MailboxSession itemStore, bool getExisting, SingleInstanceItemHandler.ContentUpdater updater)
		{
			List<object[]> list = this.QueryItems(itemStore);
			VersionedId versionedId = this.GetMostRecentItem(list);
			if (versionedId == null)
			{
				try
				{
					using (Folder folder = Folder.Bind(itemStore, this.defaultFolder))
					{
						using (Item item = MessageItem.Create(itemStore, folder.Id))
						{
							item.ClassName = this.messageClass;
							using (TextWriter textWriter = item.Body.OpenTextWriter(BodyFormat.TextPlain))
							{
								textWriter.Write(updater(null));
							}
							ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
							if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
							{
								throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(item.Id), conflictResolutionResult);
							}
							item.Load();
							versionedId = item.Id;
						}
					}
					SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, VersionedId, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Created new item itemId={1} on mailbox {2}", this, versionedId, itemStore.MailboxOwner);
					goto IL_1CD;
				}
				catch (ObjectNotFoundException ex)
				{
					SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: bind to folder failed on mailbox {1}. Exception={2}", this, itemStore.MailboxOwner, ex);
					throw new SingleInstanceItemHandlerPermanentException(Strings.FailedToGetItem(this.messageClass, this.defaultFolder.ToString()), ex);
				}
			}
			using (Item item2 = MessageItem.Bind(itemStore, versionedId))
			{
				string existingContent = null;
				if (getExisting)
				{
					using (TextReader textReader = item2.Body.OpenTextReader(BodyFormat.TextPlain))
					{
						existingContent = textReader.ReadToEnd();
					}
				}
				using (TextWriter textWriter2 = item2.Body.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter2.Write(updater(existingContent));
				}
				ConflictResolutionResult conflictResolutionResult2 = item2.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult2.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(item2.Id), conflictResolutionResult2);
				}
			}
			SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, VersionedId, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Updated item itemId={1} on mailbox {2}", this, versionedId, itemStore.MailboxOwner);
			IL_1CD:
			using (IEnumerator<object[]> enumerator = list.GetEnumerator())
			{
				List<VersionedId> list2 = new List<VersionedId>();
				for (;;)
				{
					bool flag = enumerator.MoveNext();
					if ((!flag || list2.Count == 100) && list2.Count > 0)
					{
						try
						{
							itemStore.Delete(DeleteItemFlags.HardDelete, list2.ToArray());
						}
						catch (ObjectNotFoundException)
						{
							SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal>((long)this.GetHashCode(), "{0}: ObjectNotFoundException encountred while trying to delete duplicate item on mailbox {1}", this, itemStore.MailboxOwner);
						}
						list2.Clear();
					}
					if (!flag)
					{
						break;
					}
					VersionedId versionedId2 = (VersionedId)enumerator.Current[0];
					if (versionedId != versionedId2)
					{
						list2.Add(versionedId2);
						SingleInstanceItemHandler.Tracer.TraceDebug<SingleInstanceItemHandler, VersionedId, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Deleting extra item {1} on mailbox {2}", this, versionedId2, itemStore.MailboxOwner);
					}
				}
			}
			return versionedId;
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x001B3A34 File Offset: 0x001B1C34
		private VersionedId GetMostRecentItem(List<object[]> results)
		{
			ExDateTime t = ExDateTime.MinValue;
			VersionedId result = null;
			for (int i = 0; i < results.Count; i++)
			{
				object[] array = results[i];
				object obj = array[0];
				object obj2 = array[1];
				if (!(obj is VersionedId))
				{
					throw new SingleInstanceItemHandlerTransientException(Strings.descMissingProperty("Id", (obj == null) ? "<null>" : obj.ToString()));
				}
				if (!(obj2 is ExDateTime))
				{
					throw new SingleInstanceItemHandlerTransientException(Strings.descMissingProperty("LastModifiedTime", (obj2 == null) ? "<null>" : obj2.ToString()));
				}
				VersionedId versionedId = (VersionedId)obj;
				ExDateTime exDateTime = (ExDateTime)obj2;
				if (exDateTime > t)
				{
					result = versionedId;
					t = exDateTime;
				}
			}
			return result;
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x001B3AEC File Offset: 0x001B1CEC
		private List<object[]> QueryItems(MailboxSession itemStore)
		{
			List<object[]> list = new List<object[]>();
			try
			{
				using (Folder folder = Folder.Bind(itemStore, this.defaultFolder))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, SingleInstanceItemHandler.ItemQueryReturnColumns))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(100);
							if (rows == null || rows.Length == 0 || list.Count > 100000)
							{
								break;
							}
							foreach (object[] array in rows)
							{
								string value = array[2] as string;
								if (this.messageClass.Equals(value, StringComparison.OrdinalIgnoreCase))
								{
									list.Add(array);
								}
							}
						}
					}
				}
			}
			catch (ObjectNotFoundException ex)
			{
				SingleInstanceItemHandler.Tracer.TraceError<SingleInstanceItemHandler, IExchangePrincipal, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: bind to folder failed on mailbox {1}. Exception={2}", this, itemStore.MailboxOwner, ex);
				throw new SingleInstanceItemHandlerPermanentException(Strings.FailedToGetItem(this.messageClass, this.defaultFolder.ToString()), ex);
			}
			return list;
		}

		// Token: 0x04003A2F RID: 14895
		private const int DeleteBatchSize = 100;

		// Token: 0x04003A30 RID: 14896
		private const int QueryBatchSize = 100;

		// Token: 0x04003A31 RID: 14897
		private const int MaximumQueryListSize = 100000;

		// Token: 0x04003A32 RID: 14898
		private const int MaxRetry = 5;

		// Token: 0x04003A33 RID: 14899
		private string messageClass;

		// Token: 0x04003A34 RID: 14900
		private DefaultFolderType defaultFolder;

		// Token: 0x04003A35 RID: 14901
		private string toString;

		// Token: 0x04003A36 RID: 14902
		private static readonly SortBy[] ItemQuerySortColumns = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
		};

		// Token: 0x04003A37 RID: 14903
		private static readonly PropertyDefinition[] ItemQueryReturnColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04003A38 RID: 14904
		private static readonly Trace Tracer = ExTraceGlobals.SingleInstanceItemTracer;

		// Token: 0x02000B0C RID: 2828
		// (Invoke) Token: 0x060066B3 RID: 26291
		public delegate string ContentUpdater(string existingContent);

		// Token: 0x02000B0D RID: 2829
		private enum ItemQueryReturnColumnIndex
		{
			// Token: 0x04003A3A RID: 14906
			Id,
			// Token: 0x04003A3B RID: 14907
			LastModifiedTime,
			// Token: 0x04003A3C RID: 14908
			ItemClass
		}
	}
}
