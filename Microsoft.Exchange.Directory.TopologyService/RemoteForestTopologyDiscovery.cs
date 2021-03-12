using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoteForestTopologyDiscovery : ADTopologyDiscovery
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00005A0B File Offset: 0x00003C0B
		public RemoteForestTopologyDiscovery(TopologyDiscoveryInfo topologyDiscoveryInfo, DiscoveryFlags discoveryFlags, string cdcFqdn) : base(topologyDiscoveryInfo, discoveryFlags, cdcFqdn)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005A18 File Offset: 0x00003C18
		protected override string GetPreferredSiteName()
		{
			if (!string.IsNullOrEmpty(this.preferredSiteName))
			{
				return this.preferredSiteName;
			}
			if (ConfigurationData.Instance.EnableWholeForestDiscovery)
			{
				ADTopology topology = base.TopologyDiscoveryInfo.Topology;
				if (topology != null && topology.ConfigDCElectionTime.AddMinutes((double)ConfigurationData.Instance.ConfigDCAffinityInMinutes) > DateTime.UtcNow)
				{
					this.preferredSiteName = topology.LocalSiteName;
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Preferred Site from previous topology {1}.", base.ForestFqdn, this.preferredSiteName ?? string.Empty);
				}
				if (string.IsNullOrEmpty(this.preferredSiteName))
				{
					ADServerInfo configDCInfo = TopologyProvider.GetInstance().GetConfigDCInfo(base.ForestFqdn, true);
					this.preferredSiteName = configDCInfo.SiteName;
					ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Preferred Site from Ldap Provider {1}", base.ForestFqdn, this.preferredSiteName ?? string.Empty);
				}
			}
			if (string.IsNullOrEmpty(this.preferredSiteName))
			{
				string siteName = NativeHelpers.GetSiteName(true);
				TopologyDiscoverySession topologyDiscoverySession = this.GetTopologyDiscoverySession();
				IList<ADSite> list = topologyDiscoverySession.FindAllADSites();
				if (list.Count == 0)
				{
					ExTraceGlobals.DiscoveryTracer.TraceError<string>((long)this.GetHashCode(), "{0} - No sites found in forest..", base.ForestFqdn);
					throw new TopologyDiscoveryException(Strings.NoSitesInForest(base.ForestFqdn));
				}
				List<string> list2 = new List<string>(list.Count);
				foreach (ADSite adsite in list)
				{
					list2.Add(adsite.Name);
				}
				List<StringExtensions.PrefixMatch> list3 = siteName.LongestPrefixMatch(list2);
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "{0} - ConfigurationData.Instance.MinimumPrefixMatch {1} - Longest Prefix Match {2}.", base.ForestFqdn, ConfigurationData.Instance.MinimumPrefixMatch, list3[0].Mask);
				if (ConfigurationData.Instance.MinimumPrefixMatch <= list3[0].Mask)
				{
					this.preferredSiteName = list3[0].Value;
				}
				else
				{
					Random random = new Random();
					this.preferredSiteName = list3[random.Next(list3.Count)].Value;
				}
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Preferred Site {1}.", base.ForestFqdn, this.preferredSiteName);
			return this.preferredSiteName;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005C78 File Offset: 0x00003E78
		protected override TopologyDiscoverySession GetTopologyDiscoverySession()
		{
			if (this.session == null)
			{
				ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromAccountPartitionRootOrgScopeSet(new PartitionId(base.ForestFqdn));
				adsessionSettings.IncludeCNFObject = false;
				this.session = new TopologyDiscoverySession(true, adsessionSettings);
			}
			return this.session;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005CF4 File Offset: 0x00003EF4
		protected override List<DirectoryServer> FindPrimaryDS()
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Find Primary DS", base.ForestFqdn);
			bool flag = (base.DiscoveryFlags & DiscoveryFlags.FullDiscovery) != DiscoveryFlags.None;
			List<DirectoryServer> list = this.GetTopologyDiscoverySession().FindDirectoryServers((flag && ConfigurationData.Instance.EnableWholeForestDiscovery) ? null : this.GetPreferredSiteName());
			base.ResolveAndAddPreferredCDC(list);
			this.UpdateSuitabilities(list);
			if (!this.CheckForMinimalRequiredServers(list, false) && (!flag || !ConfigurationData.Instance.EnableWholeForestDiscovery))
			{
				ExTraceGlobals.DiscoveryTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} - Find Primary DS. MinRequiredServers not found in preferred site.", base.ForestFqdn);
				List<DirectoryServer> source = this.GetTopologyDiscoverySession().FindDirectoryServers(null);
				List<DirectoryServer> source2 = (from x in source
				where !string.Equals(x.Site, this.GetPreferredSiteName(), StringComparison.OrdinalIgnoreCase) && !string.Equals(base.PreferredCDCFdqn, x.DnsName)
				select x).ToList<DirectoryServer>();
				foreach (IGrouping<string, DirectoryServer> source3 in from x in source2
				group x by x.Site)
				{
					this.UpdateSuitabilities(source3.ToList<DirectoryServer>());
					list.AddRange(source3.ToList<DirectoryServer>());
					if (this.CheckForMinimalRequiredServers(list, false))
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005E4C File Offset: 0x0000404C
		protected override List<DirectoryServer> FindSecondaryDS(out List<string> connectedSitesTested)
		{
			connectedSitesTested = new List<string>(0);
			return ADTopology.EmptyDirectoryServerList;
		}

		// Token: 0x0400003D RID: 61
		private string preferredSiteName;

		// Token: 0x0400003E RID: 62
		private TopologyDiscoverySession session;
	}
}
