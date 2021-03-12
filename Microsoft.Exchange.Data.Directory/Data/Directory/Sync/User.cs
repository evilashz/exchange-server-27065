using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000862 RID: 2146
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class User : DirectoryObject, IValidationErrorSupport
	{
		// Token: 0x06006B81 RID: 27521 RVA: 0x00173A30 File Offset: 0x00171C30
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyXmlAssignedPlan>(SyncUserSchema.AssignedPlan, ref this.assignedPlanField);
			processor.Process<DirectoryPropertyStringSingleLength1To3>(SyncOrgPersonSchema.C, ref this.cField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CloudLegacyExchangeDN, ref this.cloudLegacyExchangeDNField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.CloudMsExchArchiveStatus, ref this.cloudMSExchArchiveStatusField);
			processor.Process<DirectoryPropertyBinarySingleLength1To4000>(SyncUserSchema.CloudMsExchBlockedSendersHash, ref this.cloudMSExchBlockedSendersHashField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.CloudMsExchRecipientDisplayType, ref this.cloudMSExchRecipientDisplayTypeField);
			processor.Process<DirectoryPropertyBinarySingleLength1To12000>(SyncUserSchema.CloudMsExchSafeRecipientsHash, ref this.cloudMSExchSafeRecipientsHashField);
			processor.Process<DirectoryPropertyBinarySingleLength1To32000>(SyncUserSchema.CloudMsExchSafeSendersHash, ref this.cloudMSExchSafeSendersHashField);
			processor.Process<DirectoryPropertyStringLength1To1123>(SyncUserSchema.CloudMsExchUCVoiceMailSettings, ref this.cloudMSExchUCVoiceMailSettingsField);
			processor.Process<DirectoryPropertyStringLength1To40>(SyncUserSchema.CloudMsExchUserHoldPolicies, ref this.cloudMSExchUserHoldPoliciesField);
			processor.Process<DirectoryPropertyReferenceAddressList>(SyncUserSchema.CloudSiteMailboxOwners, ref this.cloudMSExchTeamMailboxOwnersField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.CloudSiteMailboxClosedTime, ref this.cloudMSExchTeamMailboxExpirationField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncUserSchema.CloudSharePointUrl, ref this.cloudMSExchTeamMailboxSharePointUrlField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.Co, ref this.coField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Company, ref this.companyField);
			processor.Process<DirectoryPropertyInt32SingleMin0Max65535>(SyncOrgPersonSchema.CountryCode, ref this.countryCodeField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Department, ref this.departmentField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncUserSchema.DeliverToMailboxAndForward, ref this.deliverAndRedirectField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.DisplayName, ref this.displayNameField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.IsDirSynced, ref this.dirSyncEnabledField);
			DirectoryPropertyAttributeSet directoryPropertyAttributeSet = (DirectoryPropertyAttributeSet)DirectoryObject.GetDirectoryProperty(this.singleAuthorityMetadataField);
			processor.Process<DirectoryPropertyAttributeSet>(SyncRecipientSchema.DirSyncAuthorityMetadata, ref directoryPropertyAttributeSet);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute1, ref this.extensionAttribute1Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute10, ref this.extensionAttribute10Field);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CustomAttribute11, ref this.extensionAttribute11Field);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CustomAttribute12, ref this.extensionAttribute12Field);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CustomAttribute13, ref this.extensionAttribute13Field);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CustomAttribute14, ref this.extensionAttribute14Field);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CustomAttribute15, ref this.extensionAttribute15Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute2, ref this.extensionAttribute2Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute3, ref this.extensionAttribute3Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute4, ref this.extensionAttribute4Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute5, ref this.extensionAttribute5Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute6, ref this.extensionAttribute6Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute7, ref this.extensionAttribute7Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute8, ref this.extensionAttribute8Field);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.CustomAttribute9, ref this.extensionAttribute9Field);
			processor.Process<DirectoryPropertyStringLength1To2048>(SyncRecipientSchema.ExtensionCustomAttribute1, ref this.mSExchExtensionCustomAttribute1Field);
			processor.Process<DirectoryPropertyStringLength1To2048>(SyncRecipientSchema.ExtensionCustomAttribute2, ref this.mSExchExtensionCustomAttribute2Field);
			processor.Process<DirectoryPropertyStringLength1To2048>(SyncRecipientSchema.ExtensionCustomAttribute3, ref this.mSExchExtensionCustomAttribute3Field);
			processor.Process<DirectoryPropertyStringLength1To2048>(SyncRecipientSchema.ExtensionCustomAttribute4, ref this.mSExchExtensionCustomAttribute4Field);
			processor.Process<DirectoryPropertyStringLength1To2048>(SyncRecipientSchema.ExtensionCustomAttribute5, ref this.mSExchExtensionCustomAttribute5Field);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Fax, ref this.facsimileTelephoneNumberField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.FirstName, ref this.givenNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.HomePhone, ref this.homePhoneField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncOrgPersonSchema.Notes, ref this.infoField);
			processor.Process<DirectoryPropertyStringSingleLength1To6>(SyncOrgPersonSchema.Initials, ref this.initialsField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.City, ref this.lField);
			processor.Process<DirectoryPropertyStringSingleMailNickname>(SyncRecipientSchema.Alias, ref this.mailNicknameField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.MobilePhone, ref this.mobileField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.SeniorityIndex, ref this.mSDSHABSeniorityIndexField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.PhoneticDisplayName, ref this.mSDSPhoneticDisplayNameField);
			processor.Process<DirectoryPropertyGuidSingle>(SyncUserSchema.ArchiveGuid, ref this.mSExchArchiveGuidField);
			processor.Process<DirectoryPropertyStringLength1To512>(SyncUserSchema.ArchiveName, ref this.mSExchArchiveNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncOrgPersonSchema.AssistantName, ref this.mSExchAssistantNameField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.AuditAdminFlags, ref this.mSExchAuditAdminField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.AuditDelegateFlags, ref this.mSExchAuditDelegateField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.AuditDelegateAdminFlags, ref this.mSExchAuditDelegateAdminField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.AuditOwnerFlags, ref this.mSExchAuditOwnerField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncUserSchema.AuditBypassEnabled, ref this.mSExchBypassAuditField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.ElcExpirationSuspensionEndDate, ref this.mSExchElcExpirySuspensionEndField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.ElcExpirationSuspensionStartDate, ref this.mSExchElcExpirySuspensionStartField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.ElcMailboxFlags, ref this.mSExchElcMailboxFlagsField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.ModerationEnabled, ref this.mSExchEnableModerationField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.HiddenFromAddressListsEnabled, ref this.mSExchHideFromAddressListsField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncUserSchema.ImmutableId, ref this.mSExchImmutableIdField);
			processor.Process<DirectoryPropertyStringLength1To40>(SyncUserSchema.InPlaceHoldsRaw, ref this.mSExchUserHoldPoliciesField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncRecipientSchema.LitigationHoldDate, ref this.mSExchLitigationHoldDateField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.LitigationHoldOwner, ref this.mSExchLitigationHoldOwnerField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncUserSchema.AuditEnabled, ref this.mSExchMailboxAuditEnableField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.AuditLogAgeLimit, ref this.mSExchMailboxAuditLogAgeLimitField);
			processor.Process<DirectoryPropertyGuidSingle>(SyncUserSchema.ExchangeGuid, ref this.mSExchMailboxGuidField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.RecipientDisplayType, ref this.mSExchRecipientDisplayTypeField);
			processor.Process<DirectoryPropertyInt64Single>(SyncRecipientSchema.RecipientTypeDetailsValue, ref this.mSExchRecipientTypeDetailsField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.RequireAllSendersAreAuthenticated, ref this.mSExchRequireAuthToSendToField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.RetentionComment, ref this.mSExchRetentionCommentField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.RetentionUrl, ref this.mSExchRetentionUrlField);
			processor.Process<DirectoryPropertyStringLength2To500>(SyncRecipientSchema.MailTipTranslations, ref this.mSExchSenderHintTranslationsField);
			processor.Process<DirectoryPropertyStringLength1To64>(SyncOrgPersonSchema.OtherFax, ref this.otherFacsimileTelephoneNumberField);
			processor.Process<DirectoryPropertyStringLength1To64>(SyncOrgPersonSchema.OtherHomePhone, ref this.otherHomePhoneField);
			processor.Process<DirectoryPropertyStringLength1To64>(SyncOrgPersonSchema.OtherTelephone, ref this.otherTelephoneField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Pager, ref this.pagerField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.Office, ref this.physicalDeliveryOfficeNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To40>(SyncOrgPersonSchema.PostalCode, ref this.postalCodeField);
			processor.Process<DirectoryPropertyProxyAddresses>(SyncRecipientSchema.EmailAddresses, ref this.proxyAddressesField);
			processor.Process<DirectoryPropertyProxyAddresses>(SyncRecipientSchema.SmtpAndX500Addresses, ref this.proxyAddressesField);
			processor.Process<DirectoryPropertyStringSingleLength1To454>(SyncRecipientSchema.SipAddresses, ref this.sipProxyAddressField);
			processor.Process<DirectoryPropertyXmlServiceInfo>(SyncUserSchema.ServiceInfo, ref this.serviceInfoField);
			processor.Process<DirectoryPropertyXmlServiceOriginatedResource>(SyncUserSchema.ServiceOriginatedResource, ref this.serviceOriginatedResourceField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncRecipientSchema.Cn, ref this.shadowCommonNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.LastName, ref this.snField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.OnPremisesObjectId, ref this.sourceAnchorField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.StateOrProvince, ref this.stField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncOrgPersonSchema.StreetAddress, ref this.streetAddressField);
			processor.Process<DirectoryPropertyTargetAddress>(SyncRecipientSchema.ExternalEmailAddress, ref this.targetAddressField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.TelephoneAssistant, ref this.telephoneAssistantField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Phone, ref this.telephoneNumberField);
			processor.Process<DirectoryPropertyBinarySingleLength1To102400>(SyncUserSchema.Picture, ref this.thumbnailPhotoField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.Title, ref this.titleField);
			processor.Process<DirectoryPropertyStringSingleLength1To3>(SyncUserSchema.UsageLocation, ref this.usageLocationField);
			processor.Process<DirectoryPropertyXmlValidationError>(SyncRecipientSchema.ValidationError, ref this.validationErrorField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncUserSchema.WindowsLiveID, ref this.userPrincipalNameField);
			processor.Process<DirectoryPropertyBinarySingleLength8>(SyncUserSchema.NetID, ref this.windowsLiveNetIdField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncOrgPersonSchema.WebPage, ref this.wwwHomepageField);
			processor.Process<DirectoryPropertyInt64Single>(SyncUserSchema.RemoteRecipientType, ref this.mSExchRemoteRecipientTypeField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.ModerationFlags, ref this.mSExchModerationFlagsField);
			processor.Process<DirectoryPropertyBinarySingleLength1To4000>(SyncRecipientSchema.BlockedSendersHash, ref this.mSExchBlockedSendersHashField);
			processor.Process<DirectoryPropertyBinarySingleLength1To12000>(SyncRecipientSchema.SafeRecipientsHash, ref this.mSExchSafeRecipientsHashField);
			processor.Process<DirectoryPropertyBinarySingleLength1To32000>(SyncRecipientSchema.SafeSendersHash, ref this.mSExchSafeSendersHashField);
			processor.Process<DirectoryPropertyInt32Single>(SyncUserSchema.ResourceCapacity, ref this.mSExchResourceCapacityField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncUserSchema.ResourcePropertiesDisplay, ref this.mSExchResourceDisplayField);
			processor.Process<DirectoryPropertyStringLength1To1024>(SyncUserSchema.ResourceMetaData, ref this.mSExchResourceMetadataField);
			processor.Process<DirectoryPropertyStringLength1To1024>(SyncUserSchema.ResourceSearchProperties, ref this.mSExchResourceSearchPropertiesField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.WhenSoftDeleted, ref this.softDeletionTimestampField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.MSExchUserCreatedTimestamp, ref this.mSExchUserCreatedTimestampField);
			processor.Process<DirectoryPropertyReferenceAddressList>(SyncUserSchema.SiteMailboxOwners, ref this.mSExchTeamMailboxOwnersField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.SiteMailboxClosedTime, ref this.mSExchTeamMailboxExpirationField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncUserSchema.SharePointUrl, ref this.mSExchTeamMailboxSharePointUrlField);
			processor.Process<DirectoryPropertyBinaryLength1To32768>(SyncRecipientSchema.UserCertificate, ref this.userCertificateField);
			processor.Process<DirectoryPropertyBinaryLength1To32768>(SyncRecipientSchema.UserSMimeCertificate, ref this.userSMIMECertificateField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncUserSchema.AccountEnabled, ref this.accountEnabledField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncUserSchema.StsRefreshTokensValidFrom, ref this.stsRefreshTokensValidFromField);
		}

		// Token: 0x17002641 RID: 9793
		// (get) Token: 0x06006B82 RID: 27522 RVA: 0x00174253 File Offset: 0x00172453
		// (set) Token: 0x06006B83 RID: 27523 RVA: 0x0017425B File Offset: 0x0017245B
		[XmlElement(Order = 0)]
		public DirectoryPropertyStringSingleLength1To256 AcceptedAs
		{
			get
			{
				return this.acceptedAsField;
			}
			set
			{
				this.acceptedAsField = value;
			}
		}

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x06006B84 RID: 27524 RVA: 0x00174264 File Offset: 0x00172464
		// (set) Token: 0x06006B85 RID: 27525 RVA: 0x0017426C File Offset: 0x0017246C
		[XmlElement(Order = 1)]
		public DirectoryPropertyBooleanSingle AccountEnabled
		{
			get
			{
				return this.accountEnabledField;
			}
			set
			{
				this.accountEnabledField = value;
			}
		}

		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x06006B86 RID: 27526 RVA: 0x00174275 File Offset: 0x00172475
		// (set) Token: 0x06006B87 RID: 27527 RVA: 0x0017427D File Offset: 0x0017247D
		[XmlElement(Order = 2)]
		public DirectoryPropertyXmlAlternativeSecurityId AlternativeSecurityId
		{
			get
			{
				return this.alternativeSecurityIdField;
			}
			set
			{
				this.alternativeSecurityIdField = value;
			}
		}

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x06006B88 RID: 27528 RVA: 0x00174286 File Offset: 0x00172486
		// (set) Token: 0x06006B89 RID: 27529 RVA: 0x0017428E File Offset: 0x0017248E
		[XmlElement(Order = 3)]
		public DirectoryPropertyXmlAssignedPlan AssignedPlan
		{
			get
			{
				return this.assignedPlanField;
			}
			set
			{
				this.assignedPlanField = value;
			}
		}

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x06006B8A RID: 27530 RVA: 0x00174297 File Offset: 0x00172497
		// (set) Token: 0x06006B8B RID: 27531 RVA: 0x0017429F File Offset: 0x0017249F
		[XmlElement(Order = 4)]
		public DirectoryPropertyReferenceAddressListSingle Assistant
		{
			get
			{
				return this.assistantField;
			}
			set
			{
				this.assistantField = value;
			}
		}

		// Token: 0x17002646 RID: 9798
		// (get) Token: 0x06006B8C RID: 27532 RVA: 0x001742A8 File Offset: 0x001724A8
		// (set) Token: 0x06006B8D RID: 27533 RVA: 0x001742B0 File Offset: 0x001724B0
		[XmlElement(Order = 5)]
		public DirectoryPropertyStringSingleLength1To3 C
		{
			get
			{
				return this.cField;
			}
			set
			{
				this.cField = value;
			}
		}

		// Token: 0x17002647 RID: 9799
		// (get) Token: 0x06006B8E RID: 27534 RVA: 0x001742B9 File Offset: 0x001724B9
		// (set) Token: 0x06006B8F RID: 27535 RVA: 0x001742C1 File Offset: 0x001724C1
		[XmlElement(Order = 6)]
		public DirectoryPropertyStringSingleLength1To2048 CloudLegacyExchangeDN
		{
			get
			{
				return this.cloudLegacyExchangeDNField;
			}
			set
			{
				this.cloudLegacyExchangeDNField = value;
			}
		}

		// Token: 0x17002648 RID: 9800
		// (get) Token: 0x06006B90 RID: 27536 RVA: 0x001742CA File Offset: 0x001724CA
		// (set) Token: 0x06006B91 RID: 27537 RVA: 0x001742D2 File Offset: 0x001724D2
		[XmlElement(Order = 7)]
		public DirectoryPropertyInt32Single CloudMSExchArchiveStatus
		{
			get
			{
				return this.cloudMSExchArchiveStatusField;
			}
			set
			{
				this.cloudMSExchArchiveStatusField = value;
			}
		}

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x06006B92 RID: 27538 RVA: 0x001742DB File Offset: 0x001724DB
		// (set) Token: 0x06006B93 RID: 27539 RVA: 0x001742E3 File Offset: 0x001724E3
		[XmlElement(Order = 8)]
		public DirectoryPropertyBinarySingleLength1To4000 CloudMSExchBlockedSendersHash
		{
			get
			{
				return this.cloudMSExchBlockedSendersHashField;
			}
			set
			{
				this.cloudMSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x06006B94 RID: 27540 RVA: 0x001742EC File Offset: 0x001724EC
		// (set) Token: 0x06006B95 RID: 27541 RVA: 0x001742F4 File Offset: 0x001724F4
		[XmlElement(Order = 9)]
		public DirectoryPropertyGuidSingle CloudMSExchMailboxGuid
		{
			get
			{
				return this.cloudMSExchMailboxGuidField;
			}
			set
			{
				this.cloudMSExchMailboxGuidField = value;
			}
		}

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x06006B96 RID: 27542 RVA: 0x001742FD File Offset: 0x001724FD
		// (set) Token: 0x06006B97 RID: 27543 RVA: 0x00174305 File Offset: 0x00172505
		[XmlElement(Order = 10)]
		public DirectoryPropertyInt32Single CloudMSExchRecipientDisplayType
		{
			get
			{
				return this.cloudMSExchRecipientDisplayTypeField;
			}
			set
			{
				this.cloudMSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x06006B98 RID: 27544 RVA: 0x0017430E File Offset: 0x0017250E
		// (set) Token: 0x06006B99 RID: 27545 RVA: 0x00174316 File Offset: 0x00172516
		[XmlElement(Order = 11)]
		public DirectoryPropertyBinarySingleLength1To12000 CloudMSExchSafeRecipientsHash
		{
			get
			{
				return this.cloudMSExchSafeRecipientsHashField;
			}
			set
			{
				this.cloudMSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x06006B9A RID: 27546 RVA: 0x0017431F File Offset: 0x0017251F
		// (set) Token: 0x06006B9B RID: 27547 RVA: 0x00174327 File Offset: 0x00172527
		[XmlElement(Order = 12)]
		public DirectoryPropertyBinarySingleLength1To32000 CloudMSExchSafeSendersHash
		{
			get
			{
				return this.cloudMSExchSafeSendersHashField;
			}
			set
			{
				this.cloudMSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x06006B9C RID: 27548 RVA: 0x00174330 File Offset: 0x00172530
		// (set) Token: 0x06006B9D RID: 27549 RVA: 0x00174338 File Offset: 0x00172538
		[XmlElement(Order = 13)]
		public DirectoryPropertyDateTimeSingle CloudMSExchTeamMailboxExpiration
		{
			get
			{
				return this.cloudMSExchTeamMailboxExpirationField;
			}
			set
			{
				this.cloudMSExchTeamMailboxExpirationField = value;
			}
		}

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x06006B9E RID: 27550 RVA: 0x00174341 File Offset: 0x00172541
		// (set) Token: 0x06006B9F RID: 27551 RVA: 0x00174349 File Offset: 0x00172549
		[XmlElement(Order = 14)]
		public DirectoryPropertyReferenceAddressList CloudMSExchTeamMailboxOwners
		{
			get
			{
				return this.cloudMSExchTeamMailboxOwnersField;
			}
			set
			{
				this.cloudMSExchTeamMailboxOwnersField = value;
			}
		}

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x00174352 File Offset: 0x00172552
		// (set) Token: 0x06006BA1 RID: 27553 RVA: 0x0017435A File Offset: 0x0017255A
		[XmlElement(Order = 15)]
		public DirectoryPropertyStringSingleLength1To2048 CloudMSExchTeamMailboxSharePointUrl
		{
			get
			{
				return this.cloudMSExchTeamMailboxSharePointUrlField;
			}
			set
			{
				this.cloudMSExchTeamMailboxSharePointUrlField = value;
			}
		}

		// Token: 0x17002651 RID: 9809
		// (get) Token: 0x06006BA2 RID: 27554 RVA: 0x00174363 File Offset: 0x00172563
		// (set) Token: 0x06006BA3 RID: 27555 RVA: 0x0017436B File Offset: 0x0017256B
		[XmlElement(Order = 16)]
		public DirectoryPropertyStringLength1To1123 CloudMSExchUCVoiceMailSettings
		{
			get
			{
				return this.cloudMSExchUCVoiceMailSettingsField;
			}
			set
			{
				this.cloudMSExchUCVoiceMailSettingsField = value;
			}
		}

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x00174374 File Offset: 0x00172574
		// (set) Token: 0x06006BA5 RID: 27557 RVA: 0x0017437C File Offset: 0x0017257C
		[XmlElement(Order = 17)]
		public DirectoryPropertyStringLength1To40 CloudMSExchUserHoldPolicies
		{
			get
			{
				return this.cloudMSExchUserHoldPoliciesField;
			}
			set
			{
				this.cloudMSExchUserHoldPoliciesField = value;
			}
		}

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x06006BA6 RID: 27558 RVA: 0x00174385 File Offset: 0x00172585
		// (set) Token: 0x06006BA7 RID: 27559 RVA: 0x0017438D File Offset: 0x0017258D
		[XmlElement(Order = 18)]
		public DirectoryPropertyStringSingleLength1To128 Co
		{
			get
			{
				return this.coField;
			}
			set
			{
				this.coField = value;
			}
		}

		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x00174396 File Offset: 0x00172596
		// (set) Token: 0x06006BA9 RID: 27561 RVA: 0x0017439E File Offset: 0x0017259E
		[XmlElement(Order = 19)]
		public DirectoryPropertyStringSingleLength1To64 Company
		{
			get
			{
				return this.companyField;
			}
			set
			{
				this.companyField = value;
			}
		}

		// Token: 0x17002655 RID: 9813
		// (get) Token: 0x06006BAA RID: 27562 RVA: 0x001743A7 File Offset: 0x001725A7
		// (set) Token: 0x06006BAB RID: 27563 RVA: 0x001743AF File Offset: 0x001725AF
		[XmlElement(Order = 20)]
		public DirectoryPropertyInt32SingleMin0Max65535 CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		// Token: 0x17002656 RID: 9814
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x001743B8 File Offset: 0x001725B8
		// (set) Token: 0x06006BAD RID: 27565 RVA: 0x001743C0 File Offset: 0x001725C0
		[XmlElement(Order = 21)]
		public DirectoryPropertyDateTimeSingle CreatedOn
		{
			get
			{
				return this.createdOnField;
			}
			set
			{
				this.createdOnField = value;
			}
		}

		// Token: 0x17002657 RID: 9815
		// (get) Token: 0x06006BAE RID: 27566 RVA: 0x001743C9 File Offset: 0x001725C9
		// (set) Token: 0x06006BAF RID: 27567 RVA: 0x001743D1 File Offset: 0x001725D1
		[XmlElement(Order = 22)]
		public DirectoryPropertyInt32SingleMin0 CreationType
		{
			get
			{
				return this.creationTypeField;
			}
			set
			{
				this.creationTypeField = value;
			}
		}

		// Token: 0x17002658 RID: 9816
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x001743DA File Offset: 0x001725DA
		// (set) Token: 0x06006BB1 RID: 27569 RVA: 0x001743E2 File Offset: 0x001725E2
		[XmlElement(Order = 23)]
		public DirectoryPropertyBooleanSingle DeliverAndRedirect
		{
			get
			{
				return this.deliverAndRedirectField;
			}
			set
			{
				this.deliverAndRedirectField = value;
			}
		}

		// Token: 0x17002659 RID: 9817
		// (get) Token: 0x06006BB2 RID: 27570 RVA: 0x001743EB File Offset: 0x001725EB
		// (set) Token: 0x06006BB3 RID: 27571 RVA: 0x001743F3 File Offset: 0x001725F3
		[XmlElement(Order = 24)]
		public DirectoryPropertyStringSingleLength1To64 Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x1700265A RID: 9818
		// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x001743FC File Offset: 0x001725FC
		// (set) Token: 0x06006BB5 RID: 27573 RVA: 0x00174404 File Offset: 0x00172604
		[XmlElement(Order = 25)]
		public DirectoryPropertyStringSingleLength1To1024 Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700265B RID: 9819
		// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x0017440D File Offset: 0x0017260D
		// (set) Token: 0x06006BB7 RID: 27575 RVA: 0x00174415 File Offset: 0x00172615
		[XmlElement(Order = 26)]
		public DirectoryPropertyBooleanSingle DirSyncEnabled
		{
			get
			{
				return this.dirSyncEnabledField;
			}
			set
			{
				this.dirSyncEnabledField = value;
			}
		}

		// Token: 0x1700265C RID: 9820
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x0017441E File Offset: 0x0017261E
		// (set) Token: 0x06006BB9 RID: 27577 RVA: 0x00174426 File Offset: 0x00172626
		[XmlElement(Order = 27)]
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x1700265D RID: 9821
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0017442F File Offset: 0x0017262F
		// (set) Token: 0x06006BBB RID: 27579 RVA: 0x00174437 File Offset: 0x00172637
		[XmlElement(Order = 28)]
		public DirectoryPropertyStringSingleLength1To16 EmployeeId
		{
			get
			{
				return this.employeeIdField;
			}
			set
			{
				this.employeeIdField = value;
			}
		}

		// Token: 0x1700265E RID: 9822
		// (get) Token: 0x06006BBC RID: 27580 RVA: 0x00174440 File Offset: 0x00172640
		// (set) Token: 0x06006BBD RID: 27581 RVA: 0x00174448 File Offset: 0x00172648
		[XmlElement(Order = 29)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute1
		{
			get
			{
				return this.extensionAttribute1Field;
			}
			set
			{
				this.extensionAttribute1Field = value;
			}
		}

		// Token: 0x1700265F RID: 9823
		// (get) Token: 0x06006BBE RID: 27582 RVA: 0x00174451 File Offset: 0x00172651
		// (set) Token: 0x06006BBF RID: 27583 RVA: 0x00174459 File Offset: 0x00172659
		[XmlElement(Order = 30)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute10
		{
			get
			{
				return this.extensionAttribute10Field;
			}
			set
			{
				this.extensionAttribute10Field = value;
			}
		}

		// Token: 0x17002660 RID: 9824
		// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x00174462 File Offset: 0x00172662
		// (set) Token: 0x06006BC1 RID: 27585 RVA: 0x0017446A File Offset: 0x0017266A
		[XmlElement(Order = 31)]
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute11
		{
			get
			{
				return this.extensionAttribute11Field;
			}
			set
			{
				this.extensionAttribute11Field = value;
			}
		}

		// Token: 0x17002661 RID: 9825
		// (get) Token: 0x06006BC2 RID: 27586 RVA: 0x00174473 File Offset: 0x00172673
		// (set) Token: 0x06006BC3 RID: 27587 RVA: 0x0017447B File Offset: 0x0017267B
		[XmlElement(Order = 32)]
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute12
		{
			get
			{
				return this.extensionAttribute12Field;
			}
			set
			{
				this.extensionAttribute12Field = value;
			}
		}

		// Token: 0x17002662 RID: 9826
		// (get) Token: 0x06006BC4 RID: 27588 RVA: 0x00174484 File Offset: 0x00172684
		// (set) Token: 0x06006BC5 RID: 27589 RVA: 0x0017448C File Offset: 0x0017268C
		[XmlElement(Order = 33)]
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute13
		{
			get
			{
				return this.extensionAttribute13Field;
			}
			set
			{
				this.extensionAttribute13Field = value;
			}
		}

		// Token: 0x17002663 RID: 9827
		// (get) Token: 0x06006BC6 RID: 27590 RVA: 0x00174495 File Offset: 0x00172695
		// (set) Token: 0x06006BC7 RID: 27591 RVA: 0x0017449D File Offset: 0x0017269D
		[XmlElement(Order = 34)]
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute14
		{
			get
			{
				return this.extensionAttribute14Field;
			}
			set
			{
				this.extensionAttribute14Field = value;
			}
		}

		// Token: 0x17002664 RID: 9828
		// (get) Token: 0x06006BC8 RID: 27592 RVA: 0x001744A6 File Offset: 0x001726A6
		// (set) Token: 0x06006BC9 RID: 27593 RVA: 0x001744AE File Offset: 0x001726AE
		[XmlElement(Order = 35)]
		public DirectoryPropertyStringSingleLength1To2048 ExtensionAttribute15
		{
			get
			{
				return this.extensionAttribute15Field;
			}
			set
			{
				this.extensionAttribute15Field = value;
			}
		}

		// Token: 0x17002665 RID: 9829
		// (get) Token: 0x06006BCA RID: 27594 RVA: 0x001744B7 File Offset: 0x001726B7
		// (set) Token: 0x06006BCB RID: 27595 RVA: 0x001744BF File Offset: 0x001726BF
		[XmlElement(Order = 36)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute2
		{
			get
			{
				return this.extensionAttribute2Field;
			}
			set
			{
				this.extensionAttribute2Field = value;
			}
		}

		// Token: 0x17002666 RID: 9830
		// (get) Token: 0x06006BCC RID: 27596 RVA: 0x001744C8 File Offset: 0x001726C8
		// (set) Token: 0x06006BCD RID: 27597 RVA: 0x001744D0 File Offset: 0x001726D0
		[XmlElement(Order = 37)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute3
		{
			get
			{
				return this.extensionAttribute3Field;
			}
			set
			{
				this.extensionAttribute3Field = value;
			}
		}

		// Token: 0x17002667 RID: 9831
		// (get) Token: 0x06006BCE RID: 27598 RVA: 0x001744D9 File Offset: 0x001726D9
		// (set) Token: 0x06006BCF RID: 27599 RVA: 0x001744E1 File Offset: 0x001726E1
		[XmlElement(Order = 38)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute4
		{
			get
			{
				return this.extensionAttribute4Field;
			}
			set
			{
				this.extensionAttribute4Field = value;
			}
		}

		// Token: 0x17002668 RID: 9832
		// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x001744EA File Offset: 0x001726EA
		// (set) Token: 0x06006BD1 RID: 27601 RVA: 0x001744F2 File Offset: 0x001726F2
		[XmlElement(Order = 39)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute5
		{
			get
			{
				return this.extensionAttribute5Field;
			}
			set
			{
				this.extensionAttribute5Field = value;
			}
		}

		// Token: 0x17002669 RID: 9833
		// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x001744FB File Offset: 0x001726FB
		// (set) Token: 0x06006BD3 RID: 27603 RVA: 0x00174503 File Offset: 0x00172703
		[XmlElement(Order = 40)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute6
		{
			get
			{
				return this.extensionAttribute6Field;
			}
			set
			{
				this.extensionAttribute6Field = value;
			}
		}

		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x0017450C File Offset: 0x0017270C
		// (set) Token: 0x06006BD5 RID: 27605 RVA: 0x00174514 File Offset: 0x00172714
		[XmlElement(Order = 41)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute7
		{
			get
			{
				return this.extensionAttribute7Field;
			}
			set
			{
				this.extensionAttribute7Field = value;
			}
		}

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x06006BD6 RID: 27606 RVA: 0x0017451D File Offset: 0x0017271D
		// (set) Token: 0x06006BD7 RID: 27607 RVA: 0x00174525 File Offset: 0x00172725
		[XmlElement(Order = 42)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute8
		{
			get
			{
				return this.extensionAttribute8Field;
			}
			set
			{
				this.extensionAttribute8Field = value;
			}
		}

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x06006BD8 RID: 27608 RVA: 0x0017452E File Offset: 0x0017272E
		// (set) Token: 0x06006BD9 RID: 27609 RVA: 0x00174536 File Offset: 0x00172736
		[XmlElement(Order = 43)]
		public DirectoryPropertyStringSingleLength1To1024 ExtensionAttribute9
		{
			get
			{
				return this.extensionAttribute9Field;
			}
			set
			{
				this.extensionAttribute9Field = value;
			}
		}

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x06006BDA RID: 27610 RVA: 0x0017453F File Offset: 0x0017273F
		// (set) Token: 0x06006BDB RID: 27611 RVA: 0x00174547 File Offset: 0x00172747
		[XmlElement(Order = 44)]
		public DirectoryPropertyStringSingleLength1To64 FacsimileTelephoneNumber
		{
			get
			{
				return this.facsimileTelephoneNumberField;
			}
			set
			{
				this.facsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x06006BDC RID: 27612 RVA: 0x00174550 File Offset: 0x00172750
		// (set) Token: 0x06006BDD RID: 27613 RVA: 0x00174558 File Offset: 0x00172758
		[XmlElement(Order = 45)]
		public DirectoryPropertyStringSingleLength1To64 GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x06006BDE RID: 27614 RVA: 0x00174561 File Offset: 0x00172761
		// (set) Token: 0x06006BDF RID: 27615 RVA: 0x00174569 File Offset: 0x00172769
		[XmlElement(Order = 46)]
		public DirectoryPropertyStringSingleLength1To64 HomePhone
		{
			get
			{
				return this.homePhoneField;
			}
			set
			{
				this.homePhoneField = value;
			}
		}

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x00174572 File Offset: 0x00172772
		// (set) Token: 0x06006BE1 RID: 27617 RVA: 0x0017457A File Offset: 0x0017277A
		[XmlElement(Order = 47)]
		public DirectoryPropertyStringSingleLength1To1024 Info
		{
			get
			{
				return this.infoField;
			}
			set
			{
				this.infoField = value;
			}
		}

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x06006BE2 RID: 27618 RVA: 0x00174583 File Offset: 0x00172783
		// (set) Token: 0x06006BE3 RID: 27619 RVA: 0x0017458B File Offset: 0x0017278B
		[XmlElement(Order = 48)]
		public DirectoryPropertyStringSingleLength1To6 Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x00174594 File Offset: 0x00172794
		// (set) Token: 0x06006BE5 RID: 27621 RVA: 0x0017459C File Offset: 0x0017279C
		[XmlElement(Order = 49)]
		public DirectoryPropertyInt32Single InternetEncoding
		{
			get
			{
				return this.internetEncodingField;
			}
			set
			{
				this.internetEncodingField = value;
			}
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x06006BE6 RID: 27622 RVA: 0x001745A5 File Offset: 0x001727A5
		// (set) Token: 0x06006BE7 RID: 27623 RVA: 0x001745AD File Offset: 0x001727AD
		[XmlElement(Order = 50)]
		public DirectoryPropertyStringLength1To2048 InviteResources
		{
			get
			{
				return this.inviteResourcesField;
			}
			set
			{
				this.inviteResourcesField = value;
			}
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x001745B6 File Offset: 0x001727B6
		// (set) Token: 0x06006BE9 RID: 27625 RVA: 0x001745BE File Offset: 0x001727BE
		[XmlElement(Order = 51)]
		public DirectoryPropertyStringSingleLength1To64 IPPhone
		{
			get
			{
				return this.iPPhoneField;
			}
			set
			{
				this.iPPhoneField = value;
			}
		}

		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x001745C7 File Offset: 0x001727C7
		// (set) Token: 0x06006BEB RID: 27627 RVA: 0x001745CF File Offset: 0x001727CF
		[XmlElement(Order = 52)]
		public DirectoryPropertyStringSingleLength1To128 L
		{
			get
			{
				return this.lField;
			}
			set
			{
				this.lField = value;
			}
		}

		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x06006BEC RID: 27628 RVA: 0x001745D8 File Offset: 0x001727D8
		// (set) Token: 0x06006BED RID: 27629 RVA: 0x001745E0 File Offset: 0x001727E0
		[XmlElement(Order = 53)]
		public DirectoryPropertyStringSingleLength1To256 Mail
		{
			get
			{
				return this.mailField;
			}
			set
			{
				this.mailField = value;
			}
		}

		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x06006BEE RID: 27630 RVA: 0x001745E9 File Offset: 0x001727E9
		// (set) Token: 0x06006BEF RID: 27631 RVA: 0x001745F1 File Offset: 0x001727F1
		[XmlElement(Order = 54)]
		public DirectoryPropertyStringSingleMailNickname MailNickname
		{
			get
			{
				return this.mailNicknameField;
			}
			set
			{
				this.mailNicknameField = value;
			}
		}

		// Token: 0x17002678 RID: 9848
		// (get) Token: 0x06006BF0 RID: 27632 RVA: 0x001745FA File Offset: 0x001727FA
		// (set) Token: 0x06006BF1 RID: 27633 RVA: 0x00174602 File Offset: 0x00172802
		[XmlElement(Order = 55)]
		public DirectoryPropertyStringSingleLength1To64 MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x17002679 RID: 9849
		// (get) Token: 0x06006BF2 RID: 27634 RVA: 0x0017460B File Offset: 0x0017280B
		// (set) Token: 0x06006BF3 RID: 27635 RVA: 0x00174613 File Offset: 0x00172813
		[XmlElement(Order = 56)]
		public DirectoryPropertyStringSingleLength1To64 Mobile
		{
			get
			{
				return this.mobileField;
			}
			set
			{
				this.mobileField = value;
			}
		}

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x06006BF4 RID: 27636 RVA: 0x0017461C File Offset: 0x0017281C
		// (set) Token: 0x06006BF5 RID: 27637 RVA: 0x00174624 File Offset: 0x00172824
		[XmlElement(Order = 57)]
		public DirectoryPropertyInt32Single MSDSHABSeniorityIndex
		{
			get
			{
				return this.mSDSHABSeniorityIndexField;
			}
			set
			{
				this.mSDSHABSeniorityIndexField = value;
			}
		}

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x06006BF6 RID: 27638 RVA: 0x0017462D File Offset: 0x0017282D
		// (set) Token: 0x06006BF7 RID: 27639 RVA: 0x00174635 File Offset: 0x00172835
		[XmlElement(Order = 58)]
		public DirectoryPropertyStringSingleLength1To256 MSDSPhoneticDisplayName
		{
			get
			{
				return this.mSDSPhoneticDisplayNameField;
			}
			set
			{
				this.mSDSPhoneticDisplayNameField = value;
			}
		}

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x06006BF8 RID: 27640 RVA: 0x0017463E File Offset: 0x0017283E
		// (set) Token: 0x06006BF9 RID: 27641 RVA: 0x00174646 File Offset: 0x00172846
		[XmlElement(Order = 59)]
		public DirectoryPropertyGuidSingle MSExchArchiveGuid
		{
			get
			{
				return this.mSExchArchiveGuidField;
			}
			set
			{
				this.mSExchArchiveGuidField = value;
			}
		}

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x06006BFA RID: 27642 RVA: 0x0017464F File Offset: 0x0017284F
		// (set) Token: 0x06006BFB RID: 27643 RVA: 0x00174657 File Offset: 0x00172857
		[XmlElement(Order = 60)]
		public DirectoryPropertyStringLength1To512 MSExchArchiveName
		{
			get
			{
				return this.mSExchArchiveNameField;
			}
			set
			{
				this.mSExchArchiveNameField = value;
			}
		}

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x06006BFC RID: 27644 RVA: 0x00174660 File Offset: 0x00172860
		// (set) Token: 0x06006BFD RID: 27645 RVA: 0x00174668 File Offset: 0x00172868
		[XmlElement(Order = 61)]
		public DirectoryPropertyStringSingleLength1To256 MSExchAssistantName
		{
			get
			{
				return this.mSExchAssistantNameField;
			}
			set
			{
				this.mSExchAssistantNameField = value;
			}
		}

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x06006BFE RID: 27646 RVA: 0x00174671 File Offset: 0x00172871
		// (set) Token: 0x06006BFF RID: 27647 RVA: 0x00174679 File Offset: 0x00172879
		[XmlElement(Order = 62)]
		public DirectoryPropertyInt32Single MSExchAuditAdmin
		{
			get
			{
				return this.mSExchAuditAdminField;
			}
			set
			{
				this.mSExchAuditAdminField = value;
			}
		}

		// Token: 0x17002680 RID: 9856
		// (get) Token: 0x06006C00 RID: 27648 RVA: 0x00174682 File Offset: 0x00172882
		// (set) Token: 0x06006C01 RID: 27649 RVA: 0x0017468A File Offset: 0x0017288A
		[XmlElement(Order = 63)]
		public DirectoryPropertyInt32Single MSExchAuditDelegate
		{
			get
			{
				return this.mSExchAuditDelegateField;
			}
			set
			{
				this.mSExchAuditDelegateField = value;
			}
		}

		// Token: 0x17002681 RID: 9857
		// (get) Token: 0x06006C02 RID: 27650 RVA: 0x00174693 File Offset: 0x00172893
		// (set) Token: 0x06006C03 RID: 27651 RVA: 0x0017469B File Offset: 0x0017289B
		[XmlElement(Order = 64)]
		public DirectoryPropertyInt32Single MSExchAuditDelegateAdmin
		{
			get
			{
				return this.mSExchAuditDelegateAdminField;
			}
			set
			{
				this.mSExchAuditDelegateAdminField = value;
			}
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x06006C04 RID: 27652 RVA: 0x001746A4 File Offset: 0x001728A4
		// (set) Token: 0x06006C05 RID: 27653 RVA: 0x001746AC File Offset: 0x001728AC
		[XmlElement(Order = 65)]
		public DirectoryPropertyInt32Single MSExchAuditOwner
		{
			get
			{
				return this.mSExchAuditOwnerField;
			}
			set
			{
				this.mSExchAuditOwnerField = value;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x06006C06 RID: 27654 RVA: 0x001746B5 File Offset: 0x001728B5
		// (set) Token: 0x06006C07 RID: 27655 RVA: 0x001746BD File Offset: 0x001728BD
		[XmlElement(Order = 66)]
		public DirectoryPropertyBinarySingleLength1To4000 MSExchBlockedSendersHash
		{
			get
			{
				return this.mSExchBlockedSendersHashField;
			}
			set
			{
				this.mSExchBlockedSendersHashField = value;
			}
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x06006C08 RID: 27656 RVA: 0x001746C6 File Offset: 0x001728C6
		// (set) Token: 0x06006C09 RID: 27657 RVA: 0x001746CE File Offset: 0x001728CE
		[XmlElement(Order = 67)]
		public DirectoryPropertyBooleanSingle MSExchBypassAudit
		{
			get
			{
				return this.mSExchBypassAuditField;
			}
			set
			{
				this.mSExchBypassAuditField = value;
			}
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x06006C0A RID: 27658 RVA: 0x001746D7 File Offset: 0x001728D7
		// (set) Token: 0x06006C0B RID: 27659 RVA: 0x001746DF File Offset: 0x001728DF
		[XmlElement(Order = 68)]
		public DirectoryPropertyDateTimeSingle MSExchElcExpirySuspensionEnd
		{
			get
			{
				return this.mSExchElcExpirySuspensionEndField;
			}
			set
			{
				this.mSExchElcExpirySuspensionEndField = value;
			}
		}

		// Token: 0x17002686 RID: 9862
		// (get) Token: 0x06006C0C RID: 27660 RVA: 0x001746E8 File Offset: 0x001728E8
		// (set) Token: 0x06006C0D RID: 27661 RVA: 0x001746F0 File Offset: 0x001728F0
		[XmlElement(Order = 69)]
		public DirectoryPropertyDateTimeSingle MSExchElcExpirySuspensionStart
		{
			get
			{
				return this.mSExchElcExpirySuspensionStartField;
			}
			set
			{
				this.mSExchElcExpirySuspensionStartField = value;
			}
		}

		// Token: 0x17002687 RID: 9863
		// (get) Token: 0x06006C0E RID: 27662 RVA: 0x001746F9 File Offset: 0x001728F9
		// (set) Token: 0x06006C0F RID: 27663 RVA: 0x00174701 File Offset: 0x00172901
		[XmlElement(Order = 70)]
		public DirectoryPropertyInt32Single MSExchElcMailboxFlags
		{
			get
			{
				return this.mSExchElcMailboxFlagsField;
			}
			set
			{
				this.mSExchElcMailboxFlagsField = value;
			}
		}

		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x06006C10 RID: 27664 RVA: 0x0017470A File Offset: 0x0017290A
		// (set) Token: 0x06006C11 RID: 27665 RVA: 0x00174712 File Offset: 0x00172912
		[XmlElement(Order = 71)]
		public DirectoryPropertyBooleanSingle MSExchEnableModeration
		{
			get
			{
				return this.mSExchEnableModerationField;
			}
			set
			{
				this.mSExchEnableModerationField = value;
			}
		}

		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x06006C12 RID: 27666 RVA: 0x0017471B File Offset: 0x0017291B
		// (set) Token: 0x06006C13 RID: 27667 RVA: 0x00174723 File Offset: 0x00172923
		[XmlElement(Order = 72)]
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute1
		{
			get
			{
				return this.mSExchExtensionCustomAttribute1Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute1Field = value;
			}
		}

		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x0017472C File Offset: 0x0017292C
		// (set) Token: 0x06006C15 RID: 27669 RVA: 0x00174734 File Offset: 0x00172934
		[XmlElement(Order = 73)]
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute2
		{
			get
			{
				return this.mSExchExtensionCustomAttribute2Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute2Field = value;
			}
		}

		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x06006C16 RID: 27670 RVA: 0x0017473D File Offset: 0x0017293D
		// (set) Token: 0x06006C17 RID: 27671 RVA: 0x00174745 File Offset: 0x00172945
		[XmlElement(Order = 74)]
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute3
		{
			get
			{
				return this.mSExchExtensionCustomAttribute3Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute3Field = value;
			}
		}

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x06006C18 RID: 27672 RVA: 0x0017474E File Offset: 0x0017294E
		// (set) Token: 0x06006C19 RID: 27673 RVA: 0x00174756 File Offset: 0x00172956
		[XmlElement(Order = 75)]
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute4
		{
			get
			{
				return this.mSExchExtensionCustomAttribute4Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute4Field = value;
			}
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x06006C1A RID: 27674 RVA: 0x0017475F File Offset: 0x0017295F
		// (set) Token: 0x06006C1B RID: 27675 RVA: 0x00174767 File Offset: 0x00172967
		[XmlElement(Order = 76)]
		public DirectoryPropertyStringLength1To2048 MSExchExtensionCustomAttribute5
		{
			get
			{
				return this.mSExchExtensionCustomAttribute5Field;
			}
			set
			{
				this.mSExchExtensionCustomAttribute5Field = value;
			}
		}

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06006C1C RID: 27676 RVA: 0x00174770 File Offset: 0x00172970
		// (set) Token: 0x06006C1D RID: 27677 RVA: 0x00174778 File Offset: 0x00172978
		[XmlElement(Order = 77)]
		public DirectoryPropertyBooleanSingle MSExchHideFromAddressLists
		{
			get
			{
				return this.mSExchHideFromAddressListsField;
			}
			set
			{
				this.mSExchHideFromAddressListsField = value;
			}
		}

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06006C1E RID: 27678 RVA: 0x00174781 File Offset: 0x00172981
		// (set) Token: 0x06006C1F RID: 27679 RVA: 0x00174789 File Offset: 0x00172989
		[XmlElement(Order = 78)]
		public DirectoryPropertyStringSingleLength1To256 MSExchImmutableId
		{
			get
			{
				return this.mSExchImmutableIdField;
			}
			set
			{
				this.mSExchImmutableIdField = value;
			}
		}

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x06006C20 RID: 27680 RVA: 0x00174792 File Offset: 0x00172992
		// (set) Token: 0x06006C21 RID: 27681 RVA: 0x0017479A File Offset: 0x0017299A
		[XmlElement(Order = 79)]
		public DirectoryPropertyDateTimeSingle MSExchLitigationHoldDate
		{
			get
			{
				return this.mSExchLitigationHoldDateField;
			}
			set
			{
				this.mSExchLitigationHoldDateField = value;
			}
		}

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x001747A3 File Offset: 0x001729A3
		// (set) Token: 0x06006C23 RID: 27683 RVA: 0x001747AB File Offset: 0x001729AB
		[XmlElement(Order = 80)]
		public DirectoryPropertyStringSingleLength1To1024 MSExchLitigationHoldOwner
		{
			get
			{
				return this.mSExchLitigationHoldOwnerField;
			}
			set
			{
				this.mSExchLitigationHoldOwnerField = value;
			}
		}

		// Token: 0x17002692 RID: 9874
		// (get) Token: 0x06006C24 RID: 27684 RVA: 0x001747B4 File Offset: 0x001729B4
		// (set) Token: 0x06006C25 RID: 27685 RVA: 0x001747BC File Offset: 0x001729BC
		[XmlElement(Order = 81)]
		public DirectoryPropertyBooleanSingle MSExchMailboxAuditEnable
		{
			get
			{
				return this.mSExchMailboxAuditEnableField;
			}
			set
			{
				this.mSExchMailboxAuditEnableField = value;
			}
		}

		// Token: 0x17002693 RID: 9875
		// (get) Token: 0x06006C26 RID: 27686 RVA: 0x001747C5 File Offset: 0x001729C5
		// (set) Token: 0x06006C27 RID: 27687 RVA: 0x001747CD File Offset: 0x001729CD
		[XmlElement(Order = 82)]
		public DirectoryPropertyInt32Single MSExchMailboxAuditLogAgeLimit
		{
			get
			{
				return this.mSExchMailboxAuditLogAgeLimitField;
			}
			set
			{
				this.mSExchMailboxAuditLogAgeLimitField = value;
			}
		}

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x06006C28 RID: 27688 RVA: 0x001747D6 File Offset: 0x001729D6
		// (set) Token: 0x06006C29 RID: 27689 RVA: 0x001747DE File Offset: 0x001729DE
		[XmlElement(Order = 83)]
		public DirectoryPropertyGuidSingle MSExchMailboxGuid
		{
			get
			{
				return this.mSExchMailboxGuidField;
			}
			set
			{
				this.mSExchMailboxGuidField = value;
			}
		}

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x06006C2A RID: 27690 RVA: 0x001747E7 File Offset: 0x001729E7
		// (set) Token: 0x06006C2B RID: 27691 RVA: 0x001747EF File Offset: 0x001729EF
		[XmlElement(Order = 84)]
		public DirectoryPropertyInt32Single MSExchModerationFlags
		{
			get
			{
				return this.mSExchModerationFlagsField;
			}
			set
			{
				this.mSExchModerationFlagsField = value;
			}
		}

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x06006C2C RID: 27692 RVA: 0x001747F8 File Offset: 0x001729F8
		// (set) Token: 0x06006C2D RID: 27693 RVA: 0x00174800 File Offset: 0x00172A00
		[XmlElement(Order = 85)]
		public DirectoryPropertyInt32Single MSExchRecipientDisplayType
		{
			get
			{
				return this.mSExchRecipientDisplayTypeField;
			}
			set
			{
				this.mSExchRecipientDisplayTypeField = value;
			}
		}

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x06006C2E RID: 27694 RVA: 0x00174809 File Offset: 0x00172A09
		// (set) Token: 0x06006C2F RID: 27695 RVA: 0x00174811 File Offset: 0x00172A11
		[XmlElement(Order = 86)]
		public DirectoryPropertyInt64Single MSExchRecipientTypeDetails
		{
			get
			{
				return this.mSExchRecipientTypeDetailsField;
			}
			set
			{
				this.mSExchRecipientTypeDetailsField = value;
			}
		}

		// Token: 0x17002698 RID: 9880
		// (get) Token: 0x06006C30 RID: 27696 RVA: 0x0017481A File Offset: 0x00172A1A
		// (set) Token: 0x06006C31 RID: 27697 RVA: 0x00174822 File Offset: 0x00172A22
		[XmlElement(Order = 87)]
		public DirectoryPropertyInt64Single MSExchRemoteRecipientType
		{
			get
			{
				return this.mSExchRemoteRecipientTypeField;
			}
			set
			{
				this.mSExchRemoteRecipientTypeField = value;
			}
		}

		// Token: 0x17002699 RID: 9881
		// (get) Token: 0x06006C32 RID: 27698 RVA: 0x0017482B File Offset: 0x00172A2B
		// (set) Token: 0x06006C33 RID: 27699 RVA: 0x00174833 File Offset: 0x00172A33
		[XmlElement(Order = 88)]
		public DirectoryPropertyBooleanSingle MSExchRequireAuthToSendTo
		{
			get
			{
				return this.mSExchRequireAuthToSendToField;
			}
			set
			{
				this.mSExchRequireAuthToSendToField = value;
			}
		}

		// Token: 0x1700269A RID: 9882
		// (get) Token: 0x06006C34 RID: 27700 RVA: 0x0017483C File Offset: 0x00172A3C
		// (set) Token: 0x06006C35 RID: 27701 RVA: 0x00174844 File Offset: 0x00172A44
		[XmlElement(Order = 89)]
		public DirectoryPropertyInt32Single MSExchResourceCapacity
		{
			get
			{
				return this.mSExchResourceCapacityField;
			}
			set
			{
				this.mSExchResourceCapacityField = value;
			}
		}

		// Token: 0x1700269B RID: 9883
		// (get) Token: 0x06006C36 RID: 27702 RVA: 0x0017484D File Offset: 0x00172A4D
		// (set) Token: 0x06006C37 RID: 27703 RVA: 0x00174855 File Offset: 0x00172A55
		[XmlElement(Order = 90)]
		public DirectoryPropertyStringSingleLength1To1024 MSExchResourceDisplay
		{
			get
			{
				return this.mSExchResourceDisplayField;
			}
			set
			{
				this.mSExchResourceDisplayField = value;
			}
		}

		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x06006C38 RID: 27704 RVA: 0x0017485E File Offset: 0x00172A5E
		// (set) Token: 0x06006C39 RID: 27705 RVA: 0x00174866 File Offset: 0x00172A66
		[XmlElement(Order = 91)]
		public DirectoryPropertyStringLength1To1024 MSExchResourceMetadata
		{
			get
			{
				return this.mSExchResourceMetadataField;
			}
			set
			{
				this.mSExchResourceMetadataField = value;
			}
		}

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x06006C3A RID: 27706 RVA: 0x0017486F File Offset: 0x00172A6F
		// (set) Token: 0x06006C3B RID: 27707 RVA: 0x00174877 File Offset: 0x00172A77
		[XmlElement(Order = 92)]
		public DirectoryPropertyStringLength1To1024 MSExchResourceSearchProperties
		{
			get
			{
				return this.mSExchResourceSearchPropertiesField;
			}
			set
			{
				this.mSExchResourceSearchPropertiesField = value;
			}
		}

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x06006C3C RID: 27708 RVA: 0x00174880 File Offset: 0x00172A80
		// (set) Token: 0x06006C3D RID: 27709 RVA: 0x00174888 File Offset: 0x00172A88
		[XmlElement(Order = 93)]
		public DirectoryPropertyStringSingleLength1To1024 MSExchRetentionComment
		{
			get
			{
				return this.mSExchRetentionCommentField;
			}
			set
			{
				this.mSExchRetentionCommentField = value;
			}
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x06006C3E RID: 27710 RVA: 0x00174891 File Offset: 0x00172A91
		// (set) Token: 0x06006C3F RID: 27711 RVA: 0x00174899 File Offset: 0x00172A99
		[XmlElement(Order = 94)]
		public DirectoryPropertyStringSingleLength1To2048 MSExchRetentionUrl
		{
			get
			{
				return this.mSExchRetentionUrlField;
			}
			set
			{
				this.mSExchRetentionUrlField = value;
			}
		}

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x06006C40 RID: 27712 RVA: 0x001748A2 File Offset: 0x00172AA2
		// (set) Token: 0x06006C41 RID: 27713 RVA: 0x001748AA File Offset: 0x00172AAA
		[XmlElement(Order = 95)]
		public DirectoryPropertyBinarySingleLength1To12000 MSExchSafeRecipientsHash
		{
			get
			{
				return this.mSExchSafeRecipientsHashField;
			}
			set
			{
				this.mSExchSafeRecipientsHashField = value;
			}
		}

		// Token: 0x170026A1 RID: 9889
		// (get) Token: 0x06006C42 RID: 27714 RVA: 0x001748B3 File Offset: 0x00172AB3
		// (set) Token: 0x06006C43 RID: 27715 RVA: 0x001748BB File Offset: 0x00172ABB
		[XmlElement(Order = 96)]
		public DirectoryPropertyBinarySingleLength1To32000 MSExchSafeSendersHash
		{
			get
			{
				return this.mSExchSafeSendersHashField;
			}
			set
			{
				this.mSExchSafeSendersHashField = value;
			}
		}

		// Token: 0x170026A2 RID: 9890
		// (get) Token: 0x06006C44 RID: 27716 RVA: 0x001748C4 File Offset: 0x00172AC4
		// (set) Token: 0x06006C45 RID: 27717 RVA: 0x001748CC File Offset: 0x00172ACC
		[XmlElement(Order = 97)]
		public DirectoryPropertyStringLength2To500 MSExchSenderHintTranslations
		{
			get
			{
				return this.mSExchSenderHintTranslationsField;
			}
			set
			{
				this.mSExchSenderHintTranslationsField = value;
			}
		}

		// Token: 0x170026A3 RID: 9891
		// (get) Token: 0x06006C46 RID: 27718 RVA: 0x001748D5 File Offset: 0x00172AD5
		// (set) Token: 0x06006C47 RID: 27719 RVA: 0x001748DD File Offset: 0x00172ADD
		[XmlElement(Order = 98)]
		public DirectoryPropertyDateTimeSingle MSExchTeamMailboxExpiration
		{
			get
			{
				return this.mSExchTeamMailboxExpirationField;
			}
			set
			{
				this.mSExchTeamMailboxExpirationField = value;
			}
		}

		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x06006C48 RID: 27720 RVA: 0x001748E6 File Offset: 0x00172AE6
		// (set) Token: 0x06006C49 RID: 27721 RVA: 0x001748EE File Offset: 0x00172AEE
		[XmlElement(Order = 99)]
		public DirectoryPropertyReferenceAddressList MSExchTeamMailboxOwners
		{
			get
			{
				return this.mSExchTeamMailboxOwnersField;
			}
			set
			{
				this.mSExchTeamMailboxOwnersField = value;
			}
		}

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x06006C4A RID: 27722 RVA: 0x001748F7 File Offset: 0x00172AF7
		// (set) Token: 0x06006C4B RID: 27723 RVA: 0x001748FF File Offset: 0x00172AFF
		[XmlElement(Order = 100)]
		public DirectoryPropertyReferenceAddressListSingle MSExchTeamMailboxSharePointLinkedBy
		{
			get
			{
				return this.mSExchTeamMailboxSharePointLinkedByField;
			}
			set
			{
				this.mSExchTeamMailboxSharePointLinkedByField = value;
			}
		}

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x06006C4C RID: 27724 RVA: 0x00174908 File Offset: 0x00172B08
		// (set) Token: 0x06006C4D RID: 27725 RVA: 0x00174910 File Offset: 0x00172B10
		[XmlElement(Order = 101)]
		public DirectoryPropertyStringSingleLength1To2048 MSExchTeamMailboxSharePointUrl
		{
			get
			{
				return this.mSExchTeamMailboxSharePointUrlField;
			}
			set
			{
				this.mSExchTeamMailboxSharePointUrlField = value;
			}
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x06006C4E RID: 27726 RVA: 0x00174919 File Offset: 0x00172B19
		// (set) Token: 0x06006C4F RID: 27727 RVA: 0x00174921 File Offset: 0x00172B21
		[XmlElement(Order = 102)]
		public DirectoryPropertyDateTimeSingle MSExchUserCreatedTimestamp
		{
			get
			{
				return this.mSExchUserCreatedTimestampField;
			}
			set
			{
				this.mSExchUserCreatedTimestampField = value;
			}
		}

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x06006C50 RID: 27728 RVA: 0x0017492A File Offset: 0x00172B2A
		// (set) Token: 0x06006C51 RID: 27729 RVA: 0x00174932 File Offset: 0x00172B32
		[XmlElement(Order = 103)]
		public DirectoryPropertyStringLength1To40 MSExchUserHoldPolicies
		{
			get
			{
				return this.mSExchUserHoldPoliciesField;
			}
			set
			{
				this.mSExchUserHoldPoliciesField = value;
			}
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x06006C52 RID: 27730 RVA: 0x0017493B File Offset: 0x00172B3B
		// (set) Token: 0x06006C53 RID: 27731 RVA: 0x00174943 File Offset: 0x00172B43
		[XmlElement(Order = 104)]
		public DirectoryPropertyInt32SingleMin0 MSRtcSipApplicationOptions
		{
			get
			{
				return this.mSRtcSipApplicationOptionsField;
			}
			set
			{
				this.mSRtcSipApplicationOptionsField = value;
			}
		}

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x06006C54 RID: 27732 RVA: 0x0017494C File Offset: 0x00172B4C
		// (set) Token: 0x06006C55 RID: 27733 RVA: 0x00174954 File Offset: 0x00172B54
		[XmlElement(Order = 105)]
		public DirectoryPropertyStringSingleLength1To256 MSRtcSipDeploymentLocator
		{
			get
			{
				return this.mSRtcSipDeploymentLocatorField;
			}
			set
			{
				this.mSRtcSipDeploymentLocatorField = value;
			}
		}

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x06006C56 RID: 27734 RVA: 0x0017495D File Offset: 0x00172B5D
		// (set) Token: 0x06006C57 RID: 27735 RVA: 0x00174965 File Offset: 0x00172B65
		[XmlElement(Order = 106)]
		public DirectoryPropertyStringSingleLength1To500 MSRtcSipLine
		{
			get
			{
				return this.mSRtcSipLineField;
			}
			set
			{
				this.mSRtcSipLineField = value;
			}
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x06006C58 RID: 27736 RVA: 0x0017496E File Offset: 0x00172B6E
		// (set) Token: 0x06006C59 RID: 27737 RVA: 0x00174976 File Offset: 0x00172B76
		[XmlElement(Order = 107)]
		public DirectoryPropertyInt32Single MSRtcSipOptionFlags
		{
			get
			{
				return this.mSRtcSipOptionFlagsField;
			}
			set
			{
				this.mSRtcSipOptionFlagsField = value;
			}
		}

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x06006C5A RID: 27738 RVA: 0x0017497F File Offset: 0x00172B7F
		// (set) Token: 0x06006C5B RID: 27739 RVA: 0x00174987 File Offset: 0x00172B87
		[XmlElement(Order = 108)]
		public DirectoryPropertyStringSingleLength1To512 MSRtcSipOwnerUrn
		{
			get
			{
				return this.mSRtcSipOwnerUrnField;
			}
			set
			{
				this.mSRtcSipOwnerUrnField = value;
			}
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x06006C5C RID: 27740 RVA: 0x00174990 File Offset: 0x00172B90
		// (set) Token: 0x06006C5D RID: 27741 RVA: 0x00174998 File Offset: 0x00172B98
		[XmlElement(Order = 109)]
		public DirectoryPropertyStringSingleLength1To454 MSRtcSipPrimaryUserAddress
		{
			get
			{
				return this.mSRtcSipPrimaryUserAddressField;
			}
			set
			{
				this.mSRtcSipPrimaryUserAddressField = value;
			}
		}

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x06006C5E RID: 27742 RVA: 0x001749A1 File Offset: 0x00172BA1
		// (set) Token: 0x06006C5F RID: 27743 RVA: 0x001749A9 File Offset: 0x00172BA9
		[XmlElement(Order = 110)]
		public DirectoryPropertyBooleanSingle MSRtcSipUserEnabled
		{
			get
			{
				return this.mSRtcSipUserEnabledField;
			}
			set
			{
				this.mSRtcSipUserEnabledField = value;
			}
		}

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x06006C60 RID: 27744 RVA: 0x001749B2 File Offset: 0x00172BB2
		// (set) Token: 0x06006C61 RID: 27745 RVA: 0x001749BA File Offset: 0x00172BBA
		[XmlElement(Order = 111)]
		public DirectoryPropertyBinarySingleLength1To128 OnPremiseSecurityIdentifier
		{
			get
			{
				return this.onPremiseSecurityIdentifierField;
			}
			set
			{
				this.onPremiseSecurityIdentifierField = value;
			}
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x06006C62 RID: 27746 RVA: 0x001749C3 File Offset: 0x00172BC3
		// (set) Token: 0x06006C63 RID: 27747 RVA: 0x001749CB File Offset: 0x00172BCB
		[XmlElement(Order = 112)]
		public DirectoryPropertyStringLength1To64 OtherFacsimileTelephoneNumber
		{
			get
			{
				return this.otherFacsimileTelephoneNumberField;
			}
			set
			{
				this.otherFacsimileTelephoneNumberField = value;
			}
		}

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x06006C64 RID: 27748 RVA: 0x001749D4 File Offset: 0x00172BD4
		// (set) Token: 0x06006C65 RID: 27749 RVA: 0x001749DC File Offset: 0x00172BDC
		[XmlElement(Order = 113)]
		public DirectoryPropertyStringLength1To64 OtherHomePhone
		{
			get
			{
				return this.otherHomePhoneField;
			}
			set
			{
				this.otherHomePhoneField = value;
			}
		}

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x06006C66 RID: 27750 RVA: 0x001749E5 File Offset: 0x00172BE5
		// (set) Token: 0x06006C67 RID: 27751 RVA: 0x001749ED File Offset: 0x00172BED
		[XmlElement(Order = 114)]
		public DirectoryPropertyStringLength1To512 OtherIPPhone
		{
			get
			{
				return this.otherIPPhoneField;
			}
			set
			{
				this.otherIPPhoneField = value;
			}
		}

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x06006C68 RID: 27752 RVA: 0x001749F6 File Offset: 0x00172BF6
		// (set) Token: 0x06006C69 RID: 27753 RVA: 0x001749FE File Offset: 0x00172BFE
		[XmlElement(Order = 115)]
		public DirectoryPropertyStringLength1To256 OtherMail
		{
			get
			{
				return this.otherMailField;
			}
			set
			{
				this.otherMailField = value;
			}
		}

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x06006C6A RID: 27754 RVA: 0x00174A07 File Offset: 0x00172C07
		// (set) Token: 0x06006C6B RID: 27755 RVA: 0x00174A0F File Offset: 0x00172C0F
		[XmlElement(Order = 116)]
		public DirectoryPropertyStringLength1To64 OtherMobile
		{
			get
			{
				return this.otherMobileField;
			}
			set
			{
				this.otherMobileField = value;
			}
		}

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x06006C6C RID: 27756 RVA: 0x00174A18 File Offset: 0x00172C18
		// (set) Token: 0x06006C6D RID: 27757 RVA: 0x00174A20 File Offset: 0x00172C20
		[XmlElement(Order = 117)]
		public DirectoryPropertyStringLength1To64 OtherPager
		{
			get
			{
				return this.otherPagerField;
			}
			set
			{
				this.otherPagerField = value;
			}
		}

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06006C6E RID: 27758 RVA: 0x00174A29 File Offset: 0x00172C29
		// (set) Token: 0x06006C6F RID: 27759 RVA: 0x00174A31 File Offset: 0x00172C31
		[XmlElement(Order = 118)]
		public DirectoryPropertyStringLength1To64 OtherTelephone
		{
			get
			{
				return this.otherTelephoneField;
			}
			set
			{
				this.otherTelephoneField = value;
			}
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x06006C70 RID: 27760 RVA: 0x00174A3A File Offset: 0x00172C3A
		// (set) Token: 0x06006C71 RID: 27761 RVA: 0x00174A42 File Offset: 0x00172C42
		[XmlElement(Order = 119)]
		public DirectoryPropertyStringSingleLength1To64 Pager
		{
			get
			{
				return this.pagerField;
			}
			set
			{
				this.pagerField = value;
			}
		}

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x06006C72 RID: 27762 RVA: 0x00174A4B File Offset: 0x00172C4B
		// (set) Token: 0x06006C73 RID: 27763 RVA: 0x00174A53 File Offset: 0x00172C53
		[XmlElement(Order = 120)]
		public DirectoryPropertyStringSingleLength1To128 PhysicalDeliveryOfficeName
		{
			get
			{
				return this.physicalDeliveryOfficeNameField;
			}
			set
			{
				this.physicalDeliveryOfficeNameField = value;
			}
		}

		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x06006C74 RID: 27764 RVA: 0x00174A5C File Offset: 0x00172C5C
		// (set) Token: 0x06006C75 RID: 27765 RVA: 0x00174A64 File Offset: 0x00172C64
		[XmlElement(Order = 121)]
		public DirectoryPropertyStringSingleLength1To40 PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06006C76 RID: 27766 RVA: 0x00174A6D File Offset: 0x00172C6D
		// (set) Token: 0x06006C77 RID: 27767 RVA: 0x00174A75 File Offset: 0x00172C75
		[XmlElement(Order = 122)]
		public DirectoryPropertyStringLength1To40 PostOfficeBox
		{
			get
			{
				return this.postOfficeBoxField;
			}
			set
			{
				this.postOfficeBoxField = value;
			}
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06006C78 RID: 27768 RVA: 0x00174A7E File Offset: 0x00172C7E
		// (set) Token: 0x06006C79 RID: 27769 RVA: 0x00174A86 File Offset: 0x00172C86
		[XmlElement(Order = 123)]
		public DirectoryPropertyStringSingleLength1To64 PreferredLanguage
		{
			get
			{
				return this.preferredLanguageField;
			}
			set
			{
				this.preferredLanguageField = value;
			}
		}

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06006C7A RID: 27770 RVA: 0x00174A8F File Offset: 0x00172C8F
		// (set) Token: 0x06006C7B RID: 27771 RVA: 0x00174A97 File Offset: 0x00172C97
		[XmlElement(Order = 124)]
		public DirectoryPropertyProxyAddresses ProxyAddresses
		{
			get
			{
				return this.proxyAddressesField;
			}
			set
			{
				this.proxyAddressesField = value;
			}
		}

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06006C7C RID: 27772 RVA: 0x00174AA0 File Offset: 0x00172CA0
		// (set) Token: 0x06006C7D RID: 27773 RVA: 0x00174AA8 File Offset: 0x00172CA8
		[XmlElement(Order = 125)]
		public DirectoryPropertyXmlRightsManagementUserKeySingle RightsManagementUserKey
		{
			get
			{
				return this.rightsManagementUserKeyField;
			}
			set
			{
				this.rightsManagementUserKeyField = value;
			}
		}

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06006C7E RID: 27774 RVA: 0x00174AB1 File Offset: 0x00172CB1
		// (set) Token: 0x06006C7F RID: 27775 RVA: 0x00174AB9 File Offset: 0x00172CB9
		[XmlElement(Order = 126)]
		public DirectoryPropertyXmlServiceInfo ServiceInfo
		{
			get
			{
				return this.serviceInfoField;
			}
			set
			{
				this.serviceInfoField = value;
			}
		}

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x06006C80 RID: 27776 RVA: 0x00174AC2 File Offset: 0x00172CC2
		// (set) Token: 0x06006C81 RID: 27777 RVA: 0x00174ACA File Offset: 0x00172CCA
		[XmlElement(Order = 127)]
		public DirectoryPropertyXmlServiceOriginatedResource ServiceOriginatedResource
		{
			get
			{
				return this.serviceOriginatedResourceField;
			}
			set
			{
				this.serviceOriginatedResourceField = value;
			}
		}

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x06006C82 RID: 27778 RVA: 0x00174AD3 File Offset: 0x00172CD3
		// (set) Token: 0x06006C83 RID: 27779 RVA: 0x00174ADB File Offset: 0x00172CDB
		[XmlElement(Order = 128)]
		public DirectoryPropertyStringSingleLength1To64 ShadowCommonName
		{
			get
			{
				return this.shadowCommonNameField;
			}
			set
			{
				this.shadowCommonNameField = value;
			}
		}

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x06006C84 RID: 27780 RVA: 0x00174AE4 File Offset: 0x00172CE4
		// (set) Token: 0x06006C85 RID: 27781 RVA: 0x00174AEC File Offset: 0x00172CEC
		[XmlElement(Order = 129)]
		public DirectoryPropertyStringLength1To1123 ShadowProxyAddresses
		{
			get
			{
				return this.shadowProxyAddressesField;
			}
			set
			{
				this.shadowProxyAddressesField = value;
			}
		}

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x06006C86 RID: 27782 RVA: 0x00174AF5 File Offset: 0x00172CF5
		// (set) Token: 0x06006C87 RID: 27783 RVA: 0x00174AFD File Offset: 0x00172CFD
		[XmlElement(Order = 130)]
		public DirectoryPropertyStringSingleLength1To454 SipProxyAddress
		{
			get
			{
				return this.sipProxyAddressField;
			}
			set
			{
				this.sipProxyAddressField = value;
			}
		}

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x06006C88 RID: 27784 RVA: 0x00174B06 File Offset: 0x00172D06
		// (set) Token: 0x06006C89 RID: 27785 RVA: 0x00174B0E File Offset: 0x00172D0E
		[XmlElement(Order = 131)]
		public DirectoryPropertyStringSingleLength1To64 Sn
		{
			get
			{
				return this.snField;
			}
			set
			{
				this.snField = value;
			}
		}

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x06006C8A RID: 27786 RVA: 0x00174B17 File Offset: 0x00172D17
		// (set) Token: 0x06006C8B RID: 27787 RVA: 0x00174B1F File Offset: 0x00172D1F
		[XmlElement(Order = 132)]
		public DirectoryPropertyDateTimeSingle SoftDeletionTimestamp
		{
			get
			{
				return this.softDeletionTimestampField;
			}
			set
			{
				this.softDeletionTimestampField = value;
			}
		}

		// Token: 0x170026C6 RID: 9926
		// (get) Token: 0x06006C8C RID: 27788 RVA: 0x00174B28 File Offset: 0x00172D28
		// (set) Token: 0x06006C8D RID: 27789 RVA: 0x00174B30 File Offset: 0x00172D30
		[XmlElement(Order = 133)]
		public DirectoryPropertyStringSingleLength1To256 SourceAnchor
		{
			get
			{
				return this.sourceAnchorField;
			}
			set
			{
				this.sourceAnchorField = value;
			}
		}

		// Token: 0x170026C7 RID: 9927
		// (get) Token: 0x06006C8E RID: 27790 RVA: 0x00174B39 File Offset: 0x00172D39
		// (set) Token: 0x06006C8F RID: 27791 RVA: 0x00174B41 File Offset: 0x00172D41
		[XmlElement(Order = 134)]
		public DirectoryPropertyStringSingleLength1To128 St
		{
			get
			{
				return this.stField;
			}
			set
			{
				this.stField = value;
			}
		}

		// Token: 0x170026C8 RID: 9928
		// (get) Token: 0x06006C90 RID: 27792 RVA: 0x00174B4A File Offset: 0x00172D4A
		// (set) Token: 0x06006C91 RID: 27793 RVA: 0x00174B52 File Offset: 0x00172D52
		[XmlElement(Order = 135)]
		public DirectoryPropertyStringSingleLength1To1024 Street
		{
			get
			{
				return this.streetField;
			}
			set
			{
				this.streetField = value;
			}
		}

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x06006C92 RID: 27794 RVA: 0x00174B5B File Offset: 0x00172D5B
		// (set) Token: 0x06006C93 RID: 27795 RVA: 0x00174B63 File Offset: 0x00172D63
		[XmlElement(Order = 136)]
		public DirectoryPropertyStringSingleLength1To1024 StreetAddress
		{
			get
			{
				return this.streetAddressField;
			}
			set
			{
				this.streetAddressField = value;
			}
		}

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x06006C94 RID: 27796 RVA: 0x00174B6C File Offset: 0x00172D6C
		// (set) Token: 0x06006C95 RID: 27797 RVA: 0x00174B74 File Offset: 0x00172D74
		[XmlElement(Order = 137)]
		public DirectoryPropertyDateTimeSingle StsRefreshTokensValidFrom
		{
			get
			{
				return this.stsRefreshTokensValidFromField;
			}
			set
			{
				this.stsRefreshTokensValidFromField = value;
			}
		}

		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06006C96 RID: 27798 RVA: 0x00174B7D File Offset: 0x00172D7D
		// (set) Token: 0x06006C97 RID: 27799 RVA: 0x00174B85 File Offset: 0x00172D85
		[XmlElement(Order = 138)]
		public DirectoryPropertyTargetAddress TargetAddress
		{
			get
			{
				return this.targetAddressField;
			}
			set
			{
				this.targetAddressField = value;
			}
		}

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06006C98 RID: 27800 RVA: 0x00174B8E File Offset: 0x00172D8E
		// (set) Token: 0x06006C99 RID: 27801 RVA: 0x00174B96 File Offset: 0x00172D96
		[XmlElement(Order = 139)]
		public DirectoryPropertyStringSingleLength1To64 TelephoneAssistant
		{
			get
			{
				return this.telephoneAssistantField;
			}
			set
			{
				this.telephoneAssistantField = value;
			}
		}

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06006C9A RID: 27802 RVA: 0x00174B9F File Offset: 0x00172D9F
		// (set) Token: 0x06006C9B RID: 27803 RVA: 0x00174BA7 File Offset: 0x00172DA7
		[XmlElement(Order = 140)]
		public DirectoryPropertyStringSingleLength1To64 TelephoneNumber
		{
			get
			{
				return this.telephoneNumberField;
			}
			set
			{
				this.telephoneNumberField = value;
			}
		}

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x06006C9C RID: 27804 RVA: 0x00174BB0 File Offset: 0x00172DB0
		// (set) Token: 0x06006C9D RID: 27805 RVA: 0x00174BB8 File Offset: 0x00172DB8
		[XmlElement(Order = 141)]
		public DirectoryPropertyBinarySingleLength1To102400 ThumbnailPhoto
		{
			get
			{
				return this.thumbnailPhotoField;
			}
			set
			{
				this.thumbnailPhotoField = value;
			}
		}

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x06006C9E RID: 27806 RVA: 0x00174BC1 File Offset: 0x00172DC1
		// (set) Token: 0x06006C9F RID: 27807 RVA: 0x00174BC9 File Offset: 0x00172DC9
		[XmlElement(Order = 142)]
		public DirectoryPropertyStringSingleLength1To128 Title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x06006CA0 RID: 27808 RVA: 0x00174BD2 File Offset: 0x00172DD2
		// (set) Token: 0x06006CA1 RID: 27809 RVA: 0x00174BDA File Offset: 0x00172DDA
		[XmlElement(Order = 143)]
		public DirectoryPropertyStringLength1To1123 Url
		{
			get
			{
				return this.urlField;
			}
			set
			{
				this.urlField = value;
			}
		}

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x06006CA2 RID: 27810 RVA: 0x00174BE3 File Offset: 0x00172DE3
		// (set) Token: 0x06006CA3 RID: 27811 RVA: 0x00174BEB File Offset: 0x00172DEB
		[XmlElement(Order = 144)]
		public DirectoryPropertyStringSingleLength1To3 UsageLocation
		{
			get
			{
				return this.usageLocationField;
			}
			set
			{
				this.usageLocationField = value;
			}
		}

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x06006CA4 RID: 27812 RVA: 0x00174BF4 File Offset: 0x00172DF4
		// (set) Token: 0x06006CA5 RID: 27813 RVA: 0x00174BFC File Offset: 0x00172DFC
		[XmlElement(Order = 145)]
		public DirectoryPropertyBinaryLength1To32768 UserCertificate
		{
			get
			{
				return this.userCertificateField;
			}
			set
			{
				this.userCertificateField = value;
			}
		}

		// Token: 0x170026D3 RID: 9939
		// (get) Token: 0x06006CA6 RID: 27814 RVA: 0x00174C05 File Offset: 0x00172E05
		// (set) Token: 0x06006CA7 RID: 27815 RVA: 0x00174C0D File Offset: 0x00172E0D
		[XmlElement(Order = 146)]
		public DirectoryPropertyStringSingleLength1To1024 UserPrincipalName
		{
			get
			{
				return this.userPrincipalNameField;
			}
			set
			{
				this.userPrincipalNameField = value;
			}
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x06006CA8 RID: 27816 RVA: 0x00174C16 File Offset: 0x00172E16
		// (set) Token: 0x06006CA9 RID: 27817 RVA: 0x00174C1E File Offset: 0x00172E1E
		[XmlElement(Order = 147)]
		public DirectoryPropertyBinaryLength1To32768 UserSMIMECertificate
		{
			get
			{
				return this.userSMIMECertificateField;
			}
			set
			{
				this.userSMIMECertificateField = value;
			}
		}

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x06006CAA RID: 27818 RVA: 0x00174C27 File Offset: 0x00172E27
		// (set) Token: 0x06006CAB RID: 27819 RVA: 0x00174C2F File Offset: 0x00172E2F
		[XmlElement(Order = 148)]
		public DirectoryPropertyInt32SingleMin0 UserState
		{
			get
			{
				return this.userStateField;
			}
			set
			{
				this.userStateField = value;
			}
		}

		// Token: 0x170026D6 RID: 9942
		// (get) Token: 0x06006CAC RID: 27820 RVA: 0x00174C38 File Offset: 0x00172E38
		// (set) Token: 0x06006CAD RID: 27821 RVA: 0x00174C40 File Offset: 0x00172E40
		[XmlElement(Order = 149)]
		public DirectoryPropertyInt32SingleMin0Max2 UserType
		{
			get
			{
				return this.userTypeField;
			}
			set
			{
				this.userTypeField = value;
			}
		}

		// Token: 0x170026D7 RID: 9943
		// (get) Token: 0x06006CAE RID: 27822 RVA: 0x00174C49 File Offset: 0x00172E49
		// (set) Token: 0x06006CAF RID: 27823 RVA: 0x00174C51 File Offset: 0x00172E51
		[XmlElement(Order = 150)]
		public DirectoryPropertyXmlValidationError ValidationError
		{
			get
			{
				return this.validationErrorField;
			}
			set
			{
				this.validationErrorField = value;
			}
		}

		// Token: 0x170026D8 RID: 9944
		// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x00174C5A File Offset: 0x00172E5A
		// (set) Token: 0x06006CB1 RID: 27825 RVA: 0x00174C62 File Offset: 0x00172E62
		[XmlElement(Order = 151)]
		public DirectoryPropertyBinarySingleLength8 WindowsLiveNetId
		{
			get
			{
				return this.windowsLiveNetIdField;
			}
			set
			{
				this.windowsLiveNetIdField = value;
			}
		}

		// Token: 0x170026D9 RID: 9945
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x00174C6B File Offset: 0x00172E6B
		// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x00174C73 File Offset: 0x00172E73
		[XmlElement(Order = 152)]
		public DirectoryPropertyStringSingleLength1To2048 WwwHomepage
		{
			get
			{
				return this.wwwHomepageField;
			}
			set
			{
				this.wwwHomepageField = value;
			}
		}

		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x00174C7C File Offset: 0x00172E7C
		// (set) Token: 0x06006CB5 RID: 27829 RVA: 0x00174C84 File Offset: 0x00172E84
		[XmlArrayItem("AttributeSet", IsNullable = false)]
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01", Order = 153)]
		public AttributeSet[] SingleAuthorityMetadata
		{
			get
			{
				return this.singleAuthorityMetadataField;
			}
			set
			{
				this.singleAuthorityMetadataField = value;
			}
		}

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x00174C8D File Offset: 0x00172E8D
		// (set) Token: 0x06006CB7 RID: 27831 RVA: 0x00174C95 File Offset: 0x00172E95
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x04004606 RID: 17926
		private DirectoryPropertyStringSingleLength1To256 acceptedAsField;

		// Token: 0x04004607 RID: 17927
		private DirectoryPropertyBooleanSingle accountEnabledField;

		// Token: 0x04004608 RID: 17928
		private DirectoryPropertyXmlAlternativeSecurityId alternativeSecurityIdField;

		// Token: 0x04004609 RID: 17929
		private DirectoryPropertyXmlAssignedPlan assignedPlanField;

		// Token: 0x0400460A RID: 17930
		private DirectoryPropertyReferenceAddressListSingle assistantField;

		// Token: 0x0400460B RID: 17931
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x0400460C RID: 17932
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x0400460D RID: 17933
		private DirectoryPropertyInt32Single cloudMSExchArchiveStatusField;

		// Token: 0x0400460E RID: 17934
		private DirectoryPropertyBinarySingleLength1To4000 cloudMSExchBlockedSendersHashField;

		// Token: 0x0400460F RID: 17935
		private DirectoryPropertyGuidSingle cloudMSExchMailboxGuidField;

		// Token: 0x04004610 RID: 17936
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x04004611 RID: 17937
		private DirectoryPropertyBinarySingleLength1To12000 cloudMSExchSafeRecipientsHashField;

		// Token: 0x04004612 RID: 17938
		private DirectoryPropertyBinarySingleLength1To32000 cloudMSExchSafeSendersHashField;

		// Token: 0x04004613 RID: 17939
		private DirectoryPropertyDateTimeSingle cloudMSExchTeamMailboxExpirationField;

		// Token: 0x04004614 RID: 17940
		private DirectoryPropertyReferenceAddressList cloudMSExchTeamMailboxOwnersField;

		// Token: 0x04004615 RID: 17941
		private DirectoryPropertyStringSingleLength1To2048 cloudMSExchTeamMailboxSharePointUrlField;

		// Token: 0x04004616 RID: 17942
		private DirectoryPropertyStringLength1To1123 cloudMSExchUCVoiceMailSettingsField;

		// Token: 0x04004617 RID: 17943
		private DirectoryPropertyStringLength1To40 cloudMSExchUserHoldPoliciesField;

		// Token: 0x04004618 RID: 17944
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x04004619 RID: 17945
		private DirectoryPropertyStringSingleLength1To64 companyField;

		// Token: 0x0400461A RID: 17946
		private DirectoryPropertyInt32SingleMin0Max65535 countryCodeField;

		// Token: 0x0400461B RID: 17947
		private DirectoryPropertyDateTimeSingle createdOnField;

		// Token: 0x0400461C RID: 17948
		private DirectoryPropertyInt32SingleMin0 creationTypeField;

		// Token: 0x0400461D RID: 17949
		private DirectoryPropertyBooleanSingle deliverAndRedirectField;

		// Token: 0x0400461E RID: 17950
		private DirectoryPropertyStringSingleLength1To64 departmentField;

		// Token: 0x0400461F RID: 17951
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04004620 RID: 17952
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x04004621 RID: 17953
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04004622 RID: 17954
		private DirectoryPropertyStringSingleLength1To16 employeeIdField;

		// Token: 0x04004623 RID: 17955
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x04004624 RID: 17956
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x04004625 RID: 17957
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x04004626 RID: 17958
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x04004627 RID: 17959
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x04004628 RID: 17960
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x04004629 RID: 17961
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x0400462A RID: 17962
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x0400462B RID: 17963
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x0400462C RID: 17964
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x0400462D RID: 17965
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x0400462E RID: 17966
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x0400462F RID: 17967
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x04004630 RID: 17968
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x04004631 RID: 17969
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x04004632 RID: 17970
		private DirectoryPropertyStringSingleLength1To64 facsimileTelephoneNumberField;

		// Token: 0x04004633 RID: 17971
		private DirectoryPropertyStringSingleLength1To64 givenNameField;

		// Token: 0x04004634 RID: 17972
		private DirectoryPropertyStringSingleLength1To64 homePhoneField;

		// Token: 0x04004635 RID: 17973
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x04004636 RID: 17974
		private DirectoryPropertyStringSingleLength1To6 initialsField;

		// Token: 0x04004637 RID: 17975
		private DirectoryPropertyInt32Single internetEncodingField;

		// Token: 0x04004638 RID: 17976
		private DirectoryPropertyStringLength1To2048 inviteResourcesField;

		// Token: 0x04004639 RID: 17977
		private DirectoryPropertyStringSingleLength1To64 iPPhoneField;

		// Token: 0x0400463A RID: 17978
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x0400463B RID: 17979
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x0400463C RID: 17980
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x0400463D RID: 17981
		private DirectoryPropertyStringSingleLength1To64 middleNameField;

		// Token: 0x0400463E RID: 17982
		private DirectoryPropertyStringSingleLength1To64 mobileField;

		// Token: 0x0400463F RID: 17983
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x04004640 RID: 17984
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x04004641 RID: 17985
		private DirectoryPropertyGuidSingle mSExchArchiveGuidField;

		// Token: 0x04004642 RID: 17986
		private DirectoryPropertyStringLength1To512 mSExchArchiveNameField;

		// Token: 0x04004643 RID: 17987
		private DirectoryPropertyStringSingleLength1To256 mSExchAssistantNameField;

		// Token: 0x04004644 RID: 17988
		private DirectoryPropertyInt32Single mSExchAuditAdminField;

		// Token: 0x04004645 RID: 17989
		private DirectoryPropertyInt32Single mSExchAuditDelegateField;

		// Token: 0x04004646 RID: 17990
		private DirectoryPropertyInt32Single mSExchAuditDelegateAdminField;

		// Token: 0x04004647 RID: 17991
		private DirectoryPropertyInt32Single mSExchAuditOwnerField;

		// Token: 0x04004648 RID: 17992
		private DirectoryPropertyBinarySingleLength1To4000 mSExchBlockedSendersHashField;

		// Token: 0x04004649 RID: 17993
		private DirectoryPropertyBooleanSingle mSExchBypassAuditField;

		// Token: 0x0400464A RID: 17994
		private DirectoryPropertyDateTimeSingle mSExchElcExpirySuspensionEndField;

		// Token: 0x0400464B RID: 17995
		private DirectoryPropertyDateTimeSingle mSExchElcExpirySuspensionStartField;

		// Token: 0x0400464C RID: 17996
		private DirectoryPropertyInt32Single mSExchElcMailboxFlagsField;

		// Token: 0x0400464D RID: 17997
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x0400464E RID: 17998
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x0400464F RID: 17999
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x04004650 RID: 18000
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x04004651 RID: 18001
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x04004652 RID: 18002
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x04004653 RID: 18003
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x04004654 RID: 18004
		private DirectoryPropertyStringSingleLength1To256 mSExchImmutableIdField;

		// Token: 0x04004655 RID: 18005
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x04004656 RID: 18006
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x04004657 RID: 18007
		private DirectoryPropertyBooleanSingle mSExchMailboxAuditEnableField;

		// Token: 0x04004658 RID: 18008
		private DirectoryPropertyInt32Single mSExchMailboxAuditLogAgeLimitField;

		// Token: 0x04004659 RID: 18009
		private DirectoryPropertyGuidSingle mSExchMailboxGuidField;

		// Token: 0x0400465A RID: 18010
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x0400465B RID: 18011
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x0400465C RID: 18012
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x0400465D RID: 18013
		private DirectoryPropertyInt64Single mSExchRemoteRecipientTypeField;

		// Token: 0x0400465E RID: 18014
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x0400465F RID: 18015
		private DirectoryPropertyInt32Single mSExchResourceCapacityField;

		// Token: 0x04004660 RID: 18016
		private DirectoryPropertyStringSingleLength1To1024 mSExchResourceDisplayField;

		// Token: 0x04004661 RID: 18017
		private DirectoryPropertyStringLength1To1024 mSExchResourceMetadataField;

		// Token: 0x04004662 RID: 18018
		private DirectoryPropertyStringLength1To1024 mSExchResourceSearchPropertiesField;

		// Token: 0x04004663 RID: 18019
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x04004664 RID: 18020
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x04004665 RID: 18021
		private DirectoryPropertyBinarySingleLength1To12000 mSExchSafeRecipientsHashField;

		// Token: 0x04004666 RID: 18022
		private DirectoryPropertyBinarySingleLength1To32000 mSExchSafeSendersHashField;

		// Token: 0x04004667 RID: 18023
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x04004668 RID: 18024
		private DirectoryPropertyDateTimeSingle mSExchTeamMailboxExpirationField;

		// Token: 0x04004669 RID: 18025
		private DirectoryPropertyReferenceAddressList mSExchTeamMailboxOwnersField;

		// Token: 0x0400466A RID: 18026
		private DirectoryPropertyReferenceAddressListSingle mSExchTeamMailboxSharePointLinkedByField;

		// Token: 0x0400466B RID: 18027
		private DirectoryPropertyStringSingleLength1To2048 mSExchTeamMailboxSharePointUrlField;

		// Token: 0x0400466C RID: 18028
		private DirectoryPropertyDateTimeSingle mSExchUserCreatedTimestampField;

		// Token: 0x0400466D RID: 18029
		private DirectoryPropertyStringLength1To40 mSExchUserHoldPoliciesField;

		// Token: 0x0400466E RID: 18030
		private DirectoryPropertyInt32SingleMin0 mSRtcSipApplicationOptionsField;

		// Token: 0x0400466F RID: 18031
		private DirectoryPropertyStringSingleLength1To256 mSRtcSipDeploymentLocatorField;

		// Token: 0x04004670 RID: 18032
		private DirectoryPropertyStringSingleLength1To500 mSRtcSipLineField;

		// Token: 0x04004671 RID: 18033
		private DirectoryPropertyInt32Single mSRtcSipOptionFlagsField;

		// Token: 0x04004672 RID: 18034
		private DirectoryPropertyStringSingleLength1To512 mSRtcSipOwnerUrnField;

		// Token: 0x04004673 RID: 18035
		private DirectoryPropertyStringSingleLength1To454 mSRtcSipPrimaryUserAddressField;

		// Token: 0x04004674 RID: 18036
		private DirectoryPropertyBooleanSingle mSRtcSipUserEnabledField;

		// Token: 0x04004675 RID: 18037
		private DirectoryPropertyBinarySingleLength1To128 onPremiseSecurityIdentifierField;

		// Token: 0x04004676 RID: 18038
		private DirectoryPropertyStringLength1To64 otherFacsimileTelephoneNumberField;

		// Token: 0x04004677 RID: 18039
		private DirectoryPropertyStringLength1To64 otherHomePhoneField;

		// Token: 0x04004678 RID: 18040
		private DirectoryPropertyStringLength1To512 otherIPPhoneField;

		// Token: 0x04004679 RID: 18041
		private DirectoryPropertyStringLength1To256 otherMailField;

		// Token: 0x0400467A RID: 18042
		private DirectoryPropertyStringLength1To64 otherMobileField;

		// Token: 0x0400467B RID: 18043
		private DirectoryPropertyStringLength1To64 otherPagerField;

		// Token: 0x0400467C RID: 18044
		private DirectoryPropertyStringLength1To64 otherTelephoneField;

		// Token: 0x0400467D RID: 18045
		private DirectoryPropertyStringSingleLength1To64 pagerField;

		// Token: 0x0400467E RID: 18046
		private DirectoryPropertyStringSingleLength1To128 physicalDeliveryOfficeNameField;

		// Token: 0x0400467F RID: 18047
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x04004680 RID: 18048
		private DirectoryPropertyStringLength1To40 postOfficeBoxField;

		// Token: 0x04004681 RID: 18049
		private DirectoryPropertyStringSingleLength1To64 preferredLanguageField;

		// Token: 0x04004682 RID: 18050
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x04004683 RID: 18051
		private DirectoryPropertyXmlRightsManagementUserKeySingle rightsManagementUserKeyField;

		// Token: 0x04004684 RID: 18052
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04004685 RID: 18053
		private DirectoryPropertyXmlServiceOriginatedResource serviceOriginatedResourceField;

		// Token: 0x04004686 RID: 18054
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x04004687 RID: 18055
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x04004688 RID: 18056
		private DirectoryPropertyStringSingleLength1To454 sipProxyAddressField;

		// Token: 0x04004689 RID: 18057
		private DirectoryPropertyStringSingleLength1To64 snField;

		// Token: 0x0400468A RID: 18058
		private DirectoryPropertyDateTimeSingle softDeletionTimestampField;

		// Token: 0x0400468B RID: 18059
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x0400468C RID: 18060
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x0400468D RID: 18061
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x0400468E RID: 18062
		private DirectoryPropertyStringSingleLength1To1024 streetAddressField;

		// Token: 0x0400468F RID: 18063
		private DirectoryPropertyDateTimeSingle stsRefreshTokensValidFromField;

		// Token: 0x04004690 RID: 18064
		private DirectoryPropertyTargetAddress targetAddressField;

		// Token: 0x04004691 RID: 18065
		private DirectoryPropertyStringSingleLength1To64 telephoneAssistantField;

		// Token: 0x04004692 RID: 18066
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x04004693 RID: 18067
		private DirectoryPropertyBinarySingleLength1To102400 thumbnailPhotoField;

		// Token: 0x04004694 RID: 18068
		private DirectoryPropertyStringSingleLength1To128 titleField;

		// Token: 0x04004695 RID: 18069
		private DirectoryPropertyStringLength1To1123 urlField;

		// Token: 0x04004696 RID: 18070
		private DirectoryPropertyStringSingleLength1To3 usageLocationField;

		// Token: 0x04004697 RID: 18071
		private DirectoryPropertyBinaryLength1To32768 userCertificateField;

		// Token: 0x04004698 RID: 18072
		private DirectoryPropertyStringSingleLength1To1024 userPrincipalNameField;

		// Token: 0x04004699 RID: 18073
		private DirectoryPropertyBinaryLength1To32768 userSMIMECertificateField;

		// Token: 0x0400469A RID: 18074
		private DirectoryPropertyInt32SingleMin0 userStateField;

		// Token: 0x0400469B RID: 18075
		private DirectoryPropertyInt32SingleMin0Max2 userTypeField;

		// Token: 0x0400469C RID: 18076
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x0400469D RID: 18077
		private DirectoryPropertyBinarySingleLength8 windowsLiveNetIdField;

		// Token: 0x0400469E RID: 18078
		private DirectoryPropertyStringSingleLength1To2048 wwwHomepageField;

		// Token: 0x0400469F RID: 18079
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x040046A0 RID: 18080
		private XmlAttribute[] anyAttrField;
	}
}
