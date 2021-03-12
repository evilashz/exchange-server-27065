using System;
using System.Collections.Concurrent;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200028E RID: 654
	internal static class ForestTenantRelocationsCache
	{
		// Token: 0x06001EDC RID: 7900 RVA: 0x0008A394 File Offset: 0x00088594
		public static bool IsTenantRelocationAllowed(string partitionFqdn)
		{
			return ForestTenantRelocationsCache.RelocationsAllowedCache.IsTenantRelocationAllowed(partitionFqdn);
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x0008A39C File Offset: 0x0008859C
		public static void UpdateTenantRelocationAllowedValue(Organization rootOrganization)
		{
			ForestTenantRelocationsCache.RelocationsAllowedCache.UpdateTenantRelocationAllowedValue(rootOrganization);
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x0008A3A4 File Offset: 0x000885A4
		public static string GetRidMasterName(PartitionId partitionId)
		{
			return ForestTenantRelocationsCache.RidMastersCache.GetRidMasterName(partitionId, false);
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x0008A3AD File Offset: 0x000885AD
		public static string RefreshRidMasterName(PartitionId partitionId)
		{
			return ForestTenantRelocationsCache.RidMastersCache.GetRidMasterName(partitionId, true);
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x0008A3B6 File Offset: 0x000885B6
		internal static void Reset()
		{
			ForestTenantRelocationsCache.RidMastersCache.Reset();
			ForestTenantRelocationsCache.RelocationsAllowedCache.Reset();
		}

		// Token: 0x0200028F RID: 655
		private class RelocationsAllowedCache
		{
			// Token: 0x06001EE1 RID: 7905 RVA: 0x0008A3C2 File Offset: 0x000885C2
			private static ForestTenantRelocationsCache.RelocationsAllowedCache GetInstance()
			{
				if (ForestTenantRelocationsCache.RelocationsAllowedCache.instance == null)
				{
					ForestTenantRelocationsCache.RelocationsAllowedCache.instance = new ForestTenantRelocationsCache.RelocationsAllowedCache();
				}
				return ForestTenantRelocationsCache.RelocationsAllowedCache.instance;
			}

			// Token: 0x06001EE2 RID: 7906 RVA: 0x0008A3DA File Offset: 0x000885DA
			internal static void Reset()
			{
				ForestTenantRelocationsCache.RelocationsAllowedCache.instance = null;
			}

			// Token: 0x06001EE3 RID: 7907 RVA: 0x0008A3E4 File Offset: 0x000885E4
			public static void UpdateTenantRelocationAllowedValue(Organization rootOrganization)
			{
				if (!OrganizationId.ForestWideOrgId.Equals(rootOrganization.OrganizationId))
				{
					throw new ArgumentException("rootOrganization parameter value must be root Organization");
				}
				string forestFQDN = rootOrganization.Id.GetPartitionId().ForestFQDN;
				bool value = rootOrganization.TenantRelocationsAllowed;
				ForestTenantRelocationsCache.RelocationsAllowedCache.GetInstance().tenantRelocationsAllowed[forestFQDN] = new ExpiringTenantRelocationsAllowedValue(value);
			}

			// Token: 0x06001EE4 RID: 7908 RVA: 0x0008A43C File Offset: 0x0008863C
			public static bool IsTenantRelocationAllowed(string partitionFqdn)
			{
				if (string.IsNullOrEmpty(partitionFqdn))
				{
					throw new ArgumentNullException("partitionFqdn");
				}
				if (!Datacenter.IsMultiTenancyEnabled())
				{
					return false;
				}
				ForestTenantRelocationsCache.RelocationsAllowedCache relocationsAllowedCache = ForestTenantRelocationsCache.RelocationsAllowedCache.GetInstance();
				ExpiringTenantRelocationsAllowedValue expiringTenantRelocationsAllowedValue;
				if (!relocationsAllowedCache.tenantRelocationsAllowed.TryGetValue(partitionFqdn, out expiringTenantRelocationsAllowedValue) || expiringTenantRelocationsAllowedValue.Expired)
				{
					Organization rootOrgContainer = ADSystemConfigurationSession.GetRootOrgContainer(partitionFqdn, null, null);
					expiringTenantRelocationsAllowedValue = new ExpiringTenantRelocationsAllowedValue(rootOrgContainer.TenantRelocationsAllowed);
					relocationsAllowedCache.tenantRelocationsAllowed[partitionFqdn] = expiringTenantRelocationsAllowedValue;
				}
				return expiringTenantRelocationsAllowedValue.Value;
			}

			// Token: 0x04001270 RID: 4720
			private static ForestTenantRelocationsCache.RelocationsAllowedCache instance;

			// Token: 0x04001271 RID: 4721
			private ConcurrentDictionary<string, ExpiringTenantRelocationsAllowedValue> tenantRelocationsAllowed = new ConcurrentDictionary<string, ExpiringTenantRelocationsAllowedValue>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x02000290 RID: 656
		private class RidMastersCache
		{
			// Token: 0x06001EE6 RID: 7910 RVA: 0x0008A4C3 File Offset: 0x000886C3
			private static ForestTenantRelocationsCache.RidMastersCache GetInstance()
			{
				if (ForestTenantRelocationsCache.RidMastersCache.instance == null)
				{
					ForestTenantRelocationsCache.RidMastersCache.instance = new ForestTenantRelocationsCache.RidMastersCache();
				}
				return ForestTenantRelocationsCache.RidMastersCache.instance;
			}

			// Token: 0x06001EE7 RID: 7911 RVA: 0x0008A4DB File Offset: 0x000886DB
			internal static void Reset()
			{
				ForestTenantRelocationsCache.RidMastersCache.instance = null;
			}

			// Token: 0x06001EE8 RID: 7912 RVA: 0x0008A4E4 File Offset: 0x000886E4
			public static string GetRidMasterName(PartitionId partitionId, bool forceRefresh)
			{
				if (partitionId == null)
				{
					throw new ArgumentNullException("partitionId");
				}
				ForestTenantRelocationsCache.RidMastersCache ridMastersCache = ForestTenantRelocationsCache.RidMastersCache.GetInstance();
				ExpiringRidMasterNameValue expiringRidMasterNameValue;
				if (forceRefresh || !ridMastersCache.ridMasters.TryGetValue(partitionId.ForestFQDN, out expiringRidMasterNameValue) || expiringRidMasterNameValue.Expired)
				{
					string text = ForestTenantRelocationsCache.RidMastersCache.FindRidMasterNameForPartition(partitionId);
					if (text == null)
					{
						throw new ADTransientException(DirectoryStrings.ErrorCannotFindRidMasterForPartition(partitionId.ForestFQDN));
					}
					expiringRidMasterNameValue = new ExpiringRidMasterNameValue(text);
					ridMastersCache.ridMasters[partitionId.ForestFQDN] = expiringRidMasterNameValue;
				}
				return expiringRidMasterNameValue.Value;
			}

			// Token: 0x06001EE9 RID: 7913 RVA: 0x0008A568 File Offset: 0x00088768
			private static string FindRidMasterNameForPartition(PartitionId partitionId)
			{
				string result = null;
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 213, "FindRidMasterNameForPartition", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\RelocationCache\\ForestTenantRelocationsCache.cs");
				topologyConfigurationSession.UseConfigNC = false;
				RidManagerContainer[] array = topologyConfigurationSession.Find<RidManagerContainer>(null, QueryScope.SubTree, null, null, 1);
				if (array != null && array.Length > 0)
				{
					ADObjectId fsmoRoleOwner = array[0].FsmoRoleOwner;
					if (fsmoRoleOwner != null)
					{
						topologyConfigurationSession.UseConfigNC = true;
						ADServer adserver = topologyConfigurationSession.Read<ADServer>(fsmoRoleOwner.Parent);
						if (adserver != null)
						{
							result = adserver.DnsHostName;
						}
					}
				}
				return result;
			}

			// Token: 0x04001272 RID: 4722
			private static ForestTenantRelocationsCache.RidMastersCache instance;

			// Token: 0x04001273 RID: 4723
			private ConcurrentDictionary<string, ExpiringRidMasterNameValue> ridMasters = new ConcurrentDictionary<string, ExpiringRidMasterNameValue>(StringComparer.OrdinalIgnoreCase);
		}
	}
}
