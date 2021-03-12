﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000366 RID: 870
	[XmlInclude(typeof(GetConversationItemsResponseType))]
	[XmlInclude(typeof(GetFolderResponseType))]
	[XmlInclude(typeof(MoveFolderResponseType))]
	[XmlInclude(typeof(MarkAsJunkResponseType))]
	[XmlInclude(typeof(SetClientExtensionResponseType))]
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
	[XmlInclude(typeof(GetEventsResponseType))]
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
	[XmlInclude(typeof(CreateAttachmentResponseType))]
	[XmlInclude(typeof(UpdateGroupMailboxResponseType))]
	[XmlInclude(typeof(UpdateMailboxAssociationResponseType))]
	[XmlInclude(typeof(ApplyConversationActionResponseType))]
	[XmlInclude(typeof(SendItemResponseType))]
	[XmlInclude(typeof(PostModernGroupItemResponseType))]
	[XmlInclude(typeof(CreateFolderPathResponseType))]
	[XmlInclude(typeof(CopyFolderResponseType))]
	[XmlInclude(typeof(UpdateFolderResponseType))]
	[XmlInclude(typeof(GetUserPhotoResponseType))]
	[XmlInclude(typeof(CreateFolderResponseType))]
	[XmlInclude(typeof(EmptyFolderResponseType))]
	[XmlInclude(typeof(DeleteFolderResponseType))]
	[XmlInclude(typeof(FindFolderResponseType))]
	[XmlInclude(typeof(ExportItemsResponseType))]
	[XmlInclude(typeof(UploadItemsResponseType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class BaseResponseMessageType
	{
		// Token: 0x0400149F RID: 5279
		public ArrayOfResponseMessagesType ResponseMessages;
	}
}
