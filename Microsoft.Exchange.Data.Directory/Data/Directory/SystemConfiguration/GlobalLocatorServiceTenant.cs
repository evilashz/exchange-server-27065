using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E3 RID: 739
	[Serializable]
	public class GlobalLocatorServiceTenant : ConfigurableObject
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x000974CA File Offset: 0x000956CA
		internal GlobalLocatorServiceTenant() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2012);
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x000974F3 File Offset: 0x000956F3
		// (set) Token: 0x060022A9 RID: 8873 RVA: 0x00097505 File Offset: 0x00095705
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)this[GlobalLocatorServiceTenantSchema.ExternalDirectoryOrganizationId];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x00097518 File Offset: 0x00095718
		// (set) Token: 0x060022AB RID: 8875 RVA: 0x0009752A File Offset: 0x0009572A
		public MultiValuedProperty<string> DomainNames
		{
			get
			{
				return (MultiValuedProperty<string>)this[GlobalLocatorServiceTenantSchema.DomainNames];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.DomainNames] = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x00097538 File Offset: 0x00095738
		// (set) Token: 0x060022AD RID: 8877 RVA: 0x0009754A File Offset: 0x0009574A
		public string ResourceForest
		{
			get
			{
				return (string)this[GlobalLocatorServiceTenantSchema.ResourceForest];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.ResourceForest] = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00097558 File Offset: 0x00095758
		// (set) Token: 0x060022AF RID: 8879 RVA: 0x0009756A File Offset: 0x0009576A
		public string AccountForest
		{
			get
			{
				return (string)this[GlobalLocatorServiceTenantSchema.AccountForest];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.AccountForest] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00097578 File Offset: 0x00095778
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0009758A File Offset: 0x0009578A
		public string PrimarySite
		{
			get
			{
				return (string)this[GlobalLocatorServiceTenantSchema.PrimarySite];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.PrimarySite] = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00097598 File Offset: 0x00095798
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x000975AA File Offset: 0x000957AA
		public SmtpDomain SmtpNextHopDomain
		{
			get
			{
				return (SmtpDomain)this[GlobalLocatorServiceTenantSchema.SmtpNextHopDomain];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.SmtpNextHopDomain] = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000975B8 File Offset: 0x000957B8
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x000975CA File Offset: 0x000957CA
		public GlsTenantFlags TenantFlags
		{
			get
			{
				return (GlsTenantFlags)this[GlobalLocatorServiceTenantSchema.TenantFlags];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.TenantFlags] = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x000975DD File Offset: 0x000957DD
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x000975EF File Offset: 0x000957EF
		public string TenantContainerCN
		{
			get
			{
				return (string)this[GlobalLocatorServiceTenantSchema.TenantContainerCN];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.TenantContainerCN] = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000975FD File Offset: 0x000957FD
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x0009760F File Offset: 0x0009580F
		public string ResumeCache
		{
			get
			{
				return (string)this[GlobalLocatorServiceTenantSchema.ResumeCache];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.ResumeCache] = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0009761D File Offset: 0x0009581D
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x0009762F File Offset: 0x0009582F
		public bool IsOfflineData
		{
			get
			{
				return (bool)this[GlobalLocatorServiceTenantSchema.IsOfflineData];
			}
			set
			{
				this[GlobalLocatorServiceTenantSchema.IsOfflineData] = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x00097642 File Offset: 0x00095842
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<GlobalLocatorServiceTenantSchema>();
			}
		}
	}
}
