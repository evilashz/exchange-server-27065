using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006B7 RID: 1719
	internal sealed class ExchangeTopology
	{
		// Token: 0x06004F67 RID: 20327 RVA: 0x0012430C File Offset: 0x0012250C
		internal ExchangeTopology(DateTime discoveryStarted, ExchangeTopologyScope topologyScope, ReadOnlyCollection<TopologyServer> allTopologyServers, ReadOnlyCollection<TopologySite> allTopologySites, ReadOnlyCollection<TopologySiteLink> allTopologySiteLinks, ReadOnlyCollection<MiniVirtualDirectory> allVirtualDirectories, ReadOnlyCollection<MiniEmailTransport> allEmailTransports, ReadOnlyCollection<MiniReceiveConnector> allSmtpReceiveConnectors, ReadOnlyCollection<ADServer> allAdServers, Dictionary<string, TopologySite> aDServerSiteDictionary, Dictionary<string, ReadOnlyCollection<ADServer>> siteADServerDictionary, Dictionary<string, TopologySite> siteDictionary, string localServerFqdn)
		{
			this.discoveryStarted = discoveryStarted;
			this.topologyScope = topologyScope;
			this.allTopologyServers = allTopologyServers;
			this.allTopologySites = allTopologySites;
			this.allTopologySiteLinks = allTopologySiteLinks;
			this.allVirtualDirectories = allVirtualDirectories;
			this.allEmailTransports = allEmailTransports;
			this.allSmtpReceiveConnectors = allSmtpReceiveConnectors;
			this.aDServerSiteDictionary = aDServerSiteDictionary;
			this.siteADServerDictionary = siteADServerDictionary;
			this.whenCreated = DateTime.UtcNow;
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Creating ExchangeTopology for public consumption", 25525);
			this.exchangeServerDictionary = new Dictionary<string, TopologyServer>(this.allTopologyServers.Count, StringComparer.OrdinalIgnoreCase);
			foreach (TopologyServer topologyServer in this.allTopologyServers)
			{
				this.exchangeServerDictionary.Add(topologyServer.Id.DistinguishedName, topologyServer);
				if (this.localServer == null && string.Compare(localServerFqdn, topologyServer.Fqdn, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.localServer = topologyServer;
				}
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>(0L, "PFD ADPEXT {0} - The local server is {1}", 17333, (this.localServer != null) ? this.localServer.Fqdn : "<undefined>");
			if (this.localServer != null)
			{
				this.localSite = this.localServer.TopologySite;
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Local site: {0}", (this.localSite != null) ? this.localSite.Name : "none");
			}
			else
			{
				string siteName = NativeHelpers.GetSiteName(false);
				if (string.IsNullOrEmpty(siteName))
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug(0L, "Computer doesn't belong to any site");
				}
				else
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "GetSiteName returned: {0}", siteName);
					foreach (TopologySite topologySite in this.allTopologySites)
					{
						if (string.Compare(topologySite.Name, siteName) == 0)
						{
							this.localSite = topologySite;
							ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Local site: {0}", this.localSite.Name);
							break;
						}
					}
				}
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>(0L, "PFD ADPEXT {0} - The local site is {1}", 31669, (this.localSite != null) ? this.localSite.Name : "<undefined>");
			if (allAdServers != null)
			{
				this.adServerDictionary = new Dictionary<string, ADServer>(allAdServers.Count, StringComparer.OrdinalIgnoreCase);
				foreach (ADServer adserver in allAdServers)
				{
					this.adServerDictionary.Add(adserver.DnsHostName, adserver);
				}
			}
			if (ExTraceGlobals.ExchangeTopologyTracer.IsTraceEnabled(TraceType.PfdTrace))
			{
				foreach (TopologyServer topologyServer2 in this.allTopologyServers)
				{
					ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, string>((long)this.GetHashCode(), "PFD ADPEXT {0} - Server: {1} belongs to {2}", 23477, topologyServer2.Name, (topologyServer2.TopologySite == null) ? "no site" : topologyServer2.TopologySite.Name);
					ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, string>((long)this.GetHashCode(), "PFD ADPEXT {0} - Server: FQDN for {1} is {2}", 22453, topologyServer2.Name, topologyServer2.Fqdn);
				}
				foreach (TopologySite topologySite2 in this.allTopologySites)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ITopologySiteLink topologySiteLink in topologySite2.TopologySiteLinks)
					{
						TopologySiteLink topologySiteLink2 = (TopologySiteLink)topologySiteLink;
						stringBuilder.Append(topologySiteLink2.Name);
						stringBuilder.Append(", ");
					}
					ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, StringBuilder>((long)this.GetHashCode(), "PFD ADPEXT {0} - Site: {1} links to {2}", 30645, topologySite2.Name, stringBuilder);
				}
				foreach (TopologySiteLink topologySiteLink3 in this.allTopologySiteLinks)
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					foreach (ITopologySite topologySite3 in topologySiteLink3.TopologySites)
					{
						TopologySite topologySite4 = (TopologySite)topologySite3;
						stringBuilder2.Append(topologySite4.Name);
						stringBuilder2.Append(", ");
					}
					ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, StringBuilder>((long)this.GetHashCode(), "PFD ADPEXT {0} - SiteLink: {1} connects {2}", 19381, topologySiteLink3.Name, stringBuilder2);
				}
				if (this.allVirtualDirectories != null)
				{
					foreach (MiniVirtualDirectory miniVirtualDirectory in this.allVirtualDirectories)
					{
						ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, ADObjectId>((long)this.GetHashCode(), "PFD ADPEXT {0} - VirtualDirectory: {1} on {2}", 27573, miniVirtualDirectory.Name, miniVirtualDirectory.Server);
					}
				}
				if (this.allEmailTransports != null)
				{
					foreach (MiniEmailTransport miniEmailTransport in this.allEmailTransports)
					{
						ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>((long)this.GetHashCode(), "PFD ADPEXT {0} - Email Transport: {1}", 63987, miniEmailTransport.Name);
					}
				}
				if (this.allSmtpReceiveConnectors != null)
				{
					foreach (MiniReceiveConnector miniReceiveConnector in this.allSmtpReceiveConnectors)
					{
						ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>((long)this.GetHashCode(), "PFD ADPEXT {0} - SMTP Receive Connector: {1}", 47603, miniReceiveConnector.Name);
					}
				}
				if (allAdServers != null)
				{
					foreach (ADServer adserver2 in allAdServers)
					{
						ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string, AdName>((long)this.GetHashCode(), "PFD ADPEXT {0} - Domain Controller: {1} on Site {2}", 54149, adserver2.DnsHostName, adserver2.Id.Parent.Parent.Rdn);
					}
				}
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x001249D0 File Offset: 0x00122BD0
		public static ExchangeTopology RsoTopology
		{
			get
			{
				ExchangeTopology.RefreshRsoTopology();
				return ExchangeTopology.rsoTopology;
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06004F69 RID: 20329 RVA: 0x001249DC File Offset: 0x00122BDC
		public DateTime WhenCreated
		{
			get
			{
				return this.whenCreated;
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06004F6A RID: 20330 RVA: 0x001249E4 File Offset: 0x00122BE4
		public DateTime DiscoveryStarted
		{
			get
			{
				return this.discoveryStarted;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x001249EC File Offset: 0x00122BEC
		public ReadOnlyCollection<TopologyServer> AllTopologyServers
		{
			get
			{
				return this.allTopologyServers;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x001249F4 File Offset: 0x00122BF4
		public ReadOnlyCollection<TopologySite> AllTopologySites
		{
			get
			{
				return this.allTopologySites;
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x001249FC File Offset: 0x00122BFC
		public ReadOnlyCollection<TopologySiteLink> AllTopologySiteLinks
		{
			get
			{
				return this.allTopologySiteLinks;
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x00124A04 File Offset: 0x00122C04
		public ReadOnlyCollection<MiniVirtualDirectory> AllVirtualDirectories
		{
			get
			{
				return this.allVirtualDirectories;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06004F6F RID: 20335 RVA: 0x00124A0C File Offset: 0x00122C0C
		public ReadOnlyCollection<MiniEmailTransport> AllEmailTransports
		{
			get
			{
				return this.allEmailTransports;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00124A14 File Offset: 0x00122C14
		public ReadOnlyCollection<MiniReceiveConnector> AllSmtpReceiveConnectors
		{
			get
			{
				return this.allSmtpReceiveConnectors;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06004F71 RID: 20337 RVA: 0x00124A1C File Offset: 0x00122C1C
		public TopologyServer LocalServer
		{
			get
			{
				return this.localServer;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x00124A24 File Offset: 0x00122C24
		public TopologySite LocalSite
		{
			get
			{
				return this.localSite;
			}
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00124A2C File Offset: 0x00122C2C
		public static ExchangeTopology Discover()
		{
			return ExchangeTopology.Discover(null, ExchangeTopologyScope.Complete);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x00124A35 File Offset: 0x00122C35
		public static ExchangeTopology Discover(ITopologyConfigurationSession session)
		{
			return ExchangeTopology.Discover(session, ExchangeTopologyScope.Complete);
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x00124A3E File Offset: 0x00122C3E
		public static ExchangeTopology Discover(ExchangeTopologyScope scope)
		{
			return ExchangeTopology.Discover(null, scope);
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00124A48 File Offset: 0x00122C48
		public static ExchangeTopology Discover(ITopologyConfigurationSession session, ExchangeTopologyScope scope)
		{
			ExchangeTopologyDiscovery topologyDiscovery = ExchangeTopologyDiscovery.Create(session, scope);
			return ExchangeTopologyDiscovery.Populate(topologyDiscovery);
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x00124A64 File Offset: 0x00122C64
		public TopologySite FindClosestDestinationSite(TopologySite sourceSite, ICollection<TopologySite> destinationSites)
		{
			if (destinationSites == null || destinationSites.Count == 0)
			{
				return null;
			}
			if (sourceSite == null || destinationSites.Contains(sourceSite))
			{
				return sourceSite;
			}
			ReadOnlyCollection<TopologySite> sitesSortedByCostFromSite = this.GetSitesSortedByCostFromSite(sourceSite);
			foreach (TopologySite topologySite in sitesSortedByCostFromSite)
			{
				if (destinationSites.Contains(topologySite))
				{
					return topologySite;
				}
			}
			return null;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x00124AE0 File Offset: 0x00122CE0
		public TopologyServer GetTopologyServer(ADObjectId serverId)
		{
			TopologyServer topologyServer = null;
			this.exchangeServerDictionary.TryGetValue(serverId.DistinguishedName, out topologyServer);
			ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Mapped {0} to {1}", serverId.DistinguishedName, (topologyServer == null) ? "<null>" : topologyServer.Name);
			return topologyServer;
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x00124B30 File Offset: 0x00122D30
		public ADServer GetAdServer(string fqdn)
		{
			if (this.topologyScope != ExchangeTopologyScope.Complete && this.topologyScope != ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology)
			{
				throw new InvalidOperationException("GetAdSever is only supported for Complete and ADAndExchangeServerAndSiteTopology scopes");
			}
			ADServer adserver = null;
			this.adServerDictionary.TryGetValue(fqdn, out adserver);
			ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Mapped {0} to {1}", fqdn, (adserver == null) ? "<null>" : adserver.DistinguishedName);
			return adserver;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00124B92 File Offset: 0x00122D92
		public override string ToString()
		{
			return string.Format("ExchangeTopology generated at {0} with {1} servers", this.whenCreated, this.allTopologyServers.Count);
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00124BBC File Offset: 0x00122DBC
		public TopologySite SiteFromADServer(string fqdn)
		{
			if (this.topologyScope != ExchangeTopologyScope.Complete && this.topologyScope != ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology)
			{
				throw new InvalidOperationException("SiteFromADServer is only supported for Complete and ADAndExchangeServerAndSiteTopology scopes");
			}
			TopologySite topologySite;
			if (!this.aDServerSiteDictionary.TryGetValue(fqdn, out topologySite))
			{
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "ADServer {0} is not in list of ADServers with sites.", fqdn);
				return null;
			}
			ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>(0L, "Found site {0} for ADServer {1}.", topologySite.Name, fqdn);
			return topologySite;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x00124C24 File Offset: 0x00122E24
		public ReadOnlyCollection<ADServer> ADServerFromSite(string siteDN)
		{
			if (this.topologyScope != ExchangeTopologyScope.Complete && this.topologyScope != ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology)
			{
				throw new InvalidOperationException("ADSeverFromSite is only supported for Complete and ADAndExchangeServerAndSiteTopology scopes");
			}
			ReadOnlyCollection<ADServer> readOnlyCollection = null;
			if (!this.siteADServerDictionary.TryGetValue(siteDN, out readOnlyCollection))
			{
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "{0} is not a valid key in the ADSite-ADServer list.", siteDN);
				return new ReadOnlyCollection<ADServer>(new List<ADServer>());
			}
			ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>(0L, "Found ADServers from ADSite {0}. First DC is {1}", siteDN, (readOnlyCollection.Count > 0) ? readOnlyCollection[0].DnsHostName : "<null>");
			return readOnlyCollection;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00124CAB File Offset: 0x00122EAB
		public ReadOnlyCollection<TopologySite> GetSitesSortedByCostFromSite(TopologySite sourceSite)
		{
			return new ReadOnlyCollection<TopologySite>(ExchangeTopologyDiscovery.OrderDestinationSites(this.allTopologySites, sourceSite, this.allTopologySites));
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00124CC4 File Offset: 0x00122EC4
		private static void RefreshRsoTopology()
		{
			if (ExchangeTopology.rsoTopology == null)
			{
				lock (ExchangeTopology.rsoTopologyLock)
				{
					if (ExchangeTopology.rsoTopology == null)
					{
						ExchangeTopology.rsoTopology = ExchangeTopology.Discover(ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology);
					}
					return;
				}
			}
			if (ExchangeTopology.rsoTopology.whenCreated.AddMinutes(10.0).CompareTo(DateTime.UtcNow) < 0)
			{
				lock (ExchangeTopology.rsoTopologyLock)
				{
					if (ExchangeTopology.rsoTopology.whenCreated.AddMinutes(10.0).CompareTo(DateTime.UtcNow) < 0)
					{
						ExchangeTopology exchangeTopology = ExchangeTopology.Discover(ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology);
						ExchangeTopology.rsoTopology = exchangeTopology;
					}
				}
			}
		}

		// Token: 0x04003635 RID: 13877
		private static ExchangeTopology rsoTopology = null;

		// Token: 0x04003636 RID: 13878
		private static object rsoTopologyLock = new object();

		// Token: 0x04003637 RID: 13879
		private readonly DateTime whenCreated;

		// Token: 0x04003638 RID: 13880
		private readonly DateTime discoveryStarted;

		// Token: 0x04003639 RID: 13881
		private readonly ExchangeTopologyScope topologyScope;

		// Token: 0x0400363A RID: 13882
		private ReadOnlyCollection<TopologyServer> allTopologyServers;

		// Token: 0x0400363B RID: 13883
		private ReadOnlyCollection<TopologySite> allTopologySites;

		// Token: 0x0400363C RID: 13884
		private ReadOnlyCollection<TopologySiteLink> allTopologySiteLinks;

		// Token: 0x0400363D RID: 13885
		private ReadOnlyCollection<MiniVirtualDirectory> allVirtualDirectories;

		// Token: 0x0400363E RID: 13886
		private ReadOnlyCollection<MiniEmailTransport> allEmailTransports;

		// Token: 0x0400363F RID: 13887
		private ReadOnlyCollection<MiniReceiveConnector> allSmtpReceiveConnectors;

		// Token: 0x04003640 RID: 13888
		private TopologyServer localServer;

		// Token: 0x04003641 RID: 13889
		private TopologySite localSite;

		// Token: 0x04003642 RID: 13890
		private Dictionary<string, TopologyServer> exchangeServerDictionary;

		// Token: 0x04003643 RID: 13891
		private Dictionary<string, ADServer> adServerDictionary;

		// Token: 0x04003644 RID: 13892
		private Dictionary<string, TopologySite> aDServerSiteDictionary;

		// Token: 0x04003645 RID: 13893
		private Dictionary<string, ReadOnlyCollection<ADServer>> siteADServerDictionary;
	}
}
