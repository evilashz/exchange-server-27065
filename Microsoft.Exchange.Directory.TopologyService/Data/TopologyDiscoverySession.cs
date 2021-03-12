using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Directory.TopologyService.Data
{
	// Token: 0x0200002F RID: 47
	internal class TopologyDiscoverySession : ADTopologyConfigurationSession
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public TopologyDiscoverySession(bool readOnly, ADSessionSettings sessionSettings) : base(readOnly, ConsistencyMode.PartiallyConsistent, sessionSettings)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000DAFB File Offset: 0x0000BCFB
		public virtual List<DirectoryServer> FindDirectoryServers(List<string> serversFqdn)
		{
			if (serversFqdn.IsNullOrEmpty())
			{
				throw new ArgumentNullException("dsFqdn");
			}
			Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceFunction<string>((long)this.GetHashCode(), "FindDirectoryServers (list of DS), {0}", string.Join(",", serversFqdn));
			return this.FindDirectoryServers(null, serversFqdn);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000DB39 File Offset: 0x0000BD39
		public virtual List<DirectoryServer> FindDirectoryServers(string site = null)
		{
			Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceFunction<string>((long)this.GetHashCode(), "FindDirectoryServers site {0}", (site != null) ? site : "<NULL>");
			return this.FindDirectoryServers(site, null);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		public virtual List<Tuple<int, ADObjectId>> FindConnectedSites(string site)
		{
			Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceFunction<string>((long)this.GetHashCode(), "FindConnectedSites site {0} ", (site != null) ? site : "<NULL>");
			if (string.IsNullOrEmpty(site))
			{
				throw new ArgumentNullException("site");
			}
			ADObjectId configurationNamingContext = base.GetConfigurationNamingContext();
			ADObjectId adobjectId = new ADObjectId(string.Format("CN={0},CN=Sites,{1}", site, configurationNamingContext.DistinguishedName));
			ADObjectId rootId = new ADObjectId(string.Format("cn=IP,cn=Inter-Site Transports,cn=Sites,{0}", configurationNamingContext.DistinguishedName));
			ADSiteLink[] array = base.FindPaged<ADSiteLink>(rootId, QueryScope.OneLevel, new ComparisonFilter(ComparisonOperator.Equal, ADSiteLinkSchema.Sites, adobjectId), TopologyDiscoverySession.SitesSortBy, 0).ReadAllPages();
			List<Tuple<int, ADObjectId>> list = new List<Tuple<int, ADObjectId>>();
			foreach (ADSiteLink adsiteLink in array)
			{
				using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = adsiteLink.Sites.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADObjectId siteId = enumerator.Current;
						if (!siteId.Equals(adobjectId))
						{
							Tuple<int, ADObjectId> tuple = list.FirstOrDefault((Tuple<int, ADObjectId> x) => x.Item2.Equals(siteId));
							if (tuple != null)
							{
								if (tuple.Item1 <= adsiteLink.ADCost)
								{
									continue;
								}
								list.Remove(tuple);
							}
							list.Add(new Tuple<int, ADObjectId>(adsiteLink.ADCost, siteId));
						}
					}
				}
			}
			Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceList(this.GetHashCode(), list, "Connected site links to site " + site);
			Random rand = new Random();
			List<Tuple<int, ADObjectId>> list2 = new List<Tuple<int, ADObjectId>>(list.Count);
			foreach (Tuple<int, ADObjectId> item in from x in list
			orderby rand.Next()
			select x)
			{
				list2.Add(item);
			}
			list.Clear();
			foreach (Tuple<int, ADObjectId> item2 in from x in list2
			orderby x.Item1
			select x)
			{
				list.Add(item2);
			}
			return list;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public virtual ADObjectId GetSiteADObjectId(string siteName)
		{
			if (string.IsNullOrEmpty(siteName))
			{
				throw new ArgumentNullException("siteName");
			}
			new List<DirectoryServer>();
			ADObjectId configurationNamingContext = base.GetConfigurationNamingContext();
			return new ADObjectId(string.Format("CN={0},CN=Sites,{1}", siteName, configurationNamingContext.DistinguishedName));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000DE60 File Offset: 0x0000C060
		protected override PooledLdapConnection GetConnection(string preferredServer, bool isWriteOperation, string optionalBaseDN, ref ADObjectId rootId, ADScope scope)
		{
			int i = 0;
			ADObjectId adobjectId = rootId;
			while (i < 3)
			{
				i++;
				Microsoft.Exchange.Diagnostics.Components.Data.Directory.ExTraceGlobals.GetConnectionTracer.TraceDebug<int>((long)this.GetHashCode(), "TopologyDiscoverySession.Getting connection. Attempt {0}.", i);
				try
				{
					return base.GetConnection(preferredServer, isWriteOperation, optionalBaseDN, ref rootId, scope);
				}
				catch (TransientException arg)
				{
					Microsoft.Exchange.Diagnostics.Components.Data.Directory.ExTraceGlobals.GetConnectionTracer.TraceError<TransientException>((long)this.GetHashCode(), "TopologyDiscoverySession.GettingConnection. Error {0}.", arg);
					if (i >= 3)
					{
						throw;
					}
					rootId = adobjectId;
					this.ForceResetTopologyCDC();
				}
			}
			return null;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		private void ForceResetTopologyCDC()
		{
			try
			{
				LdapTopologyProvider ldapTopologyProvider = TopologyProvider.GetInstance() as LdapTopologyProvider;
				if (ldapTopologyProvider != null)
				{
					string configDC = ldapTopologyProvider.GetConfigDC(base.SessionSettings.GetAccountOrResourceForestFqdn());
					ldapTopologyProvider.ReportServerDown(base.SessionSettings.GetAccountOrResourceForestFqdn(), configDC, ADServerRole.ConfigurationDomainController);
				}
			}
			catch (TransientException arg)
			{
				Microsoft.Exchange.Diagnostics.Components.Data.Directory.ExTraceGlobals.GetConnectionTracer.TraceError<TransientException>((long)this.GetHashCode(), "TopologyDiscoverySession.ForceResetTopologyCDC. Error {0}.", arg);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DF84 File Offset: 0x0000C184
		private List<DirectoryServer> FindDirectoryServers(string site, List<string> dsFqdns)
		{
			List<DirectoryServer> list = new List<DirectoryServer>();
			ADObjectId configurationNamingContext = base.GetConfigurationNamingContext();
			ADObjectId adobjectId = null;
			QueryScope scope = QueryScope.OneLevel;
			if (string.IsNullOrEmpty(site) || !dsFqdns.IsNullOrEmpty())
			{
				adobjectId = new ADObjectId(string.Format("CN=Sites,{0}", configurationNamingContext.DistinguishedName));
				scope = QueryScope.SubTree;
			}
			else if (!string.IsNullOrEmpty(site))
			{
				adobjectId = new ADObjectId(string.Format("CN=Servers,CN={0},CN=Sites,{1}", site, configurationNamingContext.DistinguishedName));
			}
			QueryFilter filter;
			if (!dsFqdns.IsNullOrEmpty())
			{
				List<ComparisonFilter> list2 = new List<ComparisonFilter>(dsFqdns.Count);
				foreach (string propertyValue in dsFqdns)
				{
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ADServerSchema.DnsHostName, propertyValue));
				}
				filter = QueryFilter.OrTogether(list2.ToArray());
			}
			else
			{
				filter = TopologyDiscoverySession.DnsHostNameNotEmpty;
			}
			ADServer[] array = base.FindPaged<ADServer>(adobjectId, scope, filter, null, 0).ReadAllPages();
			if (array.Length == 0)
			{
				Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceWarning<ADObjectId>((long)this.GetHashCode(), "No DS found under {0}", adobjectId);
				return list;
			}
			using (IEnumerator<NtdsDsa> enumerator2 = base.FindPaged<NtdsDsa>(adobjectId, QueryScope.SubTree, TopologyDiscoverySession.HasMasterNCs, null, 0).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					NtdsDsa ntdsdsa = enumerator2.Current;
					if (ntdsdsa.MasterNCs.FirstOrDefault((ADObjectId x) => x.Equals(x.DescendantDN(0))) == null)
					{
						Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "DS doesn't {0} have a master NC. Ignoring it.", ntdsdsa.DistinguishedName);
					}
					else
					{
						ADServer adserver = array.FirstOrDefault((ADServer x) => ntdsdsa.Id.Parent.Equals(x.Id));
						if (adserver != null)
						{
							list.Add(new DirectoryServer(adserver, ntdsdsa));
						}
						else
						{
							Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService.ExTraceGlobals.TopologyTracer.TraceWarning<string>((long)this.GetHashCode(), "NTDS doesn't {0} have DS object. Ignoring it.", ntdsdsa.DistinguishedName);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0400010E RID: 270
		private const int MaxGetConnectionAttempts = 3;

		// Token: 0x0400010F RID: 271
		private const string ServersInSiteFormat = "CN=Servers,CN={0},CN=Sites,{1}";

		// Token: 0x04000110 RID: 272
		private const string SitesFormat = "CN=Sites,{0}";

		// Token: 0x04000111 RID: 273
		private const string SiteFormat = "CN={0},CN=Sites,{1}";

		// Token: 0x04000112 RID: 274
		private const string IpInterSiteTransport = "cn=IP,cn=Inter-Site Transports,cn=Sites,{0}";

		// Token: 0x04000113 RID: 275
		private const string SystemRootFormat = "CN=System,{0}";

		// Token: 0x04000114 RID: 276
		private static readonly SortBy SitesSortBy = new SortBy(ADSiteLinkSchema.ADCost, SortOrder.Ascending);

		// Token: 0x04000115 RID: 277
		private static readonly QueryFilter HasMasterNCs = new ExistsFilter(NtdsDsaSchema.MasterNCs);

		// Token: 0x04000116 RID: 278
		private static readonly QueryFilter DnsHostNameNotEmpty = new ExistsFilter(ADServerSchema.DnsHostName);
	}
}
