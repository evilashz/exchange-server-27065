using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000953 RID: 2387
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ServicePrincipal : DirectoryObject
	{
		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x0600706A RID: 28778 RVA: 0x00177423 File Offset: 0x00175623
		// (set) Token: 0x0600706B RID: 28779 RVA: 0x0017742B File Offset: 0x0017562B
		[XmlElement(Order = 0)]
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

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x0600706C RID: 28780 RVA: 0x00177434 File Offset: 0x00175634
		// (set) Token: 0x0600706D RID: 28781 RVA: 0x0017743C File Offset: 0x0017563C
		[XmlElement(Order = 1)]
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

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x0600706E RID: 28782 RVA: 0x00177445 File Offset: 0x00175645
		// (set) Token: 0x0600706F RID: 28783 RVA: 0x0017744D File Offset: 0x0017564D
		[XmlElement(Order = 2)]
		public DirectoryPropertyXmlAppAddress AppAddress
		{
			get
			{
				return this.appAddressField;
			}
			set
			{
				this.appAddressField = value;
			}
		}

		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x06007070 RID: 28784 RVA: 0x00177456 File Offset: 0x00175656
		// (set) Token: 0x06007071 RID: 28785 RVA: 0x0017745E File Offset: 0x0017565E
		[XmlElement(Order = 3)]
		public DirectoryPropertyGuidSingle AppPrincipalId
		{
			get
			{
				return this.appPrincipalIdField;
			}
			set
			{
				this.appPrincipalIdField = value;
			}
		}

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x06007072 RID: 28786 RVA: 0x00177467 File Offset: 0x00175667
		// (set) Token: 0x06007073 RID: 28787 RVA: 0x0017746F File Offset: 0x0017566F
		[XmlElement(Order = 4)]
		public DirectoryPropertyXmlAsymmetricKey AsymmetricKey
		{
			get
			{
				return this.asymmetricKeyField;
			}
			set
			{
				this.asymmetricKeyField = value;
			}
		}

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x06007074 RID: 28788 RVA: 0x00177478 File Offset: 0x00175678
		// (set) Token: 0x06007075 RID: 28789 RVA: 0x00177480 File Offset: 0x00175680
		[XmlElement(Order = 5)]
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

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x06007076 RID: 28790 RVA: 0x00177489 File Offset: 0x00175689
		// (set) Token: 0x06007077 RID: 28791 RVA: 0x00177491 File Offset: 0x00175691
		[XmlElement(Order = 6)]
		public DirectoryPropertyXmlCredential Credential
		{
			get
			{
				return this.credentialField;
			}
			set
			{
				this.credentialField = value;
			}
		}

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x06007078 RID: 28792 RVA: 0x0017749A File Offset: 0x0017569A
		// (set) Token: 0x06007079 RID: 28793 RVA: 0x001774A2 File Offset: 0x001756A2
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

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x0600707A RID: 28794 RVA: 0x001774AB File Offset: 0x001756AB
		// (set) Token: 0x0600707B RID: 28795 RVA: 0x001774B3 File Offset: 0x001756B3
		[XmlElement(Order = 8)]
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

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x0600707C RID: 28796 RVA: 0x001774BC File Offset: 0x001756BC
		// (set) Token: 0x0600707D RID: 28797 RVA: 0x001774C4 File Offset: 0x001756C4
		[XmlElement(Order = 9)]
		public DirectoryPropertyXmlEncryptedSecretKey EncryptedSecretKey
		{
			get
			{
				return this.encryptedSecretKeyField;
			}
			set
			{
				this.encryptedSecretKeyField = value;
			}
		}

		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x0600707E RID: 28798 RVA: 0x001774CD File Offset: 0x001756CD
		// (set) Token: 0x0600707F RID: 28799 RVA: 0x001774D5 File Offset: 0x001756D5
		[XmlElement(Order = 10)]
		public DirectoryPropertyBooleanSingle ExternalUserAccountDelegationsAllowed
		{
			get
			{
				return this.externalUserAccountDelegationsAllowedField;
			}
			set
			{
				this.externalUserAccountDelegationsAllowedField = value;
			}
		}

		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x06007080 RID: 28800 RVA: 0x001774DE File Offset: 0x001756DE
		// (set) Token: 0x06007081 RID: 28801 RVA: 0x001774E6 File Offset: 0x001756E6
		[XmlElement(Order = 11)]
		public DirectoryPropertyXmlKeyDescription KeyDescription
		{
			get
			{
				return this.keyDescriptionField;
			}
			set
			{
				this.keyDescriptionField = value;
			}
		}

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x06007082 RID: 28802 RVA: 0x001774EF File Offset: 0x001756EF
		// (set) Token: 0x06007083 RID: 28803 RVA: 0x001774F7 File Offset: 0x001756F7
		[XmlElement(Order = 12)]
		public DirectoryPropertyBooleanSingle MicrosoftPolicyGroup
		{
			get
			{
				return this.microsoftPolicyGroupField;
			}
			set
			{
				this.microsoftPolicyGroupField = value;
			}
		}

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x06007084 RID: 28804 RVA: 0x00177500 File Offset: 0x00175700
		// (set) Token: 0x06007085 RID: 28805 RVA: 0x00177508 File Offset: 0x00175708
		[XmlElement(Order = 13)]
		public DirectoryPropertyStringSingleLength1To256 PreferredTokenSigningKeyThumbprint
		{
			get
			{
				return this.preferredTokenSigningKeyThumbprintField;
			}
			set
			{
				this.preferredTokenSigningKeyThumbprintField = value;
			}
		}

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x06007086 RID: 28806 RVA: 0x00177511 File Offset: 0x00175711
		// (set) Token: 0x06007087 RID: 28807 RVA: 0x00177519 File Offset: 0x00175719
		[XmlElement(Order = 14)]
		public DirectoryPropertyServicePrincipalName ServicePrincipalName
		{
			get
			{
				return this.servicePrincipalNameField;
			}
			set
			{
				this.servicePrincipalNameField = value;
			}
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x06007088 RID: 28808 RVA: 0x00177522 File Offset: 0x00175722
		// (set) Token: 0x06007089 RID: 28809 RVA: 0x0017752A File Offset: 0x0017572A
		[XmlElement(Order = 15)]
		public DirectoryPropertyXmlSharedKeyReference SharedKeyReference
		{
			get
			{
				return this.sharedKeyReferenceField;
			}
			set
			{
				this.sharedKeyReferenceField = value;
			}
		}

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x0600708A RID: 28810 RVA: 0x00177533 File Offset: 0x00175733
		// (set) Token: 0x0600708B RID: 28811 RVA: 0x0017753B File Offset: 0x0017573B
		[XmlElement(Order = 16)]
		public DirectoryPropertyBooleanSingle TrustedForDelegation
		{
			get
			{
				return this.trustedForDelegationField;
			}
			set
			{
				this.trustedForDelegationField = value;
			}
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x0600708C RID: 28812 RVA: 0x00177544 File Offset: 0x00175744
		// (set) Token: 0x0600708D RID: 28813 RVA: 0x0017754C File Offset: 0x0017574C
		[XmlElement(Order = 17)]
		public DirectoryPropertyBooleanSingle UseCustomTokenSigningKey
		{
			get
			{
				return this.useCustomTokenSigningKeyField;
			}
			set
			{
				this.useCustomTokenSigningKeyField = value;
			}
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x0600708E RID: 28814 RVA: 0x00177555 File Offset: 0x00175755
		// (set) Token: 0x0600708F RID: 28815 RVA: 0x0017755D File Offset: 0x0017575D
		[XmlElement(Order = 18)]
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

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06007090 RID: 28816 RVA: 0x00177566 File Offset: 0x00175766
		// (set) Token: 0x06007091 RID: 28817 RVA: 0x0017756E File Offset: 0x0017576E
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

		// Token: 0x06007092 RID: 28818 RVA: 0x00177577 File Offset: 0x00175777
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
		}

		// Token: 0x040048EC RID: 18668
		private DirectoryPropertyBooleanSingle accountEnabledField;

		// Token: 0x040048ED RID: 18669
		private DirectoryPropertyXmlAlternativeSecurityId alternativeSecurityIdField;

		// Token: 0x040048EE RID: 18670
		private DirectoryPropertyXmlAppAddress appAddressField;

		// Token: 0x040048EF RID: 18671
		private DirectoryPropertyGuidSingle appPrincipalIdField;

		// Token: 0x040048F0 RID: 18672
		private DirectoryPropertyXmlAsymmetricKey asymmetricKeyField;

		// Token: 0x040048F1 RID: 18673
		private DirectoryPropertyReferenceUserAndServicePrincipalSingle createdOnBehalfOfField;

		// Token: 0x040048F2 RID: 18674
		private DirectoryPropertyXmlCredential credentialField;

		// Token: 0x040048F3 RID: 18675
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040048F4 RID: 18676
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040048F5 RID: 18677
		private DirectoryPropertyXmlEncryptedSecretKey encryptedSecretKeyField;

		// Token: 0x040048F6 RID: 18678
		private DirectoryPropertyBooleanSingle externalUserAccountDelegationsAllowedField;

		// Token: 0x040048F7 RID: 18679
		private DirectoryPropertyXmlKeyDescription keyDescriptionField;

		// Token: 0x040048F8 RID: 18680
		private DirectoryPropertyBooleanSingle microsoftPolicyGroupField;

		// Token: 0x040048F9 RID: 18681
		private DirectoryPropertyStringSingleLength1To256 preferredTokenSigningKeyThumbprintField;

		// Token: 0x040048FA RID: 18682
		private DirectoryPropertyServicePrincipalName servicePrincipalNameField;

		// Token: 0x040048FB RID: 18683
		private DirectoryPropertyXmlSharedKeyReference sharedKeyReferenceField;

		// Token: 0x040048FC RID: 18684
		private DirectoryPropertyBooleanSingle trustedForDelegationField;

		// Token: 0x040048FD RID: 18685
		private DirectoryPropertyBooleanSingle useCustomTokenSigningKeyField;

		// Token: 0x040048FE RID: 18686
		private DirectoryPropertyStringSingleLength1To2048 wwwHomepageField;

		// Token: 0x040048FF RID: 18687
		private XmlAttribute[] anyAttrField;
	}
}
