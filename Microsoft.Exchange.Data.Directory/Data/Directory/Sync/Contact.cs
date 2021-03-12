using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000849 RID: 2121
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class Contact : DirectoryObject, IValidationErrorSupport
	{
		// Token: 0x06006950 RID: 26960 RVA: 0x00171A94 File Offset: 0x0016FC94
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyStringSingleLength1To3>(SyncOrgPersonSchema.C, ref this.cField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.Co, ref this.coField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Company, ref this.companyField);
			processor.Process<DirectoryPropertyInt32SingleMin0Max65535>(SyncOrgPersonSchema.CountryCode, ref this.countryCodeField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Department, ref this.departmentField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.DisplayName, ref this.displayNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CloudLegacyExchangeDN, ref this.cloudLegacyExchangeDNField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.CloudMsExchRecipientDisplayType, ref this.cloudMSExchRecipientDisplayTypeField);
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
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncOrgPersonSchema.AssistantName, ref this.mSExchAssistantNameField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.HiddenFromAddressListsEnabled, ref this.mSExchHideFromAddressListsField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncRecipientSchema.LitigationHoldDate, ref this.mSExchLitigationHoldDateField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.LitigationHoldOwner, ref this.mSExchLitigationHoldOwnerField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.ModerationEnabled, ref this.mSExchEnableModerationField);
			processor.Process<DirectoryPropertyInt64Single>(SyncRecipientSchema.RecipientTypeDetailsValue, ref this.mSExchRecipientTypeDetailsField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.RetentionComment, ref this.mSExchRetentionCommentField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.RetentionUrl, ref this.mSExchRetentionUrlField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.RequireAllSendersAreAuthenticated, ref this.mSExchRequireAuthToSendToField);
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
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncRecipientSchema.Cn, ref this.shadowCommonNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.LastName, ref this.snField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.OnPremisesObjectId, ref this.sourceAnchorField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.StateOrProvince, ref this.stField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncOrgPersonSchema.StreetAddress, ref this.streetAddressField);
			processor.Process<DirectoryPropertyTargetAddress>(SyncRecipientSchema.ExternalEmailAddress, ref this.targetAddressField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.TelephoneAssistant, ref this.telephoneAssistantField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncOrgPersonSchema.Phone, ref this.telephoneNumberField);
			processor.Process<DirectoryPropertyStringSingleLength1To128>(SyncOrgPersonSchema.Title, ref this.titleField);
			processor.Process<DirectoryPropertyXmlValidationError>(SyncRecipientSchema.ValidationError, ref this.validationErrorField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncOrgPersonSchema.WebPage, ref this.wwwHomepageField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.IsDirSynced, ref this.dirSyncEnabledField);
			DirectoryPropertyAttributeSet directoryPropertyAttributeSet = (DirectoryPropertyAttributeSet)DirectoryObject.GetDirectoryProperty(this.singleAuthorityMetadataField);
			processor.Process<DirectoryPropertyAttributeSet>(SyncRecipientSchema.DirSyncAuthorityMetadata, ref directoryPropertyAttributeSet);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.ModerationFlags, ref this.mSExchModerationFlagsField);
			processor.Process<DirectoryPropertyBinarySingleLength1To4000>(SyncRecipientSchema.BlockedSendersHash, ref this.mSExchBlockedSendersHashField);
			processor.Process<DirectoryPropertyBinarySingleLength1To12000>(SyncRecipientSchema.SafeRecipientsHash, ref this.mSExchSafeRecipientsHashField);
			processor.Process<DirectoryPropertyBinarySingleLength1To32000>(SyncRecipientSchema.SafeSendersHash, ref this.mSExchSafeSendersHashField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.RecipientDisplayType, ref this.mSExchRecipientDisplayTypeField);
			processor.Process<DirectoryPropertyBinaryLength1To32768>(SyncRecipientSchema.UserCertificate, ref this.userCertificateField);
			processor.Process<DirectoryPropertyBinaryLength1To32768>(SyncRecipientSchema.UserSMimeCertificate, ref this.userSMIMECertificateField);
		}

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06006951 RID: 26961 RVA: 0x00171FCB File Offset: 0x001701CB
		// (set) Token: 0x06006952 RID: 26962 RVA: 0x00171FD3 File Offset: 0x001701D3
		[XmlElement(Order = 0)]
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

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06006953 RID: 26963 RVA: 0x00171FDC File Offset: 0x001701DC
		// (set) Token: 0x06006954 RID: 26964 RVA: 0x00171FE4 File Offset: 0x001701E4
		[XmlElement(Order = 1)]
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

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06006955 RID: 26965 RVA: 0x00171FED File Offset: 0x001701ED
		// (set) Token: 0x06006956 RID: 26966 RVA: 0x00171FF5 File Offset: 0x001701F5
		[XmlElement(Order = 2)]
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

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x06006957 RID: 26967 RVA: 0x00171FFE File Offset: 0x001701FE
		// (set) Token: 0x06006958 RID: 26968 RVA: 0x00172006 File Offset: 0x00170206
		[XmlElement(Order = 3)]
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

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x06006959 RID: 26969 RVA: 0x0017200F File Offset: 0x0017020F
		// (set) Token: 0x0600695A RID: 26970 RVA: 0x00172017 File Offset: 0x00170217
		[XmlElement(Order = 4)]
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

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x0600695B RID: 26971 RVA: 0x00172020 File Offset: 0x00170220
		// (set) Token: 0x0600695C RID: 26972 RVA: 0x00172028 File Offset: 0x00170228
		[XmlElement(Order = 5)]
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

		// Token: 0x17002553 RID: 9555
		// (get) Token: 0x0600695D RID: 26973 RVA: 0x00172031 File Offset: 0x00170231
		// (set) Token: 0x0600695E RID: 26974 RVA: 0x00172039 File Offset: 0x00170239
		[XmlElement(Order = 6)]
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

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x0600695F RID: 26975 RVA: 0x00172042 File Offset: 0x00170242
		// (set) Token: 0x06006960 RID: 26976 RVA: 0x0017204A File Offset: 0x0017024A
		[XmlElement(Order = 7)]
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

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x06006961 RID: 26977 RVA: 0x00172053 File Offset: 0x00170253
		// (set) Token: 0x06006962 RID: 26978 RVA: 0x0017205B File Offset: 0x0017025B
		[XmlElement(Order = 8)]
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

		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x06006963 RID: 26979 RVA: 0x00172064 File Offset: 0x00170264
		// (set) Token: 0x06006964 RID: 26980 RVA: 0x0017206C File Offset: 0x0017026C
		[XmlElement(Order = 9)]
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

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x06006965 RID: 26981 RVA: 0x00172075 File Offset: 0x00170275
		// (set) Token: 0x06006966 RID: 26982 RVA: 0x0017207D File Offset: 0x0017027D
		[XmlElement(Order = 10)]
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

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x06006967 RID: 26983 RVA: 0x00172086 File Offset: 0x00170286
		// (set) Token: 0x06006968 RID: 26984 RVA: 0x0017208E File Offset: 0x0017028E
		[XmlElement(Order = 11)]
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

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x06006969 RID: 26985 RVA: 0x00172097 File Offset: 0x00170297
		// (set) Token: 0x0600696A RID: 26986 RVA: 0x0017209F File Offset: 0x0017029F
		[XmlElement(Order = 12)]
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

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x0600696B RID: 26987 RVA: 0x001720A8 File Offset: 0x001702A8
		// (set) Token: 0x0600696C RID: 26988 RVA: 0x001720B0 File Offset: 0x001702B0
		[XmlElement(Order = 13)]
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

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x001720B9 File Offset: 0x001702B9
		// (set) Token: 0x0600696E RID: 26990 RVA: 0x001720C1 File Offset: 0x001702C1
		[XmlElement(Order = 14)]
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

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x0600696F RID: 26991 RVA: 0x001720CA File Offset: 0x001702CA
		// (set) Token: 0x06006970 RID: 26992 RVA: 0x001720D2 File Offset: 0x001702D2
		[XmlElement(Order = 15)]
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

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x06006971 RID: 26993 RVA: 0x001720DB File Offset: 0x001702DB
		// (set) Token: 0x06006972 RID: 26994 RVA: 0x001720E3 File Offset: 0x001702E3
		[XmlElement(Order = 16)]
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

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x06006973 RID: 26995 RVA: 0x001720EC File Offset: 0x001702EC
		// (set) Token: 0x06006974 RID: 26996 RVA: 0x001720F4 File Offset: 0x001702F4
		[XmlElement(Order = 17)]
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

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x06006975 RID: 26997 RVA: 0x001720FD File Offset: 0x001702FD
		// (set) Token: 0x06006976 RID: 26998 RVA: 0x00172105 File Offset: 0x00170305
		[XmlElement(Order = 18)]
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

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x06006977 RID: 26999 RVA: 0x0017210E File Offset: 0x0017030E
		// (set) Token: 0x06006978 RID: 27000 RVA: 0x00172116 File Offset: 0x00170316
		[XmlElement(Order = 19)]
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

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x06006979 RID: 27001 RVA: 0x0017211F File Offset: 0x0017031F
		// (set) Token: 0x0600697A RID: 27002 RVA: 0x00172127 File Offset: 0x00170327
		[XmlElement(Order = 20)]
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

		// Token: 0x17002562 RID: 9570
		// (get) Token: 0x0600697B RID: 27003 RVA: 0x00172130 File Offset: 0x00170330
		// (set) Token: 0x0600697C RID: 27004 RVA: 0x00172138 File Offset: 0x00170338
		[XmlElement(Order = 21)]
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

		// Token: 0x17002563 RID: 9571
		// (get) Token: 0x0600697D RID: 27005 RVA: 0x00172141 File Offset: 0x00170341
		// (set) Token: 0x0600697E RID: 27006 RVA: 0x00172149 File Offset: 0x00170349
		[XmlElement(Order = 22)]
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

		// Token: 0x17002564 RID: 9572
		// (get) Token: 0x0600697F RID: 27007 RVA: 0x00172152 File Offset: 0x00170352
		// (set) Token: 0x06006980 RID: 27008 RVA: 0x0017215A File Offset: 0x0017035A
		[XmlElement(Order = 23)]
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

		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x06006981 RID: 27009 RVA: 0x00172163 File Offset: 0x00170363
		// (set) Token: 0x06006982 RID: 27010 RVA: 0x0017216B File Offset: 0x0017036B
		[XmlElement(Order = 24)]
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

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x06006983 RID: 27011 RVA: 0x00172174 File Offset: 0x00170374
		// (set) Token: 0x06006984 RID: 27012 RVA: 0x0017217C File Offset: 0x0017037C
		[XmlElement(Order = 25)]
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

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x06006985 RID: 27013 RVA: 0x00172185 File Offset: 0x00170385
		// (set) Token: 0x06006986 RID: 27014 RVA: 0x0017218D File Offset: 0x0017038D
		[XmlElement(Order = 26)]
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

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x06006987 RID: 27015 RVA: 0x00172196 File Offset: 0x00170396
		// (set) Token: 0x06006988 RID: 27016 RVA: 0x0017219E File Offset: 0x0017039E
		[XmlElement(Order = 27)]
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

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x001721A7 File Offset: 0x001703A7
		// (set) Token: 0x0600698A RID: 27018 RVA: 0x001721AF File Offset: 0x001703AF
		[XmlElement(Order = 28)]
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

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x001721B8 File Offset: 0x001703B8
		// (set) Token: 0x0600698C RID: 27020 RVA: 0x001721C0 File Offset: 0x001703C0
		[XmlElement(Order = 29)]
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

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x001721C9 File Offset: 0x001703C9
		// (set) Token: 0x0600698E RID: 27022 RVA: 0x001721D1 File Offset: 0x001703D1
		[XmlElement(Order = 30)]
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

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x001721DA File Offset: 0x001703DA
		// (set) Token: 0x06006990 RID: 27024 RVA: 0x001721E2 File Offset: 0x001703E2
		[XmlElement(Order = 31)]
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

		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x06006991 RID: 27025 RVA: 0x001721EB File Offset: 0x001703EB
		// (set) Token: 0x06006992 RID: 27026 RVA: 0x001721F3 File Offset: 0x001703F3
		[XmlElement(Order = 32)]
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

		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x06006993 RID: 27027 RVA: 0x001721FC File Offset: 0x001703FC
		// (set) Token: 0x06006994 RID: 27028 RVA: 0x00172204 File Offset: 0x00170404
		[XmlElement(Order = 33)]
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

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x06006995 RID: 27029 RVA: 0x0017220D File Offset: 0x0017040D
		// (set) Token: 0x06006996 RID: 27030 RVA: 0x00172215 File Offset: 0x00170415
		[XmlElement(Order = 34)]
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

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x06006997 RID: 27031 RVA: 0x0017221E File Offset: 0x0017041E
		// (set) Token: 0x06006998 RID: 27032 RVA: 0x00172226 File Offset: 0x00170426
		[XmlElement(Order = 35)]
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

		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x06006999 RID: 27033 RVA: 0x0017222F File Offset: 0x0017042F
		// (set) Token: 0x0600699A RID: 27034 RVA: 0x00172237 File Offset: 0x00170437
		[XmlElement(Order = 36)]
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

		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x0600699B RID: 27035 RVA: 0x00172240 File Offset: 0x00170440
		// (set) Token: 0x0600699C RID: 27036 RVA: 0x00172248 File Offset: 0x00170448
		[XmlElement(Order = 37)]
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

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x0600699D RID: 27037 RVA: 0x00172251 File Offset: 0x00170451
		// (set) Token: 0x0600699E RID: 27038 RVA: 0x00172259 File Offset: 0x00170459
		[XmlElement(Order = 38)]
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

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x0600699F RID: 27039 RVA: 0x00172262 File Offset: 0x00170462
		// (set) Token: 0x060069A0 RID: 27040 RVA: 0x0017226A File Offset: 0x0017046A
		[XmlElement(Order = 39)]
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

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x060069A1 RID: 27041 RVA: 0x00172273 File Offset: 0x00170473
		// (set) Token: 0x060069A2 RID: 27042 RVA: 0x0017227B File Offset: 0x0017047B
		[XmlElement(Order = 40)]
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

		// Token: 0x17002576 RID: 9590
		// (get) Token: 0x060069A3 RID: 27043 RVA: 0x00172284 File Offset: 0x00170484
		// (set) Token: 0x060069A4 RID: 27044 RVA: 0x0017228C File Offset: 0x0017048C
		[XmlElement(Order = 41)]
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

		// Token: 0x17002577 RID: 9591
		// (get) Token: 0x060069A5 RID: 27045 RVA: 0x00172295 File Offset: 0x00170495
		// (set) Token: 0x060069A6 RID: 27046 RVA: 0x0017229D File Offset: 0x0017049D
		[XmlElement(Order = 42)]
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

		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x060069A7 RID: 27047 RVA: 0x001722A6 File Offset: 0x001704A6
		// (set) Token: 0x060069A8 RID: 27048 RVA: 0x001722AE File Offset: 0x001704AE
		[XmlElement(Order = 43)]
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

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x060069A9 RID: 27049 RVA: 0x001722B7 File Offset: 0x001704B7
		// (set) Token: 0x060069AA RID: 27050 RVA: 0x001722BF File Offset: 0x001704BF
		[XmlElement(Order = 44)]
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

		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x060069AB RID: 27051 RVA: 0x001722C8 File Offset: 0x001704C8
		// (set) Token: 0x060069AC RID: 27052 RVA: 0x001722D0 File Offset: 0x001704D0
		[XmlElement(Order = 45)]
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

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x060069AD RID: 27053 RVA: 0x001722D9 File Offset: 0x001704D9
		// (set) Token: 0x060069AE RID: 27054 RVA: 0x001722E1 File Offset: 0x001704E1
		[XmlElement(Order = 46)]
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

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x060069AF RID: 27055 RVA: 0x001722EA File Offset: 0x001704EA
		// (set) Token: 0x060069B0 RID: 27056 RVA: 0x001722F2 File Offset: 0x001704F2
		[XmlElement(Order = 47)]
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

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x060069B1 RID: 27057 RVA: 0x001722FB File Offset: 0x001704FB
		// (set) Token: 0x060069B2 RID: 27058 RVA: 0x00172303 File Offset: 0x00170503
		[XmlElement(Order = 48)]
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

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x060069B3 RID: 27059 RVA: 0x0017230C File Offset: 0x0017050C
		// (set) Token: 0x060069B4 RID: 27060 RVA: 0x00172314 File Offset: 0x00170514
		[XmlElement(Order = 49)]
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

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x060069B5 RID: 27061 RVA: 0x0017231D File Offset: 0x0017051D
		// (set) Token: 0x060069B6 RID: 27062 RVA: 0x00172325 File Offset: 0x00170525
		[XmlElement(Order = 50)]
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

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x060069B7 RID: 27063 RVA: 0x0017232E File Offset: 0x0017052E
		// (set) Token: 0x060069B8 RID: 27064 RVA: 0x00172336 File Offset: 0x00170536
		[XmlElement(Order = 51)]
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

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x060069B9 RID: 27065 RVA: 0x0017233F File Offset: 0x0017053F
		// (set) Token: 0x060069BA RID: 27066 RVA: 0x00172347 File Offset: 0x00170547
		[XmlElement(Order = 52)]
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

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x060069BB RID: 27067 RVA: 0x00172350 File Offset: 0x00170550
		// (set) Token: 0x060069BC RID: 27068 RVA: 0x00172358 File Offset: 0x00170558
		[XmlElement(Order = 53)]
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

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x060069BD RID: 27069 RVA: 0x00172361 File Offset: 0x00170561
		// (set) Token: 0x060069BE RID: 27070 RVA: 0x00172369 File Offset: 0x00170569
		[XmlElement(Order = 54)]
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

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x060069BF RID: 27071 RVA: 0x00172372 File Offset: 0x00170572
		// (set) Token: 0x060069C0 RID: 27072 RVA: 0x0017237A File Offset: 0x0017057A
		[XmlElement(Order = 55)]
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

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x060069C1 RID: 27073 RVA: 0x00172383 File Offset: 0x00170583
		// (set) Token: 0x060069C2 RID: 27074 RVA: 0x0017238B File Offset: 0x0017058B
		[XmlElement(Order = 56)]
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

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x060069C3 RID: 27075 RVA: 0x00172394 File Offset: 0x00170594
		// (set) Token: 0x060069C4 RID: 27076 RVA: 0x0017239C File Offset: 0x0017059C
		[XmlElement(Order = 57)]
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

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x060069C5 RID: 27077 RVA: 0x001723A5 File Offset: 0x001705A5
		// (set) Token: 0x060069C6 RID: 27078 RVA: 0x001723AD File Offset: 0x001705AD
		[XmlElement(Order = 58)]
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

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x060069C7 RID: 27079 RVA: 0x001723B6 File Offset: 0x001705B6
		// (set) Token: 0x060069C8 RID: 27080 RVA: 0x001723BE File Offset: 0x001705BE
		[XmlElement(Order = 59)]
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

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x060069C9 RID: 27081 RVA: 0x001723C7 File Offset: 0x001705C7
		// (set) Token: 0x060069CA RID: 27082 RVA: 0x001723CF File Offset: 0x001705CF
		[XmlElement(Order = 60)]
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

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x060069CB RID: 27083 RVA: 0x001723D8 File Offset: 0x001705D8
		// (set) Token: 0x060069CC RID: 27084 RVA: 0x001723E0 File Offset: 0x001705E0
		[XmlElement(Order = 61)]
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

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x060069CD RID: 27085 RVA: 0x001723E9 File Offset: 0x001705E9
		// (set) Token: 0x060069CE RID: 27086 RVA: 0x001723F1 File Offset: 0x001705F1
		[XmlElement(Order = 62)]
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

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x060069CF RID: 27087 RVA: 0x001723FA File Offset: 0x001705FA
		// (set) Token: 0x060069D0 RID: 27088 RVA: 0x00172402 File Offset: 0x00170602
		[XmlElement(Order = 63)]
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

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x060069D1 RID: 27089 RVA: 0x0017240B File Offset: 0x0017060B
		// (set) Token: 0x060069D2 RID: 27090 RVA: 0x00172413 File Offset: 0x00170613
		[XmlElement(Order = 64)]
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

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x060069D3 RID: 27091 RVA: 0x0017241C File Offset: 0x0017061C
		// (set) Token: 0x060069D4 RID: 27092 RVA: 0x00172424 File Offset: 0x00170624
		[XmlElement(Order = 65)]
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

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x060069D5 RID: 27093 RVA: 0x0017242D File Offset: 0x0017062D
		// (set) Token: 0x060069D6 RID: 27094 RVA: 0x00172435 File Offset: 0x00170635
		[XmlElement(Order = 66)]
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

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x060069D7 RID: 27095 RVA: 0x0017243E File Offset: 0x0017063E
		// (set) Token: 0x060069D8 RID: 27096 RVA: 0x00172446 File Offset: 0x00170646
		[XmlElement(Order = 67)]
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

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x060069D9 RID: 27097 RVA: 0x0017244F File Offset: 0x0017064F
		// (set) Token: 0x060069DA RID: 27098 RVA: 0x00172457 File Offset: 0x00170657
		[XmlElement(Order = 68)]
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

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x060069DB RID: 27099 RVA: 0x00172460 File Offset: 0x00170660
		// (set) Token: 0x060069DC RID: 27100 RVA: 0x00172468 File Offset: 0x00170668
		[XmlElement(Order = 69)]
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

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x060069DD RID: 27101 RVA: 0x00172471 File Offset: 0x00170671
		// (set) Token: 0x060069DE RID: 27102 RVA: 0x00172479 File Offset: 0x00170679
		[XmlElement(Order = 70)]
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

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x060069DF RID: 27103 RVA: 0x00172482 File Offset: 0x00170682
		// (set) Token: 0x060069E0 RID: 27104 RVA: 0x0017248A File Offset: 0x0017068A
		[XmlElement(Order = 71)]
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

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x060069E1 RID: 27105 RVA: 0x00172493 File Offset: 0x00170693
		// (set) Token: 0x060069E2 RID: 27106 RVA: 0x0017249B File Offset: 0x0017069B
		[XmlElement(Order = 72)]
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

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x060069E3 RID: 27107 RVA: 0x001724A4 File Offset: 0x001706A4
		// (set) Token: 0x060069E4 RID: 27108 RVA: 0x001724AC File Offset: 0x001706AC
		[XmlElement(Order = 73)]
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

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x060069E5 RID: 27109 RVA: 0x001724B5 File Offset: 0x001706B5
		// (set) Token: 0x060069E6 RID: 27110 RVA: 0x001724BD File Offset: 0x001706BD
		[XmlElement(Order = 74)]
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

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x060069E7 RID: 27111 RVA: 0x001724C6 File Offset: 0x001706C6
		// (set) Token: 0x060069E8 RID: 27112 RVA: 0x001724CE File Offset: 0x001706CE
		[XmlElement(Order = 75)]
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

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x060069E9 RID: 27113 RVA: 0x001724D7 File Offset: 0x001706D7
		// (set) Token: 0x060069EA RID: 27114 RVA: 0x001724DF File Offset: 0x001706DF
		[XmlElement(Order = 76)]
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

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x060069EB RID: 27115 RVA: 0x001724E8 File Offset: 0x001706E8
		// (set) Token: 0x060069EC RID: 27116 RVA: 0x001724F0 File Offset: 0x001706F0
		[XmlElement(Order = 77)]
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

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x060069ED RID: 27117 RVA: 0x001724F9 File Offset: 0x001706F9
		// (set) Token: 0x060069EE RID: 27118 RVA: 0x00172501 File Offset: 0x00170701
		[XmlElement(Order = 78)]
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

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x060069EF RID: 27119 RVA: 0x0017250A File Offset: 0x0017070A
		// (set) Token: 0x060069F0 RID: 27120 RVA: 0x00172512 File Offset: 0x00170712
		[XmlElement(Order = 79)]
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

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x060069F1 RID: 27121 RVA: 0x0017251B File Offset: 0x0017071B
		// (set) Token: 0x060069F2 RID: 27122 RVA: 0x00172523 File Offset: 0x00170723
		[XmlElement(Order = 80)]
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

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x0017252C File Offset: 0x0017072C
		// (set) Token: 0x060069F4 RID: 27124 RVA: 0x00172534 File Offset: 0x00170734
		[XmlElement(Order = 81)]
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

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x0017253D File Offset: 0x0017073D
		// (set) Token: 0x060069F6 RID: 27126 RVA: 0x00172545 File Offset: 0x00170745
		[XmlElement(Order = 82)]
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

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x060069F7 RID: 27127 RVA: 0x0017254E File Offset: 0x0017074E
		// (set) Token: 0x060069F8 RID: 27128 RVA: 0x00172556 File Offset: 0x00170756
		[XmlElement(Order = 83)]
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

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x060069F9 RID: 27129 RVA: 0x0017255F File Offset: 0x0017075F
		// (set) Token: 0x060069FA RID: 27130 RVA: 0x00172567 File Offset: 0x00170767
		[XmlElement(Order = 84)]
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

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x060069FB RID: 27131 RVA: 0x00172570 File Offset: 0x00170770
		// (set) Token: 0x060069FC RID: 27132 RVA: 0x00172578 File Offset: 0x00170778
		[XmlElement(Order = 85)]
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

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x060069FD RID: 27133 RVA: 0x00172581 File Offset: 0x00170781
		// (set) Token: 0x060069FE RID: 27134 RVA: 0x00172589 File Offset: 0x00170789
		[XmlElement(Order = 86)]
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

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x060069FF RID: 27135 RVA: 0x00172592 File Offset: 0x00170792
		// (set) Token: 0x06006A00 RID: 27136 RVA: 0x0017259A File Offset: 0x0017079A
		[XmlElement(Order = 87)]
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

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x06006A01 RID: 27137 RVA: 0x001725A3 File Offset: 0x001707A3
		// (set) Token: 0x06006A02 RID: 27138 RVA: 0x001725AB File Offset: 0x001707AB
		[XmlElement(Order = 88)]
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

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06006A03 RID: 27139 RVA: 0x001725B4 File Offset: 0x001707B4
		// (set) Token: 0x06006A04 RID: 27140 RVA: 0x001725BC File Offset: 0x001707BC
		[XmlElement(Order = 89)]
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

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x06006A05 RID: 27141 RVA: 0x001725C5 File Offset: 0x001707C5
		// (set) Token: 0x06006A06 RID: 27142 RVA: 0x001725CD File Offset: 0x001707CD
		[XmlElement(Order = 90)]
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

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x06006A07 RID: 27143 RVA: 0x001725D6 File Offset: 0x001707D6
		// (set) Token: 0x06006A08 RID: 27144 RVA: 0x001725DE File Offset: 0x001707DE
		[XmlElement(Order = 91)]
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

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x06006A09 RID: 27145 RVA: 0x001725E7 File Offset: 0x001707E7
		// (set) Token: 0x06006A0A RID: 27146 RVA: 0x001725EF File Offset: 0x001707EF
		[XmlElement(Order = 92)]
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

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x06006A0B RID: 27147 RVA: 0x001725F8 File Offset: 0x001707F8
		// (set) Token: 0x06006A0C RID: 27148 RVA: 0x00172600 File Offset: 0x00170800
		[XmlElement(Order = 93)]
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

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x06006A0D RID: 27149 RVA: 0x00172609 File Offset: 0x00170809
		// (set) Token: 0x06006A0E RID: 27150 RVA: 0x00172611 File Offset: 0x00170811
		[XmlElement(Order = 94)]
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

		// Token: 0x170025AC RID: 9644
		// (get) Token: 0x06006A0F RID: 27151 RVA: 0x0017261A File Offset: 0x0017081A
		// (set) Token: 0x06006A10 RID: 27152 RVA: 0x00172622 File Offset: 0x00170822
		[XmlElement(Order = 95)]
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

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x06006A11 RID: 27153 RVA: 0x0017262B File Offset: 0x0017082B
		// (set) Token: 0x06006A12 RID: 27154 RVA: 0x00172633 File Offset: 0x00170833
		[XmlElement(Order = 96)]
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

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x06006A13 RID: 27155 RVA: 0x0017263C File Offset: 0x0017083C
		// (set) Token: 0x06006A14 RID: 27156 RVA: 0x00172644 File Offset: 0x00170844
		[XmlElement(Order = 97)]
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

		// Token: 0x170025AF RID: 9647
		// (get) Token: 0x06006A15 RID: 27157 RVA: 0x0017264D File Offset: 0x0017084D
		// (set) Token: 0x06006A16 RID: 27158 RVA: 0x00172655 File Offset: 0x00170855
		[XmlArrayItem("AttributeSet", IsNullable = false)]
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01", Order = 98)]
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

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x06006A17 RID: 27159 RVA: 0x0017265E File Offset: 0x0017085E
		// (set) Token: 0x06006A18 RID: 27160 RVA: 0x00172666 File Offset: 0x00170866
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

		// Token: 0x04004512 RID: 17682
		private DirectoryPropertyReferenceAddressListSingle assistantField;

		// Token: 0x04004513 RID: 17683
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x04004514 RID: 17684
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x04004515 RID: 17685
		private DirectoryPropertyBinarySingleLength1To4000 cloudMSExchBlockedSendersHashField;

		// Token: 0x04004516 RID: 17686
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x04004517 RID: 17687
		private DirectoryPropertyBinarySingleLength1To12000 cloudMSExchSafeRecipientsHashField;

		// Token: 0x04004518 RID: 17688
		private DirectoryPropertyBinarySingleLength1To32000 cloudMSExchSafeSendersHashField;

		// Token: 0x04004519 RID: 17689
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x0400451A RID: 17690
		private DirectoryPropertyStringSingleLength1To64 companyField;

		// Token: 0x0400451B RID: 17691
		private DirectoryPropertyInt32SingleMin0Max65535 countryCodeField;

		// Token: 0x0400451C RID: 17692
		private DirectoryPropertyStringSingleLength1To64 departmentField;

		// Token: 0x0400451D RID: 17693
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x0400451E RID: 17694
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x0400451F RID: 17695
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04004520 RID: 17696
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x04004521 RID: 17697
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x04004522 RID: 17698
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x04004523 RID: 17699
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x04004524 RID: 17700
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x04004525 RID: 17701
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x04004526 RID: 17702
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x04004527 RID: 17703
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x04004528 RID: 17704
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x04004529 RID: 17705
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x0400452A RID: 17706
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x0400452B RID: 17707
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x0400452C RID: 17708
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x0400452D RID: 17709
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x0400452E RID: 17710
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x0400452F RID: 17711
		private DirectoryPropertyStringSingleLength1To64 facsimileTelephoneNumberField;

		// Token: 0x04004530 RID: 17712
		private DirectoryPropertyStringSingleLength1To64 givenNameField;

		// Token: 0x04004531 RID: 17713
		private DirectoryPropertyStringSingleLength1To64 homePhoneField;

		// Token: 0x04004532 RID: 17714
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x04004533 RID: 17715
		private DirectoryPropertyStringSingleLength1To6 initialsField;

		// Token: 0x04004534 RID: 17716
		private DirectoryPropertyInt32Single internetEncodingField;

		// Token: 0x04004535 RID: 17717
		private DirectoryPropertyStringSingleLength1To64 iPPhoneField;

		// Token: 0x04004536 RID: 17718
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x04004537 RID: 17719
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x04004538 RID: 17720
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x04004539 RID: 17721
		private DirectoryPropertyStringSingleLength1To64 middleNameField;

		// Token: 0x0400453A RID: 17722
		private DirectoryPropertyStringSingleLength1To64 mobileField;

		// Token: 0x0400453B RID: 17723
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x0400453C RID: 17724
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x0400453D RID: 17725
		private DirectoryPropertyStringSingleLength1To256 mSExchAssistantNameField;

		// Token: 0x0400453E RID: 17726
		private DirectoryPropertyBinarySingleLength1To4000 mSExchBlockedSendersHashField;

		// Token: 0x0400453F RID: 17727
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x04004540 RID: 17728
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x04004541 RID: 17729
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x04004542 RID: 17730
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x04004543 RID: 17731
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x04004544 RID: 17732
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x04004545 RID: 17733
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x04004546 RID: 17734
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x04004547 RID: 17735
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x04004548 RID: 17736
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x04004549 RID: 17737
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x0400454A RID: 17738
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x0400454B RID: 17739
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x0400454C RID: 17740
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x0400454D RID: 17741
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x0400454E RID: 17742
		private DirectoryPropertyBinarySingleLength1To12000 mSExchSafeRecipientsHashField;

		// Token: 0x0400454F RID: 17743
		private DirectoryPropertyBinarySingleLength1To32000 mSExchSafeSendersHashField;

		// Token: 0x04004550 RID: 17744
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x04004551 RID: 17745
		private DirectoryPropertyStringSingleLength1To256 mSRtcSipDeploymentLocatorField;

		// Token: 0x04004552 RID: 17746
		private DirectoryPropertyStringSingleLength1To500 mSRtcSipLineField;

		// Token: 0x04004553 RID: 17747
		private DirectoryPropertyInt32Single mSRtcSipOptionFlagsField;

		// Token: 0x04004554 RID: 17748
		private DirectoryPropertyStringSingleLength1To454 mSRtcSipPrimaryUserAddressField;

		// Token: 0x04004555 RID: 17749
		private DirectoryPropertyBooleanSingle mSRtcSipUserEnabledField;

		// Token: 0x04004556 RID: 17750
		private DirectoryPropertyStringLength1To64 otherFacsimileTelephoneNumberField;

		// Token: 0x04004557 RID: 17751
		private DirectoryPropertyStringLength1To64 otherHomePhoneField;

		// Token: 0x04004558 RID: 17752
		private DirectoryPropertyStringLength1To512 otherIPPhoneField;

		// Token: 0x04004559 RID: 17753
		private DirectoryPropertyStringLength1To64 otherMobileField;

		// Token: 0x0400455A RID: 17754
		private DirectoryPropertyStringLength1To64 otherPagerField;

		// Token: 0x0400455B RID: 17755
		private DirectoryPropertyStringLength1To64 otherTelephoneField;

		// Token: 0x0400455C RID: 17756
		private DirectoryPropertyStringSingleLength1To64 pagerField;

		// Token: 0x0400455D RID: 17757
		private DirectoryPropertyStringSingleLength1To128 physicalDeliveryOfficeNameField;

		// Token: 0x0400455E RID: 17758
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x0400455F RID: 17759
		private DirectoryPropertyStringLength1To40 postOfficeBoxField;

		// Token: 0x04004560 RID: 17760
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x04004561 RID: 17761
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04004562 RID: 17762
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x04004563 RID: 17763
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x04004564 RID: 17764
		private DirectoryPropertyStringSingleLength1To454 sipProxyAddressField;

		// Token: 0x04004565 RID: 17765
		private DirectoryPropertyStringSingleLength1To64 snField;

		// Token: 0x04004566 RID: 17766
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x04004567 RID: 17767
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x04004568 RID: 17768
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x04004569 RID: 17769
		private DirectoryPropertyStringSingleLength1To1024 streetAddressField;

		// Token: 0x0400456A RID: 17770
		private DirectoryPropertyTargetAddress targetAddressField;

		// Token: 0x0400456B RID: 17771
		private DirectoryPropertyStringSingleLength1To64 telephoneAssistantField;

		// Token: 0x0400456C RID: 17772
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x0400456D RID: 17773
		private DirectoryPropertyBinarySingleLength1To102400 thumbnailPhotoField;

		// Token: 0x0400456E RID: 17774
		private DirectoryPropertyStringSingleLength1To128 titleField;

		// Token: 0x0400456F RID: 17775
		private DirectoryPropertyStringLength1To1123 urlField;

		// Token: 0x04004570 RID: 17776
		private DirectoryPropertyBinaryLength1To32768 userCertificateField;

		// Token: 0x04004571 RID: 17777
		private DirectoryPropertyBinaryLength1To32768 userSMIMECertificateField;

		// Token: 0x04004572 RID: 17778
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x04004573 RID: 17779
		private DirectoryPropertyStringSingleLength1To2048 wwwHomepageField;

		// Token: 0x04004574 RID: 17780
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x04004575 RID: 17781
		private XmlAttribute[] anyAttrField;
	}
}
