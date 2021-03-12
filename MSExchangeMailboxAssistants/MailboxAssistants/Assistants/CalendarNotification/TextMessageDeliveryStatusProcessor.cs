using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000F8 RID: 248
	internal sealed class TextMessageDeliveryStatusProcessor
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x000436F6 File Offset: 0x000418F6
		public TextMessageDeliveryStatusProcessor(DatabaseInfo databaseInfo)
		{
			this.DatabaseInfo = databaseInfo;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00043705 File Offset: 0x00041905
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0004370D File Offset: 0x0004190D
		private DatabaseInfo DatabaseInfo { get; set; }

		// Token: 0x06000A39 RID: 2617 RVA: 0x00043718 File Offset: 0x00041918
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (ObjectType.MAPI_MESSAGE != mapiEvent.ItemType)
			{
				return false;
			}
			if (((MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved | MapiEventTypeFlags.ObjectCopied) & mapiEvent.EventMask) == (MapiEventTypeFlags)0)
			{
				return false;
			}
			if (!ObjectClass.IsSmsMessage(mapiEvent.ObjectClass))
			{
				return false;
			}
			MailboxData fromCache = MailboxData.GetFromCache(mapiEvent.MailboxGuid);
			if (fromCache == null)
			{
				return true;
			}
			StoreObjectId storeObjectId = null;
			using (fromCache.CreateReadLock())
			{
				storeObjectId = fromCache.DefaultOutboxFolderId;
			}
			if (storeObjectId == null)
			{
				return false;
			}
			StoreObjectId objB = (mapiEvent.ParentEntryId == null || mapiEvent.ParentEntryId.Length == 0) ? null : StoreObjectId.FromProviderSpecificId(mapiEvent.ParentEntryId);
			StoreObjectId objB2 = (mapiEvent.OldParentEntryId == null || mapiEvent.OldParentEntryId.Length == 0) ? null : StoreObjectId.FromProviderSpecificId(mapiEvent.OldParentEntryId);
			return (((MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectCopied) & mapiEvent.EventMask) == (MapiEventTypeFlags)0 || object.Equals(storeObjectId, objB)) && ((MapiEventTypeFlags.ObjectMoved & mapiEvent.EventMask) == (MapiEventTypeFlags)0 || object.Equals(storeObjectId, objB2) || object.Equals(storeObjectId, objB));
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004380C File Offset: 0x00041A0C
		public void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			if (MailboxData.GetFromCache(mapiEvent.MailboxGuid) == null)
			{
				using (MailboxData.CachedStateWriter cachedStateWriter = new MailboxData.CachedStateWriter(mapiEvent.MailboxGuid))
				{
					if (cachedStateWriter.Get() == null)
					{
						MailboxData mailboxData = new MailboxData(itemStore);
						cachedStateWriter.Set(mailboxData);
					}
				}
			}
			MessageItem messageItem = item as MessageItem;
			if (messageItem == null)
			{
				return;
			}
			int? valueAsNullable = messageItem.GetValueAsNullable<int>(ItemSchema.InternetMessageIdHash);
			if (valueAsNullable == null)
			{
				return;
			}
			string valueOrDefault = messageItem.GetValueOrDefault<string>(ItemSchema.InternetMessageId, null);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				return;
			}
			int? valueAsNullable2 = messageItem.GetValueAsNullable<int>(MessageItemSchema.TextMessageDeliveryStatus);
			if (valueAsNullable2 == null)
			{
				return;
			}
			List<IStorePropertyBag> list = new List<IStorePropertyBag>(1);
			using (Folder folder = Folder.Bind(itemStore, DefaultFolderType.SentItems))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					new SortBy(ItemSchema.InternetMessageIdHash, SortOrder.Ascending)
				}, new PropertyDefinition[]
				{
					ItemSchema.InternetMessageIdHash,
					ItemSchema.Id,
					ItemSchema.InternetMessageId,
					MessageItemSchema.TextMessageDeliveryStatus
				}))
				{
					if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.InternetMessageIdHash, valueAsNullable)))
					{
						return;
					}
					bool flag = false;
					while (!flag)
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
						if (propertyBags == null || 0 >= propertyBags.Length)
						{
							break;
						}
						for (int i = 0; i < propertyBags.Length; i++)
						{
							int? num = propertyBags[i].TryGetProperty(ItemSchema.InternetMessageIdHash) as int?;
							if (num == null)
							{
								break;
							}
							if (num.Value != valueAsNullable)
							{
								flag = true;
								break;
							}
							string a = propertyBags[i].TryGetProperty(ItemSchema.InternetMessageId) as string;
							if (string.Equals(a, valueOrDefault))
							{
								list.Add(propertyBags[i]);
							}
						}
					}
				}
			}
			foreach (IStorePropertyBag storePropertyBag in list)
			{
				int? num2 = storePropertyBag.TryGetProperty(MessageItemSchema.TextMessageDeliveryStatus) as int?;
				if (num2 == null || !(num2 >= valueAsNullable2))
				{
					VersionedId versionedId = storePropertyBag.TryGetProperty(ItemSchema.Id) as VersionedId;
					if (versionedId != null && versionedId.ObjectId != null)
					{
						using (MessageItem messageItem2 = MessageItem.Bind(itemStore, versionedId.ObjectId))
						{
							messageItem2.OpenAsReadWrite();
							messageItem2.SetProperties(TextMessageDeliveryStatusProcessor.propertyDeliveryStatus, new object[]
							{
								valueAsNullable2
							});
							messageItem2.Save(SaveMode.ResolveConflicts);
						}
					}
				}
			}
		}

		// Token: 0x040006A6 RID: 1702
		private const int DefaultCountOfRowsToQuery = 1;

		// Token: 0x040006A7 RID: 1703
		private static readonly PropertyDefinition[] propertyDeliveryStatus = new PropertyDefinition[]
		{
			MessageItemSchema.TextMessageDeliveryStatus
		};
	}
}
