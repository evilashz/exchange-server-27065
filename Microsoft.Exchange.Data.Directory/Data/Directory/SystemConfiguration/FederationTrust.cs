using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200047B RID: 1147
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class FederationTrust : ADConfigurationObject
	{
		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000CEBE7 File Offset: 0x000CCDE7
		// (set) Token: 0x06003356 RID: 13142 RVA: 0x000CEBF9 File Offset: 0x000CCDF9
		public string ApplicationIdentifier
		{
			get
			{
				return this[FederationTrustSchema.ApplicationIdentifier] as string;
			}
			internal set
			{
				this[FederationTrustSchema.ApplicationIdentifier] = value;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000CEC07 File Offset: 0x000CCE07
		// (set) Token: 0x06003358 RID: 13144 RVA: 0x000CEC19 File Offset: 0x000CCE19
		public Uri ApplicationUri
		{
			get
			{
				return this[FederationTrustSchema.ApplicationUri] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.ApplicationUri] = value;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003359 RID: 13145 RVA: 0x000CEC27 File Offset: 0x000CCE27
		// (set) Token: 0x0600335A RID: 13146 RVA: 0x000CEC39 File Offset: 0x000CCE39
		public X509Certificate2 OrgCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgCertificate] as X509Certificate2;
			}
			internal set
			{
				this[FederationTrustSchema.OrgCertificate] = value;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000CEC47 File Offset: 0x000CCE47
		// (set) Token: 0x0600335C RID: 13148 RVA: 0x000CEC59 File Offset: 0x000CCE59
		public X509Certificate2 OrgNextCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgNextCertificate] as X509Certificate2;
			}
			internal set
			{
				this[FederationTrustSchema.OrgNextCertificate] = value;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x000CEC67 File Offset: 0x000CCE67
		// (set) Token: 0x0600335E RID: 13150 RVA: 0x000CEC79 File Offset: 0x000CCE79
		public X509Certificate2 OrgPrevCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgPrevCertificate] as X509Certificate2;
			}
			internal set
			{
				this[FederationTrustSchema.OrgPrevCertificate] = value;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x000CEC87 File Offset: 0x000CCE87
		// (set) Token: 0x06003360 RID: 13152 RVA: 0x000CEC99 File Offset: 0x000CCE99
		public string OrgPrivCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgPrivCertificate] as string;
			}
			internal set
			{
				this[FederationTrustSchema.OrgPrivCertificate] = value;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000CECA7 File Offset: 0x000CCEA7
		// (set) Token: 0x06003362 RID: 13154 RVA: 0x000CECB9 File Offset: 0x000CCEB9
		public string OrgNextPrivCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgNextPrivCertificate] as string;
			}
			internal set
			{
				this[FederationTrustSchema.OrgNextPrivCertificate] = value;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000CECC7 File Offset: 0x000CCEC7
		// (set) Token: 0x06003364 RID: 13156 RVA: 0x000CECD9 File Offset: 0x000CCED9
		public string OrgPrevPrivCertificate
		{
			get
			{
				return this[FederationTrustSchema.OrgPrevPrivCertificate] as string;
			}
			internal set
			{
				this[FederationTrustSchema.OrgPrevPrivCertificate] = value;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000CECE7 File Offset: 0x000CCEE7
		// (set) Token: 0x06003366 RID: 13158 RVA: 0x000CECF9 File Offset: 0x000CCEF9
		public X509Certificate2 TokenIssuerCertificate
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerCertificate] as X509Certificate2;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerCertificate] = value;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000CED07 File Offset: 0x000CCF07
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x000CED19 File Offset: 0x000CCF19
		public X509Certificate2 TokenIssuerPrevCertificate
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerPrevCertificate] as X509Certificate2;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerPrevCertificate] = value;
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000CED27 File Offset: 0x000CCF27
		// (set) Token: 0x0600336A RID: 13162 RVA: 0x000CED39 File Offset: 0x000CCF39
		public string PolicyReferenceUri
		{
			get
			{
				return this[FederationTrustSchema.PolicyReferenceUri] as string;
			}
			internal set
			{
				this[FederationTrustSchema.PolicyReferenceUri] = value;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000CED47 File Offset: 0x000CCF47
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x000CED59 File Offset: 0x000CCF59
		public Uri TokenIssuerMetadataEpr
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerMetadataEpr] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerMetadataEpr] = value;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000CED67 File Offset: 0x000CCF67
		// (set) Token: 0x0600336E RID: 13166 RVA: 0x000CED79 File Offset: 0x000CCF79
		public EnhancedTimeSpan MetadataPollInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[FederationTrustSchema.MetadataPollInterval];
			}
			internal set
			{
				this[FederationTrustSchema.MetadataPollInterval] = value;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000CED8C File Offset: 0x000CCF8C
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x000CED9E File Offset: 0x000CCF9E
		public FederationTrust.PartnerSTSType TokenIssuerType
		{
			get
			{
				return (FederationTrust.PartnerSTSType)this[FederationTrustSchema.TokenIssuerType];
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerType] = value;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000CEDB1 File Offset: 0x000CCFB1
		// (set) Token: 0x06003372 RID: 13170 RVA: 0x000CEDC3 File Offset: 0x000CCFC3
		public Uri TokenIssuerUri
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerUri] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerUri] = value;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000CEDD1 File Offset: 0x000CCFD1
		// (set) Token: 0x06003374 RID: 13172 RVA: 0x000CEDE3 File Offset: 0x000CCFE3
		public Uri TokenIssuerEpr
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerEpr] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerEpr] = value;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000CEDF1 File Offset: 0x000CCFF1
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x000CEE03 File Offset: 0x000CD003
		public Uri WebRequestorRedirectEpr
		{
			get
			{
				return this[FederationTrustSchema.WebRequestorRedirectEpr] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.WebRequestorRedirectEpr] = value;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000CEE11 File Offset: 0x000CD011
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x000CEE23 File Offset: 0x000CD023
		public Uri MetadataEpr
		{
			get
			{
				return this[FederationTrustSchema.MetadataEpr] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.MetadataEpr] = value;
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000CEE31 File Offset: 0x000CD031
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x000CEE43 File Offset: 0x000CD043
		public Uri MetadataPutEpr
		{
			get
			{
				return this[FederationTrustSchema.MetadataPutEpr] as Uri;
			}
			internal set
			{
				this[FederationTrustSchema.MetadataPutEpr] = value;
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000CEE51 File Offset: 0x000CD051
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000CEE63 File Offset: 0x000CD063
		public string TokenIssuerCertReference
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerCertReference] as string;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerCertReference] = value;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000CEE71 File Offset: 0x000CD071
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x000CEE83 File Offset: 0x000CD083
		public string TokenIssuerPrevCertReference
		{
			get
			{
				return this[FederationTrustSchema.TokenIssuerPrevCertReference] as string;
			}
			internal set
			{
				this[FederationTrustSchema.TokenIssuerPrevCertReference] = value;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000CEE91 File Offset: 0x000CD091
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x000CEEA3 File Offset: 0x000CD0A3
		internal string AdministratorProvisioningId
		{
			get
			{
				return this[FederationTrustSchema.AdministratorProvisioningId] as string;
			}
			set
			{
				this[FederationTrustSchema.AdministratorProvisioningId] = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003381 RID: 13185 RVA: 0x000CEEB1 File Offset: 0x000CD0B1
		// (set) Token: 0x06003382 RID: 13186 RVA: 0x000CEEC3 File Offset: 0x000CD0C3
		public FederationTrust.NamespaceProvisionerType NamespaceProvisioner
		{
			get
			{
				return (FederationTrust.NamespaceProvisionerType)this[FederationTrustSchema.NamespaceProvisioner];
			}
			internal set
			{
				this[FederationTrustSchema.NamespaceProvisioner] = value;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003383 RID: 13187 RVA: 0x000CEED6 File Offset: 0x000CD0D6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003384 RID: 13188 RVA: 0x000CEEDD File Offset: 0x000CD0DD
		internal override ADObjectSchema Schema
		{
			get
			{
				return FederationTrust.SchemaObject;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000CEEE4 File Offset: 0x000CD0E4
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchFedTrust";
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000CEEEB File Offset: 0x000CD0EB
		internal override ADObjectId ParentPath
		{
			get
			{
				return FederationTrust.FederationTrustsContainer;
			}
		}

		// Token: 0x04002390 RID: 9104
		public const string ContainerName = "Federation Trusts";

		// Token: 0x04002391 RID: 9105
		internal const string TaskNoun = "FederationTrust";

		// Token: 0x04002392 RID: 9106
		internal const string LdapName = "msExchFedTrust";

		// Token: 0x04002393 RID: 9107
		internal static readonly ADObjectId FederationTrustsContainer = new ADObjectId("CN=Federation Trusts");

		// Token: 0x04002394 RID: 9108
		private static readonly FederationTrustSchema SchemaObject = ObjectSchema.GetInstance<FederationTrustSchema>();

		// Token: 0x0200047C RID: 1148
		public enum PartnerSTSType
		{
			// Token: 0x04002396 RID: 9110
			LiveId
		}

		// Token: 0x0200047D RID: 1149
		public enum NamespaceProvisionerType
		{
			// Token: 0x04002398 RID: 9112
			LiveDomainServices,
			// Token: 0x04002399 RID: 9113
			LiveDomainServices2,
			// Token: 0x0400239A RID: 9114
			ExternalProcess
		}
	}
}
