using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000DA RID: 218
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	internal class FfoTenant : ADObject
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001AA3B File Offset: 0x00018C3B
		public override ObjectId Identity
		{
			get
			{
				return this.TenantId;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001AA43 File Offset: 0x00018C43
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x0001AA55 File Offset: 0x00018C55
		internal ADObjectId TenantId
		{
			get
			{
				return this[ADObjectSchema.OrganizationalUnitRoot] as ADObjectId;
			}
			set
			{
				this[ADObjectSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001AA63 File Offset: 0x00018C63
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001AA75 File Offset: 0x00018C75
		internal string TenantName
		{
			get
			{
				return this[ADObjectSchema.RawName] as string;
			}
			set
			{
				this[ADObjectSchema.RawName] = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001AA83 File Offset: 0x00018C83
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001AA95 File Offset: 0x00018C95
		internal IEnumerable<ADMiniDomain> VerifiedDomains
		{
			get
			{
				return this[FfoTenantSchema.VerifiedDomainsProp] as IEnumerable<ADMiniDomain>;
			}
			set
			{
				this[FfoTenantSchema.VerifiedDomainsProp] = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001AAA3 File Offset: 0x00018CA3
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001AAB5 File Offset: 0x00018CB5
		internal IEnumerable<AssignedPlan> AssignedPlans
		{
			get
			{
				return this[FfoTenantSchema.AssignedPlansProp] as IEnumerable<AssignedPlan>;
			}
			set
			{
				this[FfoTenantSchema.AssignedPlansProp] = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001AAC3 File Offset: 0x00018CC3
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001AAD5 File Offset: 0x00018CD5
		internal MultiValuedProperty<string> CompanyTags
		{
			get
			{
				return this[FfoTenantSchema.CompanyTagsProp] as MultiValuedProperty<string>;
			}
			set
			{
				this[FfoTenantSchema.CompanyTagsProp] = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x0001AAE3 File Offset: 0x00018CE3
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x0001AAF5 File Offset: 0x00018CF5
		internal string C
		{
			get
			{
				return this[FfoTenantSchema.C] as string;
			}
			set
			{
				this[FfoTenantSchema.C] = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x0001AB03 File Offset: 0x00018D03
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x0001AB15 File Offset: 0x00018D15
		internal string CompanyPartnership
		{
			get
			{
				return this[FfoTenantSchema.CompanyPartnershipProperty] as string;
			}
			set
			{
				this[FfoTenantSchema.CompanyPartnershipProperty] = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001AB23 File Offset: 0x00018D23
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0001AB35 File Offset: 0x00018D35
		internal string Description
		{
			get
			{
				return this[FfoTenantSchema.DescriptionProperty] as string;
			}
			set
			{
				this[FfoTenantSchema.DescriptionProperty] = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001AB43 File Offset: 0x00018D43
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x0001AB55 File Offset: 0x00018D55
		internal string DisplayName
		{
			get
			{
				return this[FfoTenantSchema.DisplayName] as string;
			}
			set
			{
				this[FfoTenantSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001AB63 File Offset: 0x00018D63
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0001AB75 File Offset: 0x00018D75
		internal bool IsDirSyncRunning
		{
			get
			{
				return (bool)this[FfoTenantSchema.IsDirSyncRunning];
			}
			set
			{
				this[FfoTenantSchema.IsDirSyncRunning] = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001AB88 File Offset: 0x00018D88
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0001AB9A File Offset: 0x00018D9A
		internal ResellerType ResellerType
		{
			get
			{
				return (ResellerType)this[FfoTenantSchema.ResellerTypeProperty];
			}
			set
			{
				this[FfoTenantSchema.ResellerTypeProperty] = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001ABAD File Offset: 0x00018DAD
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0001ABBF File Offset: 0x00018DBF
		internal string ServiceInstance
		{
			get
			{
				return this[FfoTenantSchema.ServiceInstanceProp] as string;
			}
			set
			{
				this[FfoTenantSchema.ServiceInstanceProp] = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001ABCD File Offset: 0x00018DCD
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0001ABDF File Offset: 0x00018DDF
		internal RmsoUpgradeStatus RmsoUpgradeStatus
		{
			get
			{
				return (RmsoUpgradeStatus)this[FfoTenantSchema.RmsoUpgradeStatusProp];
			}
			set
			{
				this[FfoTenantSchema.RmsoUpgradeStatusProp] = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0001ABF2 File Offset: 0x00018DF2
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0001AC04 File Offset: 0x00018E04
		internal string SharepointTenantAdminUrl
		{
			get
			{
				return (string)this[FfoTenantSchema.SharepointTenantAdminUrl];
			}
			set
			{
				this[FfoTenantSchema.SharepointTenantAdminUrl] = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001AC12 File Offset: 0x00018E12
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001AC24 File Offset: 0x00018E24
		internal string SharepointRootSiteUrl
		{
			get
			{
				return (string)this[FfoTenantSchema.SharepointRootSiteUrl];
			}
			set
			{
				this[FfoTenantSchema.SharepointRootSiteUrl] = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001AC32 File Offset: 0x00018E32
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001AC44 File Offset: 0x00018E44
		internal string OdmsEndpointUrl
		{
			get
			{
				return (string)this[FfoTenantSchema.OdmsEndpointUrl];
			}
			set
			{
				this[FfoTenantSchema.OdmsEndpointUrl] = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001AC52 File Offset: 0x00018E52
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0001AC64 File Offset: 0x00018E64
		internal string MigratedTo
		{
			get
			{
				return this[FfoTenantSchema.MigratedToProp] as string;
			}
			set
			{
				this[FfoTenantSchema.MigratedToProp] = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001AC72 File Offset: 0x00018E72
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0001AC84 File Offset: 0x00018E84
		internal MultiValuedProperty<string> UnifiedPolicyPreReqState
		{
			get
			{
				return this[FfoTenantSchema.UnifiedPolicyPreReqState] as MultiValuedProperty<string>;
			}
			set
			{
				this[FfoTenantSchema.UnifiedPolicyPreReqState] = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001AC92 File Offset: 0x00018E92
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		internal OrganizationStatus OrganizationStatus
		{
			get
			{
				return (OrganizationStatus)this[FfoTenantSchema.OrganizationStatusProp];
			}
			set
			{
				this[FfoTenantSchema.OrganizationStatusProp] = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		internal override ADObjectSchema Schema
		{
			get
			{
				return FfoTenant.schema;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001ACBE File Offset: 0x00018EBE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return FfoTenant.mostDerivedClass;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001ACC5 File Offset: 0x00018EC5
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000470 RID: 1136
		internal const string EmptyTagsPropertyValue = "(no tags available)";

		// Token: 0x04000471 RID: 1137
		private static readonly FfoTenantSchema schema = ObjectSchema.GetInstance<FfoTenantSchema>();

		// Token: 0x04000472 RID: 1138
		private static string mostDerivedClass = "FfoTenant";
	}
}
