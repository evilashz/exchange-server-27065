using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200000E RID: 14
	internal class ADTopologyLookup
	{
		// Token: 0x060000EC RID: 236 RVA: 0x000052E0 File Offset: 0x000034E0
		public static ADTopologyLookup CreateLocalResourceForestLookup()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			ITopologyConfigurationSession topologyConfigurationSession = new LatencyStopwatch().Invoke<ITopologyConfigurationSession>("DirectorySessionFactory.CreateTopologyConfigurationSession", () => DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 77, "CreateLocalResourceForestLookup", "f:\\15.00.1497\\sources\\dev\\um\\src\\umcommon\\ADTopologyLookup.cs"));
			return new ADTopologyLookup(topologyConfigurationSession);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000534C File Offset: 0x0000354C
		public static ADTopologyLookup CreateTenantResourceForestLookup(Guid orgId)
		{
			string resourceForestFqdnByExternalDirectoryOrganizationId = ADAccountPartitionLocator.GetResourceForestFqdnByExternalDirectoryOrganizationId(orgId);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "ADTopologyLookup: constructor - resource forest fqdn = {0}", new object[]
			{
				resourceForestFqdnByExternalDirectoryOrganizationId
			});
			PartitionId partitionId = PartitionId.LocalForest;
			if (resourceForestFqdnByExternalDirectoryOrganizationId != null)
			{
				partitionId = new PartitionId(resourceForestFqdnByExternalDirectoryOrganizationId);
			}
			ITopologyConfigurationSession topologyConfigurationSession = new LatencyStopwatch().Invoke<ITopologyConfigurationSession>("DirectorySessionFactory.CreateTopologyConfigurationSession", () => DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 101, "CreateTenantResourceForestLookup", "f:\\15.00.1497\\sources\\dev\\um\\src\\umcommon\\ADTopologyLookup.cs"));
			return new ADTopologyLookup(topologyConfigurationSession);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000053BE File Offset: 0x000035BE
		private ADTopologyLookup(ITopologyConfigurationSession session)
		{
			this.session = session;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000053F0 File Offset: 0x000035F0
		public Server GetLocalServer()
		{
			return this.InvokeWithStopwatch<Server>("ITopologyConfigurationSession.FindLocalServer", () => this.session.FindLocalServer());
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005409 File Offset: 0x00003609
		public SIPFEServerConfiguration GetLocalCallRouterSettings()
		{
			return SIPFEServerConfiguration.Find();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005440 File Offset: 0x00003640
		public IEnumerable<Server> GetEnabledExchangeServers(VersionEnum version, ServerRole role)
		{
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, CommonUtil.GetCompatibleServersWithRole(version, role), null, 100));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000054A4 File Offset: 0x000036A4
		public IEnumerable<Server> GetDatabaseAvailabilityGroupServers(VersionEnum version, ServerRole role, ADObjectId dag)
		{
			if (dag == null)
			{
				throw new ArgumentNullException("dag is null");
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				CommonUtil.GetCompatibleServersWithRole(version, role),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DatabaseAvailabilityGroup, dag)
			});
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, queryFilter, null, 100));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005534 File Offset: 0x00003734
		public IEnumerable<Server> GetAllUMServers()
		{
			ExAssert.RetailAssert(!this.isDatacenter, "This method is not intended to be used in Datacenter Environments");
			QueryFilter filter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 16UL);
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 100));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000055B0 File Offset: 0x000037B0
		public IEnumerable<Server> GetEnabledExchangeServers(VersionEnum version, ServerRole role, ADObjectId siteId)
		{
			if (siteId == null)
			{
				throw new ArgumentNullException("siteId is null");
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				CommonUtil.GetCompatibleServersWithRole(version, role),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, siteId)
			});
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, queryFilter, null, 100));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000561C File Offset: 0x0000381C
		public IEnumerable<Server> GetEnabledUMServersInSite(VersionEnum version, ADObjectId siteId)
		{
			IEnumerable<Server> enabledExchangeServers;
			if (this.isDatacenter)
			{
				enabledExchangeServers = this.GetEnabledExchangeServers(VersionEnum.Compatible, ServerRole.UnifiedMessaging, siteId);
			}
			else
			{
				enabledExchangeServers = this.GetEnabledExchangeServers(VersionEnum.Compatible, ServerRole.UnifiedMessaging);
			}
			return enabledExchangeServers;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005670 File Offset: 0x00003870
		public IEnumerable<Server> GetAllCafeServers()
		{
			ExAssert.RetailAssert(!this.isDatacenter, "This method is not intended to be used in Datacenter Environments");
			QueryFilter filter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 1UL);
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000056EC File Offset: 0x000038EC
		public IEnumerable<Server> GetEnabledUMServersInDialPlan(VersionEnum version, ADObjectId dialPlanId)
		{
			if (dialPlanId == null)
			{
				throw new ArgumentNullException("dialPlanId");
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				CommonUtil.GetCompatibleServersWithRole(version, ServerRole.UnifiedMessaging),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DialPlans, dialPlanId)
			});
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, queryFilter, null, 100));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005758 File Offset: 0x00003958
		public IEnumerable<Server> GetEnabledUMServers(VersionEnum version, ADObjectId dialPlanId, ADObjectId siteId, bool siteAffinityPreferred)
		{
			IEnumerable<Server> enumerable = null;
			if (this.isDatacenter)
			{
				enumerable = this.GetEnabledExchangeServers(version, ServerRole.UnifiedMessaging, siteId);
			}
			else
			{
				if (siteAffinityPreferred)
				{
					Server[] array = ((ADGenericPagedReader<Server>)this.GetEnabledUMServersInDialPlan(version, dialPlanId, siteId)).ReadAllPages();
					if (array.Length > 0)
					{
						enumerable = array;
					}
				}
				if (enumerable == null)
				{
					enumerable = this.GetEnabledUMServersInDialPlan(version, dialPlanId);
				}
			}
			return enumerable;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000057D0 File Offset: 0x000039D0
		public Server GetServerFromName(string serverName)
		{
			if (serverName == null)
			{
				throw new ArgumentNullException("serverName");
			}
			Server result = null;
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverName),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.Fqdn, serverName)
			});
			Server[] array = this.InvokeWithStopwatch<Server[]>("ITopologyConfigurationSession.Find<Server>", () => this.session.Find<Server>(null, QueryScope.SubTree, filter, null, 0));
			if (array != null && array.Length == 1)
			{
				result = array[0];
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005874 File Offset: 0x00003A74
		public Server GetServerFromId(ADObjectId serverId)
		{
			if (serverId == null)
			{
				throw new ArgumentNullException("serverId");
			}
			return this.InvokeWithStopwatch<Server>("ITopologyConfigurationSession.Read<Server>(", () => this.session.Read<Server>(serverId));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000058E0 File Offset: 0x00003AE0
		public UMServer GetUmServerFromId(ADObjectId serverId)
		{
			if (serverId == null)
			{
				throw new ArgumentNullException("serverId");
			}
			Server server = this.InvokeWithStopwatch<Server>("ITopologyConfigurationSession.Read<Server>(", () => this.session.Read<Server>(serverId));
			if (server != null && (server.CurrentServerRole & ServerRole.UnifiedMessaging) == ServerRole.UnifiedMessaging)
			{
				return new UMServer(server);
			}
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005954 File Offset: 0x00003B54
		public int GetLocalPartnerId()
		{
			ADSite adsite = this.InvokeWithStopwatch<ADSite>("ITopologyConfigurationSession.GetLocalSite", () => this.session.GetLocalSite());
			if (adsite == null)
			{
				return -1;
			}
			return adsite.PartnerId;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000059AC File Offset: 0x00003BAC
		private IEnumerable<Server> GetEnabledUMServersInDialPlan(VersionEnum version, ADObjectId dialPlanId, ADObjectId siteId)
		{
			if (dialPlanId == null)
			{
				throw new ArgumentNullException("dialPlanId");
			}
			if (siteId == null)
			{
				throw new ArgumentNullException("siteId");
			}
			QueryFilter queryFilter = new AndFilter(new QueryFilter[]
			{
				CommonUtil.GetCompatibleServersWithRole(version, ServerRole.UnifiedMessaging),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DialPlans, dialPlanId),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, siteId)
			});
			return this.InvokeWithStopwatch<ADPagedReader<Server>>("ITopologyConfigurationSession.FindPaged<Server>", () => this.session.FindPaged<Server>(null, QueryScope.SubTree, queryFilter, null, 100));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A35 File Offset: 0x00003C35
		protected T InvokeWithStopwatch<T>(string operationName, Func<T> func)
		{
			return this.latencyStopwatch.Invoke<T>(operationName, func);
		}

		// Token: 0x04000027 RID: 39
		private LatencyStopwatch latencyStopwatch = new LatencyStopwatch();

		// Token: 0x04000028 RID: 40
		private readonly ITopologyConfigurationSession session;

		// Token: 0x04000029 RID: 41
		private readonly bool isDatacenter = CommonConstants.DataCenterADPresent;
	}
}
