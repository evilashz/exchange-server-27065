using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A1 RID: 1185
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ArrayOfResponseMessagesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ArrayOfResponseMessages
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000A3F86 File Offset: 0x000A2186
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x000A3F93 File Offset: 0x000A2193
		[XmlElement("SendNotificationResponseMessage", typeof(SendNotificationResponseMessage))]
		[XmlElement("ExecuteDiagnosticMethodResponseMessage", typeof(ExecuteDiagnosticMethodResponseMessage))]
		[XmlElement("SyncFolderHierarchyResponseMessage", typeof(SyncFolderHierarchyResponseMessage))]
		[XmlElement("FindMailboxStatisticsByKeywordsResponseMessage", typeof(FindMailboxStatisticsByKeywordsResponseMessage))]
		[XmlElement("SearchMailboxesResponseMessage", typeof(SearchMailboxesResponseMessage))]
		[XmlElement("ConvertIdResponseMessage", typeof(ConvertIdResponseMessage))]
		[XmlElement("ApplyConversationActionResponseMessage", typeof(ApplyConversationActionResponseMessage))]
		[XmlElement("FindPeopleResponseMessage", typeof(FindPeopleResponseMessage))]
		[XmlElement("GetPersonaResponseMessage", typeof(GetPersonaResponseMessage))]
		[XmlElement("FindFolderResponseMessage", typeof(FindFolderResponseMessage))]
		[XmlElement("MarkAllItemsAsReadResponseMessage", typeof(ResponseMessage))]
		[XmlElement("GetUserPhotoResponseMessage", typeof(GetUserPhotoResponseMessage))]
		[XmlElement("GetPeopleICommunicateWithResponseMessage", typeof(GetPeopleICommunicateWithResponseMessage))]
		[XmlElement("CreateManagedFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("CreateFolderPathResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("UploadItemsResponseMessage", typeof(UploadItemsResponseMessage))]
		[XmlElement("CreateUserConfigurationResponseMessage", typeof(ResponseMessage))]
		[XmlElement("ExportItemsResponseMessage", typeof(ExportItemsResponseMessage))]
		[XmlElement("SetClientExtensionResponseMessage", typeof(ResponseMessage))]
		[XmlElement("DeleteFolderResponseMessage", typeof(ResponseMessage))]
		[XmlElement("DeleteItemResponseMessage", typeof(DeleteItemResponseMessage))]
		[XmlElement("EmptyFolderResponseMessage", typeof(ResponseMessage))]
		[XmlElement("CreateFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("CopyItemResponseMessage", typeof(ItemInfoResponseMessage))]
		[XmlElement("GetStreamingEventsResponseMessage", typeof(GetStreamingEventsResponseMessage))]
		[XmlElement("CreateItemResponseMessage", typeof(ItemInfoResponseMessage))]
		[XmlElement("MarkAsJunkResponseMessage", typeof(MarkAsJunkResponseMessage))]
		[XmlElement("SubscribeResponseMessage", typeof(SubscribeResponseMessage))]
		[XmlElement("UnsubscribeResponseMessage", typeof(ResponseMessage))]
		[XmlElement("UpdateFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("UpdateItemResponseMessage", typeof(UpdateItemResponseMessage))]
		[XmlElement("PostModernGroupItemResponseMessage", typeof(ItemInfoResponseMessage))]
		[XmlElement("UpdateItemInRecoverableItemsResponseMessage", typeof(UpdateItemInRecoverableItemsResponseMessage))]
		[XmlElement("UpdateMailboxAssociationResponseMessage", typeof(ResponseMessage))]
		[XmlElement("UpdateGroupMailboxResponseMessage", typeof(ResponseMessage))]
		[XmlElement("DeleteUserConfigurationResponseMessage", typeof(ResponseMessage))]
		[XmlElement("ExpandDLResponseMessage", typeof(ExpandDLResponseMessage))]
		[XmlElement("GetConversationItemsResponseMessage", typeof(GetConversationItemsResponseMessage))]
		[XmlElement("FindItemResponseMessage", typeof(FindItemResponseMessage))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[DataMember]
		[XmlElement("GetEventsResponseMessage", typeof(GetEventsResponseMessage))]
		[XmlElement("GetClientAccessTokenResponseMessage", typeof(GetClientAccessTokenResponseMessage))]
		[XmlElement("GetFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("GetMailTipsResponseMessage", typeof(GetMailTipsResponseMessage))]
		[XmlElement("GetItemResponseMessage", typeof(ItemInfoResponseMessage))]
		[XmlElement("GetServerTimeZonesResponseMessage", typeof(GetServerTimeZonesResponseMessage))]
		[XmlElement("GetUserConfigurationResponseMessage", typeof(GetUserConfigurationResponseMessage))]
		[XmlElement("MoveFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("MoveItemResponseMessage", typeof(ItemInfoResponseMessage))]
		[XmlElement("ResolveNamesResponseMessage", typeof(ResolveNamesResponseMessage))]
		[XmlElement("DeleteAttachmentResponseMessage", typeof(DeleteAttachmentResponseMessage))]
		[XmlElement("SendItemResponseMessage", typeof(ResponseMessage))]
		[XmlElement("CopyFolderResponseMessage", typeof(FolderInfoResponseMessage))]
		[XmlElement("GetAttachmentResponseMessage", typeof(AttachmentInfoResponseMessage))]
		[XmlElement("UpdateUserConfigurationResponseMessage", typeof(ResponseMessage))]
		[XmlElement("CreateAttachmentResponseMessage", typeof(AttachmentInfoResponseMessage))]
		[XmlElement("SyncFolderItemsResponseMessage", typeof(SyncFolderItemsResponseMessage))]
		[XmlElement("ArchiveItemResponseMessage", typeof(ItemInfoResponseMessage))]
		public ResponseMessage[] Items
		{
			get
			{
				return this.items.ToArray();
			}
			set
			{
				this.items.Clear();
				this.items.AddRange(value);
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000A3FAC File Offset: 0x000A21AC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A3FB4 File Offset: 0x000A21B4
		public ArrayOfResponseMessages()
		{
			this.Initialize();
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000A3FC2 File Offset: 0x000A21C2
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x000A3FCF File Offset: 0x000A21CF
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ResponseType[] ItemsElementName
		{
			get
			{
				return this.itemsElementName.ToArray();
			}
			set
			{
				this.itemsElementName.Clear();
				this.itemsElementName.AddRange(value);
			}
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000A3FE8 File Offset: 0x000A21E8
		internal void AddResponse(ResponseMessage message, ResponseType messageType)
		{
			this.items.Add(message);
			this.itemsElementName.Add(messageType);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000A4002 File Offset: 0x000A2202
		private void Initialize()
		{
			this.items = new List<ResponseMessage>();
			this.itemsElementName = new List<ResponseType>();
		}

		// Token: 0x04001550 RID: 5456
		private List<ResponseMessage> items;

		// Token: 0x04001551 RID: 5457
		private List<ResponseType> itemsElementName;
	}
}
