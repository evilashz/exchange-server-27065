using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000851 RID: 2129
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Group : DirectoryObject, IValidationErrorSupport
	{
		// Token: 0x06006A71 RID: 27249 RVA: 0x00172A50 File Offset: 0x00170C50
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncGroupSchema.Description, ref this.descriptionField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.CloudLegacyExchangeDN, ref this.cloudLegacyExchangeDNField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.CloudMsExchRecipientDisplayType, ref this.cloudMSExchRecipientDisplayTypeField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.DisplayName, ref this.displayNameField);
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
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.MailEnabled, ref this.mailEnabledField);
			processor.Process<DirectoryPropertyStringSingleMailNickname>(SyncRecipientSchema.Alias, ref this.mailNicknameField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.SeniorityIndex, ref this.mSDSHABSeniorityIndexField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.PhoneticDisplayName, ref this.mSDSPhoneticDisplayNameField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.ModerationEnabled, ref this.mSExchEnableModerationField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.HiddenFromAddressListsEnabled, ref this.mSExchHideFromAddressListsField);
			processor.Process<DirectoryPropertyDateTimeSingle>(SyncRecipientSchema.LitigationHoldDate, ref this.mSExchLitigationHoldDateField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.LitigationHoldOwner, ref this.mSExchLitigationHoldOwnerField);
			processor.Process<DirectoryPropertyInt32Single>(SyncRecipientSchema.ModerationFlags, ref this.mSExchModerationFlagsField);
			processor.Process<DirectoryPropertyInt64Single>(SyncGroupSchema.RecipientTypeDetailsValue, ref this.mSExchRecipientTypeDetailsField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncRecipientSchema.RetentionComment, ref this.mSExchRetentionCommentField);
			processor.Process<DirectoryPropertyStringSingleLength1To2048>(SyncRecipientSchema.RetentionUrl, ref this.mSExchRetentionUrlField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.RequireAllSendersAreAuthenticated, ref this.mSExchRequireAuthToSendToField);
			processor.Process<DirectoryPropertyStringLength2To500>(SyncRecipientSchema.MailTipTranslations, ref this.mSExchSenderHintTranslationsField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.IsHierarchicalGroup, ref this.mSOrgIsOrganizationalField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.SendOofMessageToOriginatorEnabled, ref this.oofReplyToOriginatorField);
			processor.Process<DirectoryPropertyProxyAddresses>(SyncRecipientSchema.EmailAddresses, ref this.proxyAddressesField);
			processor.Process<DirectoryPropertyProxyAddresses>(SyncRecipientSchema.SmtpAndX500Addresses, ref this.proxyAddressesField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.ReportToOriginatorEnabled, ref this.reportToOriginatorField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.ReportToManagerEnabled, ref this.reportToOwnerField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.SecurityEnabled, ref this.securityEnabledField);
			processor.Process<DirectoryPropertyStringSingleLength1To64>(SyncRecipientSchema.Cn, ref this.shadowCommonNameField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncRecipientSchema.OnPremisesObjectId, ref this.sourceAnchorField);
			processor.Process<DirectoryPropertyXmlValidationError>(SyncRecipientSchema.ValidationError, ref this.validationErrorField);
			processor.Process<DirectoryPropertyStringSingleLength1To40>(SyncGroupSchema.WellKnownObject, ref this.wellKnownObjectField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncRecipientSchema.IsDirSynced, ref this.dirSyncEnabledField);
			DirectoryPropertyAttributeSet directoryPropertyAttributeSet = (DirectoryPropertyAttributeSet)DirectoryObject.GetDirectoryProperty(this.singleAuthorityMetadataField);
			processor.Process<DirectoryPropertyAttributeSet>(SyncRecipientSchema.DirSyncAuthorityMetadata, ref directoryPropertyAttributeSet);
			processor.Process<DirectoryPropertyStringLength1To1024>(SyncGroupSchema.SharePointResources, ref this.sharepointResourcesField);
			processor.Process<DirectoryPropertyReferenceUserAndServicePrincipalSingle>(SyncGroupSchema.Creator, ref this.createdOnBehalfOfField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncGroupSchema.IsPublic, ref this.isPublicField);
		}

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x06006A72 RID: 27250 RVA: 0x00172E00 File Offset: 0x00171000
		// (set) Token: 0x06006A73 RID: 27251 RVA: 0x00172E08 File Offset: 0x00171008
		[XmlElement(Order = 0)]
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

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06006A74 RID: 27252 RVA: 0x00172E11 File Offset: 0x00171011
		// (set) Token: 0x06006A75 RID: 27253 RVA: 0x00172E19 File Offset: 0x00171019
		[XmlElement(Order = 1)]
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

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x00172E22 File Offset: 0x00171022
		// (set) Token: 0x06006A77 RID: 27255 RVA: 0x00172E2A File Offset: 0x0017102A
		[XmlElement(Order = 2)]
		public DirectoryPropertyReferenceUserAndServicePrincipalSingle CreatedOnBehalfOf
		{
			get
			{
				return this.createdOnBehalfOfField;
			}
			set
			{
				this.createdOnBehalfOfField = value;
			}
		}

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06006A78 RID: 27256 RVA: 0x00172E33 File Offset: 0x00171033
		// (set) Token: 0x06006A79 RID: 27257 RVA: 0x00172E3B File Offset: 0x0017103B
		[XmlElement(Order = 3)]
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

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06006A7A RID: 27258 RVA: 0x00172E44 File Offset: 0x00171044
		// (set) Token: 0x06006A7B RID: 27259 RVA: 0x00172E4C File Offset: 0x0017104C
		[XmlElement(Order = 4)]
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

		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x00172E55 File Offset: 0x00171055
		// (set) Token: 0x06006A7D RID: 27261 RVA: 0x00172E5D File Offset: 0x0017105D
		[XmlElement(Order = 5)]
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

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x06006A7E RID: 27262 RVA: 0x00172E66 File Offset: 0x00171066
		// (set) Token: 0x06006A7F RID: 27263 RVA: 0x00172E6E File Offset: 0x0017106E
		[XmlElement(Order = 6)]
		public DirectoryPropertyStringLength1To1024 ExchangeResources
		{
			get
			{
				return this.exchangeResourcesField;
			}
			set
			{
				this.exchangeResourcesField = value;
			}
		}

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x06006A80 RID: 27264 RVA: 0x00172E77 File Offset: 0x00171077
		// (set) Token: 0x06006A81 RID: 27265 RVA: 0x00172E7F File Offset: 0x0017107F
		[XmlElement(Order = 7)]
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

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x00172E88 File Offset: 0x00171088
		// (set) Token: 0x06006A83 RID: 27267 RVA: 0x00172E90 File Offset: 0x00171090
		[XmlElement(Order = 8)]
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

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x06006A84 RID: 27268 RVA: 0x00172E99 File Offset: 0x00171099
		// (set) Token: 0x06006A85 RID: 27269 RVA: 0x00172EA1 File Offset: 0x001710A1
		[XmlElement(Order = 9)]
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

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x06006A86 RID: 27270 RVA: 0x00172EAA File Offset: 0x001710AA
		// (set) Token: 0x06006A87 RID: 27271 RVA: 0x00172EB2 File Offset: 0x001710B2
		[XmlElement(Order = 10)]
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

		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x06006A88 RID: 27272 RVA: 0x00172EBB File Offset: 0x001710BB
		// (set) Token: 0x06006A89 RID: 27273 RVA: 0x00172EC3 File Offset: 0x001710C3
		[XmlElement(Order = 11)]
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

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x06006A8A RID: 27274 RVA: 0x00172ECC File Offset: 0x001710CC
		// (set) Token: 0x06006A8B RID: 27275 RVA: 0x00172ED4 File Offset: 0x001710D4
		[XmlElement(Order = 12)]
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

		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x00172EDD File Offset: 0x001710DD
		// (set) Token: 0x06006A8D RID: 27277 RVA: 0x00172EE5 File Offset: 0x001710E5
		[XmlElement(Order = 13)]
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

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x00172EEE File Offset: 0x001710EE
		// (set) Token: 0x06006A8F RID: 27279 RVA: 0x00172EF6 File Offset: 0x001710F6
		[XmlElement(Order = 14)]
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

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x06006A90 RID: 27280 RVA: 0x00172EFF File Offset: 0x001710FF
		// (set) Token: 0x06006A91 RID: 27281 RVA: 0x00172F07 File Offset: 0x00171107
		[XmlElement(Order = 15)]
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

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x00172F10 File Offset: 0x00171110
		// (set) Token: 0x06006A93 RID: 27283 RVA: 0x00172F18 File Offset: 0x00171118
		[XmlElement(Order = 16)]
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

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x00172F21 File Offset: 0x00171121
		// (set) Token: 0x06006A95 RID: 27285 RVA: 0x00172F29 File Offset: 0x00171129
		[XmlElement(Order = 17)]
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

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x06006A96 RID: 27286 RVA: 0x00172F32 File Offset: 0x00171132
		// (set) Token: 0x06006A97 RID: 27287 RVA: 0x00172F3A File Offset: 0x0017113A
		[XmlElement(Order = 18)]
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

		// Token: 0x170025E5 RID: 9701
		// (get) Token: 0x06006A98 RID: 27288 RVA: 0x00172F43 File Offset: 0x00171143
		// (set) Token: 0x06006A99 RID: 27289 RVA: 0x00172F4B File Offset: 0x0017114B
		[XmlElement(Order = 19)]
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

		// Token: 0x170025E6 RID: 9702
		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x00172F54 File Offset: 0x00171154
		// (set) Token: 0x06006A9B RID: 27291 RVA: 0x00172F5C File Offset: 0x0017115C
		[XmlElement(Order = 20)]
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

		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x00172F65 File Offset: 0x00171165
		// (set) Token: 0x06006A9D RID: 27293 RVA: 0x00172F6D File Offset: 0x0017116D
		[XmlElement(Order = 21)]
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

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x06006A9E RID: 27294 RVA: 0x00172F76 File Offset: 0x00171176
		// (set) Token: 0x06006A9F RID: 27295 RVA: 0x00172F7E File Offset: 0x0017117E
		[XmlElement(Order = 22)]
		public DirectoryPropertyInt32SingleMin0 GroupType
		{
			get
			{
				return this.groupTypeField;
			}
			set
			{
				this.groupTypeField = value;
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x00172F87 File Offset: 0x00171187
		// (set) Token: 0x06006AA1 RID: 27297 RVA: 0x00172F8F File Offset: 0x0017118F
		[XmlElement(Order = 23)]
		public DirectoryPropertyBooleanSingle HideDLMembership
		{
			get
			{
				return this.hideDLMembershipField;
			}
			set
			{
				this.hideDLMembershipField = value;
			}
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x06006AA2 RID: 27298 RVA: 0x00172F98 File Offset: 0x00171198
		// (set) Token: 0x06006AA3 RID: 27299 RVA: 0x00172FA0 File Offset: 0x001711A0
		[XmlElement(Order = 24)]
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

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x00172FA9 File Offset: 0x001711A9
		// (set) Token: 0x06006AA5 RID: 27301 RVA: 0x00172FB1 File Offset: 0x001711B1
		[XmlElement(Order = 25)]
		public DirectoryPropertyBooleanSingle IsPublic
		{
			get
			{
				return this.isPublicField;
			}
			set
			{
				this.isPublicField = value;
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x00172FBA File Offset: 0x001711BA
		// (set) Token: 0x06006AA7 RID: 27303 RVA: 0x00172FC2 File Offset: 0x001711C2
		[XmlElement(Order = 26)]
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

		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x00172FCB File Offset: 0x001711CB
		// (set) Token: 0x06006AA9 RID: 27305 RVA: 0x00172FD3 File Offset: 0x001711D3
		[XmlElement(Order = 27)]
		public DirectoryPropertyBooleanSingle MailEnabled
		{
			get
			{
				return this.mailEnabledField;
			}
			set
			{
				this.mailEnabledField = value;
			}
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x06006AAA RID: 27306 RVA: 0x00172FDC File Offset: 0x001711DC
		// (set) Token: 0x06006AAB RID: 27307 RVA: 0x00172FE4 File Offset: 0x001711E4
		[XmlElement(Order = 28)]
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

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x06006AAC RID: 27308 RVA: 0x00172FED File Offset: 0x001711ED
		// (set) Token: 0x06006AAD RID: 27309 RVA: 0x00172FF5 File Offset: 0x001711F5
		[XmlElement(Order = 29)]
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

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x06006AAE RID: 27310 RVA: 0x00172FFE File Offset: 0x001711FE
		// (set) Token: 0x06006AAF RID: 27311 RVA: 0x00173006 File Offset: 0x00171206
		[XmlElement(Order = 30)]
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

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x0017300F File Offset: 0x0017120F
		// (set) Token: 0x06006AB1 RID: 27313 RVA: 0x00173017 File Offset: 0x00171217
		[XmlElement(Order = 31)]
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

		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x06006AB2 RID: 27314 RVA: 0x00173020 File Offset: 0x00171220
		// (set) Token: 0x06006AB3 RID: 27315 RVA: 0x00173028 File Offset: 0x00171228
		[XmlElement(Order = 32)]
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

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x00173031 File Offset: 0x00171231
		// (set) Token: 0x06006AB5 RID: 27317 RVA: 0x00173039 File Offset: 0x00171239
		[XmlElement(Order = 33)]
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

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x06006AB6 RID: 27318 RVA: 0x00173042 File Offset: 0x00171242
		// (set) Token: 0x06006AB7 RID: 27319 RVA: 0x0017304A File Offset: 0x0017124A
		[XmlElement(Order = 34)]
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

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x00173053 File Offset: 0x00171253
		// (set) Token: 0x06006AB9 RID: 27321 RVA: 0x0017305B File Offset: 0x0017125B
		[XmlElement(Order = 35)]
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

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x06006ABA RID: 27322 RVA: 0x00173064 File Offset: 0x00171264
		// (set) Token: 0x06006ABB RID: 27323 RVA: 0x0017306C File Offset: 0x0017126C
		[XmlElement(Order = 36)]
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

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06006ABC RID: 27324 RVA: 0x00173075 File Offset: 0x00171275
		// (set) Token: 0x06006ABD RID: 27325 RVA: 0x0017307D File Offset: 0x0017127D
		[XmlElement(Order = 37)]
		public DirectoryPropertyInt32Single MSExchGroupDepartRestriction
		{
			get
			{
				return this.mSExchGroupDepartRestrictionField;
			}
			set
			{
				this.mSExchGroupDepartRestrictionField = value;
			}
		}

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06006ABE RID: 27326 RVA: 0x00173086 File Offset: 0x00171286
		// (set) Token: 0x06006ABF RID: 27327 RVA: 0x0017308E File Offset: 0x0017128E
		[XmlElement(Order = 38)]
		public DirectoryPropertyInt32Single MSExchGroupJoinRestriction
		{
			get
			{
				return this.mSExchGroupJoinRestrictionField;
			}
			set
			{
				this.mSExchGroupJoinRestrictionField = value;
			}
		}

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x00173097 File Offset: 0x00171297
		// (set) Token: 0x06006AC1 RID: 27329 RVA: 0x0017309F File Offset: 0x0017129F
		[XmlElement(Order = 39)]
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

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x001730A8 File Offset: 0x001712A8
		// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x001730B0 File Offset: 0x001712B0
		[XmlElement(Order = 40)]
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

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x001730B9 File Offset: 0x001712B9
		// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x001730C1 File Offset: 0x001712C1
		[XmlElement(Order = 41)]
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

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x001730CA File Offset: 0x001712CA
		// (set) Token: 0x06006AC7 RID: 27335 RVA: 0x001730D2 File Offset: 0x001712D2
		[XmlElement(Order = 42)]
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

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x001730DB File Offset: 0x001712DB
		// (set) Token: 0x06006AC9 RID: 27337 RVA: 0x001730E3 File Offset: 0x001712E3
		[XmlElement(Order = 43)]
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

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x06006ACA RID: 27338 RVA: 0x001730EC File Offset: 0x001712EC
		// (set) Token: 0x06006ACB RID: 27339 RVA: 0x001730F4 File Offset: 0x001712F4
		[XmlElement(Order = 44)]
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

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x06006ACC RID: 27340 RVA: 0x001730FD File Offset: 0x001712FD
		// (set) Token: 0x06006ACD RID: 27341 RVA: 0x00173105 File Offset: 0x00171305
		[XmlElement(Order = 45)]
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

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x06006ACE RID: 27342 RVA: 0x0017310E File Offset: 0x0017130E
		// (set) Token: 0x06006ACF RID: 27343 RVA: 0x00173116 File Offset: 0x00171316
		[XmlElement(Order = 46)]
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

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06006AD0 RID: 27344 RVA: 0x0017311F File Offset: 0x0017131F
		// (set) Token: 0x06006AD1 RID: 27345 RVA: 0x00173127 File Offset: 0x00171327
		[XmlElement(Order = 47)]
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

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x00173130 File Offset: 0x00171330
		// (set) Token: 0x06006AD3 RID: 27347 RVA: 0x00173138 File Offset: 0x00171338
		[XmlElement(Order = 48)]
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

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06006AD4 RID: 27348 RVA: 0x00173141 File Offset: 0x00171341
		// (set) Token: 0x06006AD5 RID: 27349 RVA: 0x00173149 File Offset: 0x00171349
		[XmlElement(Order = 49)]
		public DirectoryPropertyBooleanSingle MSOrgIsOrganizational
		{
			get
			{
				return this.mSOrgIsOrganizationalField;
			}
			set
			{
				this.mSOrgIsOrganizationalField = value;
			}
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x06006AD6 RID: 27350 RVA: 0x00173152 File Offset: 0x00171352
		// (set) Token: 0x06006AD7 RID: 27351 RVA: 0x0017315A File Offset: 0x0017135A
		[XmlElement(Order = 50)]
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

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x06006AD8 RID: 27352 RVA: 0x00173163 File Offset: 0x00171363
		// (set) Token: 0x06006AD9 RID: 27353 RVA: 0x0017316B File Offset: 0x0017136B
		[XmlElement(Order = 51)]
		public DirectoryPropertyBooleanSingle OofReplyToOriginator
		{
			get
			{
				return this.oofReplyToOriginatorField;
			}
			set
			{
				this.oofReplyToOriginatorField = value;
			}
		}

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x06006ADA RID: 27354 RVA: 0x00173174 File Offset: 0x00171374
		// (set) Token: 0x06006ADB RID: 27355 RVA: 0x0017317C File Offset: 0x0017137C
		[XmlElement(Order = 52)]
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

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x06006ADC RID: 27356 RVA: 0x00173185 File Offset: 0x00171385
		// (set) Token: 0x06006ADD RID: 27357 RVA: 0x0017318D File Offset: 0x0017138D
		[XmlElement(Order = 53)]
		public DirectoryPropertyBooleanSingle ReportToOriginator
		{
			get
			{
				return this.reportToOriginatorField;
			}
			set
			{
				this.reportToOriginatorField = value;
			}
		}

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x06006ADE RID: 27358 RVA: 0x00173196 File Offset: 0x00171396
		// (set) Token: 0x06006ADF RID: 27359 RVA: 0x0017319E File Offset: 0x0017139E
		[XmlElement(Order = 54)]
		public DirectoryPropertyBooleanSingle ReportToOwner
		{
			get
			{
				return this.reportToOwnerField;
			}
			set
			{
				this.reportToOwnerField = value;
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06006AE0 RID: 27360 RVA: 0x001731A7 File Offset: 0x001713A7
		// (set) Token: 0x06006AE1 RID: 27361 RVA: 0x001731AF File Offset: 0x001713AF
		[XmlElement(Order = 55)]
		public DirectoryPropertyBooleanSingle SecurityEnabled
		{
			get
			{
				return this.securityEnabledField;
			}
			set
			{
				this.securityEnabledField = value;
			}
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x001731B8 File Offset: 0x001713B8
		// (set) Token: 0x06006AE3 RID: 27363 RVA: 0x001731C0 File Offset: 0x001713C0
		[XmlElement(Order = 56)]
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

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06006AE4 RID: 27364 RVA: 0x001731C9 File Offset: 0x001713C9
		// (set) Token: 0x06006AE5 RID: 27365 RVA: 0x001731D1 File Offset: 0x001713D1
		[XmlElement(Order = 57)]
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

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x06006AE6 RID: 27366 RVA: 0x001731DA File Offset: 0x001713DA
		// (set) Token: 0x06006AE7 RID: 27367 RVA: 0x001731E2 File Offset: 0x001713E2
		[XmlElement(Order = 58)]
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

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x06006AE8 RID: 27368 RVA: 0x001731EB File Offset: 0x001713EB
		// (set) Token: 0x06006AE9 RID: 27369 RVA: 0x001731F3 File Offset: 0x001713F3
		[XmlElement(Order = 59)]
		public DirectoryPropertyStringLength1To1024 SharepointResources
		{
			get
			{
				return this.sharepointResourcesField;
			}
			set
			{
				this.sharepointResourcesField = value;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x06006AEA RID: 27370 RVA: 0x001731FC File Offset: 0x001713FC
		// (set) Token: 0x06006AEB RID: 27371 RVA: 0x00173204 File Offset: 0x00171404
		[XmlElement(Order = 60)]
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

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x06006AEC RID: 27372 RVA: 0x0017320D File Offset: 0x0017140D
		// (set) Token: 0x06006AED RID: 27373 RVA: 0x00173215 File Offset: 0x00171415
		[XmlElement(Order = 61)]
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

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06006AEE RID: 27374 RVA: 0x0017321E File Offset: 0x0017141E
		// (set) Token: 0x06006AEF RID: 27375 RVA: 0x00173226 File Offset: 0x00171426
		[XmlElement(Order = 62)]
		public DirectoryPropertyStringSingleLength1To40 WellKnownObject
		{
			get
			{
				return this.wellKnownObjectField;
			}
			set
			{
				this.wellKnownObjectField = value;
			}
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x0017322F File Offset: 0x0017142F
		// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x00173237 File Offset: 0x00171437
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01", Order = 63)]
		[XmlArrayItem("AttributeSet", IsNullable = false)]
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

		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x00173240 File Offset: 0x00171440
		// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x00173248 File Offset: 0x00171448
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

		// Token: 0x04004597 RID: 17815
		private DirectoryPropertyStringSingleLength1To2048 cloudLegacyExchangeDNField;

		// Token: 0x04004598 RID: 17816
		private DirectoryPropertyInt32Single cloudMSExchRecipientDisplayTypeField;

		// Token: 0x04004599 RID: 17817
		private DirectoryPropertyReferenceUserAndServicePrincipalSingle createdOnBehalfOfField;

		// Token: 0x0400459A RID: 17818
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x0400459B RID: 17819
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x0400459C RID: 17820
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x0400459D RID: 17821
		private DirectoryPropertyStringLength1To1024 exchangeResourcesField;

		// Token: 0x0400459E RID: 17822
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute1Field;

		// Token: 0x0400459F RID: 17823
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute10Field;

		// Token: 0x040045A0 RID: 17824
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute11Field;

		// Token: 0x040045A1 RID: 17825
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute12Field;

		// Token: 0x040045A2 RID: 17826
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute13Field;

		// Token: 0x040045A3 RID: 17827
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute14Field;

		// Token: 0x040045A4 RID: 17828
		private DirectoryPropertyStringSingleLength1To2048 extensionAttribute15Field;

		// Token: 0x040045A5 RID: 17829
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute2Field;

		// Token: 0x040045A6 RID: 17830
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute3Field;

		// Token: 0x040045A7 RID: 17831
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute4Field;

		// Token: 0x040045A8 RID: 17832
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute5Field;

		// Token: 0x040045A9 RID: 17833
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute6Field;

		// Token: 0x040045AA RID: 17834
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute7Field;

		// Token: 0x040045AB RID: 17835
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute8Field;

		// Token: 0x040045AC RID: 17836
		private DirectoryPropertyStringSingleLength1To1024 extensionAttribute9Field;

		// Token: 0x040045AD RID: 17837
		private DirectoryPropertyInt32SingleMin0 groupTypeField;

		// Token: 0x040045AE RID: 17838
		private DirectoryPropertyBooleanSingle hideDLMembershipField;

		// Token: 0x040045AF RID: 17839
		private DirectoryPropertyStringSingleLength1To1024 infoField;

		// Token: 0x040045B0 RID: 17840
		private DirectoryPropertyBooleanSingle isPublicField;

		// Token: 0x040045B1 RID: 17841
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x040045B2 RID: 17842
		private DirectoryPropertyBooleanSingle mailEnabledField;

		// Token: 0x040045B3 RID: 17843
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x040045B4 RID: 17844
		private DirectoryPropertyInt32Single mSDSHABSeniorityIndexField;

		// Token: 0x040045B5 RID: 17845
		private DirectoryPropertyStringSingleLength1To256 mSDSPhoneticDisplayNameField;

		// Token: 0x040045B6 RID: 17846
		private DirectoryPropertyBooleanSingle mSExchEnableModerationField;

		// Token: 0x040045B7 RID: 17847
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute1Field;

		// Token: 0x040045B8 RID: 17848
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute2Field;

		// Token: 0x040045B9 RID: 17849
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute3Field;

		// Token: 0x040045BA RID: 17850
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute4Field;

		// Token: 0x040045BB RID: 17851
		private DirectoryPropertyStringLength1To2048 mSExchExtensionCustomAttribute5Field;

		// Token: 0x040045BC RID: 17852
		private DirectoryPropertyInt32Single mSExchGroupDepartRestrictionField;

		// Token: 0x040045BD RID: 17853
		private DirectoryPropertyInt32Single mSExchGroupJoinRestrictionField;

		// Token: 0x040045BE RID: 17854
		private DirectoryPropertyBooleanSingle mSExchHideFromAddressListsField;

		// Token: 0x040045BF RID: 17855
		private DirectoryPropertyDateTimeSingle mSExchLitigationHoldDateField;

		// Token: 0x040045C0 RID: 17856
		private DirectoryPropertyStringSingleLength1To1024 mSExchLitigationHoldOwnerField;

		// Token: 0x040045C1 RID: 17857
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x040045C2 RID: 17858
		private DirectoryPropertyInt32Single mSExchRecipientDisplayTypeField;

		// Token: 0x040045C3 RID: 17859
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x040045C4 RID: 17860
		private DirectoryPropertyBooleanSingle mSExchRequireAuthToSendToField;

		// Token: 0x040045C5 RID: 17861
		private DirectoryPropertyStringSingleLength1To1024 mSExchRetentionCommentField;

		// Token: 0x040045C6 RID: 17862
		private DirectoryPropertyStringSingleLength1To2048 mSExchRetentionUrlField;

		// Token: 0x040045C7 RID: 17863
		private DirectoryPropertyStringLength2To500 mSExchSenderHintTranslationsField;

		// Token: 0x040045C8 RID: 17864
		private DirectoryPropertyBooleanSingle mSOrgIsOrganizationalField;

		// Token: 0x040045C9 RID: 17865
		private DirectoryPropertyBinarySingleLength1To128 onPremiseSecurityIdentifierField;

		// Token: 0x040045CA RID: 17866
		private DirectoryPropertyBooleanSingle oofReplyToOriginatorField;

		// Token: 0x040045CB RID: 17867
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x040045CC RID: 17868
		private DirectoryPropertyBooleanSingle reportToOriginatorField;

		// Token: 0x040045CD RID: 17869
		private DirectoryPropertyBooleanSingle reportToOwnerField;

		// Token: 0x040045CE RID: 17870
		private DirectoryPropertyBooleanSingle securityEnabledField;

		// Token: 0x040045CF RID: 17871
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x040045D0 RID: 17872
		private DirectoryPropertyStringSingleLength1To64 shadowCommonNameField;

		// Token: 0x040045D1 RID: 17873
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x040045D2 RID: 17874
		private DirectoryPropertyStringLength1To1024 sharepointResourcesField;

		// Token: 0x040045D3 RID: 17875
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x040045D4 RID: 17876
		private DirectoryPropertyXmlValidationError validationErrorField;

		// Token: 0x040045D5 RID: 17877
		private DirectoryPropertyStringSingleLength1To40 wellKnownObjectField;

		// Token: 0x040045D6 RID: 17878
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x040045D7 RID: 17879
		private XmlAttribute[] anyAttrField;
	}
}
