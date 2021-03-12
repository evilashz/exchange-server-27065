using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000717 RID: 1815
	[Serializable]
	public sealed class ImportRmsTrustedPublishingDomainResult : ADPresentationObject
	{
		// Token: 0x06004089 RID: 16521 RVA: 0x00108418 File Offset: 0x00106618
		public ImportRmsTrustedPublishingDomainResult(RMSTrustedPublishingDomain rmsTpd) : base(rmsTpd)
		{
			this.IntranetLicensingUrl = rmsTpd.IntranetLicensingUrl;
			this.ExtranetLicensingUrl = rmsTpd.ExtranetLicensingUrl;
			this.IntranetCertificationUrl = rmsTpd.IntranetCertificationUrl;
			this.ExtranetCertificationUrl = rmsTpd.ExtranetCertificationUrl;
			this.Default = rmsTpd.Default;
			this.CSPType = rmsTpd.CSPType;
			this.CSPName = rmsTpd.CSPName;
			this.KeyContainerName = rmsTpd.KeyContainerName;
			this.KeyId = rmsTpd.KeyId;
			this.KeyIdType = rmsTpd.KeyIdType;
			this.KeyNumber = rmsTpd.KeyNumber;
			base.Name = rmsTpd.Name;
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x001084DD File Offset: 0x001066DD
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ImportRmsTrustedPublishingDomainResult.schema;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x001084E4 File Offset: 0x001066E4
		// (set) Token: 0x0600408C RID: 16524 RVA: 0x001084F6 File Offset: 0x001066F6
		public Uri IntranetLicensingUrl
		{
			get
			{
				return (Uri)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.IntranetLicensingUrl];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.IntranetLicensingUrl] = value;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x00108504 File Offset: 0x00106704
		// (set) Token: 0x0600408E RID: 16526 RVA: 0x00108516 File Offset: 0x00106716
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return (Uri)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.ExtranetLicensingUrl];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.ExtranetLicensingUrl] = value;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600408F RID: 16527 RVA: 0x00108524 File Offset: 0x00106724
		// (set) Token: 0x06004090 RID: 16528 RVA: 0x00108536 File Offset: 0x00106736
		public Uri IntranetCertificationUrl
		{
			get
			{
				return (Uri)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.IntranetCertificationUrl];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.IntranetCertificationUrl] = value;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x00108544 File Offset: 0x00106744
		// (set) Token: 0x06004092 RID: 16530 RVA: 0x00108556 File Offset: 0x00106756
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return (Uri)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.ExtranetCertificationUrl];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.ExtranetCertificationUrl] = value;
			}
		}

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x00108564 File Offset: 0x00106764
		// (set) Token: 0x06004094 RID: 16532 RVA: 0x00108576 File Offset: 0x00106776
		public bool Default
		{
			get
			{
				return (bool)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.Default];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.Default] = value;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x00108589 File Offset: 0x00106789
		public int CryptoMode
		{
			get
			{
				return (int)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.CryptoMode];
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x0010859B File Offset: 0x0010679B
		// (set) Token: 0x06004097 RID: 16535 RVA: 0x001085AD File Offset: 0x001067AD
		public int CSPType
		{
			get
			{
				return (int)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.CSPType];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.CSPType] = value;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x001085C0 File Offset: 0x001067C0
		// (set) Token: 0x06004099 RID: 16537 RVA: 0x001085D2 File Offset: 0x001067D2
		public string CSPName
		{
			get
			{
				return (string)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.CSPName];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.CSPName] = value;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x001085E0 File Offset: 0x001067E0
		// (set) Token: 0x0600409B RID: 16539 RVA: 0x001085F2 File Offset: 0x001067F2
		public string KeyContainerName
		{
			get
			{
				return (string)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyContainerName];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyContainerName] = value;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x00108600 File Offset: 0x00106800
		// (set) Token: 0x0600409D RID: 16541 RVA: 0x00108612 File Offset: 0x00106812
		public string KeyId
		{
			get
			{
				return (string)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyId];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyId] = value;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x00108620 File Offset: 0x00106820
		// (set) Token: 0x0600409F RID: 16543 RVA: 0x00108632 File Offset: 0x00106832
		public string KeyIdType
		{
			get
			{
				return (string)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyIdType];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyIdType] = value;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x00108640 File Offset: 0x00106840
		// (set) Token: 0x060040A1 RID: 16545 RVA: 0x00108652 File Offset: 0x00106852
		public int KeyNumber
		{
			get
			{
				return (int)this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyNumber];
			}
			set
			{
				this[ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema.KeyNumber] = value;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x00108665 File Offset: 0x00106865
		public MultiValuedProperty<string> AddedTemplates
		{
			get
			{
				return this.addedTemplates;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x0010866D File Offset: 0x0010686D
		public MultiValuedProperty<string> UpdatedTemplates
		{
			get
			{
				return this.updatedTemplates;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x00108675 File Offset: 0x00106875
		public MultiValuedProperty<string> RemovedTemplates
		{
			get
			{
				return this.removedTemplates;
			}
		}

		// Token: 0x040028E4 RID: 10468
		private static readonly ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema schema = ObjectSchema.GetInstance<ImportRmsTrustedPublishingDomainResult.ImportRmsTrustedPublishingDomainResultSchema>();

		// Token: 0x040028E5 RID: 10469
		private readonly MultiValuedProperty<string> addedTemplates = new MultiValuedProperty<string>();

		// Token: 0x040028E6 RID: 10470
		private readonly MultiValuedProperty<string> updatedTemplates = new MultiValuedProperty<string>();

		// Token: 0x040028E7 RID: 10471
		private readonly MultiValuedProperty<string> removedTemplates = new MultiValuedProperty<string>();

		// Token: 0x02000718 RID: 1816
		private sealed class ImportRmsTrustedPublishingDomainResultSchema : ADPresentationSchema
		{
			// Token: 0x060040A6 RID: 16550 RVA: 0x00108689 File Offset: 0x00106889
			internal override ADObjectSchema GetParentSchema()
			{
				return ObjectSchema.GetInstance<RMSTrustedPublishingDomainSchema>();
			}

			// Token: 0x040028E8 RID: 10472
			public static readonly ADPropertyDefinition CSPName = RMSTrustedPublishingDomainSchema.CSPName;

			// Token: 0x040028E9 RID: 10473
			public static readonly ADPropertyDefinition CSPType = RMSTrustedPublishingDomainSchema.CSPType;

			// Token: 0x040028EA RID: 10474
			public static readonly ADPropertyDefinition KeyId = RMSTrustedPublishingDomainSchema.KeyId;

			// Token: 0x040028EB RID: 10475
			public static readonly ADPropertyDefinition KeyIdType = RMSTrustedPublishingDomainSchema.KeyIdType;

			// Token: 0x040028EC RID: 10476
			public static readonly ADPropertyDefinition KeyContainerName = RMSTrustedPublishingDomainSchema.KeyContainerName;

			// Token: 0x040028ED RID: 10477
			public static readonly ADPropertyDefinition KeyNumber = RMSTrustedPublishingDomainSchema.KeyNumber;

			// Token: 0x040028EE RID: 10478
			public static readonly ADPropertyDefinition IntranetLicensingUrl = RMSTrustedPublishingDomainSchema.IntranetLicensingUrl;

			// Token: 0x040028EF RID: 10479
			public static readonly ADPropertyDefinition ExtranetLicensingUrl = RMSTrustedPublishingDomainSchema.ExtranetLicensingUrl;

			// Token: 0x040028F0 RID: 10480
			public static readonly ADPropertyDefinition IntranetCertificationUrl = RMSTrustedPublishingDomainSchema.IntranetCertificationUrl;

			// Token: 0x040028F1 RID: 10481
			public static readonly ADPropertyDefinition ExtranetCertificationUrl = RMSTrustedPublishingDomainSchema.ExtranetCertificationUrl;

			// Token: 0x040028F2 RID: 10482
			public static readonly ADPropertyDefinition Default = RMSTrustedPublishingDomainSchema.Default;

			// Token: 0x040028F3 RID: 10483
			public static readonly ADPropertyDefinition CryptoMode = RMSTrustedPublishingDomainSchema.CryptoMode;
		}
	}
}
