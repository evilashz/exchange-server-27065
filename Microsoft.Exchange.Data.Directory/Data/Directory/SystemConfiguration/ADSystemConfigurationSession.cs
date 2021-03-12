using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationCache;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000396 RID: 918
	internal static class ADSystemConfigurationSession
	{
		// Token: 0x060029D9 RID: 10713 RVA: 0x000AF618 File Offset: 0x000AD818
		public static ITopologyConfigurationSession CreateRemoteForestSession(string fqdn, NetworkCredential credential)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			if (credential != null && (string.IsNullOrEmpty(credential.UserName) || string.IsNullOrEmpty(credential.Password)))
			{
				throw new ArgumentException("credential");
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, credential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(new PartitionId(fqdn)), 69, "CreateRemoteForestSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADSystemConfigurationSession.cs");
			ADServerInfo remoteServerFromDomainFqdn = TopologyProvider.GetInstance().GetRemoteServerFromDomainFqdn(fqdn, credential);
			topologyConfigurationSession.DomainController = remoteServerFromDomainFqdn.Fqdn;
			topologyConfigurationSession.EnforceDefaultScope = false;
			return topologyConfigurationSession;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000AF6A8 File Offset: 0x000AD8A8
		internal static ADObjectId GetFirstOrgUsersContainerId()
		{
			if (ADSystemConfigurationSession.firstOrgUsersContainerId == null)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 95, "GetFirstOrgUsersContainerId", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADSystemConfigurationSession.cs");
				topologyConfigurationSession.UseConfigNC = false;
				ADObjectId domainNamingContext = topologyConfigurationSession.GetDomainNamingContext();
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = topologyConfigurationSession.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.UsersWkGuid, domainNamingContext);
				ADObjectId id = exchangeOrganizationalUnit.Id;
				lock (ADSystemConfigurationSession.syncObj)
				{
					if (ADSystemConfigurationSession.firstOrgUsersContainerId == null)
					{
						ADSystemConfigurationSession.firstOrgUsersContainerId = id;
					}
				}
			}
			return ADSystemConfigurationSession.firstOrgUsersContainerId;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000AF740 File Offset: 0x000AD940
		internal static ADObjectId GetRootOrgContainerIdForLocalForest()
		{
			return ADSystemConfigurationSession.GetRootOrgContainerId(TopologyProvider.LocalForestFqdn, null, null);
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000AF74E File Offset: 0x000AD94E
		internal static ADObjectId GetRootOrgContainerId(string domainController, NetworkCredential credential)
		{
			return ADSystemConfigurationSession.GetRootOrgContainerId(TopologyProvider.LocalForestFqdn, domainController, credential);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000AF75C File Offset: 0x000AD95C
		internal static ADObjectId GetRootOrgContainerId(string partitionFqdn, string domainController, NetworkCredential credential)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ADObjectId rootOrgContainerId = InternalDirectoryRootOrganizationCache.GetRootOrgContainerId(partitionFqdn);
			if (rootOrgContainerId != null)
			{
				return rootOrgContainerId;
			}
			Organization rootOrgContainer = ADSystemConfigurationSession.GetRootOrgContainer(partitionFqdn, domainController, credential);
			if (rootOrgContainer == null)
			{
				return ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			}
			return rootOrgContainer.Id;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000AF798 File Offset: 0x000AD998
		internal static Organization GetRootOrgContainer(string partitionFqdn, string domainController, NetworkCredential credential)
		{
			bool flag = string.IsNullOrEmpty(domainController);
			ADObjectId adobjectId;
			if (!PartitionId.IsLocalForestPartition(partitionFqdn))
			{
				adobjectId = ADSession.GetConfigurationNamingContext(partitionFqdn);
			}
			else if (flag)
			{
				adobjectId = ADSession.GetConfigurationNamingContextForLocalForest();
			}
			else
			{
				adobjectId = ADSession.GetConfigurationNamingContext(domainController, credential);
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgBootStrapSession(adobjectId);
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, flag ? null : credential, sessionSettings, 226, "GetRootOrgContainer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADSystemConfigurationSession.cs");
			Organization[] array = topologyConfigurationSession.Find<Organization>(adobjectId, QueryScope.SubTree, null, null, 1);
			if (array != null && array.Length > 0)
			{
				if (string.IsNullOrEmpty(domainController) && credential == null)
				{
					InternalDirectoryRootOrganizationCache.PopulateCache(partitionFqdn, array[0]);
				}
				return array[0];
			}
			if (flag && (Globals.IsDatacenter || PartitionId.IsLocalForestPartition(partitionFqdn)))
			{
				throw new OrgContainerNotFoundException();
			}
			RootDse rootDse = topologyConfigurationSession.GetRootDse();
			if (rootDse.ConfigurationNamingContext.Equals(ADSession.GetConfigurationNamingContext(partitionFqdn)))
			{
				throw new OrgContainerNotFoundException();
			}
			return null;
		}

		// Token: 0x0400198B RID: 6539
		public const string InformationStoreRDN = "InformationStore";

		// Token: 0x0400198C RID: 6540
		private static readonly object syncObj = new object();

		// Token: 0x0400198D RID: 6541
		private static ADObjectId firstOrgUsersContainerId = null;
	}
}
