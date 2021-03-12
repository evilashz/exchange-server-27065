using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Directory.TopologyService.EventLog;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LocalForestTopologyDiscovery : ADTopologyDiscovery
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000048BC File Offset: 0x00002ABC
		public LocalForestTopologyDiscovery(TopologyDiscoveryInfo topologyDiscoveryInfo, DiscoveryFlags discoveryFlags, string cdcFqdn) : base(topologyDiscoveryInfo, discoveryFlags, cdcFqdn)
		{
			this.siteName = null;
			this.localComputerFqdn = null;
			this.registryDCs = false;
			this.registryGCs = false;
			this.registryCDC = false;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000048EC File Offset: 0x00002AEC
		protected override string GetPreferredSiteName()
		{
			if (string.IsNullOrEmpty(this.siteName))
			{
				this.siteName = NativeHelpers.GetSiteName(false);
				ExTraceGlobals.DiscoveryTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "GetPreferredSiteName Site Name {0}", this.siteName);
				if (string.IsNullOrEmpty(this.siteName))
				{
					ExTraceGlobals.DiscoveryTracer.TraceError((long)this.GetHashCode(), "Site is empty. Abort");
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ServerNotInSite, "ServerNotInSite", new object[0]);
					throw new TopologyDiscoveryException(Strings.LocalServerNotInSiteVerbose);
				}
			}
			return this.siteName;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000499C File Offset: 0x00002B9C
		protected override void UpdateSuitabilities(ICollection<DirectoryServer> servers)
		{
			ExTraceGlobals.DiscoveryTracer.TracePerformance<string, int>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - Entering UpdateSuitabilities for {1} DC(s).", base.ForestFqdn, servers.Count);
			string[] excludedDC = ConfigurationData.Instance.ExcludedDC;
			for (int i = 0; i < excludedDC.Length; i++)
			{
				string serverName = excludedDC[i];
				DirectoryServer directoryServer = servers.FirstOrDefault((DirectoryServer x) => x.DnsName.Equals(serverName, StringComparison.OrdinalIgnoreCase));
				if (directoryServer != null)
				{
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Disabling server {1}.", base.ForestFqdn, serverName);
					directoryServer.SuitabilityResult.IsEnabled = false;
				}
			}
			base.UpdateSuitabilities(servers);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004A6C File Offset: 0x00002C6C
		protected override DirectoryServer ChooseConfigDC(ICollection<DirectoryServer> servers, out DateTime electionTime)
		{
			electionTime = DateTime.UtcNow;
			string registryConfiguredCDC = ConfigurationData.Instance.ConfigurationDC;
			if (string.IsNullOrEmpty(registryConfiguredCDC))
			{
				ExTraceGlobals.DiscoveryTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} - ChooseConfigDC No CDC found in registry.", base.ForestFqdn);
				return base.ChooseConfigDC(servers, out electionTime);
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - ChooseConfigDC CDC from registry {1}.", base.ForestFqdn, registryConfiguredCDC);
			DirectoryServer directoryServer = servers.FirstOrDefault((DirectoryServer x) => x.DnsName.Equals(registryConfiguredCDC, StringComparison.OrdinalIgnoreCase));
			ExTraceGlobals.DiscoveryTracer.TraceDebug((long)this.GetHashCode(), "{0} - ChooseConfigDC CDC from registry '{1}' was {2}found. Suitable {3}", new object[]
			{
				base.ForestFqdn,
				registryConfiguredCDC,
				(directoryServer == null) ? "NOT " : string.Empty,
				(directoryServer == null) ? "FALSE" : directoryServer.IsSuitableForRole(ADServerRole.ConfigurationDomainController).ToString()
			});
			if (directoryServer != null && directoryServer.IsSuitableForRole(ADServerRole.ConfigurationDomainController))
			{
				return directoryServer;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_CDC_DOWN, null, new object[]
			{
				registryConfiguredCDC
			});
			return base.ChooseConfigDC(servers, out electionTime);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004BC4 File Offset: 0x00002DC4
		protected override DirectoryServer ChooseRandomConfigDC(ICollection<DirectoryServer> servers, out DateTime electionTime)
		{
			electionTime = DateTime.UtcNow;
			if (string.IsNullOrEmpty(this.localComputerFqdn))
			{
				this.localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
			}
			DirectoryServer directoryServer = servers.FirstOrDefault((DirectoryServer x) => x.IsSuitableForRole(ADServerRole.ConfigurationDomainController) && x.DnsName.Equals(this.localComputerFqdn));
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - ChooseRandomConfigDC. Is{1} selfAffinity", base.ForestFqdn, (directoryServer == null) ? "NOT" : string.Empty);
			ICollection<DirectoryServer> collection = servers;
			if (ConfigurationData.Instance.MinUserDC < collection.Count)
			{
				collection = (from x in collection
				where !x.SuitabilityResult.IsPDC
				select x).ToList<DirectoryServer>();
			}
			return directoryServer ?? base.ChooseRandomConfigDC(collection, out electionTime);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004C80 File Offset: 0x00002E80
		protected override TopologyDiscoverySession GetTopologyDiscoverySession()
		{
			if (this.session == null)
			{
				ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromRootOrgScopeSet();
				adsessionSettings.IncludeCNFObject = false;
				this.session = new TopologyDiscoverySession(true, adsessionSettings);
			}
			return this.session;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004CDC File Offset: 0x00002EDC
		protected override List<DirectoryServer> FindPrimaryDS()
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - FindPrimaryDS.", base.ForestFqdn);
			List<DirectoryServer> list = this.GetDSFromRegistry();
			if (!this.registryDCs || !this.registryGCs)
			{
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, bool, bool>((long)this.GetHashCode(), "{0} - Find servers from local site. Registry DCs {1} Registry GCs {2}", base.ForestFqdn, this.registryDCs, this.registryGCs);
				List<DirectoryServer> list2 = this.GetTopologyDiscoverySession().FindDirectoryServers(this.GetPreferredSiteName());
				base.ResolveAndAddPreferredCDC(list2);
				if (list.Count == 0)
				{
					list = list2;
				}
				else
				{
					using (List<DirectoryServer>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							DirectoryServer ds = enumerator.Current;
							if (list.Find((DirectoryServer x) => x.DnsName.Equals(ds.DnsName, StringComparison.OrdinalIgnoreCase)) == null)
							{
								if (this.registryDCs)
								{
									ds.SetSuitabilityForRole(ADServerRole.ConfigurationDomainController, false);
									ds.SetSuitabilityForRole(ADServerRole.DomainController, false);
								}
								if (ds.IsGC && this.registryGCs)
								{
									ds.SetSuitabilityForRole(ADServerRole.GlobalCatalog, false);
								}
								list.Add(ds);
							}
						}
					}
				}
			}
			this.UpdateSuitabilities(list);
			return list;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004E38 File Offset: 0x00003038
		protected override List<DirectoryServer> FindSecondaryDS(out List<string> connectedSitesTested)
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - FindSecondaryDS for Preferred Site {1}.", base.ForestFqdn, this.GetPreferredSiteName());
			if (this.registryDCs && this.registryGCs)
			{
				connectedSitesTested = new List<string>(0);
				return new List<DirectoryServer>(0);
			}
			connectedSitesTested = new List<string>();
			List<Tuple<int, ADObjectId>> list = this.GetTopologyDiscoverySession().FindConnectedSites(this.GetPreferredSiteName());
			if (list.Count<Tuple<int, ADObjectId>>() == 0)
			{
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - No Connected Sites found. Site {1}", base.ForestFqdn, this.GetPreferredSiteName());
				return ADTopology.EmptyDirectoryServerList;
			}
			List<DirectoryServer> list2 = new List<DirectoryServer>();
			foreach (Tuple<int, ADObjectId> tuple in list)
			{
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "{0} - Searching for servers in site {1} (site cost {2})", base.ForestFqdn, tuple.Item2.Name, tuple.Item1);
				connectedSitesTested.Add(tuple.Item2.Name);
				List<DirectoryServer> list3 = this.GetTopologyDiscoverySession().FindDirectoryServers(tuple.Item2.Name);
				if (list3 == null || list3.Count == 0)
				{
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - No servers in adjacent site {1}", base.ForestFqdn, tuple.Item2.Name);
				}
				else
				{
					this.UpdateSuitabilities(list3);
					list2.AddRange(list3);
					if (this.CheckForMinimalRequiredServers(list2, false))
					{
						break;
					}
				}
			}
			return list2;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004FD8 File Offset: 0x000031D8
		private List<DirectoryServer> GetDSFromRegistry()
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - GetDSFromRegistry.", base.ForestFqdn);
			List<string> list = new List<string>(ConfigurationData.Instance.DomainControllers);
			list.AddRange(ConfigurationData.Instance.GlobalCatalogs);
			if (list.Count == 0)
			{
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - There are no DCs or GCs specified in the registry.", base.ForestFqdn);
				return new List<DirectoryServer>(0);
			}
			list = list.Distinct(StringComparer.OrdinalIgnoreCase).ToList<string>();
			List<DirectoryServer> list2 = null;
			if (ExTraceGlobals.DiscoveryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Send request for DCs from registry {1}.", base.ForestFqdn, string.Join(",", list));
			}
			list2 = this.GetTopologyDiscoverySession().FindDirectoryServers(list);
			string text = null;
			using (List<string>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string regDS = enumerator.Current;
					DirectoryServer directoryServer = list2.Find((DirectoryServer x) => x.DnsName.Equals(regDS, StringComparison.OrdinalIgnoreCase));
					if (directoryServer == null)
					{
						ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Removing specified registry DC from the list {1}", base.ForestFqdn, regDS);
						ExEventLog.EventTuple tuple = DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_SERVER_BAD;
						if (regDS.Equals(ConfigurationData.Instance.ConfigurationDC, StringComparison.OrdinalIgnoreCase))
						{
							tuple = DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_CDC_BAD;
						}
						Globals.LogEvent(tuple, null, new object[]
						{
							regDS
						});
					}
					else
					{
						if (ConfigurationData.Instance.DomainControllers.Count > 0)
						{
							this.registryDCs = true;
						}
						if (directoryServer.IsGC && ConfigurationData.Instance.GlobalCatalogs.Count > 0)
						{
							this.registryGCs = true;
						}
						if (directoryServer.DnsName.Equals(ConfigurationData.Instance.ConfigurationDC, StringComparison.OrdinalIgnoreCase))
						{
							this.registryCDC = true;
							text = directoryServer.DnsName;
						}
						directoryServer.SuitabilityResult.IsEnabled = !ConfigurationData.Instance.ExcludedDC.Contains(directoryServer.DnsName, StringComparer.OrdinalIgnoreCase);
					}
				}
			}
			if (this.registryDCs)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_DC, null, new object[0]);
			}
			if (this.registryGCs)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_GC, null, new object[0]);
			}
			if (this.registryCDC)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_REG_CDC, null, new object[]
				{
					text
				});
			}
			return list2;
		}

		// Token: 0x04000031 RID: 49
		private string siteName;

		// Token: 0x04000032 RID: 50
		private string localComputerFqdn;

		// Token: 0x04000033 RID: 51
		private TopologyDiscoverySession session;

		// Token: 0x04000034 RID: 52
		private bool registryDCs;

		// Token: 0x04000035 RID: 53
		private bool registryGCs;

		// Token: 0x04000036 RID: 54
		private bool registryCDC;
	}
}
