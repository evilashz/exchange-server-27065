using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A2 RID: 1186
	[KnownType(typeof(SubscribeResponse))]
	[XmlInclude(typeof(CreateFolderResponse))]
	[XmlInclude(typeof(CreateItemResponse))]
	[XmlInclude(typeof(GetConversationItemsResponse))]
	[XmlInclude(typeof(ArchiveItemResponse))]
	[XmlInclude(typeof(CreateManagedFolderResponse))]
	[XmlInclude(typeof(CreateUserConfigurationResponse))]
	[XmlInclude(typeof(CreateUnifiedMailboxResponse))]
	[XmlInclude(typeof(DeleteUserConfigurationResponse))]
	[XmlInclude(typeof(DeprovisionResponse))]
	[XmlInclude(typeof(ExpandDLResponse))]
	[XmlInclude(typeof(ExportItemsResponse))]
	[XmlInclude(typeof(GetClientAccessTokenResponse))]
	[XmlInclude(typeof(GetEventsResponse))]
	[XmlInclude(typeof(GetUserConfigurationResponse))]
	[XmlInclude(typeof(ProvisionResponse))]
	[XmlInclude(typeof(ResolveNamesResponse))]
	[XmlInclude(typeof(SearchMailboxesResponse))]
	[XmlInclude(typeof(SubscribeResponse))]
	[XmlInclude(typeof(UnsubscribeResponse))]
	[XmlInclude(typeof(UpdateUserConfigurationResponse))]
	[XmlInclude(typeof(UploadItemsResponse))]
	[XmlInclude(typeof(InstallAppResponse))]
	[XmlInclude(typeof(UninstallAppResponse))]
	[XmlInclude(typeof(SetClientExtensionResponse))]
	[XmlInclude(typeof(GetUserPhotoResponse))]
	[XmlInclude(typeof(DisableAppResponse))]
	[XmlInclude(typeof(GetAppMarketplaceUrlResponse))]
	[XmlInclude(typeof(AddAggregatedAccountResponse))]
	[XmlInclude(typeof(MarkAsJunkResponse))]
	[XmlInclude(typeof(GetAggregatedAccountResponse))]
	[XmlInclude(typeof(RemoveAggregatedAccountResponse))]
	[XmlInclude(typeof(SetAggregatedAccountResponse))]
	[XmlInclude(typeof(ApplyConversationActionResponseMessage))]
	[XmlInclude(typeof(DeleteItemResponseMessage))]
	[KnownType(typeof(ArchiveItemResponse))]
	[KnownType(typeof(CreateManagedFolderResponse))]
	[KnownType(typeof(CreateUserConfigurationResponse))]
	[KnownType(typeof(CreateUnifiedMailboxResponse))]
	[KnownType(typeof(DeleteUserConfigurationResponse))]
	[KnownType(typeof(DeprovisionResponse))]
	[KnownType(typeof(ExpandDLResponse))]
	[KnownType(typeof(ExportItemsResponse))]
	[KnownType(typeof(GetClientAccessTokenResponse))]
	[KnownType(typeof(GetEventsResponse))]
	[KnownType(typeof(GetUserConfigurationResponse))]
	[KnownType(typeof(ProvisionResponse))]
	[KnownType(typeof(ResolveNamesResponse))]
	[KnownType(typeof(SearchMailboxesResponse))]
	[KnownType(typeof(UnsubscribeResponse))]
	[KnownType(typeof(UpdateUserConfigurationResponse))]
	[KnownType(typeof(UploadItemsResponse))]
	[KnownType(typeof(GetUserPhotoResponse))]
	[KnownType(typeof(InstallAppResponse))]
	[KnownType(typeof(UninstallAppResponse))]
	[KnownType(typeof(SetClientExtensionResponse))]
	[KnownType(typeof(DisableAppResponse))]
	[KnownType(typeof(GetAppMarketplaceUrlResponse))]
	[KnownType(typeof(AddAggregatedAccountResponse))]
	[KnownType(typeof(MarkAsJunkResponse))]
	[KnownType(typeof(GetAggregatedAccountResponse))]
	[KnownType(typeof(RemoveAggregatedAccountResponse))]
	[KnownType(typeof(SetAggregatedAccountResponse))]
	[KnownType(typeof(ApplyConversationActionResponseMessage))]
	[KnownType(typeof(DeleteItemResponseMessage))]
	[XmlInclude(typeof(ConvertIdResponse))]
	[XmlInclude(typeof(CopyFolderResponse))]
	[XmlInclude(typeof(CopyItemResponse))]
	[XmlInclude(typeof(CreateAttachmentResponse))]
	[KnownType(typeof(SendItemResponse))]
	[XmlInclude(typeof(CreateFolderPathResponse))]
	[XmlInclude(typeof(DeleteAttachmentResponse))]
	[XmlInclude(typeof(DeleteFolderResponse))]
	[XmlInclude(typeof(DeleteItemResponse))]
	[XmlInclude(typeof(EmptyFolderResponse))]
	[XmlInclude(typeof(FindFolderResponse))]
	[XmlInclude(typeof(FindItemResponse))]
	[XmlInclude(typeof(GetAttachmentResponse))]
	[KnownType(typeof(GetModernConversationAttachmentsResponse))]
	[KnownType(typeof(UpdateFolderResponse))]
	[KnownType(typeof(GetServerTimeZonesResponse))]
	[XmlInclude(typeof(GetServerTimeZonesResponse))]
	[XmlInclude(typeof(MoveFolderResponse))]
	[XmlInclude(typeof(MoveItemResponse))]
	[XmlInclude(typeof(SendItemResponse))]
	[XmlInclude(typeof(SendNotificationResponse))]
	[XmlInclude(typeof(UpdateFolderResponse))]
	[XmlInclude(typeof(UpdateItemResponse))]
	[XmlType("BaseResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(ConvertIdResponse))]
	[KnownType(typeof(CopyFolderResponse))]
	[KnownType(typeof(CopyItemResponse))]
	[KnownType(typeof(CreateAttachmentResponse))]
	[KnownType(typeof(CreateFolderResponse))]
	[KnownType(typeof(CreateItemResponse))]
	[KnownType(typeof(CreateFolderPathResponse))]
	[KnownType(typeof(DeleteAttachmentResponse))]
	[KnownType(typeof(DeleteFolderResponse))]
	[KnownType(typeof(DeleteItemResponse))]
	[KnownType(typeof(EmptyFolderResponse))]
	[KnownType(typeof(FindFolderResponse))]
	[KnownType(typeof(SendNotificationResponse))]
	[KnownType(typeof(GetAttachmentResponse))]
	[KnownType(typeof(GetConversationItemsResponse))]
	[KnownType(typeof(GetThreadedConversationItemsResponse))]
	[KnownType(typeof(GetConversationItemsDiagnosticsResponseType))]
	[KnownType(typeof(GetFolderResponse))]
	[KnownType(typeof(GetItemResponse))]
	[KnownType(typeof(MoveFolderResponse))]
	[KnownType(typeof(MoveItemResponse))]
	[KnownType(typeof(PostModernGroupItemResponse))]
	[XmlInclude(typeof(GetItemResponse))]
	[KnownType(typeof(FindItemResponse))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(GetFolderResponse))]
	[KnownType(typeof(UpdateItemResponse))]
	[KnownType(typeof(UpdateAndPostModernGroupItemResponse))]
	[KnownType(typeof(CreateResponseFromModernGroupResponse))]
	[Serializable]
	public class BaseResponseMessage : IExchangeWebMethodResponse
	{
		// Token: 0x06002387 RID: 9095 RVA: 0x000A401A File Offset: 0x000A221A
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000A4022 File Offset: 0x000A2222
		static BaseResponseMessage()
		{
			BaseResponseMessage.namespaces.Add("m", "http://schemas.microsoft.com/exchange/services/2006/messages");
			BaseResponseMessage.namespaces.Add("t", "http://schemas.microsoft.com/exchange/services/2006/types");
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000A4056 File Offset: 0x000A2256
		public BaseResponseMessage()
		{
			this.Initialize();
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000A4064 File Offset: 0x000A2264
		internal BaseResponseMessage(ResponseType responseType) : this()
		{
			this.ResponseType = responseType;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000A4073 File Offset: 0x000A2273
		private void Initialize()
		{
			this.responseMessages = new ArrayOfResponseMessages();
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000A4080 File Offset: 0x000A2280
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x000A4087 File Offset: 0x000A2287
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return BaseResponseMessage.namespaces;
			}
			set
			{
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000A4089 File Offset: 0x000A2289
		// (set) Token: 0x0600238F RID: 9103 RVA: 0x000A4091 File Offset: 0x000A2291
		[DataMember]
		public ArrayOfResponseMessages ResponseMessages
		{
			get
			{
				return this.responseMessages;
			}
			set
			{
				this.responseMessages = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000A409A File Offset: 0x000A229A
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x000A40A2 File Offset: 0x000A22A2
		[XmlIgnore]
		public ResponseType ResponseType
		{
			get
			{
				return this.responseType;
			}
			set
			{
				this.responseType = value;
			}
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000A40AB File Offset: 0x000A22AB
		internal virtual void ProcessServiceResult(ServiceResult<ServiceResultNone> result)
		{
			this.AddResponse(new ResponseMessage(result.Code, result.Error));
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000A40C4 File Offset: 0x000A22C4
		internal void AddResponse(ResponseMessage message)
		{
			this.ResponseMessages.AddResponse(message, this.ResponseType);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000A40D8 File Offset: 0x000A22D8
		internal void BuildForNoReturnValue(ServiceResult<ServiceResultNone>[] serviceResults)
		{
			ServiceResult<ServiceResultNone>.ProcessServiceResults(serviceResults, new ProcessServiceResult<ServiceResultNone>(this.ProcessServiceResult));
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000A40ED File Offset: 0x000A22ED
		ResponseType IExchangeWebMethodResponse.GetResponseType()
		{
			return this.ResponseType;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000A40F8 File Offset: 0x000A22F8
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			foreach (ResponseMessage responseMessage in this.ResponseMessages.Items)
			{
				if (responseMessage.ResponseCode != ResponseCodeType.NoError)
				{
					return responseMessage.ResponseCode;
				}
			}
			return ResponseCodeType.NoError;
		}

		// Token: 0x04001552 RID: 5458
		private static XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

		// Token: 0x04001553 RID: 5459
		private ResponseType responseType;

		// Token: 0x04001554 RID: 5460
		private ArrayOfResponseMessages responseMessages;
	}
}
