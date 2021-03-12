using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000287 RID: 647
	internal sealed class ConversationFeedLoader
	{
		// Token: 0x060010CB RID: 4299 RVA: 0x00051577 File Offset: 0x0004F777
		public ConversationFeedLoader(MailboxSession mailboxSession, ExTimeZone requestTimeZone)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("requestTimeZone", requestTimeZone);
			this.mailboxSession = mailboxSession;
			this.requestTimeZone = requestTimeZone;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000515A4 File Offset: 0x0004F7A4
		public void LoadConversationFeedItemsIfNecessary(PropertyListForViewRowDeterminer classDeterminer, IList<ConversationType> conversations)
		{
			ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyListForConversation = classDeterminer.GetToServiceObjectPropertyListForConversation();
			if (toServiceObjectPropertyListForConversation == null || toServiceObjectPropertyListForConversation.ResponseShape == null || toServiceObjectPropertyListForConversation.ResponseShape.AdditionalProperties == null)
			{
				return;
			}
			bool flag = false;
			foreach (PropertyPath propertyPath in toServiceObjectPropertyListForConversation.ResponseShape.AdditionalProperties)
			{
				PropertyUri propertyUri = propertyPath as PropertyUri;
				if (propertyUri != null && ConversationFeedLoader.ConversationFeedProperties.Contains(propertyUri.Uri))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			this.LoadConversationFeedItems(conversations);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00051624 File Offset: 0x0004F824
		public void LoadConversationFeedItems(ConversationType conversation, int? initialMemberDocumentId, int[] memberDocumentIds)
		{
			conversation.InternalInitialPost = initialMemberDocumentId;
			conversation.InternalRecentReplys = memberDocumentIds;
			this.LoadConversationFeedItems(new ConversationType[]
			{
				conversation
			});
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00051658 File Offset: 0x0004F858
		private void LoadConversationFeedItems(IList<ConversationType> conversations)
		{
			HashSet<int> hashSet = this.LoadFeedItemsDocumentIds(conversations);
			if (hashSet.Count > 0)
			{
				Dictionary<int, IStorePropertyBag> storeItems = this.LoadStoreItemsForGivenDocumentIds(hashSet);
				this.LoadConversationsWithFeedItems(conversations, storeItems);
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00051688 File Offset: 0x0004F888
		private void LoadConversationsWithFeedItems(IList<ConversationType> conversations, Dictionary<int, IStorePropertyBag> storeItems)
		{
			foreach (ConversationType conversationType in conversations)
			{
				if (conversationType.InternalInitialPost != null)
				{
					conversationType.InitialPost = this.LoadMessageForGivenDocumentId((int)conversationType.InternalInitialPost, storeItems);
				}
				if (conversationType.InternalRecentReplys != null)
				{
					List<MessageType> list = this.LoadMessagesForGivenDocumentIds((int[])conversationType.InternalRecentReplys, storeItems);
					if (list.Count > 0)
					{
						conversationType.RecentReplys = list.ToArray();
					}
					else
					{
						conversationType.RecentReplys = null;
					}
				}
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00051724 File Offset: 0x0004F924
		private MessageType LoadMessageForGivenDocumentId(int documentId, Dictionary<int, IStorePropertyBag> storeItems)
		{
			IStorePropertyBag storePropertyBag = null;
			if (!storeItems.TryGetValue(documentId, out storePropertyBag))
			{
				return null;
			}
			return this.LoadMessageType(storePropertyBag);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00051748 File Offset: 0x0004F948
		private List<MessageType> LoadMessagesForGivenDocumentIds(int[] documentIds, Dictionary<int, IStorePropertyBag> storeItems)
		{
			List<MessageType> list = new List<MessageType>(documentIds.Length);
			foreach (int documentId in documentIds)
			{
				MessageType messageType = this.LoadMessageForGivenDocumentId(documentId, storeItems);
				if (messageType != null)
				{
					list.Add(messageType);
				}
			}
			return list;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005178C File Offset: 0x0004F98C
		private HashSet<int> LoadFeedItemsDocumentIds(IList<ConversationType> conversations)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (ConversationType conversation in conversations)
			{
				int? initialPostDocumentId = this.LoadInitialPostDocumentId(conversation);
				List<int> list = this.LoadRecentReplysDocumentId(conversation, initialPostDocumentId);
				if (initialPostDocumentId != null)
				{
					hashSet.Add(initialPostDocumentId.Value);
				}
				foreach (int item in list)
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00051844 File Offset: 0x0004FA44
		private List<int> LoadRecentReplysDocumentId(ConversationType conversation, int? initialPostDocumentId)
		{
			if (conversation.InternalRecentReplys != null && conversation.InternalRecentReplys is int[])
			{
				int[] array = (int[])conversation.InternalRecentReplys;
				List<int> list = new List<int>(array.Length);
				foreach (int num in array)
				{
					if (list.Count == ConversationFeedLoader.NumberOfRecentReplysToLoad)
					{
						break;
					}
					if (initialPostDocumentId == null || initialPostDocumentId.Value != num)
					{
						list.Add(num);
					}
				}
				if (list.Count > 0)
				{
					list.Reverse();
					conversation.InternalRecentReplys = list.ToArray();
					return list;
				}
			}
			conversation.InternalRecentReplys = null;
			return new List<int>(0);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000518E8 File Offset: 0x0004FAE8
		private int? LoadInitialPostDocumentId(ConversationType conversation)
		{
			int? result = null;
			if (conversation.InternalInitialPost != null && conversation.InternalInitialPost is int)
			{
				result = new int?((int)conversation.InternalInitialPost);
			}
			else
			{
				conversation.InternalInitialPost = null;
			}
			return result;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00051930 File Offset: 0x0004FB30
		private Dictionary<int, IStorePropertyBag> LoadStoreItemsForGivenDocumentIds(HashSet<int> documentIds)
		{
			Dictionary<int, IStorePropertyBag> result;
			using (Folder folder = Folder.Bind(this.mailboxSession, DefaultFolderType.Configuration))
			{
				QueryFilter queryFilter = this.BuildQueryFilter(documentIds);
				PropertyDefinition[] documentViewStorePropertiesToLoad = ConversationFeedLoader.DocumentViewStorePropertiesToLoad;
				using (QueryResult queryResult = folder.ItemQuery(ConversationFeedLoader.DocumentViewItemQueryType, queryFilter, null, documentViewStorePropertiesToLoad))
				{
					Dictionary<int, IStorePropertyBag> dictionary = new Dictionary<int, IStorePropertyBag>(documentIds.Count);
					IStorePropertyBag[] propertyBags;
					do
					{
						propertyBags = queryResult.GetPropertyBags(documentIds.Count);
						foreach (IStorePropertyBag storePropertyBag in propertyBags)
						{
							int valueOrDefault = storePropertyBag.GetValueOrDefault<int>(ItemSchema.DocumentId, -1);
							if (valueOrDefault != -1)
							{
								dictionary.Add(valueOrDefault, storePropertyBag);
							}
						}
					}
					while (propertyBags.Length > 0 && dictionary.Count < documentIds.Count);
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00051A14 File Offset: 0x0004FC14
		private QueryFilter BuildQueryFilter(HashSet<int> documentIds)
		{
			List<QueryFilter> list = new List<QueryFilter>(documentIds.Count);
			foreach (int num in documentIds)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.DocumentId, num));
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00051A8C File Offset: 0x0004FC8C
		private MessageType LoadMessageType(IStorePropertyBag storePropertyBag)
		{
			VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
			MessageType messageType = MessageType.CreateFromStoreObjectType((versionedId != null) ? versionedId.ObjectId.ObjectType : StoreObjectType.Message);
			if (messageType is MeetingMessageType)
			{
				this.AddMeetingMessageProperties(messageType as MeetingMessageType, storePropertyBag);
			}
			messageType.Preview = storePropertyBag.GetValueOrDefault<string>(ItemSchema.Preview, null);
			messageType.DateTimeReceived = this.GetDateTimeString(storePropertyBag, ItemSchema.ReceivedTime);
			messageType.From = this.GetParticipant(storePropertyBag, ItemSchema.From);
			messageType.LikeCount = storePropertyBag.GetValueOrDefault<int>(MessageItemSchema.LikeCount, 0);
			return messageType;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00051B20 File Offset: 0x0004FD20
		private void AddMeetingMessageProperties(MeetingMessageType meetingMessageType, IStorePropertyBag storePropertyBag)
		{
			if (meetingMessageType is MeetingRequestMessageType)
			{
				(meetingMessageType as MeetingRequestMessageType).MeetingRequestType = storePropertyBag.GetValueOrDefault<RequestType>(CalendarItemBaseSchema.MeetingRequestType, RequestType.None);
			}
			meetingMessageType.Sender = this.GetParticipant(storePropertyBag, ItemSchema.Sender);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00051B54 File Offset: 0x0004FD54
		private string GetDateTimeString(IStorePropertyBag storePropertyBag, PropertyDefinition dateTimeProperty)
		{
			ExDateTime valueOrDefault = storePropertyBag.GetValueOrDefault<ExDateTime>(dateTimeProperty, ExDateTime.MinValue);
			if (valueOrDefault != ExDateTime.MinValue)
			{
				return ExDateTimeConverter.ToOffsetXsdDateTime(valueOrDefault, this.requestTimeZone);
			}
			return null;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00051B8C File Offset: 0x0004FD8C
		private SingleRecipientType GetParticipant(IStorePropertyBag storePropertyBag, PropertyDefinition property)
		{
			Participant valueOrDefault = storePropertyBag.GetValueOrDefault<Participant>(property, null);
			if (valueOrDefault != null)
			{
				return StaticParticipantResolver.DefaultInstance.ResolveToSingleRecipientType(valueOrDefault);
			}
			return null;
		}

		// Token: 0x04000C54 RID: 3156
		private static readonly Trace Tracer = ExTraceGlobals.ServiceCommandBaseCallTracer;

		// Token: 0x04000C55 RID: 3157
		private static readonly int NumberOfRecentReplysToLoad = 2;

		// Token: 0x04000C56 RID: 3158
		private static readonly ItemQueryType DocumentViewItemQueryType = ItemQueryType.DocumentIdView | ItemQueryType.PrereadExtendedProperties;

		// Token: 0x04000C57 RID: 3159
		public static readonly HashSet<PropertyUriEnum> ConversationFeedProperties = new HashSet<PropertyUriEnum>
		{
			PropertyUriEnum.ConversationInitialPost,
			PropertyUriEnum.ConversationRecentReplys
		};

		// Token: 0x04000C58 RID: 3160
		private static readonly PropertyDefinition[] DocumentViewStorePropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.DocumentId,
			ItemSchema.ReceivedTime,
			ItemSchema.Preview,
			ItemSchema.Id,
			ItemSchema.From,
			ItemSchema.Sender,
			CalendarItemBaseSchema.MeetingRequestType,
			MessageItemSchema.LikeCount
		};

		// Token: 0x04000C59 RID: 3161
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000C5A RID: 3162
		private readonly ExTimeZone requestTimeZone;
	}
}
