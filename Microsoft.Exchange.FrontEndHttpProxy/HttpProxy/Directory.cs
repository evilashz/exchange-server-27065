using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D7 RID: 215
	internal class Directory : IDirectory
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0002E164 File Offset: 0x0002C364
		public ADSite[] GetADSites()
		{
			ADSite[] sites = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 30, "GetADSites", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RpcHttp\\Directory.cs");
				ADPagedReader<ADSite> adpagedReader = topologyConfigurationSession.FindPaged<ADSite>(null, QueryScope.SubTree, null, null, 0);
				sites = adpagedReader.ReadAllPages();
			});
			return sites;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0002E1E8 File Offset: 0x0002C3E8
		public ClientAccessArray[] GetClientAccessArrays()
		{
			ClientAccessArray[] arrays = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 50, "GetClientAccessArrays", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RpcHttp\\Directory.cs");
				ADPagedReader<ClientAccessArray> adpagedReader = topologyConfigurationSession.FindPaged<ClientAccessArray>(null, QueryScope.SubTree, ClientAccessArray.PriorTo15ExchangeObjectVersionFilter, null, 0);
				arrays = adpagedReader.ReadAllPages();
			});
			return arrays;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0002E268 File Offset: 0x0002C468
		public Server[] GetServers()
		{
			Server[] servers = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 70, "GetServers", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RpcHttp\\Directory.cs");
				ADPagedReader<Server> adpagedReader = topologyConfigurationSession.FindPaged<Server>(null, QueryScope.SubTree, null, null, 0);
				servers = adpagedReader.ReadAllPages();
			});
			return servers;
		}
	}
}
