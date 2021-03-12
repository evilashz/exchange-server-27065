using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000AA RID: 170
	[XmlInclude(typeof(DeleteItemResponseMessageType))]
	[XmlInclude(typeof(UpdateDelegateResponseMessageType))]
	[XmlInclude(typeof(GetRemindersResponseMessageType))]
	[XmlInclude(typeof(GetUMSubscriberCallAnsweringDataResponseMessageType))]
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
	[XmlInclude(typeof(AddNewTelUriContactToGroupResponseMessageType))]
	[XmlInclude(typeof(AddNewImContactToGroupResponseMessageType))]
	[XmlInclude(typeof(DisableAppResponseType))]
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
	[XmlInclude(typeof(DisconnectPhoneCallResponseMessageType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(GetRoomListsResponseMessageType))]
	[XmlInclude(typeof(UnpinTeamMailboxResponseMessageType))]
	[XmlInclude(typeof(SetTeamMailboxResponseMessageType))]
	[XmlInclude(typeof(GetUserConfigurationResponseMessageType))]
	[XmlInclude(typeof(GetSharingFolderResponseMessageType))]
	[XmlInclude(typeof(RefreshSharingFolderResponseMessageType))]
	[XmlInclude(typeof(GetSharingMetadataResponseMessageType))]
	[XmlInclude(typeof(BaseDelegateResponseMessageType))]
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
	[XmlInclude(typeof(GetRoomsResponseMessageType))]
	[XmlInclude(typeof(GetPhoneCallInformationResponseMessageType))]
	[XmlInclude(typeof(PlayOnPhoneResponseMessageType))]
	[XmlInclude(typeof(MailTipsResponseMessageType))]
	[XmlInclude(typeof(GetMailTipsResponseMessageType))]
	[XmlInclude(typeof(GetPasswordExpirationDateResponseMessageType))]
	[XmlInclude(typeof(ResolveNamesResponseMessageType))]
	[XmlInclude(typeof(GetClientAccessTokenResponseMessageType))]
	[XmlInclude(typeof(FindItemResponseMessageType))]
	[XmlInclude(typeof(GetPersonaResponseMessageType))]
	[XmlInclude(typeof(FindPeopleResponseMessageType))]
	[XmlInclude(typeof(ApplyConversationActionResponseMessageType))]
	[XmlInclude(typeof(FindConversationResponseMessageType))]
	[XmlInclude(typeof(DeleteAttachmentResponseMessageType))]
	[XmlInclude(typeof(AttachmentInfoResponseMessageType))]
	[XmlInclude(typeof(ItemInfoResponseMessageType))]
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseMessageType))]
	[XmlInclude(typeof(UpdateItemResponseMessageType))]
	[XmlInclude(typeof(FindFolderResponseMessageType))]
	[XmlInclude(typeof(FolderInfoResponseMessageType))]
	[XmlInclude(typeof(ExportItemsResponseMessageType))]
	[XmlInclude(typeof(UploadItemsResponseMessageType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ResponseMessageType
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001FB8F File Offset: 0x0001DD8F
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0001FB97 File Offset: 0x0001DD97
		public string MessageText
		{
			get
			{
				return this.messageTextField;
			}
			set
			{
				this.messageTextField = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		public ResponseCodeType ResponseCode
		{
			get
			{
				return this.responseCodeField;
			}
			set
			{
				this.responseCodeField = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0001FBB1 File Offset: 0x0001DDB1
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x0001FBB9 File Offset: 0x0001DDB9
		[XmlIgnore]
		public bool ResponseCodeSpecified
		{
			get
			{
				return this.responseCodeFieldSpecified;
			}
			set
			{
				this.responseCodeFieldSpecified = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0001FBCA File Offset: 0x0001DDCA
		public int DescriptiveLinkKey
		{
			get
			{
				return this.descriptiveLinkKeyField;
			}
			set
			{
				this.descriptiveLinkKeyField = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001FBD3 File Offset: 0x0001DDD3
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0001FBDB File Offset: 0x0001DDDB
		[XmlIgnore]
		public bool DescriptiveLinkKeySpecified
		{
			get
			{
				return this.descriptiveLinkKeyFieldSpecified;
			}
			set
			{
				this.descriptiveLinkKeyFieldSpecified = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001FBE4 File Offset: 0x0001DDE4
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		public ResponseMessageTypeMessageXml MessageXml
		{
			get
			{
				return this.messageXmlField;
			}
			set
			{
				this.messageXmlField = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001FBF5 File Offset: 0x0001DDF5
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0001FBFD File Offset: 0x0001DDFD
		[XmlAttribute]
		public ResponseClassType ResponseClass
		{
			get
			{
				return this.responseClassField;
			}
			set
			{
				this.responseClassField = value;
			}
		}

		// Token: 0x04000367 RID: 871
		private string messageTextField;

		// Token: 0x04000368 RID: 872
		private ResponseCodeType responseCodeField;

		// Token: 0x04000369 RID: 873
		private bool responseCodeFieldSpecified;

		// Token: 0x0400036A RID: 874
		private int descriptiveLinkKeyField;

		// Token: 0x0400036B RID: 875
		private bool descriptiveLinkKeyFieldSpecified;

		// Token: 0x0400036C RID: 876
		private ResponseMessageTypeMessageXml messageXmlField;

		// Token: 0x0400036D RID: 877
		private ResponseClassType responseClassField;
	}
}
