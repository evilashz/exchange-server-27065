using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000187 RID: 391
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[Serializable]
	public class ServicePrincipal : DirectoryObject
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00020A43 File Offset: 0x0001EC43
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00020A4B File Offset: 0x0001EC4B
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00020A54 File Offset: 0x0001EC54
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00020A5C File Offset: 0x0001EC5C
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

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00020A65 File Offset: 0x0001EC65
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00020A6D File Offset: 0x0001EC6D
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00020A76 File Offset: 0x0001EC76
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00020A7E File Offset: 0x0001EC7E
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

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00020A87 File Offset: 0x0001EC87
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00020A8F File Offset: 0x0001EC8F
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00020A98 File Offset: 0x0001EC98
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00020AA0 File Offset: 0x0001ECA0
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

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00020AA9 File Offset: 0x0001ECA9
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00020AB1 File Offset: 0x0001ECB1
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

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00020ABA File Offset: 0x0001ECBA
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00020AC2 File Offset: 0x0001ECC2
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

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00020ACB File Offset: 0x0001ECCB
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00020AD3 File Offset: 0x0001ECD3
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00020ADC File Offset: 0x0001ECDC
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00020AE4 File Offset: 0x0001ECE4
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00020AED File Offset: 0x0001ECED
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00020AF5 File Offset: 0x0001ECF5
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

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00020AFE File Offset: 0x0001ECFE
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00020B06 File Offset: 0x0001ED06
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

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00020B0F File Offset: 0x0001ED0F
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00020B17 File Offset: 0x0001ED17
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

		// Token: 0x04000483 RID: 1155
		private DirectoryPropertyBooleanSingle accountEnabledField;

		// Token: 0x04000484 RID: 1156
		private DirectoryPropertyXmlAppAddress appAddressField;

		// Token: 0x04000485 RID: 1157
		private DirectoryPropertyGuidSingle appPrincipalIdField;

		// Token: 0x04000486 RID: 1158
		private DirectoryPropertyXmlAsymmetricKey asymmetricKeyField;

		// Token: 0x04000487 RID: 1159
		private DirectoryPropertyXmlCredential credentialField;

		// Token: 0x04000488 RID: 1160
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000489 RID: 1161
		private DirectoryPropertyXmlEncryptedSecretKey encryptedSecretKeyField;

		// Token: 0x0400048A RID: 1162
		private DirectoryPropertyBooleanSingle externalUserAccountDelegationsAllowedField;

		// Token: 0x0400048B RID: 1163
		private DirectoryPropertyXmlKeyDescription keyDescriptionField;

		// Token: 0x0400048C RID: 1164
		private DirectoryPropertyServicePrincipalName servicePrincipalNameField;

		// Token: 0x0400048D RID: 1165
		private DirectoryPropertyXmlSharedKeyReference sharedKeyReferenceField;

		// Token: 0x0400048E RID: 1166
		private DirectoryPropertyBooleanSingle trustedForDelegationField;

		// Token: 0x0400048F RID: 1167
		private XmlAttribute[] anyAttrField;
	}
}
