using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationCache
{
	// Token: 0x0200065A RID: 1626
	internal static class InternalDirectoryRootOrganizationCache
	{
		// Token: 0x06004C1A RID: 19482 RVA: 0x00119120 File Offset: 0x00117320
		internal static ADObjectId GetRootOrgContainerId(string partitionFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ADObjectIdCachableItem adobjectIdCachableItem;
			if (InternalDirectoryRootOrganizationCache.rootOrgContainerIds.TryGetValue(partitionFqdn, out adobjectIdCachableItem))
			{
				return adobjectIdCachableItem.Value;
			}
			return null;
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0011914F File Offset: 0x0011734F
		internal static TenantCULocation GetTenantCULocation(string partitionFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			return InternalDirectoryRootOrganizationCache.InternalGetTenantCULocation(partitionFqdn);
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x00119164 File Offset: 0x00117364
		internal static void PopulateCache(string partitionFqdn, Organization rootOrganization)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNull("organization", rootOrganization);
			if (InternalDirectoryRootOrganizationCache.InternalGetTenantCULocation(partitionFqdn) == TenantCULocation.Undefined)
			{
				TenantCULocation value;
				if (Globals.IsDatacenter)
				{
					value = ((rootOrganization.ForestMode == ForestModeFlags.Legacy) ? TenantCULocation.ConfigNC : TenantCULocation.DomainNC);
				}
				else
				{
					value = TenantCULocation.ConfigNC;
				}
				InternalDirectoryRootOrganizationCache.tenantCULocations.TryAdd(partitionFqdn, value);
			}
			InternalDirectoryRootOrganizationCache.rootOrgContainerIds.TryAdd(partitionFqdn, new ADObjectIdCachableItem(rootOrganization.Id));
			ForestTenantRelocationsCache.UpdateTenantRelocationAllowedValue(rootOrganization);
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x001191D4 File Offset: 0x001173D4
		internal static void InitializeForestModeFlagForSetup(string partitionFqdn, TenantCULocation cuLocation)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			if (cuLocation == TenantCULocation.Undefined)
			{
				throw new ArgumentOutOfRangeException("cuLocation has invalid value");
			}
			if (InternalDirectoryRootOrganizationCache.InternalGetTenantCULocation(partitionFqdn) == TenantCULocation.Undefined)
			{
				InternalDirectoryRootOrganizationCache.tenantCULocations.TryAdd(partitionFqdn, cuLocation);
			}
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x00119214 File Offset: 0x00117414
		private static TenantCULocation InternalGetTenantCULocation(string partitionFqdn)
		{
			TenantCULocation result;
			if (InternalDirectoryRootOrganizationCache.tenantCULocations.TryGetValue(partitionFqdn, out result))
			{
				return result;
			}
			return TenantCULocation.Undefined;
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x00119233 File Offset: 0x00117433
		[Conditional("DEBUG")]
		private static void Dbg_CheckCaller()
		{
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x00119238 File Offset: 0x00117438
		[Conditional("DEBUG")]
		private static void Dbg_CheckCaller(Func<string, string, bool> frameMatch)
		{
			StackTrace stackTrace = new StackTrace();
			foreach (StackFrame stackFrame in stackTrace.GetFrames())
			{
				MethodBase method = stackFrame.GetMethod();
				if (!(method.DeclaringType == null))
				{
					string fullName = method.DeclaringType.FullName;
					string name = method.Name;
					if (frameMatch(fullName, name))
					{
						break;
					}
				}
			}
		}

		// Token: 0x04003435 RID: 13365
		private const long RootOrgContainerIdSizeInBytes = 1048576L;

		// Token: 0x04003436 RID: 13366
		private const int RootOrgContainerIdCacheExpirationHours = 24;

		// Token: 0x04003437 RID: 13367
		private static ConcurrentDictionary<string, TenantCULocation> tenantCULocations = new ConcurrentDictionary<string, TenantCULocation>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003438 RID: 13368
		private static Cache<string, ADObjectIdCachableItem> rootOrgContainerIds = new Cache<string, ADObjectIdCachableItem>(1048576L, TimeSpan.FromHours(24.0), TimeSpan.Zero);
	}
}
