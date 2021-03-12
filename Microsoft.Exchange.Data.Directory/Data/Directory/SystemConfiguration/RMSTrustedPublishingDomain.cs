using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006AD RID: 1709
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class RMSTrustedPublishingDomain : ADConfigurationObject
	{
		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0012272F File Offset: 0x0012092F
		internal override ADObjectSchema Schema
		{
			get
			{
				return RMSTrustedPublishingDomain.adSchema;
			}
		}

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x00122736 File Offset: 0x00120936
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchControlPointTrustedPublishingDomain";
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x0012273D File Offset: 0x0012093D
		internal override ADObjectId ParentPath
		{
			get
			{
				return RMSTrustedPublishingDomain.parentPath;
			}
		}

		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x00122744 File Offset: 0x00120944
		// (set) Token: 0x06004EEB RID: 20203 RVA: 0x00122756 File Offset: 0x00120956
		public Uri IntranetLicensingUrl
		{
			get
			{
				return (Uri)this[RMSTrustedPublishingDomainSchema.IntranetLicensingUrl];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.IntranetLicensingUrl] = value;
			}
		}

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x00122764 File Offset: 0x00120964
		// (set) Token: 0x06004EED RID: 20205 RVA: 0x00122776 File Offset: 0x00120976
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return (Uri)this[RMSTrustedPublishingDomainSchema.ExtranetLicensingUrl];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.ExtranetLicensingUrl] = value;
			}
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x00122784 File Offset: 0x00120984
		// (set) Token: 0x06004EEF RID: 20207 RVA: 0x00122796 File Offset: 0x00120996
		public Uri IntranetCertificationUrl
		{
			get
			{
				return (Uri)this[RMSTrustedPublishingDomainSchema.IntranetCertificationUrl];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.IntranetCertificationUrl] = value;
			}
		}

		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x001227A4 File Offset: 0x001209A4
		// (set) Token: 0x06004EF1 RID: 20209 RVA: 0x001227B6 File Offset: 0x001209B6
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return (Uri)this[RMSTrustedPublishingDomainSchema.ExtranetCertificationUrl];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.ExtranetCertificationUrl] = value;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x001227C4 File Offset: 0x001209C4
		// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x001227D6 File Offset: 0x001209D6
		public bool Default
		{
			get
			{
				return (bool)this[RMSTrustedPublishingDomainSchema.Default];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.Default] = value;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x001227E9 File Offset: 0x001209E9
		public int CryptoMode
		{
			get
			{
				return (int)this[RMSTrustedPublishingDomainSchema.CryptoMode];
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x001227FB File Offset: 0x001209FB
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x0012280D File Offset: 0x00120A0D
		public int CSPType
		{
			get
			{
				return (int)this[RMSTrustedPublishingDomainSchema.CSPType];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.CSPType] = value;
			}
		}

		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00122820 File Offset: 0x00120A20
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x00122832 File Offset: 0x00120A32
		public string CSPName
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.CSPName];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.CSPName] = value;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00122840 File Offset: 0x00120A40
		public bool IsRMSOnline
		{
			get
			{
				return (bool)this[RMSTrustedPublishingDomainSchema.IsRMSOnline];
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x00122852 File Offset: 0x00120A52
		// (set) Token: 0x06004EFB RID: 20219 RVA: 0x00122864 File Offset: 0x00120A64
		public string KeyContainerName
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.KeyContainerName];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.KeyContainerName] = value;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x00122872 File Offset: 0x00120A72
		// (set) Token: 0x06004EFD RID: 20221 RVA: 0x00122884 File Offset: 0x00120A84
		public string KeyId
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.KeyId];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.KeyId] = value;
			}
		}

		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x00122892 File Offset: 0x00120A92
		// (set) Token: 0x06004EFF RID: 20223 RVA: 0x001228A4 File Offset: 0x00120AA4
		public string KeyIdType
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.KeyIdType];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.KeyIdType] = value;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x001228B2 File Offset: 0x00120AB2
		// (set) Token: 0x06004F01 RID: 20225 RVA: 0x001228C4 File Offset: 0x00120AC4
		public int KeyNumber
		{
			get
			{
				return (int)this[RMSTrustedPublishingDomainSchema.KeyNumber];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.KeyNumber] = value;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x001228D7 File Offset: 0x00120AD7
		// (set) Token: 0x06004F03 RID: 20227 RVA: 0x001228E9 File Offset: 0x00120AE9
		internal string SLCCertChain
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.SLCCertChain];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.SLCCertChain] = value;
			}
		}

		// Token: 0x170019F4 RID: 6644
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x001228F7 File Offset: 0x00120AF7
		// (set) Token: 0x06004F05 RID: 20229 RVA: 0x00122909 File Offset: 0x00120B09
		internal string PrivateKey
		{
			get
			{
				return (string)this[RMSTrustedPublishingDomainSchema.PrivateKey];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.PrivateKey] = value;
			}
		}

		// Token: 0x170019F5 RID: 6645
		// (get) Token: 0x06004F06 RID: 20230 RVA: 0x00122917 File Offset: 0x00120B17
		// (set) Token: 0x06004F07 RID: 20231 RVA: 0x00122929 File Offset: 0x00120B29
		[ValidateCount(0, 25)]
		internal MultiValuedProperty<string> RMSTemplates
		{
			get
			{
				return (MultiValuedProperty<string>)this[RMSTrustedPublishingDomainSchema.RMSTemplates];
			}
			set
			{
				this[RMSTrustedPublishingDomainSchema.RMSTemplates] = value;
			}
		}

		// Token: 0x040035E7 RID: 13799
		private const int MaxRMSTemplates = 25;

		// Token: 0x040035E8 RID: 13800
		private const string MostDerivedClass = "msExchControlPointTrustedPublishingDomain";

		// Token: 0x040035E9 RID: 13801
		private static readonly RMSTrustedPublishingDomainSchema adSchema = ObjectSchema.GetInstance<RMSTrustedPublishingDomainSchema>();

		// Token: 0x040035EA RID: 13802
		private static readonly ADObjectId parentPath = new ADObjectId("CN=ControlPoint Config,CN=Transport Settings");
	}
}
