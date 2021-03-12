using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020006D2 RID: 1746
	internal class RUSFinder
	{
		// Token: 0x060050D1 RID: 20689 RVA: 0x0012C13B File Offset: 0x0012A33B
		private RUSFinder()
		{
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0012C144 File Offset: 0x0012A344
		public RUSFinder(ExchangeTopology topology)
		{
			this.topo = topology;
			this.siteDictionary = new Dictionary<TopologySite, List<TopologyServer>>();
			this.siteList = new List<TopologySite>();
			ReadOnlyCollection<TopologyServer> allTopologyServers = this.topo.AllTopologyServers;
			int num = 0;
			int num2 = 0;
			foreach (TopologyServer topologyServer in allTopologyServers)
			{
				if (topologyServer.TopologySite != null && topologyServer.IsExchange2007OrLater && topologyServer.IsMailboxServer)
				{
					List<TopologyServer> list;
					if (!this.siteDictionary.TryGetValue(topologyServer.TopologySite, out list))
					{
						this.siteList.Add(topologyServer.TopologySite);
						list = new List<TopologyServer>();
						this.siteDictionary.Add(topologyServer.TopologySite, list);
						num++;
					}
					list.Add(topologyServer);
					num2++;
					if (ExTraceGlobals.RecipientUpdateServiceTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.RecipientUpdateServiceTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Found RUS Server: {0} ({1}) in site {2}", topologyServer.Name, topologyServer.Fqdn, (topologyServer.TopologySite == null) ? "no site" : topologyServer.TopologySite.Name);
					}
				}
			}
			ExTraceGlobals.RecipientUpdateServiceTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Found {0} potential RUS Servers in {1} sites.", num2, num);
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0012C298 File Offset: 0x0012A498
		public TopologySite ClosestSite(TopologySite sourceSite)
		{
			TopologySite topologySite = this.topo.FindClosestDestinationSite(sourceSite, this.siteList);
			if (topologySite != null)
			{
				this.siteList.Remove(topologySite);
			}
			ExTraceGlobals.RecipientUpdateServiceTracer.TraceDebug<string>((long)this.GetHashCode(), "ClosestSite returned site {0}.", (topologySite == null) ? "no site" : topologySite.Name);
			return topologySite;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x0012C2F0 File Offset: 0x0012A4F0
		public List<TopologyServer> ServerList(TopologySite site)
		{
			List<TopologyServer> result;
			if (!this.siteDictionary.TryGetValue(site, out result))
			{
				result = new List<TopologyServer>();
			}
			return result;
		}

		// Token: 0x040036DF RID: 14047
		private ExchangeTopology topo;

		// Token: 0x040036E0 RID: 14048
		private Dictionary<TopologySite, List<TopologyServer>> siteDictionary;

		// Token: 0x040036E1 RID: 14049
		private List<TopologySite> siteList;
	}
}
