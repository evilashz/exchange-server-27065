using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.Services.Wcf.Types;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000150 RID: 336
	[ServiceContract]
	public interface IOWAService : IJsonServiceContract
	{
		// Token: 0x06000BDD RID: 3037
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		PeopleFilter[] GetPeopleFilters();

		// Token: 0x06000BDE RID: 3038
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		SubscriptionResponseData[] SubscribeToNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData);

		// Token: 0x06000BDF RID: 3039
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonRequestWrapperType(typeof(UnsubscribeToNotificationRequest))]
		[OfflineClient(Queued = false)]
		bool UnsubscribeToNotification(SubscriptionData[] subscriptionData);

		// Token: 0x06000BE0 RID: 3040
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		SubscriptionResponseData[] SubscribeToGroupNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData);

		// Token: 0x06000BE1 RID: 3041
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonRequestWrapperType(typeof(UnsubscribeToNotificationRequest))]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool UnsubscribeToGroupNotification(SubscriptionData[] subscriptionData);

		// Token: 0x06000BE2 RID: 3042
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		SubscriptionResponseData[] SubscribeToGroupUnseenNotification(NotificationSubscribeJsonRequest request, SubscriptionData[] subscriptionData);

		// Token: 0x06000BE3 RID: 3043
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonRequestWrapperType(typeof(UnsubscribeToNotificationRequest))]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool UnsubscribeToGroupUnseenNotification(SubscriptionData[] subscriptionData);

		// Token: 0x06000BE4 RID: 3044
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		bool AddSharedFolders(string displayName, string primarySMTPAddress);

		// Token: 0x06000BE5 RID: 3045
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool RemoveSharedFolders(string primarySMTPAddress);

		// Token: 0x06000BE6 RID: 3046
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		OwaOtherMailboxConfiguration GetOtherMailboxConfiguration();

		// Token: 0x06000BE7 RID: 3047
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		NavBarData GetBposNavBarData();

		// Token: 0x06000BE8 RID: 3048
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		NavBarData GetBposShellInfoNavBarData();

		// Token: 0x06000BE9 RID: 3049
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetModernAttachmentsResponse GetModernAttachments(GetModernAttachmentsRequest request);

		// Token: 0x06000BEA RID: 3050
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		CreateUnifiedGroupResponse CreateUnifiedGroup(CreateUnifiedGroupRequest request);

		// Token: 0x06000BEB RID: 3051
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		AddMembersToUnifiedGroupResponse AddMembersToUnifiedGroup(AddMembersToUnifiedGroupRequest request);

		// Token: 0x06000BEC RID: 3052
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetPersonaNotesResponse GetNotesForPersona(GetNotesForPersonaRequest getNotesForPersonaRequest);

		// Token: 0x06000BED RID: 3053
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		GetPersonaOrganizationHierarchyResponse GetOrganizationHierarchyForPersona(GetOrganizationHierarchyForPersonaRequest getOrganizationHierarchyForPersonaRequest);

		// Token: 0x06000BEE RID: 3054
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		GetPersonaOrganizationHierarchyResponse GetPersonaOrganizationHierarchy(string galObjectGuid);

		// Token: 0x06000BEF RID: 3055
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetGroupResponse GetGroup(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string adObjectId, EmailAddressWrapper emailAddress, IndexedPageView paging, GetGroupResultSet resultSet);

		// Token: 0x06000BF0 RID: 3056
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetGroupResponse GetGroupInfo(GetGroupInfoRequest getGroupInfoRequest);

		// Token: 0x06000BF1 RID: 3057
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		GetPersonaNotesResponse GetPersonaNotes(string personaId, int maxBytesToFetch);

		// Token: 0x06000BF2 RID: 3058
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		OwaUserConfiguration GetOwaUserConfiguration();

		// Token: 0x06000BF3 RID: 3059
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		Alert[] GetSystemAlerts();

		// Token: 0x06000BF4 RID: 3060
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		bool SetNotificationSettings(NotificationSettingsJsonRequest settings);

		// Token: 0x06000BF5 RID: 3061
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		ScopeFlightsSetting[] GetFlightsSettings();

		// Token: 0x06000BF6 RID: 3062
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		ComplianceConfiguration GetComplianceConfiguration();

		// Token: 0x06000BF7 RID: 3063
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		AttachmentDataProvider AddAttachmentDataProvider(AttachmentDataProvider attachmentDataProvider);

		// Token: 0x06000BF8 RID: 3064
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[Obsolete]
		AttachmentDataProvider[] GetAttachmentDataProviders();

		// Token: 0x06000BF9 RID: 3065
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		AttachmentDataProvider[] GetAllAttachmentDataProviders(GetAttachmentDataProvidersRequest request);

		// Token: 0x06000BFA RID: 3066
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		AttachmentDataProviderType GetAttachmentDataProviderTypes();

		// Token: 0x06000BFB RID: 3067
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderItems(GetAttachmentDataProviderItemsRequest request);

		// Token: 0x06000BFC RID: 3068
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderRecentItems();

		// Token: 0x06000BFD RID: 3069
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetAttachmentDataProviderItemsResponse GetAttachmentDataProviderGroups();

		// Token: 0x06000BFE RID: 3070
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		string CreateReferenceAttachmentFromLocalFile(CreateReferenceAttachmentFromLocalFileRequest requestObject);

		// Token: 0x06000BFF RID: 3071
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonRequestWrapperType(typeof(CreateAttachmentFromAttachmentDataProviderRequest))]
		[OfflineClient(Queued = false)]
		string CreateAttachmentFromAttachmentDataProvider(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string attachmentDataProviderId, string location, string attachmentId, string subscriptionId, string channelId, string dataProviderParentItemId, string providerEndpointUrl, string cancellationId = null);

		// Token: 0x06000C00 RID: 3072
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool CancelAttachment(string cancellationId);

		// Token: 0x06000C01 RID: 3073
		[JsonRequestWrapperType(typeof(CreateReferenceAttachmentFromAttachmentDataProviderRequest))]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		CreateAttachmentResponse CreateReferenceAttachmentFromAttachmentDataProvider(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string attachmentDataProviderId, string location, string attachmentId, string dataProviderParentItemId, string providerEndpointUrl, string cancellationId = null);

		// Token: 0x06000C02 RID: 3074
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		string GetAttachmentDataProviderUploadFolderName();

		// Token: 0x06000C03 RID: 3075
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		TargetFolderMruConfiguration GetFolderMruConfiguration();

		// Token: 0x06000C04 RID: 3076
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool SetFolderMruConfiguration(TargetFolderMruConfiguration folderMruConfiguration);

		// Token: 0x06000C05 RID: 3077
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		UcwaUserConfiguration GetUcwaUserConfiguration(string sipUri);

		// Token: 0x06000C06 RID: 3078
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		OnlineMeetingType CreateOnlineMeeting(string sipUri, Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x06000C07 RID: 3079
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		OnlineMeetingType CreateMeetNow(string sipUri, string subject);

		// Token: 0x06000C08 RID: 3080
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		string GetWacIframeUrl(string attachmentId);

		// Token: 0x06000C09 RID: 3081
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		string GetWacIframeUrlForOneDrive(GetWacIframeUrlForOneDriveRequest request);

		// Token: 0x06000C0A RID: 3082
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		WacAttachmentType GetWacAttachmentInfo(string attachmentId, bool isEdit, string draftId);

		// Token: 0x06000C0B RID: 3083
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		string CreateResendDraft(string ndrMessageId, string draftsFolderId);

		// Token: 0x06000C0C RID: 3084
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		CreateAttachmentJsonResponse CreateAttachmentFromLocalFile(CreateAttachmentJsonRequest request);

		// Token: 0x06000C0D RID: 3085
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		CreateAttachmentJsonResponse CreateAttachmentFromForm();

		// Token: 0x06000C0E RID: 3086
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetFlowConversationResponse GetFlowConversation(BaseFolderId folderId, int conversationCount);

		// Token: 0x06000C0F RID: 3087
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		FindFlowConversationItemResponse FindFlowConversationItem(BaseFolderId folderId, string flowConversationId, int itemCount);

		// Token: 0x06000C10 RID: 3088
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract]
		string UploadAndShareAttachmentFromForm();

		// Token: 0x06000C11 RID: 3089
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		string UpdateAttachmentPermissions(UpdateAttachmentPermissionsRequest permissionsRequest);

		// Token: 0x06000C12 RID: 3090
		[OfflineClient(Queued = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[JsonRequestWrapperType(typeof(LogDatapointRequest))]
		bool LogDatapoint(Datapoint[] datapoints);

		// Token: 0x06000C13 RID: 3091
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		PerformInstantSearchResponse PerformInstantSearch(PerformInstantSearchRequest request);

		// Token: 0x06000C14 RID: 3092
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		EndInstantSearchSessionResponse EndInstantSearchSession(string sessionId);

		// Token: 0x06000C15 RID: 3093
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool ConnectedAccountsNotification(bool isOWALogon);

		// Token: 0x06000C16 RID: 3094
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		UploadPhotoResponse UploadPhoto(UploadPhotoRequest request);

		// Token: 0x06000C17 RID: 3095
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		UploadPhotoResponse UploadPhotoFromForm();

		// Token: 0x06000C18 RID: 3096
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		int VerifyCert(string certRawData);

		// Token: 0x06000C19 RID: 3097
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[Obsolete]
		GetCertsResponse GetCerts(GetCertsRequest request);

		// Token: 0x06000C1A RID: 3098
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetCertsResponse GetEncryptionCerts(GetCertsRequest request);

		// Token: 0x06000C1B RID: 3099
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		GetCertsInfoResponse GetCertsInfo(string certRawData, bool isSend);

		// Token: 0x06000C1C RID: 3100
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		string GetMime(Microsoft.Exchange.Services.Core.Types.ItemId itemId);

		// Token: 0x06000C1D RID: 3101
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		int InstantMessageSignIn(bool signedInManually);

		// Token: 0x06000C1E RID: 3102
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int InstantMessageSignOut(bool reserved);

		// Token: 0x06000C1F RID: 3103
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int SendChatMessage(ChatMessage message);

		// Token: 0x06000C20 RID: 3104
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool TerminateChatSession(int chatSessionId);

		// Token: 0x06000C21 RID: 3105
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		int AcceptChatSession(int chatSessionId);

		// Token: 0x06000C22 RID: 3106
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool AcceptBuddy(InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup);

		// Token: 0x06000C23 RID: 3107
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool AddImBuddy(InstantMessageBuddy instantMessageBuddy, InstantMessageGroup instantMessageGroup);

		// Token: 0x06000C24 RID: 3108
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool DeclineBuddy(InstantMessageBuddy instantMessageBuddy);

		// Token: 0x06000C25 RID: 3109
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool RemoveBuddy(InstantMessageBuddy instantMessageBuddy, Microsoft.Exchange.Services.Core.Types.ItemId contactId);

		// Token: 0x06000C26 RID: 3110
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool AddFavorite(InstantMessageBuddy instantMessageBuddy);

		// Token: 0x06000C27 RID: 3111
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		bool RemoveFavorite(Microsoft.Exchange.Services.Core.Types.ItemId personaId);

		// Token: 0x06000C28 RID: 3112
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool NotifyAppWipe(DataWipeReason wipeReason);

		// Token: 0x06000C29 RID: 3113
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool NotifyTyping(int chatSessionId, bool typingCancelled);

		// Token: 0x06000C2A RID: 3114
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int SetPresence(InstantMessagePresence presenceSetting);

		// Token: 0x06000C2B RID: 3115
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		int ResetPresence();

		// Token: 0x06000C2C RID: 3116
		[JsonRequestWrapperType(typeof(GetPresenceRequest))]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int GetPresence(string[] sipUris);

		// Token: 0x06000C2D RID: 3117
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestWrapperType(typeof(GetPresenceRequest))]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int SubscribeForPresenceUpdates(string[] sipUris);

		// Token: 0x06000C2E RID: 3118
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		int UnsubscribeFromPresenceUpdates(string sipUri);

		// Token: 0x06000C2F RID: 3119
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[JsonRequestWrapperType(typeof(GetInstantMessageProxySettingsRequest))]
		ProxySettings[] GetInstantMessageProxySettings(string[] userPrincipalNames);

		// Token: 0x06000C30 RID: 3120
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		ThemeSelectionInfoType GetThemes();

		// Token: 0x06000C31 RID: 3121
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		bool SetTheme(string theme);

		// Token: 0x06000C32 RID: 3122
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		SetUserThemeResponse SetUserTheme(SetUserThemeRequest request);

		// Token: 0x06000C33 RID: 3123
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		TimeZoneConfiguration GetTimeZone(bool needTimeZoneList);

		// Token: 0x06000C34 RID: 3124
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool SetTimeZone(string timezone);

		// Token: 0x06000C35 RID: 3125
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		bool SetUserLocale(string userLocale, bool localizeFolders);

		// Token: 0x06000C36 RID: 3126
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.HttpHeaders)]
		UserOofSettingsType GetOwaUserOofSettings();

		// Token: 0x06000C37 RID: 3127
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		bool SetOwaUserOofSettings(UserOofSettingsType userOofSettings);

		// Token: 0x06000C38 RID: 3128
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		EmailSignatureConfiguration GetOwaUserEmailSignature();

		// Token: 0x06000C39 RID: 3129
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool SetOwaUserEmailSignature(EmailSignatureConfiguration userEmailSignature);

		// Token: 0x06000C3A RID: 3130
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		int GetDaysUntilPasswordExpiration();

		// Token: 0x06000C3B RID: 3131
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		EwsRoomType[] GetRoomsInternal(string roomList);

		// Token: 0x06000C3C RID: 3132
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		OptionSummary GetOptionSummary();

		// Token: 0x06000C3D RID: 3133
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetWellKnownShapesResponse GetWellKnownShapes();

		// Token: 0x06000C3E RID: 3134
		[OfflineClient(Queued = false)]
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		IAsyncResult BeginSubscribeToPushNotification(SubscribeToPushNotificationJsonRequest pushNotificationSubscription, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000C3F RID: 3135
		SubscribeToPushNotificationJsonResponse EndSubscribeToPushNotification(IAsyncResult result);

		// Token: 0x06000C40 RID: 3136
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginUnsubscribeToPushNotification(UnsubscribeToPushNotificationJsonRequest pushNotificationSubscription, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000C41 RID: 3137
		UnsubscribeToPushNotificationJsonResponse EndUnsubscribeToPushNotification(IAsyncResult result);

		// Token: 0x06000C42 RID: 3138
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		ContactFolderResponse CreateContactFolder(BaseFolderId parentFolderId, string displayName, int priority);

		// Token: 0x06000C43 RID: 3139
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		bool DeleteContactFolder(FolderId folderId);

		// Token: 0x06000C44 RID: 3140
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		ContactFolderResponse MoveContactFolder(FolderId folderId, int priority);

		// Token: 0x06000C45 RID: 3141
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		bool SetLayoutSettings(LayoutSettingsType layoutSettings);

		// Token: 0x06000C46 RID: 3142
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		Task<GetLinkPreviewResponse> GetLinkPreview(GetLinkPreviewRequest getLinkPreviewRequest);

		// Token: 0x06000C47 RID: 3143
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		GetBingMapsPreviewResponse GetBingMapsPreview(GetBingMapsPreviewRequest getBingMapsPreviewRequest);

		// Token: 0x06000C48 RID: 3144
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		InlineExploreSpResultListType GetInlineExploreSpContent(string query, string targetUrl);

		// Token: 0x06000C49 RID: 3145
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		[OperationContract]
		int PingOwa();

		// Token: 0x06000C4A RID: 3146
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetDlpPolicyTips(GetDlpPolicyTipsRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000C4B RID: 3147
		[OperationContract]
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		string CreateAttachmentFromUri(Microsoft.Exchange.Services.Core.Types.ItemId itemId, string uri, string name, string subscriptionId);

		// Token: 0x06000C4C RID: 3148
		GetDlpPolicyTipsResponse EndGetDlpPolicyTips(IAsyncResult result);

		// Token: 0x06000C4D RID: 3149
		[OfflineClient(Queued = false)]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
		string SanitizeHtml(string input);

		// Token: 0x06000C4E RID: 3150
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OperationContract(AsyncPattern = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		IAsyncResult BeginSynchronizeWacAttachment(SynchronizeWacAttachmentRequest request, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000C4F RID: 3151
		SynchronizeWacAttachmentResponse EndSynchronizeWacAttachment(IAsyncResult result);

		// Token: 0x06000C50 RID: 3152
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		IAsyncResult BeginPublishO365Notification(O365Notification notification, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000C51 RID: 3153
		bool EndPublishO365Notification(IAsyncResult result);

		// Token: 0x06000C52 RID: 3154
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
		[OperationContract]
		[OfflineClient(Queued = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		bool SendLinkClickedSignalToSP(SendLinkClickedSignalToSPRequest sendLinkClickedRequest);

		// Token: 0x06000C53 RID: 3155
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		ValidateAggregatedConfigurationResponse ValidateAggregatedConfiguration(ValidateAggregatedConfigurationRequest request);

		// Token: 0x06000C54 RID: 3156
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[OfflineClient(Queued = false)]
		FindMembersInUnifiedGroupResponse FindMembersInUnifiedGroup(FindMembersInUnifiedGroupRequest request);

		// Token: 0x06000C55 RID: 3157
		[OfflineClient(Queued = false)]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		[JsonRequestFormat(Format = JsonRequestFormat.HeaderBodyFormat)]
		[OperationContract]
		GetRegionalConfigurationResponse GetRegionalConfiguration(GetRegionalConfigurationRequest request);
	}
}
