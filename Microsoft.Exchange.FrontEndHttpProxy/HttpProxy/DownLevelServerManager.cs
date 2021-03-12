using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000025 RID: 37
	internal sealed class DownLevelServerManager
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00005E21 File Offset: 0x00004021
		private DownLevelServerManager()
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005E44 File Offset: 0x00004044
		public static bool IsApplicable
		{
			get
			{
				switch (HttpProxyGlobals.ProtocolType)
				{
				case ProtocolType.Eas:
				case ProtocolType.Ecp:
				case ProtocolType.Ews:
				case ProtocolType.Oab:
				case ProtocolType.Owa:
				case ProtocolType.OwaCalendar:
				case ProtocolType.PowerShell:
				case ProtocolType.PowerShellLiveId:
				case ProtocolType.RpcHttp:
				case ProtocolType.Autodiscover:
				case ProtocolType.Xrop:
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005E98 File Offset: 0x00004098
		public static DownLevelServerManager Instance
		{
			get
			{
				if (DownLevelServerManager.instance == null)
				{
					lock (DownLevelServerManager.staticLock)
					{
						if (DownLevelServerManager.instance == null)
						{
							DownLevelServerManager.instance = new DownLevelServerManager();
						}
					}
				}
				return DownLevelServerManager.instance;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005EF0 File Offset: 0x000040F0
		public static bool IsServerDiscoverable(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			try
			{
				ServiceTopology currentLegacyServiceTopology = ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "IsServerDiscoverable", 176);
				currentLegacyServiceTopology.GetSite(fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "IsServerDiscoverable", 177);
			}
			catch (ServerNotFoundException)
			{
				return false;
			}
			catch (ServerNotInSiteException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005F74 File Offset: 0x00004174
		public void Initialize()
		{
			if (this.serverMapUpdateTimer != null)
			{
				return;
			}
			lock (this.instanceLock)
			{
				if (this.serverMapUpdateTimer == null)
				{
					ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[DownLevelServerManager::Initialize]: Initializing.");
					this.RefreshServerMap(false);
					this.serverMapUpdateTimer = new Timer(delegate(object o)
					{
						this.RefreshServerMap(true);
					}, null, DownLevelServerManager.DownLevelServerMapRefreshInterval.Value, DownLevelServerManager.DownLevelServerMapRefreshInterval.Value);
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006098 File Offset: 0x00004298
		public BackEndServer GetDownLevelClientAccessServerWithPreferredServer<ServiceType>(AnchorMailbox anchorMailbox, string preferredCasServerFqdn, ClientAccessType clientAccessType, RequestDetailsLogger logger, int destinationVersion) where ServiceType : HttpService
		{
			Predicate<ServiceType> predicate = null;
			Predicate<ServiceType> predicate2 = null;
			if (anchorMailbox == null)
			{
				throw new ArgumentNullException("anchorMailbox");
			}
			if (string.IsNullOrEmpty(preferredCasServerFqdn))
			{
				throw new ArgumentException("preferredCasServerFqdn cannot be empty!");
			}
			ServiceTopology currentLegacyServiceTopology = ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetDownLevelClientAccessServerWithPreferredServer", 253);
			Site site = currentLegacyServiceTopology.GetSite(preferredCasServerFqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetDownLevelClientAccessServerWithPreferredServer", 254);
			Dictionary<string, List<DownLevelServerStatusEntry>> downLevelServerMap = this.GetDownLevelServerMap();
			List<DownLevelServerStatusEntry> list = null;
			if (!downLevelServerMap.TryGetValue(site.DistinguishedName, out list))
			{
				string text = string.Format("Unable to find site {0} in the down level server map.", site.DistinguishedName);
				ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetDownLevelClientAccessServerWithPreferredServer]: {0}", text);
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					this.RefreshServerMap(true);
				});
				throw new NoAvailableDownLevelBackEndException(text);
			}
			DownLevelServerStatusEntry downLevelServerStatusEntry = list.Find((DownLevelServerStatusEntry backend) => preferredCasServerFqdn.Equals(backend.BackEndServer.Fqdn, StringComparison.OrdinalIgnoreCase));
			if (downLevelServerStatusEntry == null)
			{
				string text2 = string.Format("Unable to find preferred server {0} in the back end server map.", preferredCasServerFqdn);
				ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetDownLevelClientAccessServerWithPreferredServer]: {0}", text2);
				throw new NoAvailableDownLevelBackEndException(text2);
			}
			if (downLevelServerStatusEntry.IsHealthy)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<DownLevelServerStatusEntry>((long)this.GetHashCode(), "[DownLevelServerManager::GetDownLevelClientAccessServerWithPreferredServer]: The preferred server {0} is healthy.", downLevelServerStatusEntry);
				return downLevelServerStatusEntry.BackEndServer;
			}
			ServiceType serviceType = default(ServiceType);
			if (destinationVersion < Server.E14MinVersion)
			{
				try
				{
					List<DownLevelServerStatusEntry> serverList = list;
					ServiceTopology topology = currentLegacyServiceTopology;
					Site targetSite = site;
					if (predicate == null)
					{
						predicate = ((ServiceType service) => service.ServerVersionNumber >= Server.E2007MinVersion && service.ServerVersionNumber < Server.E14MinVersion);
					}
					serviceType = this.GetClientAccessServiceFromList<ServiceType>(serverList, topology, anchorMailbox, targetSite, clientAccessType, predicate, logger, DownLevelServerManager.DownlevelExchangeServerVersion.Exchange2007);
				}
				catch (NoAvailableDownLevelBackEndException)
				{
					ExTraceGlobals.VerboseTracer.TraceError((long)this.GetHashCode(), "[DownLevelServerManager::GetDownLevelClientAccessServerWithPreferredServer]: No E12 CAS could be found for E12 destination. Looking for E14 CAS.");
				}
			}
			if (serviceType == null)
			{
				List<DownLevelServerStatusEntry> serverList2 = list;
				ServiceTopology topology2 = currentLegacyServiceTopology;
				Site targetSite2 = site;
				if (predicate2 == null)
				{
					predicate2 = ((ServiceType service) => service.ServerVersionNumber >= Server.E14MinVersion && service.ServerVersionNumber < Server.E15MinVersion);
				}
				serviceType = this.GetClientAccessServiceFromList<ServiceType>(serverList2, topology2, anchorMailbox, targetSite2, clientAccessType, predicate2, logger, DownLevelServerManager.DownlevelExchangeServerVersion.Exchange2010);
			}
			return new BackEndServer(serviceType.ServerFullyQualifiedDomainName, serviceType.ServerVersionNumber);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006308 File Offset: 0x00004508
		public BackEndServer GetDownLevelClientAccessServer<ServiceType>(AnchorMailbox anchorMailbox, BackEndServer mailboxServer, ClientAccessType clientAccessType, RequestDetailsLogger logger, bool calculateRedirectUrl, out Uri redirectUrl) where ServiceType : HttpService
		{
			if (anchorMailbox == null)
			{
				throw new ArgumentNullException("anchorMailbox");
			}
			if (mailboxServer == null)
			{
				throw new ArgumentNullException("mailboxServer");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (!DownLevelServerManager.IsApplicable)
			{
				throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.EndpointNotFound, string.Format("{0} does not support down level server proxy.", HttpProxyGlobals.ProtocolType));
			}
			redirectUrl = null;
			if (mailboxServer.Version < Server.E14MinVersion)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, int, string>((long)this.GetHashCode(), "[DownLevelServerManager::GetDownLevelClientAccessServer]: Found mailbox server version {0}, which was pre-E14 minimum version {1}, so returning mailbox server FQDN {2}", mailboxServer.Version, Server.E14MinVersion, mailboxServer.Fqdn);
				return mailboxServer;
			}
			ServiceTopology currentLegacyServiceTopology = ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetDownLevelClientAccessServer", 393);
			Site site = currentLegacyServiceTopology.GetSite(mailboxServer.Fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetDownLevelClientAccessServer", 394);
			ServiceType result = this.GetClientAccessServiceInSite<ServiceType>(currentLegacyServiceTopology, anchorMailbox, site, clientAccessType, (ServiceType service) => service.ServerVersionNumber >= Server.E14MinVersion && service.ServerVersionNumber < Server.E15MinVersion, logger);
			if (calculateRedirectUrl && !Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoCrossSiteRedirect.Enabled && result != null && !string.IsNullOrEmpty(result.ServerFullyQualifiedDomainName))
			{
				Site member = HttpProxyGlobals.LocalSite.Member;
				if (!member.DistinguishedName.Equals(result.Site.DistinguishedName))
				{
					HttpService httpService = currentLegacyServiceTopology.FindAny<ServiceType>(ClientAccessType.External, (ServiceType externalService) => externalService != null && externalService.ServerFullyQualifiedDomainName.Equals(result.ServerFullyQualifiedDomainName, StringComparison.OrdinalIgnoreCase), "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetDownLevelClientAccessServer", 419);
					if (httpService != null)
					{
						redirectUrl = httpService.Url;
					}
				}
			}
			return new BackEndServer(result.ServerFullyQualifiedDomainName, result.ServerVersionNumber);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000064EC File Offset: 0x000046EC
		public BackEndServer GetRandomDownLevelClientAccessServer()
		{
			Dictionary<string, List<DownLevelServerStatusEntry>> downLevelServerMap = this.GetDownLevelServerMap();
			string distinguishedName = HttpProxyGlobals.LocalSite.Member.DistinguishedName;
			List<DownLevelServerStatusEntry> serverList = null;
			if (downLevelServerMap.TryGetValue(distinguishedName, out serverList))
			{
				serverList = downLevelServerMap[distinguishedName];
				BackEndServer backEndServer = this.PickRandomServerInSite(serverList);
				if (backEndServer != null)
				{
					return backEndServer;
				}
			}
			for (int i = 0; i < downLevelServerMap.Count; i++)
			{
				if (!(downLevelServerMap.ElementAt(i).Key == distinguishedName))
				{
					serverList = downLevelServerMap.ElementAt(i).Value;
					BackEndServer backEndServer = this.PickRandomServerInSite(serverList);
					if (backEndServer != null)
					{
						return backEndServer;
					}
				}
			}
			string text = string.Format("Unable to find a healthy downlevel server in any site.", new object[0]);
			ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetRandomDownlevelClientAccessServer]: {0}", text);
			throw new NoAvailableDownLevelBackEndException(text);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000065B4 File Offset: 0x000047B4
		public void Close()
		{
			lock (this.instanceLock)
			{
				if (this.pingManager != null)
				{
					this.pingManager.Dispose();
					this.pingManager = null;
				}
				if (this.serverMapUpdateTimer != null)
				{
					this.serverMapUpdateTimer.Dispose();
					this.serverMapUpdateTimer = null;
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006624 File Offset: 0x00004824
		internal static int[] GetShuffledList(int length, int randomNumberSeed)
		{
			Random random = new Random(randomNumberSeed);
			int[] array = new int[length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			array.Shuffle(random);
			return array;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000066AC File Offset: 0x000048AC
		internal List<DownLevelServerStatusEntry> GetFilteredServerListByVersion(List<DownLevelServerStatusEntry> serverList, DownLevelServerManager.DownlevelExchangeServerVersion serverVersion)
		{
			if (ExTraceGlobals.VerboseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetFilteredServerListByVersion]: Filtering ServerList by Version: {0}", serverVersion.ToString());
			}
			switch (serverVersion)
			{
			case DownLevelServerManager.DownlevelExchangeServerVersion.Exchange2007:
				return serverList.FindAll((DownLevelServerStatusEntry server) => server.BackEndServer.Version >= Server.E2007MinVersion && server.BackEndServer.Version < Server.E14MinVersion);
			case DownLevelServerManager.DownlevelExchangeServerVersion.Exchange2010:
				return serverList.FindAll((DownLevelServerStatusEntry server) => server.BackEndServer.Version >= Server.E14MinVersion && server.BackEndServer.Version < Server.E15MinVersion);
			default:
				return serverList;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006742 File Offset: 0x00004942
		private Dictionary<string, List<DownLevelServerStatusEntry>> GetDownLevelServerMap()
		{
			return this.downLevelServers;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000674C File Offset: 0x0000494C
		private BackEndServer PickRandomServerInSite(List<DownLevelServerStatusEntry> serverList)
		{
			Random random = new Random();
			int num = random.Next(serverList.Count);
			int num2 = num;
			DownLevelServerStatusEntry downLevelServerStatusEntry;
			for (;;)
			{
				downLevelServerStatusEntry = serverList[num2];
				if (downLevelServerStatusEntry.IsHealthy)
				{
					break;
				}
				num2++;
				if (num2 >= serverList.Count)
				{
					num2 = 0;
				}
				if (num2 == num)
				{
					goto Block_3;
				}
			}
			return downLevelServerStatusEntry.BackEndServer;
			Block_3:
			return null;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000679C File Offset: 0x0000499C
		private void RefreshServerMap(bool isTimer)
		{
			ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[DownLevelServerManager::RefreshServerMap]: Refreshing server map.");
			Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_RefreshingDownLevelServerMap, null, new object[]
			{
				HttpProxyGlobals.ProtocolType.ToString()
			});
			try
			{
				this.InternalRefresh();
			}
			catch (Exception exception)
			{
				if (!isTimer)
				{
					throw;
				}
				Diagnostics.ReportException(exception, FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, "Exception from RefreshServerMap: {0}");
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000682C File Offset: 0x00004A2C
		private ServiceType GetClientAccessServiceInSite<ServiceType>(ServiceTopology topology, AnchorMailbox anchorMailbox, Site targetSite, ClientAccessType clientAccessType, Predicate<ServiceType> otherFilter, RequestDetailsLogger logger) where ServiceType : HttpService
		{
			Dictionary<string, List<DownLevelServerStatusEntry>> downLevelServerMap = this.GetDownLevelServerMap();
			List<DownLevelServerStatusEntry> serverList = null;
			if (!downLevelServerMap.TryGetValue(targetSite.DistinguishedName, out serverList))
			{
				string text = string.Format("Unable to find site {0} in the down level server map.", targetSite.DistinguishedName);
				ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceInSite]: {0}", text);
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					this.RefreshServerMap(true);
				});
				throw new NoAvailableDownLevelBackEndException(text);
			}
			return this.GetClientAccessServiceFromList<ServiceType>(serverList, topology, anchorMailbox, targetSite, clientAccessType, otherFilter, logger, DownLevelServerManager.DownlevelExchangeServerVersion.Exchange2010);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006918 File Offset: 0x00004B18
		private ServiceType GetClientAccessServiceFromList<ServiceType>(List<DownLevelServerStatusEntry> serverList, ServiceTopology topology, AnchorMailbox anchorMailbox, Site targetSite, ClientAccessType clientAccessType, Predicate<ServiceType> otherFilter, RequestDetailsLogger logger, DownLevelServerManager.DownlevelExchangeServerVersion targetDownlevelExchangeServerVersion) where ServiceType : HttpService
		{
			string text = anchorMailbox.ToCookieKey();
			int hashCode = HttpProxyBackEndHelper.GetHashCode(text);
			serverList = this.GetFilteredServerListByVersion(serverList, targetDownlevelExchangeServerVersion);
			int[] shuffledList = DownLevelServerManager.GetShuffledList(serverList.Count, hashCode);
			if (ExTraceGlobals.VerboseTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string, int, string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: HashKey: {0}, HashCode: {1}, Anchor mailbox {2}.", text, hashCode, anchorMailbox.ToString());
			}
			for (int i = 0; i < shuffledList.Length; i++)
			{
				int num = shuffledList[i];
				DownLevelServerStatusEntry currentServer = serverList[num];
				if (ExTraceGlobals.VerboseTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<string, int, bool>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: Back end server {0} is selected by current index {1}. IsHealthy = {2}", currentServer.BackEndServer.Fqdn, num, currentServer.IsHealthy);
				}
				if (currentServer.IsHealthy)
				{
					ServiceType serviceType = topology.FindAny<ServiceType>(clientAccessType, (ServiceType service) => service != null && service.ServerFullyQualifiedDomainName.Equals(currentServer.BackEndServer.Fqdn, StringComparison.OrdinalIgnoreCase) && !service.IsOutOfService && otherFilter(service), "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerManager.cs", "GetClientAccessServiceFromList", 767);
					if (serviceType != null)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<Uri, string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: Found service {0} matching back end server {1}.", serviceType.Url, currentServer.BackEndServer.Fqdn);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(logger, "DownLevelTargetRandomHashing", string.Format("{0}/{1}", i, serverList.Count));
						return serviceType;
					}
					ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: Back end server {0} cannot be found by ServiceDiscovery.", currentServer.BackEndServer.Fqdn);
				}
				else
				{
					ExTraceGlobals.VerboseTracer.TraceWarning<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: Back end server {0} is marked as unhealthy.", currentServer.BackEndServer.Fqdn);
				}
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(logger, "DownLevelTargetRandomHashingFailure", string.Format("{0}", serverList.Count));
			this.TriggerServerMapRefreshIfNeeded(topology, serverList);
			string text2 = string.Format("Unable to find proper back end service for {0} in site {1}.", anchorMailbox, targetSite.DistinguishedName);
			ExTraceGlobals.VerboseTracer.TraceError<string>((long)this.GetHashCode(), "[DownLevelServerManager::GetClientAccessServiceFromList]: {0}", text2);
			throw new NoAvailableDownLevelBackEndException(text2);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006B60 File Offset: 0x00004D60
		private void TriggerServerMapRefreshIfNeeded(ServiceTopology topology, List<DownLevelServerStatusEntry> serverList)
		{
			bool flag = false;
			if (serverList.Count == 0)
			{
				flag = true;
			}
			foreach (DownLevelServerStatusEntry downLevelServerStatusEntry in serverList)
			{
				if (!DownLevelServerManager.IsServerDiscoverable(downLevelServerStatusEntry.BackEndServer.Fqdn))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				ThreadPool.QueueUserWorkItem(delegate(object o)
				{
					this.RefreshServerMap(true);
				});
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006C2C File Offset: 0x00004E2C
		private void InternalRefresh()
		{
			Exception ex = null;
			Server[] array = null;
			try
			{
				ITopologyConfigurationSession configurationSession = DirectoryHelper.GetConfigurationSession();
				ADPagedReader<Server> adpagedReader = configurationSession.FindPaged<Server>(null, QueryScope.SubTree, DownLevelServerManager.ServerVersionFilter, null, 0);
				array = adpagedReader.ReadAllPages();
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (DataValidationException ex3)
			{
				ex = ex3;
			}
			catch (DataSourceOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.VerboseTracer.TraceError<Exception>((long)this.GetHashCode(), "[DownLevelServerManager::RefreshServerMap]: Active Directory exception: {0}", ex);
				Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_ErrorRefreshDownLevelServerMap, null, new object[]
				{
					HttpProxyGlobals.ProtocolType.ToString(),
					ex.ToString()
				});
				return;
			}
			Dictionary<string, List<DownLevelServerStatusEntry>> downLevelServerMap = this.GetDownLevelServerMap();
			Dictionary<string, List<DownLevelServerStatusEntry>> dictionary = new Dictionary<string, List<DownLevelServerStatusEntry>>(downLevelServerMap.Count, StringComparer.OrdinalIgnoreCase);
			Server[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Server server = array2[i];
				if ((server.CurrentServerRole & ServerRole.ClientAccess) > ServerRole.None && server.ServerSite != null)
				{
					List<DownLevelServerStatusEntry> list = null;
					if (!dictionary.TryGetValue(server.ServerSite.DistinguishedName, out list))
					{
						list = new List<DownLevelServerStatusEntry>();
						dictionary.Add(server.ServerSite.DistinguishedName, list);
					}
					DownLevelServerStatusEntry downLevelServerStatusEntry = null;
					List<DownLevelServerStatusEntry> list2 = null;
					if (downLevelServerMap.TryGetValue(server.ServerSite.DistinguishedName, out list2))
					{
						downLevelServerStatusEntry = list2.Find((DownLevelServerStatusEntry x) => x.BackEndServer.Fqdn.Equals(server.Fqdn, StringComparison.OrdinalIgnoreCase));
					}
					if (downLevelServerStatusEntry == null)
					{
						downLevelServerStatusEntry = new DownLevelServerStatusEntry
						{
							BackEndServer = new BackEndServer(server.Fqdn, server.VersionNumber),
							IsHealthy = true
						};
					}
					list.Add(downLevelServerStatusEntry);
					list.Sort((DownLevelServerStatusEntry x, DownLevelServerStatusEntry y) => x.BackEndServer.Fqdn.CompareTo(y.BackEndServer.Fqdn));
				}
			}
			this.downLevelServers = dictionary;
			if (dictionary.Count > 0 && DownLevelServerManager.DownLevelServerPingEnabled.Value && this.pingManager == null)
			{
				this.pingManager = new DownLevelServerPingManager(new Func<Dictionary<string, List<DownLevelServerStatusEntry>>>(this.GetDownLevelServerMap));
			}
		}

		// Token: 0x04000057 RID: 87
		private static readonly QueryFilter ServerVersionFilter = new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E15MinVersion);

		// Token: 0x04000058 RID: 88
		private static readonly TimeSpanAppSettingsEntry DownLevelServerMapRefreshInterval = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("DownLevelServerMapRefreshInterval"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(360.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000059 RID: 89
		private static readonly BoolAppSettingsEntry DownLevelServerPingEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("DownLevelServerPingEnabled"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.DownLevelServerPing.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400005A RID: 90
		private static DownLevelServerManager instance = null;

		// Token: 0x0400005B RID: 91
		private static object staticLock = new object();

		// Token: 0x0400005C RID: 92
		private object instanceLock = new object();

		// Token: 0x0400005D RID: 93
		private DownLevelServerPingManager pingManager;

		// Token: 0x0400005E RID: 94
		private Dictionary<string, List<DownLevelServerStatusEntry>> downLevelServers = new Dictionary<string, List<DownLevelServerStatusEntry>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400005F RID: 95
		private Timer serverMapUpdateTimer;

		// Token: 0x02000026 RID: 38
		internal enum DownlevelExchangeServerVersion
		{
			// Token: 0x04000064 RID: 100
			Exchange2007,
			// Token: 0x04000065 RID: 101
			Exchange2010
		}
	}
}
