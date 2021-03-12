using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000242 RID: 578
	internal class FindFlowConversationItem : ServiceCommand<FindFlowConversationItemResponse>
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
		public FindFlowConversationItem(CallContext callContext, BaseFolderId folderId, string flowConversationId, int requestedItemCount) : base(callContext)
		{
			this.folderId = folderId;
			this.flowConversationId = flowConversationId;
			this.requestedItemCount = Math.Min((long)requestedItemCount, (long)((ulong)FindFlowConversationItem.MAX_ITEMS));
			this.replyAllExtractor = new ReplyAllExtractor(base.MailboxIdentityMailboxSession, XSOFactory.Default);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0004DD44 File Offset: 0x0004BF44
		protected override FindFlowConversationItemResponse InternalExecute()
		{
			QueryFilter flowConversationFilter = GetFlowConversation.GetFlowConversationFilter(this.folderId, base.MailboxIdentityMailboxSession);
			IdAndSession folderIdAndSession = GetFlowConversation.GetFolderIdAndSession(this.folderId, base.MailboxIdentityMailboxSession, base.IdConverter);
			SortBy sortBy = new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending);
			List<FlowItem> list = new List<FlowItem>();
			ConversationFactory conversationFactory = new ConversationFactory(base.MailboxIdentityMailboxSession);
			using (Folder folder = Folder.Bind((MailboxSession)folderIdAndSession.Session, folderIdAndSession.Id))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, flowConversationFilter, new SortBy[]
				{
					sortBy
				}, FindFlowConversationItem.requiredProperties))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(10000);
					foreach (IStorePropertyBag storePropertyBag in propertyBags)
					{
						if ((long)list.Count == this.requestedItemCount)
						{
							break;
						}
						string text = this.GenerateParticipantsHash(storePropertyBag);
						if (text.Equals(this.flowConversationId))
						{
							FlowItem flowItem = new FlowItem();
							ExDateTime valueOrDefault = storePropertyBag.GetValueOrDefault<ExDateTime>(ItemSchema.ReceivedTime, ExDateTime.Now);
							bool valueOrDefault2 = storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsRead, true);
							IParticipant valueOrDefault3 = storePropertyBag.GetValueOrDefault<IParticipant>(ItemSchema.From, null);
							VersionedId valueOrDefault4 = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
							ConversationId valueOrDefault5 = storePropertyBag.GetValueOrDefault<ConversationId>(ItemSchema.ConversationId, null);
							flowItem.ItemId = IdConverter.ConvertStoreItemIdToItemId(valueOrDefault4, base.MailboxIdentityMailboxSession);
							flowItem.Sender = FindFlowConversationItem.ConvertParticipantToEmailAddressWrapper(valueOrDefault3);
							flowItem.IsRead = valueOrDefault2;
							flowItem.ReceivedTimeUtc = ExDateTimeConverter.ToSoapHeaderTimeZoneRelatedXsdDateTime(valueOrDefault);
							Conversation key = conversationFactory.CreateConversation(valueOrDefault5, FindFlowConversationItem.ConversationCreatorRelevantProperties);
							if (!this.conversationMap.ContainsKey(key))
							{
								this.conversationMap[key] = new List<StoreObjectId>();
							}
							this.conversationMap[key].Add(valueOrDefault4.ObjectId);
							this.flowItemsMap[valueOrDefault4.ObjectId] = flowItem;
							list.Add(flowItem);
						}
					}
				}
			}
			foreach (KeyValuePair<Conversation, List<StoreObjectId>> keyValuePair in this.conversationMap)
			{
				Conversation key2 = keyValuePair.Key;
				List<StoreObjectId> value = keyValuePair.Value;
				key2.LoadItemParts(value);
				foreach (StoreObjectId storeObjectId in value)
				{
					ItemPart itemPart = key2.GetItemPart(storeObjectId);
					this.flowItemsMap[storeObjectId].ItemBody = itemPart.BodyPart;
				}
			}
			return new FindFlowConversationItemResponse
			{
				Items = list.ToArray()
			};
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0004E05C File Offset: 0x0004C25C
		private string GenerateParticipantsHash(IStorePropertyBag propertyBag)
		{
			ParticipantSet participantSet = this.replyAllExtractor.RetrieveReplyAllParticipants(propertyBag);
			List<string> list = new List<string>(participantSet.Count);
			foreach (IParticipant participant in participantSet)
			{
				list.Add(participant.DisplayName.ToLower());
			}
			StringBuilder stringBuilder = new StringBuilder(list.Count * 255);
			list.Sort(StringComparer.InvariantCultureIgnoreCase);
			foreach (string value in list)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			string result;
			using (SHA256CryptoServiceProvider sha256CryptoServiceProvider = new SHA256CryptoServiceProvider())
			{
				result = Convert.ToBase64String(sha256CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(stringBuilder.ToString())));
			}
			return result;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0004E170 File Offset: 0x0004C370
		private IdAndSession GetFolderIdAndSession()
		{
			return base.IdConverter.ConvertFolderIdToIdAndSession(this.folderId, IdConverter.ConvertOption.IgnoreChangeKey);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0004E184 File Offset: 0x0004C384
		private static EmailAddressWrapper ConvertParticipantToEmailAddressWrapper(IParticipant participant)
		{
			ParticipantInformationDictionary participantInformation = EWSSettings.ParticipantInformation;
			ParticipantInformation participantInformationOrCreateNew = participantInformation.GetParticipantInformationOrCreateNew(participant);
			return EmailAddressWrapper.FromParticipantInformation(participantInformationOrCreateNew);
		}

		// Token: 0x04000C0C RID: 3084
		private static readonly PropertyDefinition[] requiredProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.From,
			ItemSchema.ReceivedTime,
			MessageItemSchema.IsRead,
			ItemSchema.ConversationId,
			StoreObjectSchema.ParentItemId
		};

		// Token: 0x04000C0D RID: 3085
		private static PropertyDefinition[] ConversationCreatorRelevantProperties = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x04000C0E RID: 3086
		private static readonly uint MAX_ITEMS = 50U;

		// Token: 0x04000C0F RID: 3087
		private readonly ReplyAllExtractor replyAllExtractor;

		// Token: 0x04000C10 RID: 3088
		private readonly BaseFolderId folderId;

		// Token: 0x04000C11 RID: 3089
		private readonly string flowConversationId;

		// Token: 0x04000C12 RID: 3090
		private readonly Dictionary<Conversation, List<StoreObjectId>> conversationMap = new Dictionary<Conversation, List<StoreObjectId>>();

		// Token: 0x04000C13 RID: 3091
		private readonly Dictionary<StoreObjectId, FlowItem> flowItemsMap = new Dictionary<StoreObjectId, FlowItem>();

		// Token: 0x04000C14 RID: 3092
		private readonly long requestedItemCount;
	}
}
