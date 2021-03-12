using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000848 RID: 2120
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class Company : DirectoryObject
	{
		// Token: 0x0600690A RID: 26890 RVA: 0x0017173C File Offset: 0x0016F93C
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyXmlAssignedPlan>(SyncCompanySchema.AssignedPlan, ref this.assignedPlanField);
			processor.Process<DirectoryPropertyStringSingleLength1To3>(SyncCompanySchema.C, ref this.cField);
			processor.Process<DirectoryPropertyXmlCompanyPartnershipSingle>(SyncCompanySchema.CompanyPartnership, ref this.companyPartnershipField);
			processor.Process<DirectoryPropertyStringLength1To256>(SyncCompanySchema.CompanyTags, ref this.companyTagsField);
			processor.Process<DirectoryPropertyStringSingleLength1To1024>(SyncCompanySchema.Description, ref this.descriptionField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncCompanySchema.DisplayName, ref this.displayNameField);
			processor.Process<DirectoryPropertyBooleanSingle>(SyncCompanySchema.IsDirSyncRunning, ref this.dirSyncEnabledField);
			processor.Process<DirectoryPropertyXmlDirSyncStatus>(SyncCompanySchema.DirSyncStatus, ref this.dirSyncStatusField);
			processor.Process<DirectoryPropertyXmlDirSyncStatus>(SyncCompanySchema.DirSyncStatusAck, ref this.dirSyncStatusAckField);
			processor.Process<DirectoryPropertyXmlServiceInfo>(SyncCompanySchema.ServiceInfo, ref this.serviceInfoField);
			processor.Process<DirectoryPropertyInt32SingleMin0Max4>(SyncCompanySchema.TenantType, ref this.tenantTypeField);
			processor.Process<DirectoryPropertyInt32SingleMin0>(SyncCompanySchema.QuotaAmount, ref this.quotaAmountField);
			processor.Process<DirectoryPropertyXmlCompanyVerifiedDomain>(SyncCompanySchema.VerifiedDomain, ref this.verifiedDomainField);
			processor.Process<DirectoryPropertyXmlRightsManagementTenantKey>(SyncCompanySchema.RightsManagementTenantKey, ref this.rightsManagementTenantKeyField);
			processor.Process<DirectoryPropertyXmlRightsManagementTenantConfigurationSingle>(SyncCompanySchema.RightsManagementTenantConfiguration, ref this.rightsManagementTenantConfigurationField);
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x0600690B RID: 26891 RVA: 0x00171848 File Offset: 0x0016FA48
		// (set) Token: 0x0600690C RID: 26892 RVA: 0x00171850 File Offset: 0x0016FA50
		[XmlElement(Order = 0)]
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

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x0600690D RID: 26893 RVA: 0x00171859 File Offset: 0x0016FA59
		// (set) Token: 0x0600690E RID: 26894 RVA: 0x00171861 File Offset: 0x0016FA61
		[XmlElement(Order = 1)]
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

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x0600690F RID: 26895 RVA: 0x0017186A File Offset: 0x0016FA6A
		// (set) Token: 0x06006910 RID: 26896 RVA: 0x00171872 File Offset: 0x0016FA72
		[XmlElement(Order = 2)]
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

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x06006911 RID: 26897 RVA: 0x0017187B File Offset: 0x0016FA7B
		// (set) Token: 0x06006912 RID: 26898 RVA: 0x00171883 File Offset: 0x0016FA83
		[XmlElement(Order = 3)]
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

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x06006913 RID: 26899 RVA: 0x0017188C File Offset: 0x0016FA8C
		// (set) Token: 0x06006914 RID: 26900 RVA: 0x00171894 File Offset: 0x0016FA94
		[XmlElement(Order = 4)]
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

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x06006915 RID: 26901 RVA: 0x0017189D File Offset: 0x0016FA9D
		// (set) Token: 0x06006916 RID: 26902 RVA: 0x001718A5 File Offset: 0x0016FAA5
		[XmlElement(Order = 5)]
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

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x06006917 RID: 26903 RVA: 0x001718AE File Offset: 0x0016FAAE
		// (set) Token: 0x06006918 RID: 26904 RVA: 0x001718B6 File Offset: 0x0016FAB6
		[XmlElement(Order = 6)]
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

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x06006919 RID: 26905 RVA: 0x001718BF File Offset: 0x0016FABF
		// (set) Token: 0x0600691A RID: 26906 RVA: 0x001718C7 File Offset: 0x0016FAC7
		[XmlElement(Order = 7)]
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

		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x0600691B RID: 26907 RVA: 0x001718D0 File Offset: 0x0016FAD0
		// (set) Token: 0x0600691C RID: 26908 RVA: 0x001718D8 File Offset: 0x0016FAD8
		[XmlElement(Order = 8)]
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

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x0600691D RID: 26909 RVA: 0x001718E1 File Offset: 0x0016FAE1
		// (set) Token: 0x0600691E RID: 26910 RVA: 0x001718E9 File Offset: 0x0016FAE9
		[XmlElement(Order = 9)]
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

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x0600691F RID: 26911 RVA: 0x001718F2 File Offset: 0x0016FAF2
		// (set) Token: 0x06006920 RID: 26912 RVA: 0x001718FA File Offset: 0x0016FAFA
		[XmlElement(Order = 10)]
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

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x06006921 RID: 26913 RVA: 0x00171903 File Offset: 0x0016FB03
		// (set) Token: 0x06006922 RID: 26914 RVA: 0x0017190B File Offset: 0x0016FB0B
		[XmlElement(Order = 11)]
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

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x06006923 RID: 26915 RVA: 0x00171914 File Offset: 0x0016FB14
		// (set) Token: 0x06006924 RID: 26916 RVA: 0x0017191C File Offset: 0x0016FB1C
		[XmlElement(Order = 12)]
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

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x06006925 RID: 26917 RVA: 0x00171925 File Offset: 0x0016FB25
		// (set) Token: 0x06006926 RID: 26918 RVA: 0x0017192D File Offset: 0x0016FB2D
		[XmlElement(Order = 13)]
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

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x06006927 RID: 26919 RVA: 0x00171936 File Offset: 0x0016FB36
		// (set) Token: 0x06006928 RID: 26920 RVA: 0x0017193E File Offset: 0x0016FB3E
		[XmlElement(Order = 14)]
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

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x06006929 RID: 26921 RVA: 0x00171947 File Offset: 0x0016FB47
		// (set) Token: 0x0600692A RID: 26922 RVA: 0x0017194F File Offset: 0x0016FB4F
		[XmlElement(Order = 15)]
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

		// Token: 0x1700253B RID: 9531
		// (get) Token: 0x0600692B RID: 26923 RVA: 0x00171958 File Offset: 0x0016FB58
		// (set) Token: 0x0600692C RID: 26924 RVA: 0x00171960 File Offset: 0x0016FB60
		[XmlElement(Order = 16)]
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

		// Token: 0x1700253C RID: 9532
		// (get) Token: 0x0600692D RID: 26925 RVA: 0x00171969 File Offset: 0x0016FB69
		// (set) Token: 0x0600692E RID: 26926 RVA: 0x00171971 File Offset: 0x0016FB71
		[XmlElement(Order = 17)]
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

		// Token: 0x1700253D RID: 9533
		// (get) Token: 0x0600692F RID: 26927 RVA: 0x0017197A File Offset: 0x0016FB7A
		// (set) Token: 0x06006930 RID: 26928 RVA: 0x00171982 File Offset: 0x0016FB82
		[XmlElement(Order = 18)]
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

		// Token: 0x1700253E RID: 9534
		// (get) Token: 0x06006931 RID: 26929 RVA: 0x0017198B File Offset: 0x0016FB8B
		// (set) Token: 0x06006932 RID: 26930 RVA: 0x00171993 File Offset: 0x0016FB93
		[XmlElement(Order = 19)]
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

		// Token: 0x1700253F RID: 9535
		// (get) Token: 0x06006933 RID: 26931 RVA: 0x0017199C File Offset: 0x0016FB9C
		// (set) Token: 0x06006934 RID: 26932 RVA: 0x001719A4 File Offset: 0x0016FBA4
		[XmlElement(Order = 20)]
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

		// Token: 0x17002540 RID: 9536
		// (get) Token: 0x06006935 RID: 26933 RVA: 0x001719AD File Offset: 0x0016FBAD
		// (set) Token: 0x06006936 RID: 26934 RVA: 0x001719B5 File Offset: 0x0016FBB5
		[XmlElement(Order = 21)]
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

		// Token: 0x17002541 RID: 9537
		// (get) Token: 0x06006937 RID: 26935 RVA: 0x001719BE File Offset: 0x0016FBBE
		// (set) Token: 0x06006938 RID: 26936 RVA: 0x001719C6 File Offset: 0x0016FBC6
		[XmlElement(Order = 22)]
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

		// Token: 0x17002542 RID: 9538
		// (get) Token: 0x06006939 RID: 26937 RVA: 0x001719CF File Offset: 0x0016FBCF
		// (set) Token: 0x0600693A RID: 26938 RVA: 0x001719D7 File Offset: 0x0016FBD7
		[XmlElement(Order = 23)]
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

		// Token: 0x17002543 RID: 9539
		// (get) Token: 0x0600693B RID: 26939 RVA: 0x001719E0 File Offset: 0x0016FBE0
		// (set) Token: 0x0600693C RID: 26940 RVA: 0x001719E8 File Offset: 0x0016FBE8
		[XmlElement(Order = 24)]
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

		// Token: 0x17002544 RID: 9540
		// (get) Token: 0x0600693D RID: 26941 RVA: 0x001719F1 File Offset: 0x0016FBF1
		// (set) Token: 0x0600693E RID: 26942 RVA: 0x001719F9 File Offset: 0x0016FBF9
		[XmlElement(Order = 25)]
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

		// Token: 0x17002545 RID: 9541
		// (get) Token: 0x0600693F RID: 26943 RVA: 0x00171A02 File Offset: 0x0016FC02
		// (set) Token: 0x06006940 RID: 26944 RVA: 0x00171A0A File Offset: 0x0016FC0A
		[XmlElement(Order = 26)]
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

		// Token: 0x17002546 RID: 9542
		// (get) Token: 0x06006941 RID: 26945 RVA: 0x00171A13 File Offset: 0x0016FC13
		// (set) Token: 0x06006942 RID: 26946 RVA: 0x00171A1B File Offset: 0x0016FC1B
		[XmlElement(Order = 27)]
		public DirectoryPropertyXmlTakeoverAction TakeoverAction
		{
			get
			{
				return this.takeoverActionField;
			}
			set
			{
				this.takeoverActionField = value;
			}
		}

		// Token: 0x17002547 RID: 9543
		// (get) Token: 0x06006943 RID: 26947 RVA: 0x00171A24 File Offset: 0x0016FC24
		// (set) Token: 0x06006944 RID: 26948 RVA: 0x00171A2C File Offset: 0x0016FC2C
		[XmlElement(Order = 28)]
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

		// Token: 0x17002548 RID: 9544
		// (get) Token: 0x06006945 RID: 26949 RVA: 0x00171A35 File Offset: 0x0016FC35
		// (set) Token: 0x06006946 RID: 26950 RVA: 0x00171A3D File Offset: 0x0016FC3D
		[XmlElement(Order = 29)]
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

		// Token: 0x17002549 RID: 9545
		// (get) Token: 0x06006947 RID: 26951 RVA: 0x00171A46 File Offset: 0x0016FC46
		// (set) Token: 0x06006948 RID: 26952 RVA: 0x00171A4E File Offset: 0x0016FC4E
		[XmlElement(Order = 30)]
		public DirectoryPropertyInt32SingleMin0Max4 TenantType
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

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06006949 RID: 26953 RVA: 0x00171A57 File Offset: 0x0016FC57
		// (set) Token: 0x0600694A RID: 26954 RVA: 0x00171A5F File Offset: 0x0016FC5F
		[XmlElement(Order = 31)]
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

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x0600694B RID: 26955 RVA: 0x00171A68 File Offset: 0x0016FC68
		// (set) Token: 0x0600694C RID: 26956 RVA: 0x00171A70 File Offset: 0x0016FC70
		[XmlElement(Order = 32)]
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

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x0600694D RID: 26957 RVA: 0x00171A79 File Offset: 0x0016FC79
		// (set) Token: 0x0600694E RID: 26958 RVA: 0x00171A81 File Offset: 0x0016FC81
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

		// Token: 0x040044F0 RID: 17648
		private DirectoryPropertyXmlAssignedPlan assignedPlanField;

		// Token: 0x040044F1 RID: 17649
		private DirectoryPropertyXmlAuthorizedParty authorizedPartyField;

		// Token: 0x040044F2 RID: 17650
		private DirectoryPropertyStringSingleLength1To3 cField;

		// Token: 0x040044F3 RID: 17651
		private DirectoryPropertyStringSingleLength1To128 coField;

		// Token: 0x040044F4 RID: 17652
		private DirectoryPropertyXmlCompanyPartnershipSingle companyPartnershipField;

		// Token: 0x040044F5 RID: 17653
		private DirectoryPropertyStringLength1To256 companyTagsField;

		// Token: 0x040044F6 RID: 17654
		private DirectoryPropertyInt32Single complianceRequirementsField;

		// Token: 0x040044F7 RID: 17655
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040044F8 RID: 17656
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x040044F9 RID: 17657
		private DirectoryPropertyXmlDirSyncStatus dirSyncStatusAckField;

		// Token: 0x040044FA RID: 17658
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040044FB RID: 17659
		private DirectoryPropertyStringSingleLength1To128 lField;

		// Token: 0x040044FC RID: 17660
		private DirectoryPropertyStringSingleLength1To1123 partnerCommerceUrlField;

		// Token: 0x040044FD RID: 17661
		private DirectoryPropertyStringSingleLength1To128 partnerCompanyNameField;

		// Token: 0x040044FE RID: 17662
		private DirectoryPropertyStringSingleLength1To1123 partnerHelpUrlField;

		// Token: 0x040044FF RID: 17663
		private DirectoryPropertyStringLength1To256 partnerServiceTypeField;

		// Token: 0x04004500 RID: 17664
		private DirectoryPropertyStringLength1To1123 partnerSupportEmailField;

		// Token: 0x04004501 RID: 17665
		private DirectoryPropertyStringLength1To64 partnerSupportTelephoneField;

		// Token: 0x04004502 RID: 17666
		private DirectoryPropertyStringSingleLength1To1123 partnerSupportUrlField;

		// Token: 0x04004503 RID: 17667
		private DirectoryPropertyStringSingleLength1To40 postalCodeField;

		// Token: 0x04004504 RID: 17668
		private DirectoryPropertyStringSingleLength1To64 preferredLanguageField;

		// Token: 0x04004505 RID: 17669
		private DirectoryPropertyInt32SingleMin0 quotaAmountField;

		// Token: 0x04004506 RID: 17670
		private DirectoryPropertyXmlRightsManagementTenantConfigurationSingle rightsManagementTenantConfigurationField;

		// Token: 0x04004507 RID: 17671
		private DirectoryPropertyXmlRightsManagementTenantKey rightsManagementTenantKeyField;

		// Token: 0x04004508 RID: 17672
		private DirectoryPropertyXmlServiceInfo serviceInfoField;

		// Token: 0x04004509 RID: 17673
		private DirectoryPropertyStringSingleLength1To128 stField;

		// Token: 0x0400450A RID: 17674
		private DirectoryPropertyStringSingleLength1To1024 streetField;

		// Token: 0x0400450B RID: 17675
		private DirectoryPropertyXmlTakeoverAction takeoverActionField;

		// Token: 0x0400450C RID: 17676
		private DirectoryPropertyStringLength1To256 technicalNotificationMailField;

		// Token: 0x0400450D RID: 17677
		private DirectoryPropertyStringSingleLength1To64 telephoneNumberField;

		// Token: 0x0400450E RID: 17678
		private DirectoryPropertyInt32SingleMin0Max4 tenantTypeField;

		// Token: 0x0400450F RID: 17679
		private DirectoryPropertyXmlCompanyVerifiedDomain verifiedDomainField;

		// Token: 0x04004510 RID: 17680
		private DirectoryPropertyXmlDirSyncStatus dirSyncStatusField;

		// Token: 0x04004511 RID: 17681
		private XmlAttribute[] anyAttrField;
	}
}
