using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000077 RID: 119
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseMessage))]
	[XmlInclude(typeof(DeleteAttachmentResponseMessage))]
	[XmlInclude(typeof(FindFolderResponseMessage))]
	[KnownType(typeof(AddImGroupResponseMessage))]
	[KnownType(typeof(RemoveImContactFromGroupResponseMessage))]
	[KnownType(typeof(AddImContactToGroupResponseMessage))]
	[XmlInclude(typeof(PerformInstantSearchResponse))]
	[XmlInclude(typeof(ExpandDLResponseMessage))]
	[XmlInclude(typeof(FindMailboxStatisticsByKeywordsResponseMessage))]
	[XmlInclude(typeof(GetDiscoverySearchConfigurationResponse))]
	[XmlInclude(typeof(GetEventsResponseMessage))]
	[XmlInclude(typeof(GetHoldOnMailboxesResponse))]
	[XmlInclude(typeof(GetNonIndexableItemDetailsResponse))]
	[XmlInclude(typeof(GetNonIndexableItemStatisticsResponse))]
	[XmlInclude(typeof(GetPasswordExpirationDateResponse))]
	[XmlInclude(typeof(GetSearchableMailboxesResponse))]
	[XmlInclude(typeof(GetStreamingEventsResponseMessage))]
	[XmlInclude(typeof(GetUserConfigurationResponseMessage))]
	[XmlInclude(typeof(GetUserRetentionPolicyTagsResponse))]
	[XmlInclude(typeof(ResolveNamesResponseMessage))]
	[XmlInclude(typeof(SearchMailboxesResponseMessage))]
	[XmlInclude(typeof(SetHoldOnMailboxesResponse))]
	[XmlInclude(typeof(SetImListMigrationCompletedResponseMessage))]
	[XmlInclude(typeof(SubscribeResponseMessage))]
	[XmlInclude(typeof(SyncFolderHierarchyResponseMessage))]
	[XmlInclude(typeof(SyncFolderItemsResponseMessage))]
	[XmlInclude(typeof(InstallAppResponseMessage))]
	[XmlInclude(typeof(UninstallAppResponseMessage))]
	[XmlInclude(typeof(GetClientExtensionResponse))]
	[XmlInclude(typeof(GetEncryptionConfigurationResponse))]
	[XmlInclude(typeof(SetEncryptionConfigurationResponse))]
	[XmlInclude(typeof(GetUserPhotoResponseMessage))]
	[XmlInclude(typeof(GetPeopleICommunicateWithResponseMessage))]
	[XmlInclude(typeof(DisableAppResponseMessage))]
	[XmlInclude(typeof(GetAppMarketplaceUrlResponseMessage))]
	[XmlInclude(typeof(AddAggregatedAccountResponseMessage))]
	[XmlInclude(typeof(MarkAsJunkResponseMessage))]
	[XmlInclude(typeof(GetAggregatedAccountResponseMessage))]
	[XmlInclude(typeof(RemoveAggregatedAccountResponseMessage))]
	[XmlInclude(typeof(SetAggregatedAccountResponseMessage))]
	[XmlInclude(typeof(ApplyConversationActionResponseMessage))]
	[XmlInclude(typeof(DeleteItemResponseMessage))]
	[KnownType(typeof(CreateUnifiedMailboxResponseMessage))]
	[KnownType(typeof(ExpandDLResponseMessage))]
	[KnownType(typeof(FindMailboxStatisticsByKeywordsResponseMessage))]
	[KnownType(typeof(GetClientAccessTokenResponseMessage))]
	[KnownType(typeof(GetDiscoverySearchConfigurationResponse))]
	[KnownType(typeof(GetEventsResponseMessage))]
	[KnownType(typeof(GetHoldOnMailboxesResponse))]
	[KnownType(typeof(GetPasswordExpirationDateResponse))]
	[KnownType(typeof(GetSearchableMailboxesResponse))]
	[KnownType(typeof(GetStreamingEventsResponseMessage))]
	[KnownType(typeof(GetUserConfigurationResponseMessage))]
	[KnownType(typeof(GetUserRetentionPolicyTagsResponse))]
	[KnownType(typeof(ResolveNamesResponseMessage))]
	[KnownType(typeof(SearchMailboxesResponseMessage))]
	[KnownType(typeof(SetHoldOnMailboxesResponse))]
	[KnownType(typeof(SetImListMigrationCompletedResponseMessage))]
	[KnownType(typeof(SubscribeResponseMessage))]
	[KnownType(typeof(SyncFolderHierarchyResponseMessage))]
	[KnownType(typeof(SyncFolderItemsResponseMessage))]
	[KnownType(typeof(InstallAppResponseMessage))]
	[KnownType(typeof(UninstallAppResponseMessage))]
	[KnownType(typeof(GetClientExtensionResponse))]
	[KnownType(typeof(GetEncryptionConfigurationResponse))]
	[KnownType(typeof(SetEncryptionConfigurationResponse))]
	[KnownType(typeof(GetUserPhotoResponseMessage))]
	[KnownType(typeof(GetPeopleICommunicateWithResponseMessage))]
	[KnownType(typeof(DisableAppResponseMessage))]
	[KnownType(typeof(GetAppMarketplaceUrlResponseMessage))]
	[KnownType(typeof(AddAggregatedAccountResponseMessage))]
	[KnownType(typeof(SubscribeToPushNotificationResponse))]
	[KnownType(typeof(UnsubscribeToPushNotificationResponse))]
	[KnownType(typeof(MarkAsJunkResponseMessage))]
	[KnownType(typeof(GetAggregatedAccountResponseMessage))]
	[KnownType(typeof(RemoveAggregatedAccountResponseMessage))]
	[KnownType(typeof(SetAggregatedAccountResponseMessage))]
	[KnownType(typeof(GetInboxRulesResponse))]
	[KnownType(typeof(ApplyConversationActionResponseMessage))]
	[KnownType(typeof(DeleteItemResponseMessage))]
	[XmlInclude(typeof(AddDistributionGroupToImListResponseMessage))]
	[XmlInclude(typeof(AddImContactToGroupResponseMessage))]
	[XmlInclude(typeof(RemoveImContactFromGroupResponseMessage))]
	[XmlInclude(typeof(AddImGroupResponseMessage))]
	[XmlInclude(typeof(AddNewImContactToGroupResponseMessage))]
	[XmlInclude(typeof(AddNewTelUriContactToGroupResponseMessage))]
	[XmlInclude(typeof(AttachmentInfoResponseMessage))]
	[XmlInclude(typeof(ConvertIdResponseMessage))]
	[XmlInclude(typeof(FindItemResponseMessage))]
	[XmlInclude(typeof(FindPeopleResponseMessage))]
	[XmlInclude(typeof(FolderInfoResponseMessage))]
	[XmlInclude(typeof(GetConversationItemsResponseMessage))]
	[XmlInclude(typeof(GetImItemListResponseMessage))]
	[XmlInclude(typeof(GetImItemsResponseMessage))]
	[XmlInclude(typeof(GetInboxRulesResponse))]
	[XmlInclude(typeof(GetPersonaResponseMessage))]
	[XmlInclude(typeof(ItemInfoResponseMessage))]
	[XmlInclude(typeof(RemoveContactFromImListResponseMessage))]
	[XmlInclude(typeof(RemoveDistributionGroupFromImListResponseMessage))]
	[XmlInclude(typeof(RemoveImGroupResponseMessage))]
	[XmlInclude(typeof(SetImGroupResponseMessage))]
	[XmlInclude(typeof(UpdateInboxRulesResponse))]
	[XmlInclude(typeof(UpdateItemResponseMessage))]
	[XmlInclude(typeof(EndInstantSearchSessionResponse))]
	[XmlInclude(typeof(GetUserUnifiedGroupsResponseMessage))]
	[XmlType("ResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(AddDistributionGroupToImListResponseMessage))]
	[XmlInclude(typeof(CreateUnifiedMailboxResponseMessage))]
	[KnownType(typeof(AddNewImContactToGroupResponseMessage))]
	[KnownType(typeof(AddNewTelUriContactToGroupResponseMessage))]
	[KnownType(typeof(AttachmentInfoResponseMessage))]
	[KnownType(typeof(ConvertIdResponseMessage))]
	[KnownType(typeof(DeleteAttachmentResponseMessage))]
	[KnownType(typeof(FindFolderResponseMessage))]
	[KnownType(typeof(FindItemResponseMessage))]
	[KnownType(typeof(FindPeopleResponseMessage))]
	[KnownType(typeof(FolderInfoResponseMessage))]
	[KnownType(typeof(GetConversationItemsResponseMessage))]
	[KnownType(typeof(GetThreadedConversationItemsResponseMessage))]
	[KnownType(typeof(GetConversationItemsDiagnosticsResponseMessage))]
	[KnownType(typeof(GetImItemListResponseMessage))]
	[KnownType(typeof(GetImItemsResponseMessage))]
	[KnownType(typeof(GetPersonaResponseMessage))]
	[KnownType(typeof(ItemInfoResponseMessage))]
	[KnownType(typeof(RemoveContactFromImListResponseMessage))]
	[KnownType(typeof(RemoveDistributionGroupFromImListResponseMessage))]
	[KnownType(typeof(RemoveImGroupResponseMessage))]
	[KnownType(typeof(SetImGroupResponseMessage))]
	[KnownType(typeof(UpdateInboxRulesResponse))]
	[KnownType(typeof(UpdateItemResponseMessage))]
	[KnownType(typeof(GetUserUnifiedGroupsResponseMessage))]
	[KnownType(typeof(GetClutterStateResponse))]
	[KnownType(typeof(SetClutterStateResponse))]
	[KnownType(typeof(PerformInstantSearchResponse))]
	[KnownType(typeof(EndInstantSearchSessionResponse))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(GetModernConversationAttachmentsResponseMessage))]
	[KnownType(typeof(LogPushNotificationDataResponse))]
	public class ResponseMessage : IExchangeWebMethodResponse
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		private void InternalInitialize(ServiceError error)
		{
			this.messageXml = error.MessageXml;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EECC File Offset: 0x0000D0CC
		private string GetMessageXmlAsString()
		{
			XmlNodeArray xmlNodeArray = this.MessageXml;
			if (xmlNodeArray != null)
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(xmlNodeArray.GetType(), "http://www.w3.org/2001/XMLSchema-instance");
				using (MemoryStream memoryStream = new MemoryStream())
				{
					try
					{
						safeXmlSerializer.Serialize(memoryStream, xmlNodeArray, ResponseMessage.namespaces);
						memoryStream.Position = 0L;
						using (StreamReader streamReader = new StreamReader(memoryStream))
						{
							return streamReader.ReadToEnd();
						}
					}
					catch (InvalidOperationException ex)
					{
						ExTraceGlobals.UtilCallTracer.TraceError<string, string>((long)this.GetHashCode(), "GetMessageXmlAsString failed to serialize MessageXml. Exception class: {0}, Message: {1}", ex.GetType().FullName, ex.Message);
					}
				}
			}
			return null;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000EF94 File Offset: 0x0000D194
		static ResponseMessage()
		{
			ResponseMessage.namespaces.Add("m", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ResponseMessage.namespaces.Add("t", "http://schemas.microsoft.com/exchange/services/2006/types");
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		public ResponseMessage()
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		internal ResponseMessage(ServiceResultCode code, ServiceError error)
		{
			this.Initialize(code, error);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		internal bool StopsBatchProcessing
		{
			get
			{
				return this.error != null && this.error.StopsBatchProcessing;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		internal void Initialize(ServiceResultCode code, ServiceError error)
		{
			switch (code)
			{
			case ServiceResultCode.Success:
				this.responseClass = ResponseClass.Success;
				return;
			case ServiceResultCode.Warning:
				this.error = error;
				this.InternalInitialize(error);
				this.responseClass = ResponseClass.Warning;
				return;
			case ServiceResultCode.Error:
				this.error = error;
				this.InternalInitialize(error);
				this.responseClass = ResponseClass.Error;
				return;
			default:
				return;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F058 File Offset: 0x0000D258
		protected void ExecuteServiceCommandIfRequired()
		{
			if (!this.hasServiceCommandExecuted)
			{
				try
				{
					ServiceDiagnostics.SendWatsonReportOnUnhandledException(delegate
					{
						this.InternalExecuteServiceCommand();
					});
				}
				finally
				{
					this.MessageText = ((this.error != null) ? this.error.MessageText : null);
					this.ResponseCode = ((this.error != null) ? this.error.MessageKey : ResponseCodeType.NoError);
					this.DescriptiveLinkKey = ((this.error != null) ? this.error.DescriptiveLinkId : 0);
				}
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		protected virtual void InternalExecuteServiceCommand()
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000F0EE File Offset: 0x0000D2EE
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000F0FC File Offset: 0x0000D2FC
		[XmlElement("MessageText", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string MessageText
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.messageText;
			}
			set
			{
				this.hasServiceCommandExecuted = true;
				this.messageText = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000F10C File Offset: 0x0000D30C
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000F11A File Offset: 0x0000D31A
		[IgnoreDataMember]
		[XmlElement("ResponseCode")]
		public ResponseCodeType ResponseCode
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.responseCode;
			}
			set
			{
				this.hasServiceCommandExecuted = true;
				this.responseCode = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F12A File Offset: 0x0000D32A
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000F137 File Offset: 0x0000D337
		[DataMember(Name = "ResponseCode", Order = 2)]
		[XmlIgnore]
		public string ResponseCodeString
		{
			get
			{
				return EnumUtilities.ToString<ResponseCodeType>(this.ResponseCode);
			}
			set
			{
				this.ResponseCode = EnumUtilities.Parse<ResponseCodeType>(value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F145 File Offset: 0x0000D345
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000F153 File Offset: 0x0000D353
		[DataMember(EmitDefaultValue = false, Order = 3)]
		[XmlElement("DescriptiveLinkKey")]
		public int DescriptiveLinkKey
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.descriptiveLinkKey;
			}
			set
			{
				this.hasServiceCommandExecuted = true;
				this.descriptiveLinkKey = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F163 File Offset: 0x0000D363
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000F171 File Offset: 0x0000D371
		[XmlElement("MessageXml")]
		[IgnoreDataMember]
		public XmlNodeArray MessageXml
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.messageXml;
			}
			set
			{
				this.messageXml = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F17A File Offset: 0x0000D37A
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000F191 File Offset: 0x0000D391
		[XmlIgnore]
		[DataMember(Name = "MessageXml", EmitDefaultValue = false, Order = 4)]
		public string MessageXmlString
		{
			get
			{
				if (this.messageXmlStringSet)
				{
					return this.messageXmlString;
				}
				return this.GetMessageXmlAsString();
			}
			set
			{
				this.messageXmlStringSet = true;
				this.messageXmlString = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000F1AF File Offset: 0x0000D3AF
		[XmlAttribute("ResponseClass")]
		[IgnoreDataMember]
		public ResponseClass ResponseClass
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.responseClass;
			}
			set
			{
				this.hasServiceCommandExecuted = true;
				this.responseClass = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000F1BF File Offset: 0x0000D3BF
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000F1CC File Offset: 0x0000D3CC
		[XmlIgnore]
		[DataMember(Name = "ResponseClass", Order = 5)]
		public string ResponseClassString
		{
			get
			{
				return EnumUtilities.ToString<ResponseClass>(this.ResponseClass);
			}
			set
			{
				this.ResponseClass = EnumUtilities.Parse<ResponseClass>(value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000F1DA File Offset: 0x0000D3DA
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000F1EE File Offset: 0x0000D3EE
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MessageIndexSpecified
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.error != null;
			}
			set
			{
				throw new InvalidOperationException("ResponseMessage.MessageIndexSpecified.set should never be called.");
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000F20E File Offset: 0x0000D40E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool DescriptiveLinkKeySpecified
		{
			get
			{
				this.ExecuteServiceCommandIfRequired();
				return this.error != null;
			}
			set
			{
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000F210 File Offset: 0x0000D410
		public virtual ResponseType GetResponseType()
		{
			ExTraceGlobals.UtilCallTracer.TraceError((long)this.GetHashCode(), "Override GetResponseType in your response derived class, the base class doesn't provide implementation.");
			throw new InvalidOperationException("Override GetResponseType in your response derived class");
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F232 File Offset: 0x0000D432
		ResponseCodeType IExchangeWebMethodResponse.GetErrorCodeToLog()
		{
			return this.ResponseCode;
		}

		// Token: 0x040005B1 RID: 1457
		private ServiceError error;

		// Token: 0x040005B2 RID: 1458
		private ResponseClass responseClass;

		// Token: 0x040005B3 RID: 1459
		private string messageText;

		// Token: 0x040005B4 RID: 1460
		private ResponseCodeType responseCode;

		// Token: 0x040005B5 RID: 1461
		private int descriptiveLinkKey;

		// Token: 0x040005B6 RID: 1462
		private XmlNodeArray messageXml;

		// Token: 0x040005B7 RID: 1463
		private bool messageXmlStringSet;

		// Token: 0x040005B8 RID: 1464
		private string messageXmlString;

		// Token: 0x040005B9 RID: 1465
		protected static XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

		// Token: 0x040005BA RID: 1466
		private bool hasServiceCommandExecuted;
	}
}
