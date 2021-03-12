using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200040C RID: 1036
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SmimeAdminSettingsType : UserConfigurationBaseType
	{
		// Token: 0x06002254 RID: 8788 RVA: 0x0007EE58 File Offset: 0x0007D058
		public SmimeAdminSettingsType(OrganizationId organizationId) : this(SmimeAdminSettingsType.ReadSmimeSettingsFromAd(organizationId))
		{
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0007EE66 File Offset: 0x0007D066
		public SmimeAdminSettingsType(ISmimeSettingsProvider settingsProvider) : base("OWA.SmimeAdminSettingsType")
		{
			this.smimeConfigurationContainer = settingsProvider;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0007EE7A File Offset: 0x0007D07A
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x0007EE87 File Offset: 0x0007D087
		[DataMember]
		public bool CheckCRLOnSend
		{
			get
			{
				return this.smimeConfigurationContainer.OWACheckCRLOnSend;
			}
			set
			{
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x0007EE89 File Offset: 0x0007D089
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x0007EE96 File Offset: 0x0007D096
		[DataMember]
		public uint DLExpansionTimeout
		{
			get
			{
				return this.smimeConfigurationContainer.OWADLExpansionTimeout;
			}
			set
			{
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x0007EE98 File Offset: 0x0007D098
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x0007EEA5 File Offset: 0x0007D0A5
		[DataMember]
		public bool UseSecondaryProxiesWhenFindingCertificates
		{
			get
			{
				return this.smimeConfigurationContainer.OWAUseSecondaryProxiesWhenFindingCertificates;
			}
			set
			{
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0007EEA7 File Offset: 0x0007D0A7
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0007EEB4 File Offset: 0x0007D0B4
		[DataMember]
		public uint CRLConnectionTimeout
		{
			get
			{
				return this.smimeConfigurationContainer.OWACRLConnectionTimeout;
			}
			set
			{
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007EEB6 File Offset: 0x0007D0B6
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x0007EEC3 File Offset: 0x0007D0C3
		[DataMember]
		public uint CRLRetrievalTimeout
		{
			get
			{
				return this.smimeConfigurationContainer.OWACRLRetrievalTimeout;
			}
			set
			{
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x0007EEC5 File Offset: 0x0007D0C5
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x0007EED2 File Offset: 0x0007D0D2
		[DataMember]
		public bool DisableCRLCheck
		{
			get
			{
				return this.smimeConfigurationContainer.OWADisableCRLCheck;
			}
			set
			{
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x0007EED4 File Offset: 0x0007D0D4
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x0007EEE1 File Offset: 0x0007D0E1
		[DataMember]
		public bool AlwaysSign
		{
			get
			{
				return this.smimeConfigurationContainer.OWAAlwaysSign;
			}
			set
			{
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x0007EEE3 File Offset: 0x0007D0E3
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x0007EEF0 File Offset: 0x0007D0F0
		[DataMember]
		public bool AlwaysEncrypt
		{
			get
			{
				return this.smimeConfigurationContainer.OWAAlwaysEncrypt;
			}
			set
			{
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x0007EEF2 File Offset: 0x0007D0F2
		// (set) Token: 0x06002267 RID: 8807 RVA: 0x0007EEFF File Offset: 0x0007D0FF
		[DataMember]
		public bool ClearSign
		{
			get
			{
				return this.smimeConfigurationContainer.OWAClearSign;
			}
			set
			{
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0007EF01 File Offset: 0x0007D101
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0007EF0E File Offset: 0x0007D10E
		[DataMember]
		public bool IncludeCertificateChainWithoutRootCertificate
		{
			get
			{
				return this.smimeConfigurationContainer.OWAIncludeCertificateChainWithoutRootCertificate;
			}
			set
			{
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x0007EF10 File Offset: 0x0007D110
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x0007EF1D File Offset: 0x0007D11D
		[DataMember]
		public bool IncludeCertificateChainAndRootCertificate
		{
			get
			{
				return this.smimeConfigurationContainer.OWAIncludeCertificateChainAndRootCertificate;
			}
			set
			{
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0007EF1F File Offset: 0x0007D11F
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x0007EF2C File Offset: 0x0007D12C
		[DataMember]
		public bool EncryptTemporaryBuffers
		{
			get
			{
				return this.smimeConfigurationContainer.OWAEncryptTemporaryBuffers;
			}
			set
			{
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0007EF2E File Offset: 0x0007D12E
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x0007EF3B File Offset: 0x0007D13B
		[DataMember]
		public bool SignedEmailCertificateInclusion
		{
			get
			{
				return this.smimeConfigurationContainer.OWASignedEmailCertificateInclusion;
			}
			set
			{
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x0007EF3D File Offset: 0x0007D13D
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x0007EF4A File Offset: 0x0007D14A
		[DataMember]
		public uint BccEncryptedEmailForking
		{
			get
			{
				return this.smimeConfigurationContainer.OWABCCEncryptedEmailForking;
			}
			set
			{
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0007EF4C File Offset: 0x0007D14C
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x0007EF59 File Offset: 0x0007D159
		[DataMember]
		public bool IncludeSMIMECapabilitiesInMessage
		{
			get
			{
				return this.smimeConfigurationContainer.OWAIncludeSMIMECapabilitiesInMessage;
			}
			set
			{
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0007EF5B File Offset: 0x0007D15B
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x0007EF68 File Offset: 0x0007D168
		[DataMember]
		public bool CopyRecipientHeaders
		{
			get
			{
				return this.smimeConfigurationContainer.OWACopyRecipientHeaders;
			}
			set
			{
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0007EF6A File Offset: 0x0007D16A
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x0007EF77 File Offset: 0x0007D177
		[DataMember]
		public bool OnlyUseSmartCard
		{
			get
			{
				return this.smimeConfigurationContainer.OWAOnlyUseSmartCard;
			}
			set
			{
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0007EF79 File Offset: 0x0007D179
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x0007EF86 File Offset: 0x0007D186
		[DataMember]
		public bool TripleWrapSignedEncryptedMail
		{
			get
			{
				return this.smimeConfigurationContainer.OWATripleWrapSignedEncryptedMail;
			}
			set
			{
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007EF88 File Offset: 0x0007D188
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0007EF95 File Offset: 0x0007D195
		[DataMember]
		public bool UseKeyIdentifier
		{
			get
			{
				return this.smimeConfigurationContainer.OWAUseKeyIdentifier;
			}
			set
			{
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0007EF97 File Offset: 0x0007D197
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x0007EFA4 File Offset: 0x0007D1A4
		[DataMember]
		public string EncryptionAlgorithms
		{
			get
			{
				return this.smimeConfigurationContainer.OWAEncryptionAlgorithms;
			}
			set
			{
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0007EFA6 File Offset: 0x0007D1A6
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0007EFB3 File Offset: 0x0007D1B3
		[DataMember]
		public string SigningAlgorithms
		{
			get
			{
				return this.smimeConfigurationContainer.OWASigningAlgorithms;
			}
			set
			{
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0007EFB5 File Offset: 0x0007D1B5
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0007EFC2 File Offset: 0x0007D1C2
		[DataMember]
		public bool ForceSmimeClientUpgrade
		{
			get
			{
				return this.smimeConfigurationContainer.OWAForceSMIMEClientUpgrade;
			}
			set
			{
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0007EFC4 File Offset: 0x0007D1C4
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0007EFD1 File Offset: 0x0007D1D1
		[DataMember]
		public string SenderCertificateAttributesToDisplay
		{
			get
			{
				return this.smimeConfigurationContainer.OWASenderCertificateAttributesToDisplay;
			}
			set
			{
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0007EFD3 File Offset: 0x0007D1D3
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0007EFE0 File Offset: 0x0007D1E0
		[DataMember]
		public bool AllowUserChoiceOfSigningCertificate
		{
			get
			{
				return this.smimeConfigurationContainer.OWAAllowUserChoiceOfSigningCertificate;
			}
			set
			{
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x0007EFE2 File Offset: 0x0007D1E2
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x0007EFEF File Offset: 0x0007D1EF
		public string SMIMECertificateIssuingCAFull
		{
			get
			{
				return this.smimeConfigurationContainer.SMIMECertificateIssuingCAFull();
			}
			set
			{
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x0007EFF1 File Offset: 0x0007D1F1
		internal override UserConfigurationPropertySchemaBase Schema
		{
			get
			{
				return UserOptionPropertySchema.Instance;
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x0007EFF8 File Offset: 0x0007D1F8
		internal static ISmimeSettingsProvider ReadSmimeSettingsFromAd(OrganizationId organizationId)
		{
			CachedOrganizationConfiguration instance = CachedOrganizationConfiguration.GetInstance(organizationId, TimeSpan.FromHours(1.0), CachedOrganizationConfiguration.ConfigurationTypes.All);
			return instance.SmimeSettings.Configuration;
		}

		// Token: 0x04001320 RID: 4896
		private const string ConfigurationName = "OWA.SmimeAdminSettingsType";

		// Token: 0x04001321 RID: 4897
		private readonly ISmimeSettingsProvider smimeConfigurationContainer;
	}
}
