using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x020006B8 RID: 1720
	internal sealed class ExchangeTopologyDiscovery
	{
		// Token: 0x06004F80 RID: 20352 RVA: 0x00124DBE File Offset: 0x00122FBE
		private ExchangeTopologyDiscovery(DateTime discoveryStarted, ExchangeTopologyScope topologyScope)
		{
			this.discoveryStarted = discoveryStarted;
			this.topologyScope = topologyScope;
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x00124DD4 File Offset: 0x00122FD4
		public DateTime DiscoveryStarted
		{
			get
			{
				return this.discoveryStarted;
			}
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x00124DDC File Offset: 0x00122FDC
		internal static bool IncludeServices(ExchangeTopologyScope topologyScope)
		{
			return topologyScope == ExchangeTopologyScope.Complete;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x00124DE2 File Offset: 0x00122FE2
		internal static bool IncludeADServers(ExchangeTopologyScope topologyScope)
		{
			return topologyScope == ExchangeTopologyScope.Complete || topologyScope == ExchangeTopologyScope.ADAndExchangeServerAndSiteTopology;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x00124DF0 File Offset: 0x00122FF0
		internal static ExchangeTopologyDiscovery Create(ITopologyConfigurationSession session, ExchangeTopologyScope scope)
		{
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, ExchangeTopologyScope, string>(0L, "PFD ADPEXT {0} - Exchange Topology discovery with scope {1} started using {2} AD session", 21429, scope, (session != null) ? "caller specified" : "internally created");
			if (session == null)
			{
				session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 155, "Create", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ExchangeTopology\\ExchangeTopologyDiscovery.cs");
			}
			ExchangeTopologyDiscovery exchangeTopologyDiscovery = new ExchangeTopologyDiscovery(DateTime.UtcNow, scope);
			exchangeTopologyDiscovery.ReadFromAD(session);
			return exchangeTopologyDiscovery;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00124E5B File Offset: 0x0012305B
		internal static ExchangeTopology Populate(ExchangeTopologyDiscovery topologyDiscovery)
		{
			topologyDiscovery.BuildInitialLinks();
			topologyDiscovery.MatchServersWithSites();
			topologyDiscovery.CreateTopology();
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, int>(0L, "PFD ADPEXT {0} - Exchange Topology discovery complete (hash code: {1})", 28341, topologyDiscovery.preparedTopology.GetHashCode());
			return topologyDiscovery.preparedTopology;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x00124E98 File Offset: 0x00123098
		private void ReadFromAD(ITopologyConfigurationSession session)
		{
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Reading topology-related objects from the Active Directory", 29621);
			this.sites = new List<ADSite>();
			this.siteLinks = new List<ADSiteLink>();
			this.servers = new List<MiniTopologyServer>();
			this.virtualDirectories = new List<MiniVirtualDirectory>();
			this.emailTransports = new List<MiniEmailTransport>();
			this.smtpReceiveConnectors = new List<MiniReceiveConnector>();
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Getting the local computer FQDN", 21685);
			this.localServerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>(0L, "PFD ADPEXT {0} - Local computer FQDN is {1}", 29877, this.localServerFqdn);
			bool flag = ExchangeTopologyDiscovery.IncludeServices(this.topologyScope);
			bool flag2 = ExchangeTopologyDiscovery.IncludeADServers(this.topologyScope);
			string text = "CN=Sites," + session.ConfigurationNamingContext.DistinguishedName;
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>(0L, "PFD ADPEXT {0} - Reading all Active Directory sites under {1}", 19637, text);
			ADObjectId rootId = new ADObjectId(text, Guid.Empty);
			ADPagedReader<ADSite> adpagedReader = session.FindPaged<ADSite>(rootId, QueryScope.OneLevel, null, null, 0);
			foreach (ADSite adsite in adpagedReader)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found site {0}", adsite.Name);
				this.sites.Add(adsite);
			}
			string text2 = "CN=IP,CN=Inter-Site Transports," + text;
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, string>(0L, "PFD ADPEXT {0} - Reading all IP Active Directory site links under {1}", 23733, text2);
			ADPagedReader<ADSiteLink> adpagedReader2 = session.FindPaged<ADSiteLink>(new ADObjectId(text2, Guid.Empty), QueryScope.OneLevel, null, null, 0);
			foreach (ADSiteLink adsiteLink in adpagedReader2)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found site link {0}", adsiteLink.Name);
				this.siteLinks.Add(adsiteLink);
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Reading all Exchange servers from the Active Directory", 31925);
			ADPagedReader<MiniTopologyServer> adpagedReader3 = session.FindPaged<MiniTopologyServer>(session.ConfigurationNamingContext, QueryScope.SubTree, null, null, 0, null);
			foreach (MiniTopologyServer miniTopologyServer in adpagedReader3)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found server {0}", miniTopologyServer.Name);
				this.servers.Add(miniTopologyServer);
			}
			if (flag)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Reading all Exchange virtual directories from the Active Directory", 17589);
				ADPagedReader<MiniVirtualDirectory> adpagedReader4 = session.FindPaged<MiniVirtualDirectory>(session.ConfigurationNamingContext, QueryScope.SubTree, null, null, 0, null);
				foreach (MiniVirtualDirectory miniVirtualDirectory in adpagedReader4)
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found virtual directory {0}", miniVirtualDirectory.Name);
					this.virtualDirectories.Add(miniVirtualDirectory);
				}
			}
			if (flag)
			{
				ADPagedReader<MiniEmailTransport> adpagedReader5 = session.FindPaged<MiniEmailTransport>(session.ConfigurationNamingContext, QueryScope.SubTree, null, null, 0, null);
				foreach (MiniEmailTransport miniEmailTransport in adpagedReader5)
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found email transport configuration {0}", miniEmailTransport.Name);
					this.emailTransports.Add(miniEmailTransport);
				}
			}
			if (flag2)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Reading all Active Directory domain controllers", 25781);
				this.aDServers = session.FindServerWithNtdsdsa(null, false, false);
			}
			else
			{
				this.aDServers = new ReadOnlyCollection<ADServer>(new List<ADServer>());
			}
			if (flag)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchSmtpReceiveConnector"),
					new BitMaskAndFilter(ReceiveConnectorSchema.ProtocolOptions, 8192UL)
				});
				ADPagedReader<MiniReceiveConnector> adpagedReader6 = session.FindPaged<MiniReceiveConnector>(session.ConfigurationNamingContext, QueryScope.SubTree, filter, null, 0, null);
				foreach (MiniReceiveConnector miniReceiveConnector in adpagedReader6)
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Found receive connector configuration {0}", miniReceiveConnector.Name);
					this.smtpReceiveConnectors.Add(miniReceiveConnector);
				}
			}
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x001252FC File Offset: 0x001234FC
		private void BuildInitialLinks()
		{
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Creating topology objects and building links", 18613);
			this.aDServerSiteDictionary = new Dictionary<string, TopologySite>(StringComparer.OrdinalIgnoreCase);
			this.siteADServerDictionary = new Dictionary<string, List<ADServer>>(StringComparer.OrdinalIgnoreCase);
			this.topoSites = new List<TopologySite>(this.sites.Count);
			this.topoSiteLinks = new List<TopologySiteLink>(this.siteLinks.Count);
			this.siteDictionary = new Dictionary<string, TopologySite>(this.sites.Count, StringComparer.OrdinalIgnoreCase);
			this.topoServers = new List<TopologyServer>(this.servers.Count);
			Dictionary<TopologySite, List<ITopologySiteLink>> dictionary = new Dictionary<TopologySite, List<ITopologySiteLink>>(this.sites.Count);
			bool flag = ExchangeTopologyDiscovery.IncludeADServers(this.topologyScope);
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Creating TopologySites and hash them by site DN", 22709);
			foreach (ADSite site in this.sites)
			{
				TopologySite topologySite = new TopologySite(site);
				this.siteDictionary.Add(topologySite.DistinguishedName, topologySite);
				dictionary.Add(topologySite, new List<ITopologySiteLink>());
				this.topoSites.Add(topologySite);
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Creating TopologySiteLinks and filling the TopologySites collection", 30901);
			foreach (ADSiteLink adsiteLink in this.siteLinks)
			{
				TopologySiteLink topologySiteLink = new TopologySiteLink(adsiteLink);
				List<ITopologySite> list = new List<ITopologySite>(topologySiteLink.Sites.Count);
				foreach (ADObjectId adobjectId in topologySiteLink.Sites)
				{
					TopologySite topologySite2 = null;
					if (!this.siteDictionary.TryGetValue(adobjectId.DistinguishedName, out topologySite2))
					{
						ExTraceGlobals.ExchangeTopologyTracer.TraceWarning<string, string>(0L, "Site link {0} points to nonexistent site {1} (likely a transient replication issue). Ignoring this pointer.", adsiteLink.Name, adobjectId.DistinguishedName);
					}
					else
					{
						list.Add(topologySite2);
						dictionary[topologySite2].Add(topologySiteLink);
					}
				}
				topologySiteLink.TopologySites = new ReadOnlyCollection<ITopologySite>(list);
				this.topoSiteLinks.Add(topologySiteLink);
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Filling each site's TopologySiteLinks collection", 16565);
			foreach (TopologySite topologySite3 in this.topoSites)
			{
				List<ITopologySiteLink> list2 = null;
				if (!dictionary.TryGetValue(topologySite3, out list2))
				{
					if (this.topoSites.Count > 1)
					{
						ExTraceGlobals.ExchangeTopologyTracer.TraceWarning<string>(0L, "Site {0} is not linked to any sites.", topologySite3.Name);
					}
					list2 = new List<ITopologySiteLink>();
				}
				topologySite3.TopologySiteLinks = new ReadOnlyCollection<ITopologySiteLink>(list2);
			}
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Creating TopologyServers", 24757);
			foreach (MiniTopologyServer server in this.servers)
			{
				TopologyServer item = new TopologyServer(server);
				this.topoServers.Add(item);
			}
			if (flag)
			{
				ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Filling the ADServerSite dictionary with domain controller site mappings", 28853);
				foreach (ADServer adserver in this.aDServers)
				{
					string distinguishedName = adserver.Id.Parent.Parent.DistinguishedName;
					TopologySite topologySite4;
					if (!this.siteDictionary.TryGetValue(distinguishedName, out topologySite4))
					{
						ExTraceGlobals.ExchangeTopologyTracer.TraceWarning<string, string>(0L, "AD Server {0} points to nonexistent site {1} (likely a replication issue).", adserver.DnsHostName, distinguishedName);
					}
					else
					{
						this.aDServerSiteDictionary[adserver.DnsHostName] = topologySite4;
						this.aDServerSiteDictionary[adserver.Name] = topologySite4;
						ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>(0L, "ADServer {0} is assigned a static site {1}.", adserver.DnsHostName, topologySite4.Name);
						List<ADServer> list3;
						if (!this.siteADServerDictionary.TryGetValue(distinguishedName, out list3))
						{
							list3 = new List<ADServer>();
							this.siteADServerDictionary.Add(distinguishedName, list3);
							ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Adding site {0} to the siteADServerDictionary.", topologySite4.Name);
						}
						list3.Add(adserver);
						ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>(0L, "Adding ADServer {0} to the siteADServerDictionary under site {1}.", adserver.DnsHostName, topologySite4.Name);
					}
				}
			}
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0012586C File Offset: 0x00123A6C
		internal static List<TopologySite> OrderDestinationSites(ICollection<TopologySite> sites, TopologySite sourceSite, ICollection<TopologySite> destinationSites)
		{
			int num = (sites == null) ? 0 : sites.Count;
			int num2 = (destinationSites == null) ? 0 : destinationSites.Count;
			Dictionary<TopologySite, int> costMap = new Dictionary<TopologySite, int>(num + num2);
			if (num != 0 && num2 != 0 && sourceSite != null)
			{
				foreach (TopologySite key in sites)
				{
					costMap[key] = int.MaxValue;
				}
				foreach (TopologySite key2 in destinationSites)
				{
					costMap[key2] = int.MaxValue;
				}
				costMap[sourceSite] = 0;
				List<TopologySite> list = new List<TopologySite>(costMap.Count);
				list.Add(sourceSite);
				while (list.Count > 0)
				{
					list.Sort((TopologySite a, TopologySite b) => costMap[b].CompareTo(costMap[a]));
					TopologySite topologySite = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					foreach (ITopologySiteLink topologySiteLink in topologySite.TopologySiteLinks)
					{
						TopologySiteLink topologySiteLink2 = (TopologySiteLink)topologySiteLink;
						foreach (ITopologySite topologySite2 in topologySiteLink2.TopologySites)
						{
							TopologySite topologySite3 = (TopologySite)topologySite2;
							if (topologySite3 != topologySite && costMap.ContainsKey(topologySite3))
							{
								int num3 = costMap[topologySite] + topologySiteLink2.Cost;
								if (num3 < costMap[topologySite3])
								{
									costMap[topologySite3] = num3;
									if (!list.Contains(topologySite3))
									{
										list.Add(topologySite3);
									}
								}
							}
						}
					}
				}
			}
			List<TopologySite> list2 = new List<TopologySite>(num2);
			foreach (TopologySite item in costMap.Keys)
			{
				if (destinationSites.Contains(item))
				{
					list2.Add(item);
				}
			}
			list2.Sort((TopologySite a, TopologySite b) => costMap[a].CompareTo(costMap[b]));
			return list2;
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x00125B30 File Offset: 0x00123D30
		private void MatchServersWithSites()
		{
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int>(0L, "PFD ADPEXT {0} - Assigning servers to sites", 26805);
			foreach (TopologyServer topologyServer in this.topoServers)
			{
				if (topologyServer.ServerSite != null)
				{
					TopologySite topologySite;
					if (this.siteDictionary.TryGetValue(topologyServer.ServerSite.DistinguishedName, out topologySite) && topologySite != null)
					{
						topologyServer.TopologySite = topologySite;
						ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string, string>(0L, "Server {0} is assigned to site {1} because site is specified on the AD server object.", topologyServer.Fqdn, topologySite.Name);
					}
					else
					{
						ExTraceGlobals.ExchangeTopologyTracer.TraceError<string, string>(0L, "Warning: msExchServerSite property of {0} points to nonexistent site {1} (likely a replication issue).", topologyServer.Fqdn, topologyServer.ServerSite.Name);
					}
				}
				else if (topologyServer.IsExchange2007OrLater)
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceError<string>(0L, "Warning: msExchServerSite property of {0} does not exist (likely a replication issue).", topologyServer.Fqdn);
				}
				else
				{
					ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "msExchServerSite property of {0} does not exist because its version is Exchange 2003 or earlier.", topologyServer.Fqdn);
				}
				if (topologyServer.TopologySite == null && topologyServer.IsExchange2007OrLater)
				{
					Globals.LogExchangeTopologyEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_SERVER_DOES_NOT_HAVE_SITE, topologyServer.Name, new object[]
					{
						topologyServer.Name
					});
				}
			}
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x00125C74 File Offset: 0x00123E74
		private void CreateTopology()
		{
			bool flag = ExchangeTopologyDiscovery.IncludeServices(this.topologyScope);
			bool flag2 = ExchangeTopologyDiscovery.IncludeADServers(this.topologyScope);
			Dictionary<string, ReadOnlyCollection<ADServer>> dictionary = null;
			if (flag2)
			{
				dictionary = new Dictionary<string, ReadOnlyCollection<ADServer>>(this.topoSites.Count);
				foreach (string key in this.siteADServerDictionary.Keys)
				{
					dictionary.Add(key, new ReadOnlyCollection<ADServer>(this.siteADServerDictionary[key]));
				}
			}
			this.preparedTopology = new ExchangeTopology(this.discoveryStarted, this.topologyScope, new ReadOnlyCollection<TopologyServer>(this.topoServers), new ReadOnlyCollection<TopologySite>(this.topoSites), new ReadOnlyCollection<TopologySiteLink>(this.topoSiteLinks), flag ? new ReadOnlyCollection<MiniVirtualDirectory>(this.virtualDirectories) : null, flag ? new ReadOnlyCollection<MiniEmailTransport>(this.emailTransports) : null, flag ? new ReadOnlyCollection<MiniReceiveConnector>(this.smtpReceiveConnectors) : null, flag2 ? this.aDServers : null, flag2 ? this.aDServerSiteDictionary : null, dictionary, this.siteDictionary, this.localServerFqdn);
			TimeSpan timeSpan = this.preparedTopology.WhenCreated - this.discoveryStarted;
			ExTraceGlobals.ExchangeTopologyTracer.TracePfd<int, double>((long)this.GetHashCode(), "PFD ADPEXT {0} - It took {0} ms to create the Exchange topology", 20149, timeSpan.TotalMilliseconds);
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (currentProcess != null)
				{
					int num = 0;
					foreach (TopologyServer topologyServer in this.topoServers)
					{
						if (topologyServer.TopologySite == null)
						{
							if (topologyServer.IsExchange2007OrLater)
							{
								num++;
							}
							else
							{
								ExTraceGlobals.ExchangeTopologyTracer.TraceDebug<string>(0L, "Server {0} belongs to no site because its version is Exchange 2003 or earlier.", topologyServer.Fqdn);
							}
						}
					}
					ExchangeTopologyPerformanceCountersInstance instance = ExchangeTopologyPerformanceCounters.GetInstance(currentProcess.Id.ToString());
					instance.LastExchangeTopologyDiscoveryTimeSeconds.RawValue = (long)Math.Round(timeSpan.TotalSeconds);
					instance.ExchangeTopologyDiscoveriesPerformed.Increment();
					instance.SitelessServers.RawValue = (long)num;
				}
			}
		}

		// Token: 0x04003646 RID: 13894
		private readonly DateTime discoveryStarted;

		// Token: 0x04003647 RID: 13895
		private readonly ExchangeTopologyScope topologyScope;

		// Token: 0x04003648 RID: 13896
		private List<ADSite> sites;

		// Token: 0x04003649 RID: 13897
		private List<ADSiteLink> siteLinks;

		// Token: 0x0400364A RID: 13898
		private List<MiniTopologyServer> servers;

		// Token: 0x0400364B RID: 13899
		private List<MiniVirtualDirectory> virtualDirectories;

		// Token: 0x0400364C RID: 13900
		private List<MiniEmailTransport> emailTransports;

		// Token: 0x0400364D RID: 13901
		private List<MiniReceiveConnector> smtpReceiveConnectors;

		// Token: 0x0400364E RID: 13902
		private ReadOnlyCollection<ADServer> aDServers;

		// Token: 0x0400364F RID: 13903
		private ExchangeTopology preparedTopology;

		// Token: 0x04003650 RID: 13904
		private Dictionary<string, TopologySite> aDServerSiteDictionary;

		// Token: 0x04003651 RID: 13905
		private Dictionary<string, List<ADServer>> siteADServerDictionary;

		// Token: 0x04003652 RID: 13906
		private List<TopologySite> topoSites;

		// Token: 0x04003653 RID: 13907
		private List<TopologySiteLink> topoSiteLinks;

		// Token: 0x04003654 RID: 13908
		private Dictionary<string, TopologySite> siteDictionary;

		// Token: 0x04003655 RID: 13909
		private List<TopologyServer> topoServers;

		// Token: 0x04003656 RID: 13910
		private string localServerFqdn;

		// Token: 0x020006B9 RID: 1721
		internal class Simple
		{
			// Token: 0x17001A23 RID: 6691
			// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00125EC4 File Offset: 0x001240C4
			// (set) Token: 0x06004F8D RID: 20365 RVA: 0x00125ECC File Offset: 0x001240CC
			public DateTime DiscoveryStarted { get; private set; }

			// Token: 0x17001A24 RID: 6692
			// (get) Token: 0x06004F8E RID: 20366 RVA: 0x00125ED5 File Offset: 0x001240D5
			// (set) Token: 0x06004F8F RID: 20367 RVA: 0x00125EDD File Offset: 0x001240DD
			public ExchangeTopologyScope TopologyScope { get; private set; }

			// Token: 0x17001A25 RID: 6693
			// (get) Token: 0x06004F90 RID: 20368 RVA: 0x00125EE6 File Offset: 0x001240E6
			// (set) Token: 0x06004F91 RID: 20369 RVA: 0x00125EEE File Offset: 0x001240EE
			public string LocalServerFqdn { get; private set; }

			// Token: 0x17001A26 RID: 6694
			// (get) Token: 0x06004F92 RID: 20370 RVA: 0x00125EF7 File Offset: 0x001240F7
			// (set) Token: 0x06004F93 RID: 20371 RVA: 0x00125EFF File Offset: 0x001240FF
			public SimpleADObject.SimpleList<SimpleADObject> Sites { get; private set; }

			// Token: 0x17001A27 RID: 6695
			// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00125F08 File Offset: 0x00124108
			// (set) Token: 0x06004F95 RID: 20373 RVA: 0x00125F10 File Offset: 0x00124110
			public SimpleADObject.SimpleList<SimpleADObject> SiteLinks { get; private set; }

			// Token: 0x17001A28 RID: 6696
			// (get) Token: 0x06004F96 RID: 20374 RVA: 0x00125F19 File Offset: 0x00124119
			// (set) Token: 0x06004F97 RID: 20375 RVA: 0x00125F21 File Offset: 0x00124121
			public SimpleADObject.SimpleList<SimpleADObject> Servers { get; private set; }

			// Token: 0x17001A29 RID: 6697
			// (get) Token: 0x06004F98 RID: 20376 RVA: 0x00125F2A File Offset: 0x0012412A
			// (set) Token: 0x06004F99 RID: 20377 RVA: 0x00125F32 File Offset: 0x00124132
			public SimpleADObject.SimpleList<SimpleADObject> VirtualDirectories { get; private set; }

			// Token: 0x17001A2A RID: 6698
			// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00125F3B File Offset: 0x0012413B
			// (set) Token: 0x06004F9B RID: 20379 RVA: 0x00125F43 File Offset: 0x00124143
			public SimpleADObject.SimpleList<SimpleADObject> EmailTransports { get; private set; }

			// Token: 0x17001A2B RID: 6699
			// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00125F4C File Offset: 0x0012414C
			// (set) Token: 0x06004F9D RID: 20381 RVA: 0x00125F54 File Offset: 0x00124154
			public SimpleADObject.SimpleList<SimpleADObject> SmtpReceiveConnectors { get; private set; }

			// Token: 0x17001A2C RID: 6700
			// (get) Token: 0x06004F9E RID: 20382 RVA: 0x00125F5D File Offset: 0x0012415D
			// (set) Token: 0x06004F9F RID: 20383 RVA: 0x00125F65 File Offset: 0x00124165
			public SimpleADObject.SimpleList<SimpleADObject> ADServers { get; private set; }

			// Token: 0x06004FA0 RID: 20384 RVA: 0x00125F70 File Offset: 0x00124170
			public static ExchangeTopologyDiscovery.Simple CreateFrom(ExchangeTopologyDiscovery topologyDiscovery)
			{
				ArgumentValidator.ThrowIfNull("topologyDiscovery", topologyDiscovery);
				return new ExchangeTopologyDiscovery.Simple
				{
					DiscoveryStarted = topologyDiscovery.discoveryStarted,
					TopologyScope = topologyDiscovery.topologyScope,
					LocalServerFqdn = topologyDiscovery.localServerFqdn,
					Sites = SimpleADObject.CreateList<ADSite>(topologyDiscovery.sites),
					SiteLinks = SimpleADObject.CreateList<ADSiteLink>(topologyDiscovery.siteLinks),
					Servers = SimpleADObject.CreateList<MiniTopologyServer>(topologyDiscovery.servers),
					VirtualDirectories = SimpleADObject.CreateList<MiniVirtualDirectory>(topologyDiscovery.virtualDirectories),
					EmailTransports = SimpleADObject.CreateList<MiniEmailTransport>(topologyDiscovery.emailTransports),
					SmtpReceiveConnectors = SimpleADObject.CreateList<MiniReceiveConnector>(topologyDiscovery.smtpReceiveConnectors),
					ADServers = SimpleADObject.CreateList<ADServer>(topologyDiscovery.aDServers)
				};
			}

			// Token: 0x06004FA1 RID: 20385 RVA: 0x0012602C File Offset: 0x0012422C
			public static ExchangeTopologyDiscovery CreateFrom(ExchangeTopologyDiscovery.Simple topology)
			{
				ArgumentValidator.ThrowIfNull("topology", topology);
				ExchangeTopologyDiscovery exchangeTopologyDiscovery = new ExchangeTopologyDiscovery(topology.DiscoveryStarted, topology.TopologyScope);
				exchangeTopologyDiscovery.localServerFqdn = topology.LocalServerFqdn;
				exchangeTopologyDiscovery.sites = SimpleADObject.CreateList<ADSite>(topology.Sites, ExchangeTopologyDiscovery.Simple.aDSiteSchema, null);
				exchangeTopologyDiscovery.siteLinks = SimpleADObject.CreateList<ADSiteLink>(topology.SiteLinks, ExchangeTopologyDiscovery.Simple.aDSiteLinkSchema, null);
				exchangeTopologyDiscovery.servers = SimpleADObject.CreateList<MiniTopologyServer>(topology.Servers, ExchangeTopologyDiscovery.Simple.serverSchema, null);
				exchangeTopologyDiscovery.virtualDirectories = SimpleADObject.CreateList<MiniVirtualDirectory>(topology.VirtualDirectories, ExchangeTopologyDiscovery.Simple.virtualDirectorySchema, null);
				exchangeTopologyDiscovery.emailTransports = SimpleADObject.CreateList<MiniEmailTransport>(topology.EmailTransports, ExchangeTopologyDiscovery.Simple.emailTransportSchema, null);
				exchangeTopologyDiscovery.smtpReceiveConnectors = SimpleADObject.CreateList<MiniReceiveConnector>(topology.SmtpReceiveConnectors, ExchangeTopologyDiscovery.Simple.receiveConnectorSchema, null);
				List<ADServer> list = SimpleADObject.CreateList<ADServer>(topology.ADServers, ExchangeTopologyDiscovery.Simple.aDServerSchema, null);
				exchangeTopologyDiscovery.aDServers = new ReadOnlyCollection<ADServer>(list);
				return exchangeTopologyDiscovery;
			}

			// Token: 0x06004FA2 RID: 20386 RVA: 0x0012610C File Offset: 0x0012430C
			public static ExchangeTopologyDiscovery.Simple Deserialize(byte[][] data)
			{
				ArgumentValidator.ThrowIfNull("data", data);
				if (data.Length != 3 || data[0] == null || data[1] == null || data[2] == null)
				{
					throw new ArgumentException("data");
				}
				ExchangeTopologyDiscovery.Simple simple = new ExchangeTopologyDiscovery.Simple();
				using (MemoryStream memoryStream = new MemoryStream(data[0]))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						simple.DiscoveryStarted = DateTime.FromBinary(binaryReader.ReadInt64());
						simple.TopologyScope = (ExchangeTopologyScope)binaryReader.ReadInt32();
						simple.LocalServerFqdn = binaryReader.ReadString();
						simple.Sites = SimpleADObject.ReadList(binaryReader);
						simple.SiteLinks = SimpleADObject.ReadList(binaryReader);
						simple.Servers = SimpleADObject.ReadList(binaryReader);
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream(data[1]))
				{
					using (BinaryReader binaryReader2 = new BinaryReader(memoryStream2))
					{
						simple.VirtualDirectories = SimpleADObject.ReadList(binaryReader2);
						simple.EmailTransports = SimpleADObject.ReadList(binaryReader2);
						simple.SmtpReceiveConnectors = SimpleADObject.ReadList(binaryReader2);
					}
				}
				using (MemoryStream memoryStream3 = new MemoryStream(data[2]))
				{
					using (BinaryReader binaryReader3 = new BinaryReader(memoryStream3))
					{
						simple.ADServers = SimpleADObject.ReadList(binaryReader3);
					}
				}
				return simple;
			}

			// Token: 0x06004FA3 RID: 20387 RVA: 0x0012629C File Offset: 0x0012449C
			public static byte[][] Rescope(ExchangeTopologyScope topologyScope, byte[][] data)
			{
				byte[][] array = new byte[3][];
				data.CopyTo(array, 0);
				bool flag = ExchangeTopologyDiscovery.IncludeServices(topologyScope);
				bool flag2 = ExchangeTopologyDiscovery.IncludeADServers(topologyScope);
				if (!flag)
				{
					array[1] = new byte[12];
				}
				if (!flag2)
				{
					array[2] = new byte[4];
				}
				return array;
			}

			// Token: 0x06004FA4 RID: 20388 RVA: 0x001262E0 File Offset: 0x001244E0
			public byte[][] Serialize()
			{
				byte[][] result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						byte[][] array = new byte[3][];
						binaryWriter.Write(this.DiscoveryStarted.ToBinary());
						binaryWriter.Write((int)this.TopologyScope);
						binaryWriter.Write(this.LocalServerFqdn);
						SimpleADObject.WriteList(binaryWriter, this.Sites);
						SimpleADObject.WriteList(binaryWriter, this.SiteLinks);
						SimpleADObject.WriteList(binaryWriter, this.Servers);
						array[0] = memoryStream.ToArray();
						memoryStream.SetLength(0L);
						SimpleADObject.WriteList(binaryWriter, this.VirtualDirectories);
						SimpleADObject.WriteList(binaryWriter, this.EmailTransports);
						SimpleADObject.WriteList(binaryWriter, this.SmtpReceiveConnectors);
						array[1] = memoryStream.ToArray();
						memoryStream.SetLength(0L);
						SimpleADObject.WriteList(binaryWriter, this.ADServers);
						array[2] = memoryStream.ToArray();
						result = array;
					}
				}
				return result;
			}

			// Token: 0x06004FA5 RID: 20389 RVA: 0x001263E4 File Offset: 0x001245E4
			public bool Equals(ExchangeTopologyDiscovery.Simple right)
			{
				return right != null && (this.TopologyScope == right.TopologyScope && this.LocalServerFqdn == right.LocalServerFqdn && SimpleADObject.ListEquals(this.Sites, right.Sites) && SimpleADObject.ListEquals(this.SiteLinks, right.SiteLinks) && SimpleADObject.ListEquals(this.Servers, right.Servers) && SimpleADObject.ListEquals(this.VirtualDirectories, right.VirtualDirectories) && SimpleADObject.ListEquals(this.EmailTransports, right.EmailTransports) && SimpleADObject.ListEquals(this.SmtpReceiveConnectors, right.SmtpReceiveConnectors)) && SimpleADObject.ListEquals(this.ADServers, right.ADServers);
			}

			// Token: 0x04003657 RID: 13911
			private static ADObjectSchema aDSiteSchema = new ADSite().Schema;

			// Token: 0x04003658 RID: 13912
			private static ADObjectSchema aDSiteLinkSchema = new ADSiteLink().Schema;

			// Token: 0x04003659 RID: 13913
			private static ADObjectSchema serverSchema = new MiniTopologyServer().Schema;

			// Token: 0x0400365A RID: 13914
			private static ADObjectSchema virtualDirectorySchema = new MiniVirtualDirectory().Schema;

			// Token: 0x0400365B RID: 13915
			private static ADObjectSchema emailTransportSchema = new MiniEmailTransport().Schema;

			// Token: 0x0400365C RID: 13916
			private static ADObjectSchema receiveConnectorSchema = new MiniReceiveConnector().Schema;

			// Token: 0x0400365D RID: 13917
			private static ADObjectSchema aDServerSchema = new ADServer().Schema;
		}
	}
}
