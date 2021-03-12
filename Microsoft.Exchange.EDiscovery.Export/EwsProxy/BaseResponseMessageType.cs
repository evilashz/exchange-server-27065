using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000285 RID: 645
	[XmlInclude(typeof(GetEventsResponseType))]
	[XmlInclude(typeof(UpdateGroupMailboxResponseType))]
	[XmlInclude(typeof(UpdateMailboxAssociationResponseType))]
	[XmlInclude(typeof(GetUserPhotoResponseType))]
	[XmlInclude(typeof(CreateAttachmentResponseType))]
	[XmlInclude(typeof(SetClientExtensionResponseType))]
	[XmlInclude(typeof(GetConversationItemsResponseType))]
	[XmlInclude(typeof(MarkAllItemsAsReadResponseType))]
	[XmlInclude(typeof(SearchMailboxesResponseType))]
	[XmlInclude(typeof(FindMailboxStatisticsByKeywordsResponseType))]
	[XmlInclude(typeof(UpdateUserConfigurationResponseType))]
	[XmlInclude(typeof(GetUserConfigurationResponseType))]
	[XmlInclude(typeof(DeleteUserConfigurationResponseType))]
	[XmlInclude(typeof(CreateUserConfigurationResponseType))]
	[XmlInclude(typeof(ConvertIdResponseType))]
	[XmlInclude(typeof(SyncFolderItemsResponseType))]
	[XmlInclude(typeof(SyncFolderHierarchyResponseType))]
	[XmlInclude(typeof(SendNotificationResponseType))]
	[XmlInclude(typeof(GetStreamingEventsResponseType))]
	[XmlInclude(typeof(UnsubscribeResponseType))]
	[XmlInclude(typeof(SubscribeResponseType))]
	[XmlInclude(typeof(CreateManagedFolderResponseType))]
	[XmlInclude(typeof(GetServerTimeZonesResponseType))]
	[XmlInclude(typeof(ExpandDLResponseType))]
	[XmlInclude(typeof(ResolveNamesResponseType))]
	[XmlInclude(typeof(GetClientAccessTokenResponseType))]
	[XmlInclude(typeof(ArchiveItemResponseType))]
	[XmlInclude(typeof(FindItemResponseType))]
	[XmlInclude(typeof(DeleteItemResponseType))]
	[XmlInclude(typeof(CopyItemResponseType))]
	[XmlInclude(typeof(MoveItemResponseType))]
	[XmlInclude(typeof(GetItemResponseType))]
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseType))]
	[XmlInclude(typeof(UpdateItemResponseType))]
	[XmlInclude(typeof(CreateItemResponseType))]
	[XmlInclude(typeof(GetAttachmentResponseType))]
	[XmlInclude(typeof(DeleteAttachmentResponseType))]
	[DebuggerStepThrough]
	[XmlInclude(typeof(UploadItemsResponseType))]
	[XmlInclude(typeof(ApplyConversationActionResponseType))]
	[XmlInclude(typeof(SendItemResponseType))]
	[XmlInclude(typeof(PostModernGroupItemResponseType))]
	[XmlInclude(typeof(CreateFolderPathResponseType))]
	[XmlInclude(typeof(CopyFolderResponseType))]
	[XmlInclude(typeof(MoveFolderResponseType))]
	[XmlInclude(typeof(UpdateFolderResponseType))]
	[XmlInclude(typeof(GetFolderResponseType))]
	[XmlInclude(typeof(CreateFolderResponseType))]
	[XmlInclude(typeof(EmptyFolderResponseType))]
	[XmlInclude(typeof(DeleteFolderResponseType))]
	[XmlInclude(typeof(FindFolderResponseType))]
	[XmlInclude(typeof(ExportItemsResponseType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(MarkAsJunkResponseType))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class BaseResponseMessageType
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0002786F File Offset: 0x00025A6F
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x00027877 File Offset: 0x00025A77
		public ArrayOfResponseMessagesType ResponseMessages
		{
			get
			{
				return this.responseMessagesField;
			}
			set
			{
				this.responseMessagesField = value;
			}
		}

		// Token: 0x0400104D RID: 4173
		private ArrayOfResponseMessagesType responseMessagesField;
	}
}
