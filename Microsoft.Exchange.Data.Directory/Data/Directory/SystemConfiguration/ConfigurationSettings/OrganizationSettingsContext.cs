using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000663 RID: 1635
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OrganizationSettingsContext : SettingsContextBase
	{
		// Token: 0x06004C76 RID: 19574 RVA: 0x0011A9B8 File Offset: 0x00118BB8
		public OrganizationSettingsContext(OrganizationId orgId, SettingsContextBase nextContext = null) : base(nextContext)
		{
			if (orgId != OrganizationId.ForestWideOrgId)
			{
				this.Initialize(OrganizationSettingsContext.OrgCache.Get(orgId));
			}
		}

		// Token: 0x06004C77 RID: 19575 RVA: 0x0011A9DF File Offset: 0x00118BDF
		public OrganizationSettingsContext(ExchangeConfigurationUnit org, SettingsContextBase nextContext = null) : base(nextContext)
		{
			this.Initialize(org);
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x0011A9EF File Offset: 0x00118BEF
		public override string OrganizationName
		{
			get
			{
				return this.orgName;
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x0011A9F7 File Offset: 0x00118BF7
		public override ExchangeObjectVersion OrganizationVersion
		{
			get
			{
				return this.orgVersion;
			}
		}

		// Token: 0x06004C7A RID: 19578 RVA: 0x0011A9FF File Offset: 0x00118BFF
		private void Initialize(ExchangeConfigurationUnit org)
		{
			if (org != null)
			{
				this.orgName = org.Name;
				this.orgVersion = org.AdminDisplayVersion;
			}
		}

		// Token: 0x0400345A RID: 13402
		private static readonly OrganizationSettingsContext.OrganizationCache OrgCache = new OrganizationSettingsContext.OrganizationCache();

		// Token: 0x0400345B RID: 13403
		private string orgName;

		// Token: 0x0400345C RID: 13404
		private ExchangeObjectVersion orgVersion;

		// Token: 0x02000664 RID: 1636
		private class OrganizationCache : LazyLookupTimeoutCache<OrganizationId, ExchangeConfigurationUnit>
		{
			// Token: 0x06004C7C RID: 19580 RVA: 0x0011AA28 File Offset: 0x00118C28
			public OrganizationCache() : base(8, 16, false, TimeSpan.FromHours(1.0), TimeSpan.FromHours(1.0))
			{
			}

			// Token: 0x06004C7D RID: 19581 RVA: 0x0011AAA4 File Offset: 0x00118CA4
			protected override ExchangeConfigurationUnit CreateOnCacheMiss(OrganizationId orgId, ref bool shouldAdd)
			{
				shouldAdd = false;
				if (orgId == OrganizationId.ForestWideOrgId)
				{
					return null;
				}
				ExchangeConfigurationUnit org = null;
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId), 135, "CreateOnCacheMiss", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationSettings\\OrganizationSettingsContext.cs");
					org = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(orgId.ConfigurationUnit);
				});
				shouldAdd = true;
				return org;
			}
		}
	}
}
