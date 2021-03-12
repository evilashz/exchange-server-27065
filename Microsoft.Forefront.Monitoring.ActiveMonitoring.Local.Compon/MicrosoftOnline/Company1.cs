using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200019C RID: 412
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(TypeName = "Company", Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class Company1 : DirectoryObject
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00022AA9 File Offset: 0x00020CA9
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00022AB1 File Offset: 0x00020CB1
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00022ABA File Offset: 0x00020CBA
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00022AC2 File Offset: 0x00020CC2
		public DirectoryPropertyXmlAuthorizedParty AuthorizedParty
		{
			get
			{
				return this.authorizedPartyField;
			}
			set
			{
				this.authorizedPartyField = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00022ACB File Offset: 0x00020CCB
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00022AD3 File Offset: 0x00020CD3
		public DirectoryPropertyStringLength1To256 AuthorizedServiceInstance
		{
			get
			{
				return this.authorizedServiceInstanceField;
			}
			set
			{
				this.authorizedServiceInstanceField = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00022ADC File Offset: 0x00020CDC
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00022AE4 File Offset: 0x00020CE4
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00022AED File Offset: 0x00020CED
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00022AF5 File Offset: 0x00020CF5
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00022AFE File Offset: 0x00020CFE
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x00022B06 File Offset: 0x00020D06
		public DirectoryPropertyDateTimeSingle CompanyLastDirSyncTime
		{
			get
			{
				return this.companyLastDirSyncTimeField;
			}
			set
			{
				this.companyLastDirSyncTimeField = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00022B0F File Offset: 0x00020D0F
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00022B17 File Offset: 0x00020D17
		public DirectoryPropertyXmlCompanyPartnershipSingle CompanyPartnership
		{
			get
			{
				return this.companyPartnershipField;
			}
			set
			{
				this.companyPartnershipField = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00022B20 File Offset: 0x00020D20
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x00022B28 File Offset: 0x00020D28
		public DirectoryPropertyStringLength1To256 CompanyTags
		{
			get
			{
				return this.companyTagsField;
			}
			set
			{
				this.companyTagsField = value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00022B31 File Offset: 0x00020D31
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00022B39 File Offset: 0x00020D39
		public DirectoryPropertyInt32Single ComplianceRequirements
		{
			get
			{
				return this.complianceRequirementsField;
			}
			set
			{
				this.complianceRequirementsField = value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00022B42 File Offset: 0x00020D42
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00022B4A File Offset: 0x00020D4A
		public DirectoryPropertyXmlContextMoveStatusSingle ContextMoveFrom
		{
			get
			{
				return this.contextMoveFromField;
			}
			set
			{
				this.contextMoveFromField = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00022B53 File Offset: 0x00020D53
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00022B5B File Offset: 0x00020D5B
		public DirectoryPropertyBinarySingleLength1To102400 ContextMoveSyncCookie
		{
			get
			{
				return this.contextMoveSyncCookieField;
			}
			set
			{
				this.contextMoveSyncCookieField = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00022B64 File Offset: 0x00020D64
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00022B6C File Offset: 0x00020D6C
		public DirectoryPropertyXmlContextMoveStatusSingle ContextMoveTo
		{
			get
			{
				return this.contextMoveToField;
			}
			set
			{
				this.contextMoveToField = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00022B75 File Offset: 0x00020D75
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00022B7D File Offset: 0x00020D7D
		public DirectoryPropertyXmlContextMoveWatermarksSingle ContextMoveWatermarks
		{
			get
			{
				return this.contextMoveWatermarksField;
			}
			set
			{
				this.contextMoveWatermarksField = value;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00022B86 File Offset: 0x00020D86
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00022B8E File Offset: 0x00020D8E
		public DirectoryPropertyDateTimeSingle CreationTime
		{
			get
			{
				return this.creationTimeField;
			}
			set
			{
				this.creationTimeField = value;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00022B97 File Offset: 0x00020D97
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00022B9F File Offset: 0x00020D9F
		public DirectoryPropertyXmlSearchForUsers CustomView
		{
			get
			{
				return this.customViewField;
			}
			set
			{
				this.customViewField = value;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00022BA8 File Offset: 0x00020DA8
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x00022BB0 File Offset: 0x00020DB0
		public DirectoryPropertyStringSingleLength1To128 DefaultGeography
		{
			get
			{
				return this.defaultGeographyField;
			}
			set
			{
				this.defaultGeographyField = value;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00022BB9 File Offset: 0x00020DB9
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00022BC1 File Offset: 0x00020DC1
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00022BCA File Offset: 0x00020DCA
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x00022BD2 File Offset: 0x00020DD2
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00022BDB File Offset: 0x00020DDB
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00022BE3 File Offset: 0x00020DE3
		public DirectoryPropertyXmlDirSyncStatus DirSyncStatusAck
		{
			get
			{
				return this.dirSyncStatusAckField;
			}
			set
			{
				this.dirSyncStatusAckField = value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00022BEC File Offset: 0x00020DEC
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00022BF4 File Offset: 0x00020DF4
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00022BFD File Offset: 0x00020DFD
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00022C05 File Offset: 0x00020E05
		public DirectoryPropertyGuid FeatureDescriptorIds
		{
			get
			{
				return this.featureDescriptorIdsField;
			}
			set
			{
				this.featureDescriptorIdsField = value;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00022C0E File Offset: 0x00020E0E
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00022C16 File Offset: 0x00020E16
		public DirectoryPropertyInt32SingleMin0 FirstLoginObjectCount
		{
			get
			{
				return this.firstLoginObjectCountField;
			}
			set
			{
				this.firstLoginObjectCountField = value;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00022C1F File Offset: 0x00020E1F
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00022C27 File Offset: 0x00020E27
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00022C30 File Offset: 0x00020E30
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00022C38 File Offset: 0x00020E38
		public DirectoryPropertyInt32SingleMin0 LiveAuthorizationScope
		{
			get
			{
				return this.liveAuthorizationScopeField;
			}
			set
			{
				this.liveAuthorizationScopeField = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00022C41 File Offset: 0x00020E41
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00022C49 File Offset: 0x00020E49
		public DirectoryPropertyStringLength1To256 MarketingNotificationEmails
		{
			get
			{
				return this.marketingNotificationEmailsField;
			}
			set
			{
				this.marketingNotificationEmailsField = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00022C52 File Offset: 0x00020E52
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x00022C5A File Offset: 0x00020E5A
		public DirectoryPropertyXmlMigrationDetail MigrationDetail
		{
			get
			{
				return this.migrationDetailField;
			}
			set
			{
				this.migrationDetailField = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00022C63 File Offset: 0x00020E63
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x00022C6B File Offset: 0x00020E6B
		public DirectoryPropertyInt32SingleMin0 MigrationState
		{
			get
			{
				return this.migrationStateField;
			}
			set
			{
				this.migrationStateField = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00022C74 File Offset: 0x00020E74
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x00022C7C File Offset: 0x00020E7C
		public DirectoryPropertyGuidSingle OcpMessageId
		{
			get
			{
				return this.ocpMessageIdField;
			}
			set
			{
				this.ocpMessageIdField = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00022C85 File Offset: 0x00020E85
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x00022C8D File Offset: 0x00020E8D
		public DirectoryPropertyGuid OcpOrganizationId
		{
			get
			{
				return this.ocpOrganizationIdField;
			}
			set
			{
				this.ocpOrganizationIdField = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00022C96 File Offset: 0x00020E96
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x00022C9E File Offset: 0x00020E9E
		public DirectoryPropertyXmlPropagationTask OrgIdPropagationTask
		{
			get
			{
				return this.orgIdPropagationTaskField;
			}
			set
			{
				this.orgIdPropagationTaskField = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00022CA7 File Offset: 0x00020EA7
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00022CAF File Offset: 0x00020EAF
		public DirectoryPropertyBinarySingleLength1To28 OwnerIdentifier
		{
			get
			{
				return this.ownerIdentifierField;
			}
			set
			{
				this.ownerIdentifierField = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00022CB8 File Offset: 0x00020EB8
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00022CC0 File Offset: 0x00020EC0
		public DirectoryPropertyStringSingleLength1To256 PartnerBillingSupportEmail
		{
			get
			{
				return this.partnerBillingSupportEmailField;
			}
			set
			{
				this.partnerBillingSupportEmailField = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00022CC9 File Offset: 0x00020EC9
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00022CD1 File Offset: 0x00020ED1
		public DirectoryPropertyStringSingleLength1To1123 PartnerCommerceUrl
		{
			get
			{
				return this.partnerCommerceUrlField;
			}
			set
			{
				this.partnerCommerceUrlField = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00022CDA File Offset: 0x00020EDA
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00022CE2 File Offset: 0x00020EE2
		public DirectoryPropertyStringSingleLength1To128 PartnerCompanyName
		{
			get
			{
				return this.partnerCompanyNameField;
			}
			set
			{
				this.partnerCompanyNameField = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00022CEB File Offset: 0x00020EEB
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00022CF3 File Offset: 0x00020EF3
		public DirectoryPropertyStringSingleLength1To1123 PartnerHelpUrl
		{
			get
			{
				return this.partnerHelpUrlField;
			}
			set
			{
				this.partnerHelpUrlField = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00022CFC File Offset: 0x00020EFC
		// (set) Token: 0x06000D25 RID: 3365 RVA: 0x00022D04 File Offset: 0x00020F04
		public DirectoryPropertyStringLength1To256 PartnerServiceType
		{
			get
			{
				return this.partnerServiceTypeField;
			}
			set
			{
				this.partnerServiceTypeField = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00022D0D File Offset: 0x00020F0D
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x00022D15 File Offset: 0x00020F15
		public DirectoryPropertyStringLength1To1123 PartnerSupportEmail
		{
			get
			{
				return this.partnerSupportEmailField;
			}
			set
			{
				this.partnerSupportEmailField = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00022D1E File Offset: 0x00020F1E
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00022D26 File Offset: 0x00020F26
		public DirectoryPropertyStringLength1To64 PartnerSupportTelephone
		{
			get
			{
				return this.partnerSupportTelephoneField;
			}
			set
			{
				this.partnerSupportTelephoneField = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00022D2F File Offset: 0x00020F2F
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00022D37 File Offset: 0x00020F37
		public DirectoryPropertyStringSingleLength1To1123 PartnerSupportUrl
		{
			get
			{
				return this.partnerSupportUrlField;
			}
			set
			{
				this.partnerSupportUrlField = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00022D40 File Offset: 0x00020F40
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00022D48 File Offset: 0x00020F48
		public DirectoryPropertyXmlAnySingle PortalSetting
		{
			get
			{
				return this.portalSettingField;
			}
			set
			{
				this.portalSettingField = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00022D51 File Offset: 0x00020F51
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x00022D59 File Offset: 0x00020F59
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00022D62 File Offset: 0x00020F62
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00022D6A File Offset: 0x00020F6A
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00022D73 File Offset: 0x00020F73
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x00022D7B File Offset: 0x00020F7B
		public DirectoryPropertyXmlPropagationTask PropagationTask
		{
			get
			{
				return this.propagationTaskField;
			}
			set
			{
				this.propagationTaskField = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00022D84 File Offset: 0x00020F84
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00022D8C File Offset: 0x00020F8C
		public DirectoryPropertyXmlProvisionedPlan ProvisionedPlan
		{
			get
			{
				return this.provisionedPlanField;
			}
			set
			{
				this.provisionedPlanField = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00022D95 File Offset: 0x00020F95
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00022D9D File Offset: 0x00020F9D
		public DirectoryPropertyStringLength1To256 ProvisionedServiceInstance
		{
			get
			{
				return this.provisionedServiceInstanceField;
			}
			set
			{
				this.provisionedServiceInstanceField = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00022DA6 File Offset: 0x00020FA6
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00022DAE File Offset: 0x00020FAE
		public DirectoryPropertyInt32SingleMin0 QuotaAmount
		{
			get
			{
				return this.quotaAmountField;
			}
			set
			{
				this.quotaAmountField = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00022DB7 File Offset: 0x00020FB7
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00022DBF File Offset: 0x00020FBF
		public DirectoryPropertyStringSingleLength1To16 ReplicationScope
		{
			get
			{
				return this.replicationScopeField;
			}
			set
			{
				this.replicationScopeField = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00022DC8 File Offset: 0x00020FC8
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00022DD0 File Offset: 0x00020FD0
		public DirectoryPropertyXmlRightsManagementTenantConfigurationSingle RightsManagementTenantConfiguration
		{
			get
			{
				return this.rightsManagementTenantConfigurationField;
			}
			set
			{
				this.rightsManagementTenantConfigurationField = value;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00022DD9 File Offset: 0x00020FD9
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00022DE1 File Offset: 0x00020FE1
		public DirectoryPropertyXmlRightsManagementTenantKey RightsManagementTenantKey
		{
			get
			{
				return this.rightsManagementTenantKeyField;
			}
			set
			{
				this.rightsManagementTenantKeyField = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00022DEA File Offset: 0x00020FEA
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00022DF2 File Offset: 0x00020FF2
		public DirectoryPropertyBooleanSingle SelfServePasswordResetEnabled
		{
			get
			{
				return this.selfServePasswordResetEnabledField;
			}
			set
			{
				this.selfServePasswordResetEnabledField = value;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00022DFB File Offset: 0x00020FFB
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00022E03 File Offset: 0x00021003
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

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00022E0C File Offset: 0x0002100C
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00022E14 File Offset: 0x00021014
		public DirectoryPropertyInt32Single SliceId
		{
			get
			{
				return this.sliceIdField;
			}
			set
			{
				this.sliceIdField = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00022E1D File Offset: 0x0002101D
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00022E25 File Offset: 0x00021025
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00022E2E File Offset: 0x0002102E
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00022E36 File Offset: 0x00021036
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00022E3F File Offset: 0x0002103F
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00022E47 File Offset: 0x00021047
		public DirectoryPropertyXmlStrongAuthenticationPolicy StrongAuthenticationPolicy
		{
			get
			{
				return this.strongAuthenticationPolicyField;
			}
			set
			{
				this.strongAuthenticationPolicyField = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00022E50 File Offset: 0x00021050
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00022E58 File Offset: 0x00021058
		public DirectoryPropertyStringLength1To256 TechnicalNotificationMail
		{
			get
			{
				return this.technicalNotificationMailField;
			}
			set
			{
				this.technicalNotificationMailField = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00022E61 File Offset: 0x00021061
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x00022E69 File Offset: 0x00021069
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00022E72 File Offset: 0x00021072
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00022E7A File Offset: 0x0002107A
		public DirectoryPropertyInt32SingleMin0Max3 TenantType
		{
			get
			{
				return this.tenantTypeField;
			}
			set
			{
				this.tenantTypeField = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00022E83 File Offset: 0x00021083
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00022E8B File Offset: 0x0002108B
		public DirectoryPropertyGuidSingle ThrottlePolicyId
		{
			get
			{
				return this.throttlePolicyIdField;
			}
			set
			{
				this.throttlePolicyIdField = value;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00022E94 File Offset: 0x00021094
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x00022E9C File Offset: 0x0002109C
		public DirectoryPropertyXmlCompanyUnverifiedDomain UnverifiedDomain
		{
			get
			{
				return this.unverifiedDomainField;
			}
			set
			{
				this.unverifiedDomainField = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00022EA5 File Offset: 0x000210A5
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00022EAD File Offset: 0x000210AD
		public DirectoryPropertyXmlCompanyVerifiedDomain VerifiedDomain
		{
			get
			{
				return this.verifiedDomainField;
			}
			set
			{
				this.verifiedDomainField = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00022EB6 File Offset: 0x000210B6
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00022EBE File Offset: 0x000210BE
		public DirectoryPropertyXmlDirSyncStatus DirSyncStatus
		{
			get
			{
				return this.dirSyncStatusField;
			}
			set
			{
				this.dirSyncStatusField = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00022EC7 File Offset: 0x000210C7
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x00022ECF File Offset: 0x000210CF
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

		// Token: 0x04000661 RID: 1633
		private DirectoryPropertyXmlAssignedPlan assignedPlanField;

		// Token: 0x04000662 RID: 1634
		private DirectoryPropertyXmlAuthorizedParty authorizedPartyField;

		// Token: 0x04000663 RID: 1635
		private DirectoryPropertyStringLength1To256 authorizedServiceInstanceField;

		// Token: 0x04000664 RID: 1636
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x04000665 RID: 1637
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x04000666 RID: 1638
		private DirectoryPropertyDateTimeSingle companyLastDirSyncTimeField;

		// Token: 0x04000667 RID: 1639
		private DirectoryPropertyXmlCompanyPartnershipSingle companyPartnershipField;

		// Token: 0x04000668 RID: 1640
		private DirectoryPropertyStringLength1To256 companyTagsField;

		// Token: 0x04000669 RID: 1641
		private DirectoryPropertyInt32Single complianceRequirementsField;

		// Token: 0x0400066A RID: 1642
		private DirectoryPropertyXmlContextMoveStatusSingle contextMoveFromField;

		// Token: 0x0400066B RID: 1643
		private DirectoryPropertyBinarySingleLength1To102400 contextMoveSyncCookieField;

		// Token: 0x0400066C RID: 1644
		private DirectoryPropertyXmlContextMoveStatusSingle contextMoveToField;

		// Token: 0x0400066D RID: 1645
		private DirectoryPropertyXmlContextMoveWatermarksSingle contextMoveWatermarksField;

		// Token: 0x0400066E RID: 1646
		private DirectoryPropertyDateTimeSingle creationTimeField;

		// Token: 0x0400066F RID: 1647
		private DirectoryPropertyXmlSearchForUsers customViewField;

		// Token: 0x04000670 RID: 1648
		private DirectoryPropertyStringSingleLength1To128 defaultGeographyField;

		// Token: 0x04000671 RID: 1649
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000672 RID: 1650
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x04000673 RID: 1651
		private DirectoryPropertyXmlDirSyncStatus dirSyncStatusAckField;

		// Token: 0x04000674 RID: 1652
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000675 RID: 1653
		private DirectoryPropertyGuid featureDescriptorIdsField;

		// Token: 0x04000676 RID: 1654
		private DirectoryPropertyInt32SingleMin0 firstLoginObjectCountField;

		// Token: 0x04000677 RID: 1655
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x04000678 RID: 1656
		private DirectoryPropertyInt32SingleMin0 liveAuthorizationScopeField;

		// Token: 0x04000679 RID: 1657
		private DirectoryPropertyStringLength1To256 marketingNotificationEmailsField;

		// Token: 0x0400067A RID: 1658
		private DirectoryPropertyXmlMigrationDetail migrationDetailField;

		// Token: 0x0400067B RID: 1659
		private DirectoryPropertyInt32SingleMin0 migrationStateField;

		// Token: 0x0400067C RID: 1660
		private DirectoryPropertyGuidSingle ocpMessageIdField;

		// Token: 0x0400067D RID: 1661
		private DirectoryPropertyGuid ocpOrganizationIdField;

		// Token: 0x0400067E RID: 1662
		private DirectoryPropertyXmlPropagationTask orgIdPropagationTaskField;

		// Token: 0x0400067F RID: 1663
		private DirectoryPropertyBinarySingleLength1To28 ownerIdentifierField;

		// Token: 0x04000680 RID: 1664
		private DirectoryPropertyStringSingleLength1To256 partnerBillingSupportEmailField;

		// Token: 0x04000681 RID: 1665
		private DirectoryPropertyStringSingleLength1To1123 partnerCommerceUrlField;

		// Token: 0x04000682 RID: 1666
		private DirectoryPropertyStringSingleLength1To128 partnerCompanyNameField;

		// Token: 0x04000683 RID: 1667
		private DirectoryPropertyStringSingleLength1To1123 partnerHelpUrlField;

		// Token: 0x04000684 RID: 1668
		private DirectoryPropertyStringLength1To256 partnerServiceTypeField;

		// Token: 0x04000685 RID: 1669
		private DirectoryPropertyStringLength1To1123 partnerSupportEmailField;

		// Token: 0x04000686 RID: 1670
		private DirectoryPropertyStringLength1To64 partnerSupportTelephoneField;

		// Token: 0x04000687 RID: 1671
		private DirectoryPropertyStringSingleLength1To1123 partnerSupportUrlField;

		// Token: 0x04000688 RID: 1672
		private DirectoryPropertyXmlAnySingle portalSettingField;

		// Token: 0x04000689 RID: 1673
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x0400068A RID: 1674
		private DirectoryPropertyStringSingleLength1To64 preferredLanguageField;

		// Token: 0x0400068B RID: 1675
		private DirectoryPropertyXmlPropagationTask propagationTaskField;

		// Token: 0x0400068C RID: 1676
		private DirectoryPropertyXmlProvisionedPlan provisionedPlanField;

		// Token: 0x0400068D RID: 1677
		private DirectoryPropertyStringLength1To256 provisionedServiceInstanceField;

		// Token: 0x0400068E RID: 1678
		private DirectoryPropertyInt32SingleMin0 quotaAmountField;

		// Token: 0x0400068F RID: 1679
		private DirectoryPropertyStringSingleLength1To16 replicationScopeField;

		// Token: 0x04000690 RID: 1680
		private DirectoryPropertyXmlRightsManagementTenantConfigurationSingle rightsManagementTenantConfigurationField;

		// Token: 0x04000691 RID: 1681
		private DirectoryPropertyXmlRightsManagementTenantKey rightsManagementTenantKeyField;

		// Token: 0x04000692 RID: 1682
		private DirectoryPropertyBooleanSingle selfServePasswordResetEnabledField;

		// Token: 0x04000693 RID: 1683
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04000694 RID: 1684
		private DirectoryPropertyInt32Single sliceIdField;

		// Token: 0x04000695 RID: 1685
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x04000696 RID: 1686
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x04000697 RID: 1687
		private DirectoryPropertyXmlStrongAuthenticationPolicy strongAuthenticationPolicyField;

		// Token: 0x04000698 RID: 1688
		private DirectoryPropertyStringLength1To256 technicalNotificationMailField;

		// Token: 0x04000699 RID: 1689
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x0400069A RID: 1690
		private DirectoryPropertyInt32SingleMin0Max3 tenantTypeField;

		// Token: 0x0400069B RID: 1691
		private DirectoryPropertyGuidSingle throttlePolicyIdField;

		// Token: 0x0400069C RID: 1692
		private DirectoryPropertyXmlCompanyUnverifiedDomain unverifiedDomainField;

		// Token: 0x0400069D RID: 1693
		private DirectoryPropertyXmlCompanyVerifiedDomain verifiedDomainField;

		// Token: 0x0400069E RID: 1694
		private DirectoryPropertyXmlDirSyncStatus dirSyncStatusField;

		// Token: 0x0400069F RID: 1695
		private XmlAttribute[] anyAttrField;
	}
}
