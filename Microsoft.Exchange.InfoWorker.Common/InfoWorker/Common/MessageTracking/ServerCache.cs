using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002E2 RID: 738
	internal sealed class ServerCache
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x00065FC8 File Offset: 0x000641C8
		private static bool CheckWriteToStatsLog()
		{
			int defaultValue = VariantConfiguration.InvariantNoFlightingSnapshot.MessageTracking.StatsLogging.Enabled ? 1 : 0;
			int num = ServerCache.TryReadRegistryKey<int>("WriteToStatsLog", defaultValue);
			return num == 1;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00066008 File Offset: 0x00064208
		private string GetServerNameForMailboxDatabase(ADObjectId mailboxDatabaseId)
		{
			return this.mailboxDatabaseConfigCache.Get(mailboxDatabaseId);
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00066023 File Offset: 0x00064223
		public static ServerCache Instance
		{
			get
			{
				return ServerCache.instance;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0006602A File Offset: 0x0006422A
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x00066032 File Offset: 0x00064232
		public HostId HostId { get; private set; }

		// Token: 0x0600156D RID: 5485 RVA: 0x0006603C File Offset: 0x0006423C
		public static KeyType TryReadRegistryKey<KeyType>(string value, KeyType defaultValue)
		{
			Exception ex = null;
			try
			{
				object value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports", value, defaultValue);
				if (value2 == null || !(value2 is KeyType))
				{
					return defaultValue;
				}
				return (KeyType)((object)value2);
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, string, Exception>(0, "Failed to read registry key: {0}\\{1}, {2}", "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports", value, ex);
			}
			return defaultValue;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000660BC File Offset: 0x000642BC
		public bool DiagnosticsDisabled
		{
			get
			{
				return (this.diagnosticsDisabledRegistryValue & 1) != 0;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000660CC File Offset: 0x000642CC
		public bool ReportNonFatalBugs
		{
			get
			{
				return this.reportNonFatalBugs == 1;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000660D7 File Offset: 0x000642D7
		public bool UseE14RtmEwsSchema
		{
			get
			{
				return this.useE14RtmEwsSchema == 1;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x000660E2 File Offset: 0x000642E2
		public int MaxRecipientsInReferrals
		{
			get
			{
				return this.maxRecipientsInReferrals;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000660EA File Offset: 0x000642EA
		public int MaxTrackingEventBudgetForSingleQuery
		{
			get
			{
				return this.maxTrackingEventBudgetForSingleQuery;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000660F2 File Offset: 0x000642F2
		public int MaxTrackingEventBudgetForAllQueries
		{
			get
			{
				return this.maxTrackingEventBudgetForAllQueries;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000660FA File Offset: 0x000642FA
		public int MaxDiagnosticsEvents
		{
			get
			{
				return this.maxDiagnosticsEvents;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00066102 File Offset: 0x00064302
		public int IWTimeoutSeconds
		{
			get
			{
				if (this.iwTimeoutSeconds > 0)
				{
					return this.iwTimeoutSeconds;
				}
				return 30;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00066116 File Offset: 0x00064316
		public int HelpdeskTimeoutSeconds
		{
			get
			{
				if (this.helpdeskTimeoutSeconds > 0)
				{
					return this.helpdeskTimeoutSeconds;
				}
				return 210;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0006612D File Offset: 0x0006432D
		public TimeSpan ExpectedLoggingLatency
		{
			get
			{
				return this.expectedLoggingLatency;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00066135 File Offset: 0x00064335
		public bool IsTimeoutOverrideConfigured
		{
			get
			{
				return this.iwTimeoutSeconds > 0 && this.helpdeskTimeoutSeconds > 0;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0006614B File Offset: 0x0006434B
		public int NumberOfThreadsAllowed
		{
			get
			{
				return this.numberOfThreadsAllowed;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00066153 File Offset: 0x00064353
		public int RowsBeforeTimeBudgetCheck
		{
			get
			{
				return this.rowsBeforeTimeBudgetCheck;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0006615B File Offset: 0x0006435B
		public bool WriteToStatsLogs
		{
			get
			{
				return this.writeToStatsLog;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00066164 File Offset: 0x00064364
		public bool InitializeIfNeeded(HostId identity)
		{
			if (this.HostId == HostId.NotInitialized)
			{
				lock (this.initLock)
				{
					if (this.HostId == HostId.NotInitialized)
					{
						CommonDiagnosticsLog.Initialize(identity);
						ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 500, "InitializeIfNeeded", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\ServerCache.cs");
						Server server = topologyConfigurationSession.FindLocalServer();
						if (server == null || string.IsNullOrEmpty(server.Fqdn) || string.IsNullOrEmpty(server.Domain))
						{
							TraceWrapper.SearchLibraryTracer.TraceError<string, string>(this.GetHashCode(), "Failed to get local server, or it is invalid Fqdn={0}, Domain={1}", (server == null) ? "<null>" : server.Fqdn, (server == null) ? "<null>" : server.Domain);
							return false;
						}
						this.localServer = server;
						ADSite localSite = topologyConfigurationSession.GetLocalSite();
						if (localSite == null)
						{
							TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Failed to get local site.", new object[0]);
							return false;
						}
						this.localServerSiteId = localSite.Id;
						this.HostId = identity;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00066294 File Offset: 0x00064494
		public ADObjectId GetLocalServerSiteId(DirectoryContext directoryContext)
		{
			return this.localServerSiteId;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0006629C File Offset: 0x0006449C
		public Server GetLocalServer()
		{
			return this.localServer;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000662A4 File Offset: 0x000644A4
		public ServerInfo GetUserServer(ADUser trackingUser)
		{
			ADObjectId database = trackingUser.Database;
			if (database == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ADObjectId>(this.GetHashCode(), "Null DataBase ID for user: {0}", trackingUser.Id);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Null Database attribute for user {0}", new object[]
				{
					trackingUser.Id
				});
			}
			string serverNameForMailboxDatabase = this.GetServerNameForMailboxDatabase(database);
			return this.FindMailboxOrHubServer(serverNameForMailboxDatabase, 34UL);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00066308 File Offset: 0x00064508
		public List<ServerInfo> GetDagServers(ServerInfo server)
		{
			List<ServerInfo> list = new List<ServerInfo>(8);
			ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
			bool flag = false;
			if (databaseAvailabilityGroup != null)
			{
				IList<string> list2 = this.databaseAvailabilityGroupCache.Get(databaseAvailabilityGroup);
				foreach (string text in list2)
				{
					if (string.Equals(text, server.Key, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
					}
					ServerInfo item = this.transportServerConfigCache.Get(text);
					list.Add(item);
				}
			}
			if (!flag)
			{
				if (databaseAvailabilityGroup == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Server {0} does not have a DAG specified", server.Key);
					list.Add(server);
				}
				else
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string, ADObjectId>(this.GetHashCode(), "Server {0} was not present in DAG {1}", server.Key, databaseAvailabilityGroup);
					TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Server {0} not in DatabaseAvailabilityGroup {1}", new object[]
					{
						server.Key,
						databaseAvailabilityGroup
					});
				}
			}
			return list;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0006640C File Offset: 0x0006460C
		public bool ReadStatusReportingEnabled(DirectoryContext directoryContext)
		{
			OrganizationConfigCache.Item item = this.organizationConfigCache.Get(directoryContext.OrganizationId);
			bool readTrackingEnabled = item.ReadTrackingEnabled;
			TraceWrapper.SearchLibraryTracer.TraceDebug<bool>(this.GetHashCode(), "Read Tracking Enabled = {0}", readTrackingEnabled);
			return readTrackingEnabled;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00066449 File Offset: 0x00064649
		public Uri GetCasServerUri(ADObjectId site, out int serverVersion)
		{
			return ServerCache.Instance.GetCasServerUri(site, Globals.E14Version, out serverVersion);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000664F0 File Offset: 0x000646F0
		public Uri GetCasServerUri(ADObjectId site, int minServerVersionRequested, out int serverVersion)
		{
			SortedDictionary<int, List<WebServicesService>> uriVersions = null;
			serverVersion = 0;
			try
			{
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\ServerCache.cs", "GetCasServerUri", 673);
				currentServiceTopology.ForEach<WebServicesService>(delegate(WebServicesService service)
				{
					if (service.ServerVersionNumber >= minServerVersionRequested && service.Site.Id.Equals(site) && service.ClientAccessType == ClientAccessType.InternalNLBBypass)
					{
						if (uriVersions == null)
						{
							uriVersions = new SortedDictionary<int, List<WebServicesService>>();
						}
						int key2 = service.ServerVersionNumber >> 16;
						if (!uriVersions.ContainsKey(key2))
						{
							uriVersions[key2] = new List<WebServicesService>();
						}
						uriVersions[key2].Add(service);
					}
				}, "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\ServerCache.cs", "GetCasServerUri", 674);
			}
			catch (ServiceDiscoveryTransientException ex)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ServiceDiscoveryTransientException>(this.GetHashCode(), "Transient exception getting Internal CAS URI: {0}", ex);
				TrackingTransientException.RaiseETX(ErrorCode.CASUriDiscoveryFailure, site.ToString(), ex.ToString());
			}
			catch (ServiceDiscoveryPermanentException ex2)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<ServiceDiscoveryPermanentException>(this.GetHashCode(), "Permanent exception getting Internal CAS URI: {0}", ex2);
				TrackingFatalException.RaiseETX(ErrorCode.CASUriDiscoveryFailure, site.ToString(), ex2.ToString());
			}
			if (uriVersions != null && uriVersions.Count > 0)
			{
				int key = uriVersions.Last<KeyValuePair<int, List<WebServicesService>>>().Key;
				List<WebServicesService> value = uriVersions.Last<KeyValuePair<int, List<WebServicesService>>>().Value;
				int index = ServerCache.rand.Next(value.Count);
				WebServicesService webServicesService = value.ElementAt(index);
				TraceWrapper.SearchLibraryTracer.TraceDebug<Uri, string>(this.GetHashCode(), "Using CAS URI: {0}, Version {1}", webServicesService.Url, new ServerVersion(webServicesService.ServerVersionNumber).ToString());
				serverVersion = webServicesService.ServerVersionNumber;
				return webServicesService.Url;
			}
			TraceWrapper.SearchLibraryTracer.TraceError<string, string>(this.GetHashCode(), "Failed to find any CAS server in site: {0}, with min version {1}", site.ToString(), new ServerVersion(minServerVersionRequested).ToString());
			return null;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000666BC File Offset: 0x000648BC
		public List<ServerInfo> GetCasServers(ADObjectId site)
		{
			SiteConfigCache.Item item = this.siteConfigCache.Get(site);
			if (item == null || item.CasServerInfos.Count == 0)
			{
				return null;
			}
			return item.CasServerInfos;
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000666EE File Offset: 0x000648EE
		public ServerInfo GetHubServer(string name)
		{
			return this.transportServerConfigCache.Get(name);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x000666FC File Offset: 0x000648FC
		public int GetHubServersInSite(ADObjectId site, out List<string> hubServerFqdns, out HashSet<string> hubServerFqdnTable)
		{
			hubServerFqdns = null;
			hubServerFqdnTable = null;
			SiteConfigCache.Item item = this.siteConfigCache.Get(site);
			if (item == null || item.HubServerFqdns.Count == 0)
			{
				return 0;
			}
			hubServerFqdns = item.HubServerFqdns;
			hubServerFqdnTable = item.HubServerTable;
			return hubServerFqdns.Count;
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00066748 File Offset: 0x00064948
		public string GetDefaultDomain(OrganizationId organizationId)
		{
			OrganizationConfigCache.Item item = this.organizationConfigCache.Get(organizationId);
			if (item == null || string.IsNullOrEmpty(item.DefaultDomain))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Cannot get domain from org-id cache", new object[0]);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "No default domain found for Organization {0}", new object[]
				{
					organizationId
				});
			}
			return item.DefaultDomain;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000667AC File Offset: 0x000649AC
		public bool IsDomainAuthoritativeForOrganization(OrganizationId organizationId, string domain)
		{
			OrganizationConfigCache.Item item = this.organizationConfigCache.Get(organizationId);
			return item != null && item.AuthoritativeDomains != null && item.AuthoritativeDomains.Count != 0 && item.AuthoritativeDomains.Contains(domain);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000667EC File Offset: 0x000649EC
		public bool IsDomainInternalRelayForOrganization(OrganizationId organizationId, string domain)
		{
			OrganizationConfigCache.Item item = this.organizationConfigCache.Get(organizationId);
			return item != null && item.InternalRelayDomains != null && item.InternalRelayDomains.Count != 0 && item.InternalRelayDomains.Contains(domain);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0006682C File Offset: 0x00064A2C
		public bool TryGetOrganizationId(string domain, out OrganizationId organizationId)
		{
			return this.organizationConfigCache.TryGetOrganizationId(domain, out organizationId);
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0006683B File Offset: 0x00064A3B
		public ServerInfo FindMailboxOrHubServer(string serverNameOrFqdn, ulong serverRoleMask)
		{
			return this.transportServerConfigCache.FindServer(serverNameOrFqdn, serverRoleMask);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0006684C File Offset: 0x00064A4C
		public bool IsRemoteTrustedOrg(OrganizationId organizationId, string domain)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<OrganizationId, string>(this.GetHashCode(), "Looking for organization relationship for Org: {0} and domain: {1}", organizationId, domain);
			OrganizationRelationship organizationRelationship = this.TryGetOrganizationRelationship(organizationId, domain);
			if (organizationRelationship == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Organization relationship not found", new object[0]);
				return false;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Organization relationship found", new object[0]);
			if (!organizationRelationship.Enabled)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Org relationship disabled,", new object[0]);
				return false;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Organization relationship is enabled", new object[0]);
			if (!organizationRelationship.DeliveryReportEnabled)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Delivery Report disabled for relationship.", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0006691C File Offset: 0x00064B1C
		public IEnumerable<ADObjectId> GetAllSitesInOrg(ITopologyConfigurationSession session)
		{
			if (ExDateTime.UtcNow > this.allInOrgSiteIdsLastUpdated + ServerCache.allSiteIdsInOrgCacheInterval)
			{
				bool flag = ServerCache.Instance.WriteToStatsLogs && ServerCache.Instance.HostId == HostId.ECPApplicationPool;
				if (flag)
				{
					KeyValuePair<string, object>[] eventData = new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Event", "AllSitesRefreshStart")
					};
					CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.DeliveryReportsCache, eventData);
				}
				lock (this.initLock)
				{
					if (ExDateTime.UtcNow > this.allInOrgSiteIdsLastUpdated + ServerCache.allSiteIdsInOrgCacheInterval)
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<ExDateTime>(this.GetHashCode(), "Updating all sites id cache. It was last updated at {0}.", this.allInOrgSiteIdsLastUpdated);
						List<ADObjectId> list = new List<ADObjectId>();
						foreach (ADSite adsite in session.FindAllPaged<ADSite>())
						{
							list.Add(adsite.Id);
						}
						this.allInOrgSiteIds = list;
						this.allInOrgSiteIdsLastUpdated = ExDateTime.UtcNow;
					}
				}
				if (flag)
				{
					KeyValuePair<string, object>[] eventData = new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Event", "AllSitesRefreshDone")
					};
					CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.DeliveryReportsCache, eventData);
				}
			}
			return this.allInOrgSiteIds;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00066AA0 File Offset: 0x00064CA0
		public SmtpAddress GetOrgMailboxForDomain(string domain)
		{
			return this.domainOrgMailboxCache.Get(domain);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00066AB0 File Offset: 0x00064CB0
		private OrganizationRelationship TryGetOrganizationRelationship(OrganizationId orgId, string targetDomain)
		{
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(orgId);
			if (organizationIdCacheValue == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Null cache value returned from OrganizationIdCacheValue", new object[0]);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "Organization Relationships could not be read for organization {0}", new object[]
				{
					orgId
				});
			}
			return organizationIdCacheValue.GetOrganizationRelationship(targetDomain);
		}

		// Token: 0x04000DF9 RID: 3577
		private const string DeliveryReportsRegkey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\DeliveryReports";

		// Token: 0x04000DFA RID: 3578
		internal const string CacheKey = "Key";

		// Token: 0x04000DFB RID: 3579
		internal const string CacheEvent = "Event";

		// Token: 0x04000DFC RID: 3580
		internal const string CacheType = "Type";

		// Token: 0x04000DFD RID: 3581
		internal const string CacheReason = "Reason";

		// Token: 0x04000DFE RID: 3582
		internal const string Cached = "Cached";

		// Token: 0x04000DFF RID: 3583
		private static readonly Random rand = new Random();

		// Token: 0x04000E00 RID: 3584
		private static readonly ServerCache instance = new ServerCache();

		// Token: 0x04000E01 RID: 3585
		private static readonly TimeSpan allSiteIdsInOrgCacheInterval = TimeSpan.FromHours(5.0);

		// Token: 0x04000E02 RID: 3586
		private object initLock = new object();

		// Token: 0x04000E03 RID: 3587
		private ADObjectId localServerSiteId;

		// Token: 0x04000E04 RID: 3588
		private Server localServer;

		// Token: 0x04000E05 RID: 3589
		private OrganizationConfigCache organizationConfigCache = new OrganizationConfigCache(VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled);

		// Token: 0x04000E06 RID: 3590
		private SiteConfigCache siteConfigCache = new SiteConfigCache();

		// Token: 0x04000E07 RID: 3591
		private TransportServerConfigCache transportServerConfigCache = new TransportServerConfigCache();

		// Token: 0x04000E08 RID: 3592
		private MailboxDatabaseConfigCache mailboxDatabaseConfigCache = new MailboxDatabaseConfigCache();

		// Token: 0x04000E09 RID: 3593
		private DatabaseAvailabilityGroupCache databaseAvailabilityGroupCache = new DatabaseAvailabilityGroupCache();

		// Token: 0x04000E0A RID: 3594
		private DomainOrgMailboxCache domainOrgMailboxCache = new DomainOrgMailboxCache();

		// Token: 0x04000E0B RID: 3595
		private int diagnosticsDisabledRegistryValue = ServerCache.TryReadRegistryKey<int>("DiagnosticsDisabled", 0);

		// Token: 0x04000E0C RID: 3596
		private int reportNonFatalBugs = ServerCache.TryReadRegistryKey<int>("ReportNonFatalBugs", 0);

		// Token: 0x04000E0D RID: 3597
		private int useE14RtmEwsSchema = ServerCache.TryReadRegistryKey<int>("UseE14RtmEwsSchema", 0);

		// Token: 0x04000E0E RID: 3598
		private int maxRecipientsInReferrals = ServerCache.TryReadRegistryKey<int>("MaximumRecipientsInReferrals", 10000);

		// Token: 0x04000E0F RID: 3599
		private int maxTrackingEventBudgetForSingleQuery = ServerCache.TryReadRegistryKey<int>("MaximumTrackingEventBudgetForSingleQuery", 1000000);

		// Token: 0x04000E10 RID: 3600
		private int maxTrackingEventBudgetForAllQueries = ServerCache.TryReadRegistryKey<int>("MaximumTrackingEventBudgetForAllQueries", 2000000);

		// Token: 0x04000E11 RID: 3601
		private int maxDiagnosticsEvents = ServerCache.TryReadRegistryKey<int>("MaximumDiagnosticsEvents", 512);

		// Token: 0x04000E12 RID: 3602
		private int iwTimeoutSeconds = ServerCache.TryReadRegistryKey<int>("IWTimeoutSeconds", 0);

		// Token: 0x04000E13 RID: 3603
		private int helpdeskTimeoutSeconds = ServerCache.TryReadRegistryKey<int>("HelpdeskTimeoutSeconds", 0);

		// Token: 0x04000E14 RID: 3604
		private int numberOfThreadsAllowed = ServerCache.TryReadRegistryKey<int>("NumberOfThreadsAllowed", 128);

		// Token: 0x04000E15 RID: 3605
		private int rowsBeforeTimeBudgetCheck = ServerCache.TryReadRegistryKey<int>("RowsBeforeTimeBudgetCheck", 1024);

		// Token: 0x04000E16 RID: 3606
		private TimeSpan expectedLoggingLatency = TimeSpan.FromSeconds((double)ServerCache.TryReadRegistryKey<int>("ExpectedLoggingLatencySeconds", 300));

		// Token: 0x04000E17 RID: 3607
		private bool writeToStatsLog = ServerCache.CheckWriteToStatsLog();

		// Token: 0x04000E18 RID: 3608
		private List<ADObjectId> allInOrgSiteIds;

		// Token: 0x04000E19 RID: 3609
		private ExDateTime allInOrgSiteIdsLastUpdated = ExDateTime.MinValue;

		// Token: 0x020002E3 RID: 739
		[Flags]
		private enum DeliveryReportsRegkeyFlags
		{
			// Token: 0x04000E1C RID: 3612
			DisableDiagnosticsGlobally = 1
		}
	}
}
