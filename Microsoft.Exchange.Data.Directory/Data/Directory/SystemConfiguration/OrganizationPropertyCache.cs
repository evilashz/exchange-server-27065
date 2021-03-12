using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000644 RID: 1604
	internal static class OrganizationPropertyCache
	{
		// Token: 0x06004B83 RID: 19331 RVA: 0x00116518 File Offset: 0x00114718
		public static bool TryGetOrganizationProperties(OrganizationId organizationId, out OrganizationProperties organizationProperties)
		{
			if (OrganizationPropertyCache.orgPropertiesCache.TryGetValue(organizationId, out organizationProperties))
			{
				OrganizationPropertyCache.Tracer.TraceDebug<OrganizationId>(0L, "OrganizationProperties for '{0}' found in the cache", organizationId);
				return true;
			}
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				organizationProperties = new OrganizationProperties(true, null);
				OrganizationPropertyCache.Tracer.TraceDebug(0L, "OrganizationProperties for ForestWideOrgId has been manually constructed.");
			}
			else if (!OrganizationPropertyCache.TryGetOrganizationPropertiesFromAD(organizationId, ref organizationProperties))
			{
				OrganizationPropertyCache.Tracer.TraceError<OrganizationId>(0L, "TryGetOrganizationProperties('{0}') returns false because org is not found in AD!", organizationId);
				return false;
			}
			OrganizationPropertyCache.orgPropertiesCache.InsertAbsolute(organizationId, organizationProperties, CacheTimeToLive.OrgPropertyCacheTimeToLive, null);
			OrganizationPropertyCache.Tracer.TraceDebug<OrganizationId>(0L, "OrganizationProperties for '{0}' located in AD and added to the cache", organizationId);
			return true;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x001165B3 File Offset: 0x001147B3
		internal static void RemoveCacheEntry(OrganizationId organizationId)
		{
			OrganizationPropertyCache.orgPropertiesCache.Remove(organizationId);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x001165C4 File Offset: 0x001147C4
		private static bool TryGetOrganizationPropertiesFromAD(OrganizationId organizationId, ref OrganizationProperties organizationProperties)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 271, "TryGetOrganizationPropertiesFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\OrganizationPropertyCache.cs");
			ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(organizationId.ConfigurationUnit, OrganizationPropertyCache.orgProperties);
			if (adrawEntry == null)
			{
				OrganizationPropertyCache.Tracer.TraceError<OrganizationId>(0L, "CU for '{0}' is not found in AD", organizationId);
				return false;
			}
			organizationProperties = new OrganizationProperties((bool)adrawEntry[OrganizationSchema.SkipToUAndParentalControlCheck], (string)adrawEntry[ExchangeConfigurationUnitSchema.ServicePlan]);
			organizationProperties.ShowAdminAccessWarning = (bool)adrawEntry[OrganizationSchema.ShowAdminAccessWarning];
			organizationProperties.HideAdminAccessWarning = (bool)adrawEntry[OrganizationSchema.HideAdminAccessWarning];
			organizationProperties.ActivityBasedAuthenticationTimeoutEnabled = !(bool)adrawEntry[OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled];
			organizationProperties.ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled = !(bool)adrawEntry[OrganizationSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled];
			organizationProperties.ActivityBasedAuthenticationTimeoutInterval = (EnhancedTimeSpan)adrawEntry[OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval];
			organizationProperties.IsLicensingEnforced = (bool)adrawEntry[OrganizationSchema.IsLicensingEnforced];
			organizationProperties.IsTenantAccessBlocked = (bool)adrawEntry[OrganizationSchema.IsTenantAccessBlocked];
			return true;
		}

		// Token: 0x040033DF RID: 13279
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033E0 RID: 13280
		private static TimeoutCache<OrganizationId, OrganizationProperties> orgPropertiesCache = new TimeoutCache<OrganizationId, OrganizationProperties>(16, 1024, false);

		// Token: 0x040033E1 RID: 13281
		private static ADPropertyDefinition[] orgProperties = new ADPropertyDefinition[]
		{
			OrganizationSchema.SkipToUAndParentalControlCheck,
			ExchangeConfigurationUnitSchema.ServicePlan,
			OrganizationSchema.HideAdminAccessWarning,
			OrganizationSchema.ShowAdminAccessWarning,
			OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled,
			OrganizationSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled,
			OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval,
			OrganizationSchema.IsLicensingEnforced,
			OrganizationSchema.IsTenantAccessBlocked
		};
	}
}
