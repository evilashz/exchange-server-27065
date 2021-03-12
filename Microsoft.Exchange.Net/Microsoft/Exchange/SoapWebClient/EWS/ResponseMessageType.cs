using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200018B RID: 395
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseMessageType))]
	[XmlInclude(typeof(AddNewImContactToGroupResponseMessageType))]
	[XmlInclude(typeof(UninstallAppResponseType))]
	[XmlInclude(typeof(InstallAppResponseType))]
	[XmlInclude(typeof(MarkAsJunkResponseMessageType))]
	[XmlInclude(typeof(GetAppMarketplaceUrlResponseMessageType))]
	[XmlInclude(typeof(GetAppManifestsResponseType))]
	[XmlInclude(typeof(SetEncryptionConfigurationResponseType))]
	[XmlInclude(typeof(EncryptionConfigurationResponseType))]
	[XmlInclude(typeof(ClientExtensionResponseType))]
	[XmlInclude(typeof(GetConversationItemsResponseMessageType))]
	[XmlInclude(typeof(GetNonIndexableItemDetailsResponseMessageType))]
	[XmlInclude(typeof(GetNonIndexableItemStatisticsResponseMessageType))]
	[XmlInclude(typeof(SetHoldOnMailboxesResponseMessageType))]
	[XmlInclude(typeof(GetHoldOnMailboxesResponseMessageType))]
	[XmlInclude(typeof(GetDiscoverySearchConfigurationResponseMessageType))]
	[XmlInclude(typeof(SearchMailboxesResponseMessageType))]
	[XmlInclude(typeof(GetSearchableMailboxesResponseMessageType))]
	[XmlInclude(typeof(FindMailboxStatisticsByKeywordsResponseMessageType))]
	[XmlInclude(typeof(UpdateInboxRulesResponseType))]
	[XmlInclude(typeof(GetInboxRulesResponseType))]
	[XmlInclude(typeof(GetMessageTrackingReportResponseMessageType))]
	[XmlInclude(typeof(FindMessageTrackingReportResponseMessageType))]
	[XmlInclude(typeof(ServiceConfigurationResponseMessageType))]
	[XmlInclude(typeof(GetServiceConfigurationResponseMessageType))]
	[XmlInclude(typeof(PerformReminderActionResponseMessageType))]
	[XmlInclude(typeof(GetRemindersResponseMessageType))]
	[XmlInclude(typeof(GetRoomsResponseMessageType))]
	[XmlInclude(typeof(GetRoomListsResponseMessageType))]
	[XmlInclude(typeof(UnpinTeamMailboxResponseMessageType))]
	[XmlInclude(typeof(SetTeamMailboxResponseMessageType))]
	[XmlInclude(typeof(GetUserConfigurationResponseMessageType))]
	[XmlInclude(typeof(AddNewTelUriContactToGroupResponseMessageType))]
	[XmlInclude(typeof(RefreshSharingFolderResponseMessageType))]
	[XmlInclude(typeof(GetSharingMetadataResponseMessageType))]
	[XmlInclude(typeof(BaseDelegateResponseMessageType))]
	[XmlInclude(typeof(UpdateDelegateResponseMessageType))]
	[XmlInclude(typeof(RemoveDelegateResponseMessageType))]
	[XmlInclude(typeof(AddDelegateResponseMessageType))]
	[XmlInclude(typeof(GetDelegateResponseMessageType))]
	[XmlInclude(typeof(DelegateUserResponseMessageType))]
	[XmlInclude(typeof(ConvertIdResponseMessageType))]
	[XmlInclude(typeof(SyncFolderItemsResponseMessageType))]
	[XmlInclude(typeof(SyncFolderHierarchyResponseMessageType))]
	[XmlInclude(typeof(SendNotificationResponseMessageType))]
	[XmlInclude(typeof(GetStreamingEventsResponseMessageType))]
	[XmlInclude(typeof(GetEventsResponseMessageType))]
	[XmlInclude(typeof(SubscribeResponseMessageType))]
	[XmlInclude(typeof(GetServerTimeZonesResponseMessageType))]
	[XmlInclude(typeof(ExpandDLResponseMessageType))]
	[XmlInclude(typeof(GetUMPromptResponseMessageType))]
	[XmlInclude(typeof(GetUMPromptNamesResponseMessageType))]
	[XmlInclude(typeof(CreateUMPromptResponseMessageType))]
	[XmlInclude(typeof(DeleteUMPromptsResponseMessageType))]
	[XmlInclude(typeof(DisconnectPhoneCallResponseMessageType))]
	[XmlInclude(typeof(GetPhoneCallInformationResponseMessageType))]
	[XmlInclude(typeof(PlayOnPhoneResponseMessageType))]
	[XmlInclude(typeof(MailTipsResponseMessageType))]
	[XmlInclude(typeof(GetMailTipsResponseMessageType))]
	[XmlInclude(typeof(GetPasswordExpirationDateResponseMessageType))]
	[XmlInclude(typeof(ResolveNamesResponseMessageType))]
	[XmlInclude(typeof(GetClientAccessTokenResponseMessageType))]
	[XmlInclude(typeof(FindItemResponseMessageType))]
	[XmlInclude(typeof(DeleteItemResponseMessageType))]
	[XmlInclude(typeof(GetPersonaResponseMessageType))]
	[XmlInclude(typeof(FindPeopleResponseMessageType))]
	[XmlInclude(typeof(ApplyConversationActionResponseMessageType))]
	[XmlInclude(typeof(FindConversationResponseMessageType))]
	[XmlInclude(typeof(DeleteAttachmentResponseMessageType))]
	[XmlInclude(typeof(AttachmentInfoResponseMessageType))]
	[XmlInclude(typeof(ItemInfoResponseMessageType))]
	[XmlInclude(typeof(GetUMSubscriberCallAnsweringDataResponseMessageType))]
	[XmlInclude(typeof(UpdateItemResponseMessageType))]
	[XmlInclude(typeof(FindFolderResponseMessageType))]
	[XmlInclude(typeof(FolderInfoResponseMessageType))]
	[XmlInclude(typeof(ExportItemsResponseMessageType))]
	[XmlInclude(typeof(UploadItemsResponseMessageType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(GetSharingFolderResponseMessageType))]
	[XmlInclude(typeof(DisableAppResponseType))]
	[XmlInclude(typeof(GetClientIntentResponseMessageType))]
	[XmlInclude(typeof(GetUMPinResponseMessageType))]
	[XmlInclude(typeof(SaveUMPinResponseMessageType))]
	[XmlInclude(typeof(ValidateUMPinResponseMessageType))]
	[XmlInclude(typeof(ResetUMMailboxResponseMessageType))]
	[XmlInclude(typeof(InitUMMailboxResponseMessageType))]
	[XmlInclude(typeof(GetUMCallSummaryResponseMessageType))]
	[XmlInclude(typeof(GetUMCallDataRecordsResponseMessageType))]
	[XmlInclude(typeof(CreateUMCallDataRecordResponseMessageType))]
	[XmlInclude(typeof(CompleteFindInGALSpeechRecognitionResponseMessageType))]
	[XmlInclude(typeof(StartFindInGALSpeechRecognitionResponseMessageType))]
	[XmlInclude(typeof(GetUserPhotoResponseMessageType))]
	[XmlInclude(typeof(GetUserRetentionPolicyTagsResponseMessageType))]
	[XmlInclude(typeof(SetImListMigrationCompletedResponseMessageType))]
	[XmlInclude(typeof(SetImGroupResponseMessageType))]
	[XmlInclude(typeof(RemoveImGroupResponseMessageType))]
	[XmlInclude(typeof(RemoveDistributionGroupFromImListResponseMessageType))]
	[XmlInclude(typeof(RemoveContactFromImListResponseMessageType))]
	[XmlInclude(typeof(GetImItemsResponseMessageType))]
	[XmlInclude(typeof(GetImItemListResponseMessageType))]
	[XmlInclude(typeof(AddDistributionGroupToImListResponseMessageType))]
	[XmlInclude(typeof(AddImGroupResponseMessageType))]
	[XmlInclude(typeof(RemoveImContactFromGroupResponseMessageType))]
	[XmlInclude(typeof(AddImContactToGroupResponseMessageType))]
	[Serializable]
	public class ResponseMessageType
	{
		// Token: 0x040007B9 RID: 1977
		public string MessageText;

		// Token: 0x040007BA RID: 1978
		public ResponseCodeType ResponseCode;

		// Token: 0x040007BB RID: 1979
		[XmlIgnore]
		public bool ResponseCodeSpecified;

		// Token: 0x040007BC RID: 1980
		public int DescriptiveLinkKey;

		// Token: 0x040007BD RID: 1981
		[XmlIgnore]
		public bool DescriptiveLinkKeySpecified;

		// Token: 0x040007BE RID: 1982
		public ResponseMessageTypeMessageXml MessageXml;

		// Token: 0x040007BF RID: 1983
		[XmlAttribute]
		public ResponseClassType ResponseClass;
	}
}
