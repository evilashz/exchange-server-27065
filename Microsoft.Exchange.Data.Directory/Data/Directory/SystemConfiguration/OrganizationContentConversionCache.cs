﻿using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000646 RID: 1606
	internal static class OrganizationContentConversionCache
	{
		// Token: 0x06004B90 RID: 19344 RVA: 0x001167C0 File Offset: 0x001149C0
		public static bool TryGetOrganizationContentConversionProperties(OrganizationId organizationId, out OrganizationContentConversionProperties organizationProperties)
		{
			organizationProperties = null;
			if (organizationId == null)
			{
				OrganizationContentConversionCache.Tracer.TraceError(0L, "organizationId is null.  No configurations loaded.");
				return false;
			}
			if (OrganizationContentConversionCache.orgPropertiesCache.TryGetValue(organizationId, out organizationProperties))
			{
				OrganizationContentConversionCache.Tracer.TraceDebug<OrganizationId>(0L, "OrganizationContentConversionsProperties for '{0}' found in the cache", organizationId);
			}
			else
			{
				organizationProperties = OrganizationContentConversionCache.GetOrganizationContentConversionPropertiesFromAD(organizationId);
				OrganizationContentConversionCache.orgPropertiesCache.InsertAbsolute(organizationId, organizationProperties, CacheTimeToLive.OrgPropertyCacheTimeToLive, null);
			}
			return organizationProperties.ValidOrganization;
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x00116830 File Offset: 0x00114A30
		internal static void RemoveCacheEntry(OrganizationId organizationId)
		{
			OrganizationContentConversionCache.orgPropertiesCache.Remove(organizationId);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x00116840 File Offset: 0x00114A40
		private static OrganizationContentConversionProperties GetOrganizationContentConversionPropertiesFromAD(OrganizationId organizationId)
		{
			OrganizationContentConversionProperties organizationContentConversionProperties = new OrganizationContentConversionProperties();
			if (organizationId == null)
			{
				organizationId = OrganizationId.ForestWideOrgId;
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, organizationId, organizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 147, "GetOrganizationContentConversionPropertiesFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\OrganizationContentConversionCache.cs");
			ADObjectId entryId;
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				Organization orgContainer = tenantOrTopologyConfigurationSession.GetOrgContainer();
				entryId = orgContainer.Id;
			}
			else
			{
				entryId = organizationId.ConfigurationUnit;
			}
			ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(entryId, OrganizationContentConversionCache.orgProperties);
			if (adrawEntry == null)
			{
				OrganizationContentConversionCache.Tracer.TraceError<OrganizationId>(0L, "CU for the org '{0}' is not found in AD!", organizationId);
				organizationContentConversionProperties.ValidOrganization = false;
			}
			else
			{
				OrganizationContentConversionCache.Tracer.TraceDebug<OrganizationId>(0L, "CU for '{0}' located in AD.", organizationId);
				organizationContentConversionProperties.ValidOrganization = true;
				organizationContentConversionProperties.PreferredInternetCodePageForShiftJis = Organization.MapIntToPreferredInternetCodePageForShiftJis((int)adrawEntry[OrganizationSchema.PreferredInternetCodePageForShiftJis]);
				organizationContentConversionProperties.RequiredCharsetCoverage = (int)adrawEntry[OrganizationSchema.RequiredCharsetCoverage];
				organizationContentConversionProperties.ByteEncoderTypeFor7BitCharsets = (int)adrawEntry[OrganizationSchema.ByteEncoderTypeFor7BitCharsets];
			}
			return organizationContentConversionProperties;
		}

		// Token: 0x040033E6 RID: 13286
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033E7 RID: 13287
		private static TimeoutCache<OrganizationId, OrganizationContentConversionProperties> orgPropertiesCache = new TimeoutCache<OrganizationId, OrganizationContentConversionProperties>(16, 1024, false);

		// Token: 0x040033E8 RID: 13288
		private static ADPropertyDefinition[] orgProperties = new ADPropertyDefinition[]
		{
			OrganizationSchema.PreferredInternetCodePageForShiftJis,
			OrganizationSchema.RequiredCharsetCoverage,
			OrganizationSchema.ByteEncoderTypeFor7BitCharsets
		};
	}
}
