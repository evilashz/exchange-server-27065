using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D64 RID: 3428
	internal sealed class ServiceTopology
	{
		// Token: 0x06007677 RID: 30327 RVA: 0x0020AD8C File Offset: 0x00208F8C
		internal ServiceTopology(ExchangeTopology topology, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug((long)this.GetHashCode(), "ServiceTopology::Constructor. Creating a ServiceTopology object...");
			if (topology.LocalServer == null)
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug((long)this.GetHashCode(), "ServiceTopology::ServiceTopology.ctor. Cannot find the local server from ExchangeTopology. topology.LocalServer == null.");
				throw new ServerNotFoundException(ServerStrings.ExServerNotFound("localhost"), "localhost");
			}
			string fqdn = topology.LocalServer.Fqdn;
			this.discoveryStarted = topology.DiscoveryStarted;
			Dictionary<string, TopologyServerInfo> dictionary = new Dictionary<string, TopologyServerInfo>(topology.AllTopologyServers.Count, StringComparer.OrdinalIgnoreCase);
			Dictionary<string, Site> dictionary2 = new Dictionary<string, Site>(topology.AllTopologyServers.Count, StringComparer.OrdinalIgnoreCase);
			List<string> list = new List<string>();
			Dictionary<Site, List<TopologyServerInfo>> dictionary3 = new Dictionary<Site, List<TopologyServerInfo>>(topology.AllTopologySites.Count);
			ServiceTopology.All all = new ServiceTopology.All(topology);
			foreach (TopologyServer topologyServer in topology.AllTopologyServers)
			{
				if (topologyServer.TopologySite != null)
				{
					if (!dictionary2.ContainsKey(topologyServer.Fqdn))
					{
						TopologyServerInfo topologyServerInfo = TopologyServerInfo.Get(topologyServer, all);
						Site site = topologyServerInfo.Site;
						dictionary.Add(topologyServer.Fqdn, topologyServerInfo);
						dictionary2.Add(topologyServer.Fqdn, site);
						List<TopologyServerInfo> list2;
						if (!dictionary3.TryGetValue(site, out list2))
						{
							list2 = new List<TopologyServerInfo>();
							dictionary3.Add(site, list2);
						}
						list2.Add(topologyServerInfo);
						ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<string, string, Site>((long)this.GetHashCode(), "ServiceTopology::Constructor. Found Server in topology. ServerDn = {0}. Server Fqdn = {1}. Site = {2}.", topologyServer.DistinguishedName, topologyServer.Fqdn, site);
					}
					else
					{
						string arg = string.Empty;
						foreach (KeyValuePair<string, TopologyServerInfo> keyValuePair in all.Servers)
						{
							if (keyValuePair.Value.ServerFullyQualifiedDomainName.Equals(topologyServer.Fqdn))
							{
								arg = keyValuePair.Key;
								break;
							}
						}
						ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ServiceTopology::Constructor. There are two servers with the same fqdn in the topology. The second server was ignored. Fqdn = {0}. Server1Dn = {1}. Server2Dn = {2}.", topologyServer.Fqdn, arg, topologyServer.DistinguishedName);
					}
				}
				else
				{
					ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "ServiceTopology::Constructor. Found a Server in the topology without a defined Site. Server = {0}.", topologyServer.DistinguishedName);
					if (fqdn.Equals(topologyServer.Fqdn))
					{
						throw new ServerNotInSiteException(ServerStrings.ExCurrentServerNotInSite(fqdn), fqdn);
					}
					if (!list.Contains(topologyServer.Fqdn))
					{
						list.Add(topologyServer.Fqdn);
					}
				}
			}
			Dictionary<ServiceType, List<Service>> serviceLists = new Dictionary<ServiceType, List<Service>>();
			Dictionary<ServiceType, List<Service>> serviceLists2 = new Dictionary<ServiceType, List<Service>>();
			ReadOnlyCollection<MiniVirtualDirectory> allVirtualDirectories = topology.AllVirtualDirectories;
			if (allVirtualDirectories != null)
			{
				foreach (MiniVirtualDirectory miniVirtualDirectory in allVirtualDirectories)
				{
					TopologyServerInfo serverInfo;
					if (all.Servers.TryGetValue(miniVirtualDirectory.Server.DistinguishedName, out serverInfo))
					{
						if (HttpService.IsFrontEndRole(miniVirtualDirectory, serverInfo))
						{
							this.AddHttpServiceToDictionaries(fqdn, serviceLists2, serverInfo, miniVirtualDirectory, ClientAccessType.Unknown, null, AuthenticationMethod.None);
						}
						if (miniVirtualDirectory.InternalUrl != null)
						{
							AuthenticationMethod authenticationMethod = ServiceTopology.GetAuthenticationMethod(miniVirtualDirectory[MiniVirtualDirectorySchema.InternalAuthenticationMethodFlags]);
							this.AddHttpServiceToDictionaries(fqdn, serviceLists, serverInfo, miniVirtualDirectory, ClientAccessType.Internal, miniVirtualDirectory.InternalUrl, authenticationMethod);
						}
						if (miniVirtualDirectory.IsWebServices && miniVirtualDirectory.InternalNLBBypassUrl != null)
						{
							AuthenticationMethod authenticationMethod2 = ServiceTopology.GetAuthenticationMethod(miniVirtualDirectory[MiniVirtualDirectorySchema.InternalAuthenticationMethodFlags]);
							this.AddHttpServiceToDictionaries(fqdn, serviceLists, serverInfo, miniVirtualDirectory, ClientAccessType.InternalNLBBypass, miniVirtualDirectory.InternalNLBBypassUrl, authenticationMethod2);
						}
						if (miniVirtualDirectory.ExternalUrl != null)
						{
							AuthenticationMethod authenticationMethod3 = ServiceTopology.GetAuthenticationMethod(miniVirtualDirectory[MiniVirtualDirectorySchema.ExternalAuthenticationMethodFlags]);
							this.AddHttpServiceToDictionaries(fqdn, serviceLists, serverInfo, miniVirtualDirectory, ClientAccessType.External, miniVirtualDirectory.ExternalUrl, authenticationMethod3);
						}
					}
				}
			}
			ReadOnlyCollection<MiniEmailTransport> allEmailTransports = topology.AllEmailTransports;
			if (allEmailTransports != null)
			{
				foreach (MiniEmailTransport miniEmailTransport in allEmailTransports)
				{
					if (miniEmailTransport.IsPop3 || miniEmailTransport.IsImap4)
					{
						MiniEmailTransport miniEmailTransport2 = miniEmailTransport;
						TopologyServerInfo topologyServerInfo2;
						if (all.Servers.TryGetValue(miniEmailTransport2.Server.DistinguishedName, out topologyServerInfo2))
						{
							bool flag = miniEmailTransport2.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010);
							ServerVersion serverVersion = new ServerVersion(topologyServerInfo2.VersionNumber);
							flag |= (VariantConfiguration.InvariantNoFlightingSnapshot.DataStorage.CheckR3Coexistence.Enabled && serverVersion.Major == Server.Exchange2009MajorVersion && serverVersion.Build == 482);
							if ((miniEmailTransport2.InternalConnectionSettings != null && miniEmailTransport2.InternalConnectionSettings.Count > 0) || flag)
							{
								this.AddEmailTransportServiceToDictionaries(fqdn, serviceLists, topologyServerInfo2, miniEmailTransport2, ClientAccessType.Internal, AuthenticationMethod.None);
							}
							if ((miniEmailTransport2.ExternalConnectionSettings != null && miniEmailTransport2.ExternalConnectionSettings.Count > 0) || flag)
							{
								this.AddEmailTransportServiceToDictionaries(fqdn, serviceLists, topologyServerInfo2, miniEmailTransport2, ClientAccessType.External, AuthenticationMethod.None);
							}
						}
					}
				}
			}
			ReadOnlyCollection<MiniReceiveConnector> allSmtpReceiveConnectors = topology.AllSmtpReceiveConnectors;
			if (allSmtpReceiveConnectors != null)
			{
				foreach (MiniReceiveConnector miniReceiveConnector in allSmtpReceiveConnectors)
				{
					TopologyServerInfo serverInfo2;
					if (all.Servers.TryGetValue(miniReceiveConnector.Server.DistinguishedName, out serverInfo2))
					{
						this.AddSmtpServiceToDictionaries(fqdn, serviceLists, serverInfo2, miniReceiveConnector, ClientAccessType.External);
						this.AddSmtpServiceToDictionaries(fqdn, serviceLists, serverInfo2, miniReceiveConnector, ClientAccessType.Internal);
					}
				}
			}
			Dictionary<string, Site> dictionary4 = new Dictionary<string, Site>(topology.AllTopologySites.Count, StringComparer.OrdinalIgnoreCase);
			foreach (TopologySite topologySite in topology.AllTopologySites)
			{
				dictionary4[topologySite.DistinguishedName] = Site.Get(topologySite, all);
			}
			this.localServerInfo = TopologyServerInfo.Get(topology.LocalServer, all);
			this.serverToSiteDictionary = dictionary2;
			this.services = serviceLists;
			this.cafeServices = serviceLists2;
			this.serversWithoutSite = list;
			this.siteToServersDictionary = dictionary3;
			this.siteDictionary = dictionary4;
			this.serverFqdnDictionary = dictionary;
			this.connectionCostCalculator = new ConnectionCostCalculator(topology.AllTopologySites.Count);
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x0020B454 File Offset: 0x00209654
		private ServiceTopology(ServiceTopology completeTopology)
		{
			this.siteToServersDictionary = new Dictionary<Site, List<TopologyServerInfo>>(completeTopology.siteToServersDictionary.Count);
			this.serverToSiteDictionary = new Dictionary<string, Site>(completeTopology.serverToSiteDictionary.Count, StringComparer.OrdinalIgnoreCase);
			this.serverFqdnDictionary = new Dictionary<string, TopologyServerInfo>(completeTopology.serverFqdnDictionary.Count, StringComparer.OrdinalIgnoreCase);
			foreach (TopologyServerInfo topologyServerInfo in completeTopology.serverFqdnDictionary.Values)
			{
				if (topologyServerInfo.VersionNumber < Server.E15MinVersion)
				{
					this.serverFqdnDictionary.Add(topologyServerInfo.ServerFullyQualifiedDomainName, topologyServerInfo);
					Site site = topologyServerInfo.Site;
					this.serverToSiteDictionary.Add(topologyServerInfo.ServerFullyQualifiedDomainName, site);
					List<TopologyServerInfo> list;
					if (!this.siteToServersDictionary.TryGetValue(site, out list))
					{
						list = new List<TopologyServerInfo>();
						this.siteToServersDictionary.Add(site, list);
					}
					list.Add(topologyServerInfo);
				}
			}
			this.services = new Dictionary<ServiceType, List<Service>>(completeTopology.services.Count);
			foreach (KeyValuePair<ServiceType, List<Service>> keyValuePair in completeTopology.services)
			{
				List<Service> list2 = new List<Service>();
				foreach (Service service in keyValuePair.Value)
				{
					if (service.ServerVersionNumber <= Server.E15MinVersion)
					{
						list2.Add(service);
					}
				}
				this.services.Add(keyValuePair.Key, list2);
			}
			this.cafeServices = null;
			this.serversWithoutSite = completeTopology.serversWithoutSite;
			this.localServerInfo = completeTopology.localServerInfo;
			this.discoveryStarted = completeTopology.DiscoveryStarted;
			this.siteDictionary = completeTopology.siteDictionary;
			this.creationTime = completeTopology.creationTime;
			this.connectionCostCalculator = new ConnectionCostCalculator(this.siteDictionary.Count);
		}

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x0020B680 File Offset: 0x00209880
		public int TopologyRequestCount
		{
			get
			{
				return this.topologyRequestCount;
			}
		}

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x0020B688 File Offset: 0x00209888
		// (set) Token: 0x0600767B RID: 30331 RVA: 0x0020B690 File Offset: 0x00209890
		internal ExDateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
			set
			{
				this.creationTime = value;
			}
		}

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x0600767C RID: 30332 RVA: 0x0020B699 File Offset: 0x00209899
		internal DateTime DiscoveryStarted
		{
			get
			{
				return this.discoveryStarted;
			}
		}

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x0600767D RID: 30333 RVA: 0x0020B6A1 File Offset: 0x002098A1
		private static Random Random
		{
			get
			{
				if (ServiceTopology.randomObject == null)
				{
					ServiceTopology.randomObject = new Random();
				}
				return ServiceTopology.randomObject;
			}
		}

		// Token: 0x0600767E RID: 30334 RVA: 0x0020B6B9 File Offset: 0x002098B9
		public static ServiceTopology GetCurrentLegacyServiceTopology([CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return ServiceCache.GetCurrentLegacyServiceTopology();
		}

		// Token: 0x0600767F RID: 30335 RVA: 0x0020B6CD File Offset: 0x002098CD
		public static ServiceTopology GetCurrentLegacyServiceTopology(TimeSpan getServiceTopologyTimeout, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return ServiceCache.GetCurrentLegacyServiceTopology(getServiceTopologyTimeout);
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x0020B6E2 File Offset: 0x002098E2
		public static ServiceTopology GetCurrentServiceTopology([CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return ServiceCache.GetCurrentServiceTopology();
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x0020B6F6 File Offset: 0x002098F6
		public static ServiceTopology GetCurrentServiceTopology(TimeSpan getServiceTopologyTimeout, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return ServiceCache.GetCurrentServiceTopology(getServiceTopologyTimeout);
		}

		// Token: 0x06007682 RID: 30338 RVA: 0x0020B70C File Offset: 0x0020990C
		public static int ServicesOnCurrentServerFirst(Service left, Service right, ServiceTopology serviceTopology, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			int num = serviceTopology.IsServiceOnCurrentServer<Service>(left, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "ServicesOnCurrentServerFirst", 705) ? 1 : 0;
			int num2 = serviceTopology.IsServiceOnCurrentServer<Service>(right, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "ServicesOnCurrentServerFirst", 706) ? 1 : 0;
			return num2 - num;
		}

		// Token: 0x06007683 RID: 30339 RVA: 0x0020B768 File Offset: 0x00209968
		public static int CasMbxServicesFirst(Service left, Service right, string mailboxServerFqdn, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			int num = (left.ServerFullyQualifiedDomainName == mailboxServerFqdn) ? 1 : 0;
			int num2 = (right.ServerFullyQualifiedDomainName == mailboxServerFqdn) ? 1 : 0;
			return num2 - num;
		}

		// Token: 0x06007684 RID: 30340 RVA: 0x0020B7AD File Offset: 0x002099AD
		public static bool IsOnSite(Service service, Site site, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return service.Site.Equals(site);
		}

		// Token: 0x06007685 RID: 30341 RVA: 0x0020B7CC File Offset: 0x002099CC
		public Site GetSite(string serverFullyQualifiedDomainName, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			Site result;
			if (this.serverToSiteDictionary.TryGetValue(serverFullyQualifiedDomainName, out result))
			{
				return result;
			}
			if (this.serversWithoutSite.Contains(serverFullyQualifiedDomainName))
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "ServiceTopology::FindServices. The server does not have a site defined. Server = {0}.", serverFullyQualifiedDomainName);
				throw new ServerNotInSiteException(ServerStrings.ExServerNotInSite(serverFullyQualifiedDomainName), serverFullyQualifiedDomainName);
			}
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "ServiceTopology::FindServices. The server we are accessing is not found in the Topology. Server = {0}.", serverFullyQualifiedDomainName);
			ServiceCache.TriggerCacheRefreshDueToCacheMiss(this);
			throw new ServerNotFoundException(ServerStrings.ExServerNotFound(serverFullyQualifiedDomainName), serverFullyQualifiedDomainName);
		}

		// Token: 0x06007686 RID: 30342 RVA: 0x0020B860 File Offset: 0x00209A60
		public bool IsServiceOnCurrentServer<T>(T service, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			return string.Equals(this.localServerInfo.ServerFullyQualifiedDomainName, service.ServerFullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007687 RID: 30343 RVA: 0x0020B8AC File Offset: 0x00209AAC
		public bool TryGetConnectionCost(Site site1, Site site2, out int cost, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			Site site3;
			Site site4;
			if (this.TryGetEquivalentSiteInThisTopology(site1, out site3) && this.TryGetEquivalentSiteInThisTopology(site2, out site4))
			{
				return this.connectionCostCalculator.TryGetConnectionCost(site3, site4, out cost);
			}
			cost = int.MaxValue;
			return false;
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x0020B8F8 File Offset: 0x00209AF8
		public bool TryGetRandomServerFromCurrentSite(ServerRole serverRole, out string serverFqdn, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			int num;
			return this.TryGetRandomServerFromCurrentSite(serverRole, 0, out serverFqdn, out num, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServerFromCurrentSite", 907);
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x0020B930 File Offset: 0x00209B30
		public bool TryGetRandomServerFromCurrentSite(ServerRole serverRole, int minimumServerVersion, out string serverFqdn, out int serverVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return this.TryGetRandomServerFromCurrentSite(serverRole, minimumServerVersion, false, out serverFqdn, out serverVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServerFromCurrentSite", 938);
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x0020B968 File Offset: 0x00209B68
		public bool TryGetRandomServerFromCurrentSite(ServerRole serverRole, int serverVersion, bool exactVersion, out string serverFqdn, out int foundVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return this.TryGetRandomServerFromCurrentSite(serverRole, serverVersion, exactVersion ? ServiceTopology.RandomServerSearchType.ExactVersion : ServiceTopology.RandomServerSearchType.MinimumVersion, out serverFqdn, out foundVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServerFromCurrentSite", 967);
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x0020B9A8 File Offset: 0x00209BA8
		public bool TryGetRandomServerFromCurrentSite(ServerRole serverRole, int serverVersion, ServiceTopology.RandomServerSearchType searchType, out string serverFqdn, out int foundVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if ((this.localServerInfo.Role & serverRole) > ServerRole.None && ((ServiceTopology.RandomServerSearchType.ExactVersion == searchType && this.localServerInfo.VersionNumber == serverVersion) || (searchType == ServiceTopology.RandomServerSearchType.MinimumVersion && this.localServerInfo.VersionNumber >= serverVersion) || (ServiceTopology.RandomServerSearchType.MinimumVersionMatchMajor == searchType && this.localServerInfo.VersionNumber >= serverVersion && new ServerVersion(this.localServerInfo.VersionNumber).Major == new ServerVersion(serverVersion).Major)))
			{
				serverFqdn = this.localServerInfo.ServerFullyQualifiedDomainName;
				foundVersion = this.localServerInfo.VersionNumber;
				return true;
			}
			return this.TryGetRandomServer(this.localServerInfo.Site, serverRole, serverVersion, searchType, out serverFqdn, out foundVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServerFromCurrentSite", 1015);
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x0020BA74 File Offset: 0x00209C74
		public bool TryGetRandomServerFromCurrentSite(ServerRole serverRole, ServerVersion serverVersion, ServiceTopology.RandomServerSearchType searchType, out string serverFqdn, out int foundVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if ((this.localServerInfo.Role & serverRole) > ServerRole.None && ((ServiceTopology.RandomServerSearchType.ExactVersion == searchType && this.localServerInfo.AdminDisplayVersionNumber == serverVersion) || (searchType == ServiceTopology.RandomServerSearchType.MinimumVersion && ServerVersion.Compare(this.localServerInfo.AdminDisplayVersionNumber, serverVersion) >= 0) || (ServiceTopology.RandomServerSearchType.MinimumVersionMatchMajor == searchType && ServerVersion.Compare(this.localServerInfo.AdminDisplayVersionNumber, serverVersion) >= 0 && new ServerVersion(this.localServerInfo.VersionNumber).Major == serverVersion.Major)))
			{
				serverFqdn = this.localServerInfo.ServerFullyQualifiedDomainName;
				foundVersion = this.localServerInfo.VersionNumber;
				return true;
			}
			return this.TryGetRandomServer(this.localServerInfo.Site, serverRole, serverVersion, searchType, out serverFqdn, out foundVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServerFromCurrentSite", 1058);
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x0020BB4C File Offset: 0x00209D4C
		public bool TryGetRandomServer(Site site, ServerRole serverRole, out string serverFqdn, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			int num;
			return this.TryGetRandomServer(site, serverRole, 0, out serverFqdn, out num, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServer", 1081);
		}

		// Token: 0x0600768E RID: 30350 RVA: 0x0020BB84 File Offset: 0x00209D84
		public bool TryGetRandomServer(Site site, ServerRole serverRole, int minimumServerVersion, out string serverFqdn, out int serverVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return this.TryGetRandomServer(site, serverRole, minimumServerVersion, ServiceTopology.RandomServerSearchType.MinimumVersion, out serverFqdn, out serverVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "TryGetRandomServer", 1112);
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x0020BC70 File Offset: 0x00209E70
		public bool TryGetRandomServer(Site site, ServerRole serverRole, int serverVersion, ServiceTopology.RandomServerSearchType searchType, out string serverFqdn, out int foundVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			List<TopologyServerInfo> list;
			if (this.siteToServersDictionary.TryGetValue(site, out list))
			{
				List<TopologyServerInfo> list2;
				if (ServiceTopology.RandomServerSearchType.ExactVersion == searchType)
				{
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && server.VersionNumber == serverVersion);
				}
				else if (searchType == ServiceTopology.RandomServerSearchType.MinimumVersion)
				{
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && server.VersionNumber >= serverVersion);
				}
				else
				{
					int nextVersion = new ServerVersion(new ServerVersion(serverVersion).Major + 1, 0, 0, 0).ToInt();
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && server.VersionNumber >= serverVersion && server.VersionNumber < nextVersion);
				}
				if (list2.Count > 0)
				{
					int index = ServiceTopology.Random.Next(list2.Count);
					serverFqdn = list2[index].ServerFullyQualifiedDomainName;
					foundVersion = list2[index].VersionNumber;
					return true;
				}
			}
			serverFqdn = null;
			foundVersion = 0;
			return false;
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x0020BE54 File Offset: 0x0020A054
		public bool TryGetRandomServer(Site site, ServerRole serverRole, ServerVersion serverVersion, ServiceTopology.RandomServerSearchType searchType, out string serverFqdn, out int foundVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			List<TopologyServerInfo> list;
			if (this.siteToServersDictionary.TryGetValue(site, out list))
			{
				List<TopologyServerInfo> list2;
				if (ServiceTopology.RandomServerSearchType.ExactVersion == searchType)
				{
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && server.AdminDisplayVersionNumber == serverVersion);
				}
				else if (searchType == ServiceTopology.RandomServerSearchType.MinimumVersion)
				{
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && ServerVersion.Compare(server.AdminDisplayVersionNumber, serverVersion) >= 0);
				}
				else
				{
					ServerVersion nextVersion = new ServerVersion(serverVersion.Major + 1, 0, 0, 0);
					list2 = list.FindAll((TopologyServerInfo server) => (server.Role & serverRole) != ServerRole.None && !server.IsOutOfService && ServerVersion.Compare(server.AdminDisplayVersionNumber, serverVersion) >= 0 && ServerVersion.Compare(server.AdminDisplayVersionNumber, nextVersion) < 0);
				}
				if (list2.Count > 0)
				{
					int index = ServiceTopology.Random.Next(list2.Count);
					serverFqdn = list2[index].ServerFullyQualifiedDomainName;
					foundVersion = list2[index].VersionNumber;
					return true;
				}
			}
			serverFqdn = null;
			foundVersion = 0;
			return false;
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x0020BF98 File Offset: 0x0020A198
		public bool TryGetServerForRoleAndVersion(ServerRole serverRole, ServerVersion version, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			foreach (KeyValuePair<Site, List<TopologyServerInfo>> keyValuePair in this.siteToServersDictionary)
			{
				if (keyValuePair.Value.Count((TopologyServerInfo server) => (server.Role & serverRole) == serverRole && server.AdminDisplayVersionNumber.Equals(version)) > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x0020C034 File Offset: 0x0020A234
		public bool TryGetServerVersion(string serverFqdn, out int serverVersion, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (this.serverFqdnDictionary.ContainsKey(serverFqdn))
			{
				serverVersion = this.serverFqdnDictionary[serverFqdn].VersionNumber;
				return true;
			}
			serverVersion = 0;
			return false;
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x0020C06C File Offset: 0x0020A26C
		public bool IsServerOutOfService(string serverFqdn, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			TopologyServerInfo topologyServerInfo;
			return this.serverFqdnDictionary.TryGetValue(serverFqdn, out topologyServerInfo) && topologyServerInfo.IsOutOfService;
		}

		// Token: 0x06007694 RID: 30356 RVA: 0x0020C100 File Offset: 0x0020A300
		public T FindAny<T>(ClientAccessType clientAccessType, Predicate<T> serviceFilter, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			Func<Service, T> func = null;
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			EnumValidator.ThrowIfInvalid<ClientAccessType>(clientAccessType, "clientAccessType");
			if (serviceFilter == null)
			{
				throw new ArgumentNullException("serviceFilter");
			}
			ServiceType serviceType = ServiceTopology.ServiceTypeMapper<T>.GetServiceType();
			List<Service> list = null;
			if (!this.services.TryGetValue(serviceType, out list) || list == null || list.Count <= 0)
			{
				return default(T);
			}
			IEnumerable<Service> source = list.Where(delegate(Service service)
			{
				T t = (T)((object)service);
				return t.ClientAccessType == clientAccessType && serviceFilter((T)((object)service));
			});
			if (func == null)
			{
				func = ((Service service) => (T)((object)service));
			}
			List<T> list2 = source.Select(func).ToList<T>();
			if (list2.Count == 0)
			{
				return default(T);
			}
			int index = ServiceTopology.Random.Next(list2.Count);
			return list2[index];
		}

		// Token: 0x06007695 RID: 30357 RVA: 0x0020C218 File Offset: 0x0020A418
		public T FindAnyCafeService<T>(Predicate<T> serviceFilter, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			Func<Service, T> func = null;
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (serviceFilter == null)
			{
				throw new ArgumentNullException("serviceFilter");
			}
			ServiceType serviceType = ServiceTopology.ServiceTypeMapper<T>.GetServiceType();
			List<Service> list = null;
			if (this.cafeServices.TryGetValue(serviceType, out list) && list != null && list.Count > 0)
			{
				IEnumerable<Service> source = from service in list
				where serviceFilter((T)((object)service))
				select service;
				if (func == null)
				{
					func = ((Service service) => (T)((object)service));
				}
				List<T> list2 = source.Select(func).ToList<T>();
				if (list2.Count > 0)
				{
					int index = ServiceTopology.Random.Next(list2.Count);
					return list2[index];
				}
			}
			ServiceCache.TriggerCacheRefreshDueToCacheMiss(this);
			return default(T);
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x0020C2EF File Offset: 0x0020A4EF
		public IList<T> FindAll<T>(IExchangePrincipal mailboxUser, ClientAccessType clientAccessType, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return this.FindAll<T>(mailboxUser, clientAccessType, (T service) => true, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "FindAll", 1440);
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x0020C440 File Offset: 0x0020A640
		public IList<T> FindAll<T>(IExchangePrincipal mailboxUser, ClientAccessType clientAccessType, Predicate<T> serviceFilter, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (mailboxUser == null)
			{
				throw new ArgumentNullException("mailboxUser");
			}
			if (serviceFilter == null)
			{
				throw new ArgumentNullException("serviceFilter");
			}
			EnumValidator.ThrowIfInvalid<ClientAccessType>(clientAccessType, "clientAccessType");
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug((long)this.GetHashCode(), "ServiceTopology::FindAll<{0}>. Finding services. CurrentFQDN = {1}. UserFQDN = {2}. ClientAccessType = {3}.", new object[]
			{
				typeof(T).ToString(),
				this.localServerInfo.ServerFullyQualifiedDomainName,
				mailboxUser.MailboxInfo.Location.ServerFqdn,
				clientAccessType
			});
			Site mailboxSite = this.GetSite(mailboxUser.MailboxInfo.Location.ServerFqdn, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "FindAll", 1488);
			ServiceTopology.ServiceComparer<T> comparer = new ServiceTopology.ServiceComparer<T>(this, mailboxUser.MailboxInfo.Location.ServerFqdn);
			SortedDictionary<T, List<T>> serviceLists = new SortedDictionary<T, List<T>>(comparer);
			List<T> list = new List<T>();
			this.ForEach<T>(delegate(T service)
			{
				if (ServiceTopology.IsOnSite(service, mailboxSite, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "FindAll", 1496) && service.ClientAccessType == clientAccessType && serviceFilter(service))
				{
					if (!serviceLists.ContainsKey(service))
					{
						serviceLists.Add(service, new List<T>());
					}
					int num = ServiceTopology.Random.Next(serviceLists[service].Count + 1);
					ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug((long)this.GetHashCode(), "ServiceTopology::FindAll<{0}>. Inserting a service into {1} of {2}. FQDN = {3}.", new object[]
					{
						typeof(T).ToString(),
						num,
						serviceLists[service].Count,
						service.ServerFullyQualifiedDomainName
					});
					serviceLists[service].Insert(num, service);
				}
			}, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "FindAll", 1492);
			foreach (KeyValuePair<T, List<T>> keyValuePair in serviceLists)
			{
				list.AddRange(keyValuePair.Value);
			}
			return list.AsReadOnly();
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x0020C5D8 File Offset: 0x0020A7D8
		public void ForEach<T>(Action<T> action, [CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0) where T : Service
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			ServiceType serviceType = ServiceTopology.ServiceTypeMapper<T>.GetServiceType();
			List<Service> list = null;
			if (this.services.TryGetValue(serviceType, out list))
			{
				foreach (Service service in list)
				{
					action((T)((object)service));
				}
			}
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x0020C660 File Offset: 0x0020A860
		public void IncrementRequestCount([CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			Interlocked.Increment(ref this.topologyRequestCount);
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x0020C67B File Offset: 0x0020A87B
		public ServiceTopology ToLegacyServiceTopology([CallerFilePath] string callerFilePath = null, [CallerMemberName] string memberName = null, [CallerLineNumber] int callerFileLine = 0)
		{
			ServiceTopologyLog.Instance.Append(callerFilePath, memberName, callerFileLine);
			return new ServiceTopology(this);
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x0020C690 File Offset: 0x0020A890
		private static void AddServiceToServiceListDictionary(Dictionary<ServiceType, List<Service>> serviceLists, Service service)
		{
			ServiceType serviceType = service.ServiceType;
			List<Service> list;
			if (!serviceLists.TryGetValue(serviceType, out list))
			{
				list = new List<Service>();
				serviceLists.Add(serviceType, list);
			}
			list.Add(service);
		}

		// Token: 0x0600769C RID: 30364 RVA: 0x0020C6C4 File Offset: 0x0020A8C4
		private static AuthenticationMethod GetAuthenticationMethod(object authenticationMethodObject)
		{
			if (authenticationMethodObject is AuthenticationMethodFlags)
			{
				AuthenticationMethod authenticationMethod = (AuthenticationMethod)authenticationMethodObject;
				if (EnumValidator.IsValidValue<AuthenticationMethod>(authenticationMethod))
				{
					return authenticationMethod;
				}
			}
			return AuthenticationMethod.None;
		}

		// Token: 0x0600769D RID: 30365 RVA: 0x0020C6EC File Offset: 0x0020A8EC
		private bool IsAServiceOnCurrentServer<T>(T[] services) where T : Service
		{
			foreach (T service in services)
			{
				if (this.IsServiceOnCurrentServer<T>(service, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "IsAServiceOnCurrentServer", 1646))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x0020C730 File Offset: 0x0020A930
		private void AddHttpServiceToDictionaries(string localServerFqdn, Dictionary<ServiceType, List<Service>> serviceLists, TopologyServerInfo serverInfo, MiniVirtualDirectory virtualDirectory, ClientAccessType clientAccessType, Uri url, AuthenticationMethod authenticationMethod)
		{
			Service service = ServiceTypeInfo.CreateHttpService(virtualDirectory, serverInfo, url, clientAccessType, authenticationMethod);
			ServiceTopology.AddServiceToServiceListDictionary(serviceLists, service);
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<Service, string, Site>((long)this.GetHashCode(), "ServiceTopology::AddServiceToDictionaries. Found Service. Service = {0}. ServerFqdn = {1}. Site = {2}.", service, serverInfo.ServerFullyQualifiedDomainName, serverInfo.Site);
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x0020C778 File Offset: 0x0020A978
		private void AddEmailTransportServiceToDictionaries(string localServerFqdn, Dictionary<ServiceType, List<Service>> serviceLists, TopologyServerInfo serverInfo, MiniEmailTransport emailTransport, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod)
		{
			Service service = ServiceTypeInfo.CreateEmailTransportService(emailTransport, serverInfo, clientAccessType, authenticationMethod);
			ServiceTopology.AddServiceToServiceListDictionary(serviceLists, service);
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<Service, string, Site>((long)this.GetHashCode(), "ServiceTopology::AddServiceToDictionaries. Found Service. Service = {0}. ServerFqdn = {1}. Site = {2}.", service, serverInfo.ServerFullyQualifiedDomainName, serverInfo.Site);
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x0020C7BC File Offset: 0x0020A9BC
		private void AddSmtpServiceToDictionaries(string localServerFqdn, Dictionary<ServiceType, List<Service>> serviceLists, TopologyServerInfo serverInfo, MiniReceiveConnector smtpReceiveConnector, ClientAccessType clientAccessType)
		{
			Service service = ServiceTypeInfo.CreateSmtpService(smtpReceiveConnector, serverInfo, clientAccessType);
			if (service != null)
			{
				ServiceTopology.AddServiceToServiceListDictionary(serviceLists, service);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<Service, string, Site>((long)this.GetHashCode(), "ServiceTopology::AddServiceToDictionaries. Found Service. Service = {0}. ServerFqdn = {1}. Site = {2}.", service, serverInfo.ServerFullyQualifiedDomainName, serverInfo.Site);
			}
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x0020C801 File Offset: 0x0020AA01
		private bool TryGetEquivalentSiteInThisTopology(Site foreignSite, out Site equivalentSite)
		{
			return this.siteDictionary.TryGetValue(foreignSite.DistinguishedName, out equivalentSite);
		}

		// Token: 0x04005225 RID: 21029
		private static readonly Service[] EmptyServiceArray = Array<Service>.Empty;

		// Token: 0x04005226 RID: 21030
		[ThreadStatic]
		private static Random randomObject;

		// Token: 0x04005227 RID: 21031
		private readonly Dictionary<ServiceType, List<Service>> services;

		// Token: 0x04005228 RID: 21032
		private readonly Dictionary<ServiceType, List<Service>> cafeServices;

		// Token: 0x04005229 RID: 21033
		private readonly Dictionary<string, Site> serverToSiteDictionary;

		// Token: 0x0400522A RID: 21034
		private readonly List<string> serversWithoutSite;

		// Token: 0x0400522B RID: 21035
		private readonly Dictionary<Site, List<TopologyServerInfo>> siteToServersDictionary;

		// Token: 0x0400522C RID: 21036
		private readonly Dictionary<string, TopologyServerInfo> serverFqdnDictionary;

		// Token: 0x0400522D RID: 21037
		private readonly TopologyServerInfo localServerInfo;

		// Token: 0x0400522E RID: 21038
		private readonly DateTime discoveryStarted;

		// Token: 0x0400522F RID: 21039
		private readonly ConnectionCostCalculator connectionCostCalculator;

		// Token: 0x04005230 RID: 21040
		private readonly Dictionary<string, Site> siteDictionary;

		// Token: 0x04005231 RID: 21041
		private ExDateTime creationTime = ExDateTime.UtcNow;

		// Token: 0x04005232 RID: 21042
		private int topologyRequestCount;

		// Token: 0x02000D65 RID: 3429
		public enum RandomServerSearchType
		{
			// Token: 0x04005234 RID: 21044
			MinimumVersion,
			// Token: 0x04005235 RID: 21045
			ExactVersion,
			// Token: 0x04005236 RID: 21046
			MinimumVersionMatchMajor
		}

		// Token: 0x02000D66 RID: 3430
		internal class All
		{
			// Token: 0x060076A6 RID: 30374 RVA: 0x0020C824 File Offset: 0x0020AA24
			public All(ExchangeTopology topology)
			{
				this.Servers = new Dictionary<string, TopologyServerInfo>((topology != null) ? topology.AllTopologyServers.Count : 0, StringComparer.OrdinalIgnoreCase);
				this.Sites = new Dictionary<string, Site>((topology != null) ? topology.AllTopologySites.Count : 0, StringComparer.OrdinalIgnoreCase);
				this.SiteLinks = new Dictionary<string, SiteLink>((topology != null) ? topology.AllTopologySiteLinks.Count : 0, StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x04005237 RID: 21047
			public readonly Dictionary<string, TopologyServerInfo> Servers;

			// Token: 0x04005238 RID: 21048
			public readonly Dictionary<string, Site> Sites;

			// Token: 0x04005239 RID: 21049
			public readonly Dictionary<string, SiteLink> SiteLinks;
		}

		// Token: 0x02000D67 RID: 3431
		private static class ServiceTypeMapper<T> where T : Service
		{
			// Token: 0x060076A7 RID: 30375 RVA: 0x0020C89A File Offset: 0x0020AA9A
			public static ServiceType GetServiceType()
			{
				return ServiceTopology.ServiceTypeMapper<T>.serviceType;
			}

			// Token: 0x0400523A RID: 21050
			private static ServiceType serviceType = ServiceTypeInfo.GetServiceType(typeof(T));
		}

		// Token: 0x02000D68 RID: 3432
		private class ServiceComparer<T> : IComparer<T> where T : Service
		{
			// Token: 0x060076A9 RID: 30377 RVA: 0x0020C8B7 File Offset: 0x0020AAB7
			public ServiceComparer(ServiceTopology serviceTopology, string fqdn)
			{
				this.serviceTopology = serviceTopology;
				this.serverFQDN = fqdn;
			}

			// Token: 0x060076AA RID: 30378 RVA: 0x0020C8D0 File Offset: 0x0020AAD0
			public int Compare(T x, T y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				int num = ServiceTopology.ServicesOnCurrentServerFirst(x, y, this.serviceTopology, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "Compare", 1889);
				if (num == 0)
				{
					num = ServiceTopology.CasMbxServicesFirst(x, y, this.serverFQDN, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceTopology.cs", "Compare", 1893);
				}
				return num;
			}

			// Token: 0x0400523B RID: 21051
			private ServiceTopology serviceTopology;

			// Token: 0x0400523C RID: 21052
			private string serverFQDN;
		}
	}
}
