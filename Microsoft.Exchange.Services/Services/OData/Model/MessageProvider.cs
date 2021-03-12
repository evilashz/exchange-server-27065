using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EAD RID: 3757
	internal class MessageProvider : ExchangeServiceProvider
	{
		// Token: 0x060061CC RID: 25036 RVA: 0x0013166F File Offset: 0x0012F86F
		public MessageProvider(IExchangeService exchangeService) : base(exchangeService)
		{
		}

		// Token: 0x060061CD RID: 25037 RVA: 0x00131678 File Offset: 0x0012F878
		public static Message ItemTypeToEntity(ItemType itemType, IList<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("itemType", itemType);
			ArgumentValidator.ThrowIfNull("properties", properties);
			Message message = EwsServiceObjectFactory.CreateEntity<Message>(itemType);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				propertyDefinition.EwsPropertyProvider.GetPropertyFromDataSource(message, propertyDefinition, itemType);
			}
			return message;
		}

		// Token: 0x060061CE RID: 25038 RVA: 0x001316E8 File Offset: 0x0012F8E8
		public Message Read(string id, MessageQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string ewsId = EwsIdConverter.ODataIdToEwsId(id);
			return this.InternalRead(ewsId, queryAdapter);
		}

		// Token: 0x060061CF RID: 25039 RVA: 0x00131710 File Offset: 0x0012F910
		public Message Create(string parentFolderId, Message template, MessageDisposition messageDisposition)
		{
			ArgumentValidator.ThrowIfNull("template", template);
			CreateItemRequest createItemRequest = new CreateItemRequest();
			ItemType itemType = EwsServiceObjectFactory.CreateServiceObject<ItemType>(template);
			foreach (PropertyDefinition propertyDefinition in template.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanCreate))
				{
					propertyDefinition.EwsPropertyProvider.SetPropertyToDataSource(template, propertyDefinition, itemType);
				}
			}
			createItemRequest.Items = new NonEmptyArrayOfAllItemsType();
			createItemRequest.Items.Add(itemType);
			if (!string.IsNullOrEmpty(parentFolderId))
			{
				createItemRequest.SavedItemFolderId = new TargetFolderId(EwsIdConverter.CreateFolderIdFromEwsId(EwsIdConverter.ODataIdToEwsId(parentFolderId)));
			}
			createItemRequest.ItemShape = MessageQueryAdapter.Default.GetResponseShape(false);
			createItemRequest.MessageDisposition = messageDisposition.ToString();
			Message message = template;
			using (IDisposableResponse<CreateItemResponse> disposableResponse = base.ExchangeService.CreateItem(createItemRequest, null))
			{
				ItemInfoResponseMessage itemInfoResponseMessage = disposableResponse.Response.ResponseMessages.Items[0] as ItemInfoResponseMessage;
				ItemType itemType2 = itemInfoResponseMessage.Items.Items[0];
				message.Id = EntitySchema.Id.ToString();
				if (itemType2 != null)
				{
					message = MessageProvider.ItemTypeToEntity(itemType2, MessageQueryAdapter.Default.RequestedProperties);
				}
			}
			return message;
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x00131858 File Offset: 0x0012FA58
		public void Delete(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			DeleteItemRequest deleteItemRequest = new DeleteItemRequest();
			deleteItemRequest.DeleteType = DisposalType.SoftDelete;
			deleteItemRequest.Ids = new BaseItemId[]
			{
				new ItemId(id2, null)
			};
			base.ExchangeService.DeleteItem(deleteItemRequest, null);
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x001318AC File Offset: 0x0012FAAC
		public Message Update(string id, Message changeEntity, string changeKey)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNull("changeEntity", changeEntity);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			UpdateItemRequest updateItemRequest = new UpdateItemRequest();
			ItemChange itemChange = new ItemChange();
			if (!string.IsNullOrEmpty(changeKey))
			{
				itemChange.ItemId = new ItemId(id2, changeKey);
			}
			else
			{
				GetItemRequest request = new GetItemRequest
				{
					Ids = new BaseItemId[]
					{
						new ItemId(id2, null)
					},
					ItemShape = MessageQueryAdapter.IdOnlyResponseType
				};
				GetItemResponse item = base.ExchangeService.GetItem(request, null);
				ItemInfoResponseMessage itemInfoResponseMessage = item.ResponseMessages.Items[0] as ItemInfoResponseMessage;
				ItemType itemType = itemInfoResponseMessage.Items.Items[0];
				itemChange.ItemId = itemType.ItemId;
			}
			updateItemRequest.ItemChanges = new ItemChange[]
			{
				itemChange
			};
			updateItemRequest.MessageDisposition = MessageDisposition.SaveOnly.ToString();
			List<PropertyUpdate> list = new List<PropertyUpdate>();
			foreach (PropertyDefinition propertyDefinition in changeEntity.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanUpdate))
				{
					EwsPropertyProvider ewsPropertyProvider = propertyDefinition.EwsPropertyProvider.GetEwsPropertyProvider(changeEntity.Schema);
					ItemType itemType2 = EwsServiceObjectFactory.CreateServiceObject<ItemType>(changeEntity);
					propertyDefinition.EwsPropertyProvider.SetPropertyToDataSource(changeEntity, propertyDefinition, itemType2);
					PropertyUpdate propertyUpdate = ewsPropertyProvider.GetPropertyUpdate(itemType2, changeEntity[propertyDefinition]);
					list.Add(propertyUpdate);
				}
			}
			itemChange.PropertyUpdates = list.ToArray();
			base.ExchangeService.UpdateItem(updateItemRequest, null);
			return this.Read(id, null);
		}

		// Token: 0x060061D2 RID: 25042 RVA: 0x00131A48 File Offset: 0x0012FC48
		public IFindEntitiesResult<Message> Find(string parentFolderId, MessageQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parentFolderId", parentFolderId);
			string id = EwsIdConverter.ODataIdToEwsId(parentFolderId);
			queryAdapter = (queryAdapter ?? MessageQueryAdapter.Default);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id);
			FindItemRequest findItemRequest = new FindItemRequest();
			findItemRequest.ParentFolderIds = new BaseFolderId[]
			{
				baseFolderId
			};
			findItemRequest.Traversal = ItemQueryTraversal.Shallow;
			findItemRequest.Paging = queryAdapter.GetPaging();
			findItemRequest.Restriction = queryAdapter.GetRestriction();
			findItemRequest.SortOrder = queryAdapter.GetSortOrder();
			findItemRequest.ItemShape = queryAdapter.GetResponseShape(true);
			FindItemResponse findItemResponse = base.ExchangeService.FindItem(findItemRequest, null);
			FindItemResponseMessage findItemResponseMessage = findItemResponse.ResponseMessages.Items[0] as FindItemResponseMessage;
			Message[] array = new Message[findItemResponseMessage.ParentFolder.Items.Length];
			for (int i = 0; i < findItemResponseMessage.ParentFolder.Items.Length; i++)
			{
				ItemType itemType = findItemResponseMessage.ParentFolder.Items[i];
				if (queryAdapter.FindNeedsReread)
				{
					array[i] = this.InternalRead(itemType.ItemId.Id, queryAdapter);
				}
				else
				{
					array[i] = MessageProvider.ItemTypeToEntity(itemType, queryAdapter.RequestedProperties);
					if (queryAdapter.ODataQueryOptions.Expands(ItemSchema.Attachments.Name))
					{
						array[i].Attachments = this.ExpandAttachment(itemType);
					}
				}
			}
			return new FindEntitiesResult<Message>(array, findItemResponseMessage.ParentFolder.TotalItemsInView);
		}

		// Token: 0x060061D3 RID: 25043 RVA: 0x00131BA8 File Offset: 0x0012FDA8
		public Message Copy(string id, string destinationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNullOrEmpty("destinationId", destinationId);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			string id3 = EwsIdConverter.ODataIdToEwsId(destinationId);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id3);
			CopyItemRequest copyItemRequest = new CopyItemRequest();
			copyItemRequest.Ids = new BaseItemId[]
			{
				new ItemId
				{
					Id = id2
				}
			};
			copyItemRequest.ToFolderId = new TargetFolderId(baseFolderId);
			CopyItemResponse copyItemResponse = base.ExchangeService.CopyItem(copyItemRequest, null);
			ItemInfoResponseMessage itemInfoResponseMessage = copyItemResponse.ResponseMessages.Items[0] as ItemInfoResponseMessage;
			ItemType itemType = itemInfoResponseMessage.Items.Items[0];
			return this.InternalRead(itemType.ItemId.Id, null);
		}

		// Token: 0x060061D4 RID: 25044 RVA: 0x00131C64 File Offset: 0x0012FE64
		public Message Move(string id, string destinationId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNullOrEmpty("destinationId", destinationId);
			string text = EwsIdConverter.ODataIdToEwsId(id);
			string id2 = EwsIdConverter.ODataIdToEwsId(destinationId);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id2);
			MoveItemRequest moveItemRequest = new MoveItemRequest();
			moveItemRequest.Ids = new BaseItemId[]
			{
				new ItemId
				{
					Id = text
				}
			};
			moveItemRequest.ToFolderId = new TargetFolderId(baseFolderId);
			MoveItemResponse moveItemResponse = base.ExchangeService.MoveItem(moveItemRequest, null);
			ItemInfoResponseMessage itemInfoResponseMessage = moveItemResponse.ResponseMessages.Items[0] as ItemInfoResponseMessage;
			ItemType itemType = itemInfoResponseMessage.Items.Items[0];
			return this.InternalRead((itemType != null) ? itemType.ItemId.Id : text, null);
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x00131D30 File Offset: 0x0012FF30
		public Message PerformMessageResponseAction(string id, MessageResponseType responseType, bool send, string comment, Recipient[] toRecipients)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			GetItemRequest request = new GetItemRequest
			{
				Ids = new BaseItemId[]
				{
					new ItemId(id2, null)
				},
				ItemShape = MessageQueryAdapter.IdOnlyResponseType
			};
			GetItemResponse item = base.ExchangeService.GetItem(request, null);
			ItemInfoResponseMessage itemInfoResponseMessage = item.ResponseMessages.Items[0] as ItemInfoResponseMessage;
			ItemType itemType = itemInfoResponseMessage.Items.Items[0];
			SmartResponseType smartResponseType = null;
			switch (responseType)
			{
			case MessageResponseType.Reply:
				smartResponseType = new ReplyToItemType();
				break;
			case MessageResponseType.ReplyAll:
				smartResponseType = new ReplyAllToItemType();
				break;
			case MessageResponseType.Forward:
				smartResponseType = new ForwardItemType();
				break;
			}
			smartResponseType.ReferenceItemId = itemType.ItemId;
			CreateItemRequest createItemRequest = new CreateItemRequest();
			createItemRequest.Items = new NonEmptyArrayOfAllItemsType();
			createItemRequest.Items.Add(smartResponseType);
			if (send)
			{
				if (!string.IsNullOrEmpty(comment))
				{
					smartResponseType.NewBodyContent = new BodyContentType
					{
						BodyType = BodyType.HTML,
						Value = comment
					};
				}
				if (responseType == MessageResponseType.Forward)
				{
					if (toRecipients == null || toRecipients.Length == 0)
					{
						throw new ArgumentNullException("toRecipients");
					}
					smartResponseType.ToRecipients = Array.ConvertAll<Recipient, EmailAddressWrapper>(toRecipients, (Recipient x) => x.ToEmailAddressWrapper());
				}
				createItemRequest.MessageDisposition = MessageDisposition.SendAndSaveCopy.ToString();
				createItemRequest.ItemShape = MessageQueryAdapter.IdOnlyResponseType;
			}
			else
			{
				createItemRequest.MessageDisposition = MessageDisposition.SaveOnly.ToString();
				createItemRequest.SavedItemFolderId = new TargetFolderId(new DistinguishedFolderId
				{
					Id = DistinguishedFolderIdName.drafts
				});
				createItemRequest.ItemShape = MessageQueryAdapter.Default.GetResponseShape(false);
			}
			Message result = null;
			using (IDisposableResponse<CreateItemResponse> disposableResponse = base.ExchangeService.CreateItem(createItemRequest, null))
			{
				ItemInfoResponseMessage itemInfoResponseMessage2 = disposableResponse.Response.ResponseMessages.Items[0] as ItemInfoResponseMessage;
				ItemType itemType2 = itemInfoResponseMessage2.Items.Items[0];
				if (!send)
				{
					result = MessageProvider.ItemTypeToEntity(itemType2, MessageQueryAdapter.Default.RequestedProperties);
				}
			}
			return result;
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x00131F5C File Offset: 0x0013015C
		public void SendDraft(string id)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			GetItemRequest request = new GetItemRequest
			{
				Ids = new BaseItemId[]
				{
					new ItemId(id2, null)
				},
				ItemShape = MessageQueryAdapter.IdOnlyResponseType
			};
			GetItemResponse item = base.ExchangeService.GetItem(request, null);
			ItemInfoResponseMessage itemInfoResponseMessage = item.ResponseMessages.Items[0] as ItemInfoResponseMessage;
			ItemType itemType = itemInfoResponseMessage.Items.Items[0];
			SendItemRequest request2 = new SendItemRequest
			{
				Ids = new BaseItemId[]
				{
					itemType.ItemId
				},
				SaveItemToFolder = true
			};
			base.ExchangeService.SendItem(request2, null);
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0013201C File Offset: 0x0013021C
		private Message InternalRead(string ewsId, MessageQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ewsId", ewsId);
			queryAdapter = (queryAdapter ?? MessageQueryAdapter.Default);
			GetItemRequest request = new GetItemRequest
			{
				Ids = new BaseItemId[]
				{
					new ItemId(ewsId, null)
				},
				ItemShape = queryAdapter.GetResponseShape(false)
			};
			GetItemResponse item = base.ExchangeService.GetItem(request, null);
			ItemInfoResponseMessage itemInfoResponseMessage = item.ResponseMessages.Items[0] as ItemInfoResponseMessage;
			ItemType itemType = itemInfoResponseMessage.Items.Items[0];
			Message message = MessageProvider.ItemTypeToEntity(itemType, queryAdapter.RequestedProperties);
			if (queryAdapter.ODataQueryOptions.Expands(ItemSchema.Attachments.Name))
			{
				message.Attachments = this.ExpandAttachment(itemType);
			}
			return message;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x001320D8 File Offset: 0x001302D8
		private IEnumerable<Attachment> ExpandAttachment(ItemType item)
		{
			AttachmentProvider attachmentProvider = new AttachmentProvider(base.ExchangeService);
			if (item.HasAttachments == null || item.HasAttachments.Value)
			{
				return attachmentProvider.Find(item.ItemId.Id, null);
			}
			return MessageProvider.emptyAttachmentsList;
		}

		// Token: 0x040034DF RID: 13535
		private static readonly List<Attachment> emptyAttachmentsList = new List<Attachment>();
	}
}
