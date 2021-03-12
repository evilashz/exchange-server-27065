using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB1 RID: 3761
	internal class ContactProvider : ExchangeServiceProvider
	{
		// Token: 0x060061EC RID: 25068 RVA: 0x00132969 File Offset: 0x00130B69
		public ContactProvider(IExchangeService exchangeService) : base(exchangeService)
		{
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x00132974 File Offset: 0x00130B74
		public static Contact ItemTypeToEntity(ItemType itemType, IList<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("itemType", itemType);
			ArgumentValidator.ThrowIfNull("properties", properties);
			Contact contact = EwsServiceObjectFactory.CreateEntity<Contact>(itemType);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				propertyDefinition.EwsPropertyProvider.GetPropertyFromDataSource(contact, propertyDefinition, itemType);
			}
			return contact;
		}

		// Token: 0x060061EE RID: 25070 RVA: 0x001329E4 File Offset: 0x00130BE4
		public Contact Read(string id, ContactQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			return this.InternalRead(EwsIdConverter.ODataIdToEwsId(id), queryAdapter);
		}

		// Token: 0x060061EF RID: 25071 RVA: 0x00132A00 File Offset: 0x00130C00
		public IFindEntitiesResult<Contact> Find(string parentFolderId, ContactQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parentFolderId", parentFolderId);
			string id = EwsIdConverter.ODataIdToEwsId(parentFolderId);
			queryAdapter = (queryAdapter ?? ContactQueryAdapter.Default);
			BaseFolderId baseFolderId = EwsIdConverter.CreateFolderIdFromEwsId(id);
			FindItemRequest request = new FindItemRequest
			{
				ParentFolderIds = new BaseFolderId[]
				{
					baseFolderId
				},
				Traversal = ItemQueryTraversal.Shallow,
				Paging = queryAdapter.GetPaging(),
				Restriction = queryAdapter.GetRestriction(),
				SortOrder = queryAdapter.GetSortOrder(),
				ItemShape = queryAdapter.GetResponseShape(true)
			};
			FindItemResponse findItemResponse = base.ExchangeService.FindItem(request, null);
			FindItemResponseMessage findItemResponseMessage = findItemResponse.ResponseMessages.Items[0] as FindItemResponseMessage;
			int num = findItemResponseMessage.ParentFolder.Items.Length;
			List<Contact> list = new List<Contact>();
			for (int i = 0; i < num; i++)
			{
				ContactItemType contactItemType = findItemResponseMessage.ParentFolder.Items[i] as ContactItemType;
				if (contactItemType != null)
				{
					if (queryAdapter.FindNeedsReread)
					{
						list.Add(this.InternalRead(contactItemType.ItemId.Id, queryAdapter));
					}
					else
					{
						list.Add(ContactProvider.ItemTypeToEntity(contactItemType, queryAdapter.RequestedProperties));
					}
				}
			}
			return new FindEntitiesResult<Contact>(list, findItemResponseMessage.ParentFolder.TotalItemsInView);
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x00132B40 File Offset: 0x00130D40
		public Contact Create(string parentFolderId, Contact template)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parentFolderId", parentFolderId);
			ArgumentValidator.ThrowIfNull("template", template);
			ItemType itemType = EwsServiceObjectFactory.CreateServiceObject<ItemType>(template);
			CreateItemRequest createItemRequest = new CreateItemRequest();
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
			createItemRequest.ItemShape = ContactQueryAdapter.Default.GetResponseShape(false);
			Contact result;
			using (IDisposableResponse<CreateItemResponse> disposableResponse = base.ExchangeService.CreateItem(createItemRequest, null))
			{
				ItemInfoResponseMessage itemInfoResponseMessage = disposableResponse.Response.ResponseMessages.Items[0] as ItemInfoResponseMessage;
				ItemType itemType2 = itemInfoResponseMessage.Items.Items[0];
				result = ContactProvider.ItemTypeToEntity(itemType2, ContactQueryAdapter.Default.RequestedProperties);
			}
			return result;
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x00132C6C File Offset: 0x00130E6C
		public Contact Update(string id, Contact changeEntity, string changeKey)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("id", id);
			ArgumentValidator.ThrowIfNull("changeEntity", changeEntity);
			string id2 = EwsIdConverter.ODataIdToEwsId(id);
			ItemId itemId;
			if (string.IsNullOrEmpty(changeKey))
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
				itemId = itemType.ItemId;
			}
			else
			{
				itemId = new ItemId(id2, changeKey);
			}
			UpdateItemRequest updateItemRequest = new UpdateItemRequest
			{
				ItemChanges = new ItemChange[]
				{
					new ItemChange
					{
						ItemId = itemId
					}
				}
			};
			List<PropertyUpdate> list = new List<PropertyUpdate>();
			foreach (PropertyDefinition propertyDefinition in changeEntity.PropertyBag.GetProperties())
			{
				if (propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanUpdate))
				{
					ContactItemType contactItemType = EwsServiceObjectFactory.CreateServiceObject<ContactItemType>(changeEntity);
					EwsPropertyProvider ewsPropertyProvider = propertyDefinition.EwsPropertyProvider.GetEwsPropertyProvider(changeEntity.Schema);
					if (ewsPropertyProvider.IsMultiValueProperty)
					{
						List<PropertyUpdate> propertyUpdateList = ewsPropertyProvider.GetPropertyUpdateList(changeEntity, propertyDefinition, changeEntity[propertyDefinition]);
						list.AddRange(propertyUpdateList);
					}
					else
					{
						ewsPropertyProvider.SetPropertyToDataSource(changeEntity, propertyDefinition, contactItemType);
						PropertyUpdate propertyUpdate = ewsPropertyProvider.GetPropertyUpdate(contactItemType, changeEntity[propertyDefinition]);
						list.Add(propertyUpdate);
					}
				}
			}
			updateItemRequest.ItemChanges[0].PropertyUpdates = list.ToArray();
			base.ExchangeService.UpdateItem(updateItemRequest, null);
			return this.Read(id, null);
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x00132E2C File Offset: 0x0013102C
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

		// Token: 0x060061F3 RID: 25075 RVA: 0x00132E80 File Offset: 0x00131080
		private Contact InternalRead(string ewsId, ContactQueryAdapter queryAdapter = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("ewsId", ewsId);
			queryAdapter = (queryAdapter ?? ContactQueryAdapter.Default);
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
			ContactItemType itemType = itemInfoResponseMessage.Items.Items[0] as ContactItemType;
			return ContactProvider.ItemTypeToEntity(itemType, queryAdapter.RequestedProperties);
		}
	}
}
