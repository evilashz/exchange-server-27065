using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using www.outlook.com.highavailability.ServerLocator.v1;

namespace Microsoft.Exchange.Data.ApplicationLogic.Cafe
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxServerLocator : DisposeTrackableBase
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0002051B File Offset: 0x0001E71B
		private Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.CafeTracer;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00020522 File Offset: 0x0001E722
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002052A File Offset: 0x0001E72A
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00020533 File Offset: 0x0001E733
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0002053B File Offset: 0x0001E73B
		public Dictionary<Guid, BackEndServer> AvailabilityGroupServers { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00020544 File Offset: 0x0001E744
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0002054C File Offset: 0x0001E74C
		public string ResourceForestFqdn { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00020555 File Offset: 0x0001E755
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002055D File Offset: 0x0001E75D
		public string DomainController { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00020566 File Offset: 0x0001E766
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002056E File Offset: 0x0001E76E
		public long Latency { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00020577 File Offset: 0x0001E777
		public List<long> GlsLatencies
		{
			get
			{
				return this.glsLatencies;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0002057F File Offset: 0x0001E77F
		public List<long> DirectoryLatencies
		{
			get
			{
				return this.resourceForestLatencies;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0002058F File Offset: 0x0001E78F
		public string[] LocatorServiceHosts
		{
			get
			{
				return this.contactedServers.ConvertAll<string>((ADObjectId x) => x.Name).ToArray();
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000205BE File Offset: 0x0001E7BE
		// (set) Token: 0x06000800 RID: 2048 RVA: 0x000205C6 File Offset: 0x0001E7C6
		public bool BatchRequest { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000205CF File Offset: 0x0001E7CF
		// (set) Token: 0x06000802 RID: 2050 RVA: 0x000205D7 File Offset: 0x0001E7D7
		public bool IsSourceCachedData { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x000205E0 File Offset: 0x0001E7E0
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x000205E8 File Offset: 0x0001E7E8
		internal bool SkipServerLocatorQuery { get; set; }

		// Token: 0x06000805 RID: 2053 RVA: 0x000205F1 File Offset: 0x0001E7F1
		private MailboxServerLocator(Guid databaseGuid, string tenantAcceptedDomain) : this(databaseGuid, tenantAcceptedDomain, true)
		{
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000205FC File Offset: 0x0001E7FC
		private MailboxServerLocator(Guid databaseGuid, string tenantAcceptedDomain, bool batchRequest)
		{
			this.glsLatencies = new List<long>();
			this.resourceForestLatencies = new List<long>();
			this.lockObject = new object();
			this.contactedServers = new List<ADObjectId>();
			base..ctor();
			if (databaseGuid.Equals(Guid.Empty))
			{
				throw new ArgumentNullException("databaseGuid");
			}
			this.DatabaseGuid = databaseGuid;
			if (!string.IsNullOrEmpty(tenantAcceptedDomain))
			{
				this.ResourceForestFqdn = this.GetResourceForestFqdnByAcceptedDomainName(tenantAcceptedDomain);
				this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "[MailboxServerLocator.Ctor] Resolved resource forest {0} from tenant accepted domain {1}.", this.ResourceForestFqdn, tenantAcceptedDomain);
			}
			this.BatchRequest = batchRequest;
			this.IsSourceCachedData = this.BatchRequest;
			this.InitializeConfigSession();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000206A7 File Offset: 0x0001E8A7
		private MailboxServerLocator(Guid databaseGuid, Fqdn resourceForestFqdn) : this(databaseGuid, resourceForestFqdn, true)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000206B4 File Offset: 0x0001E8B4
		private MailboxServerLocator(Guid databaseGuid, Fqdn resourceForestFqdn, bool batchRequest)
		{
			this.glsLatencies = new List<long>();
			this.resourceForestLatencies = new List<long>();
			this.lockObject = new object();
			this.contactedServers = new List<ADObjectId>();
			base..ctor();
			if (databaseGuid.Equals(Guid.Empty))
			{
				throw new ArgumentNullException("databaseGuid");
			}
			this.DatabaseGuid = databaseGuid;
			this.ResourceForestFqdn = resourceForestFqdn;
			this.BatchRequest = batchRequest;
			this.IsSourceCachedData = this.BatchRequest;
			this.InitializeConfigSession();
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00020738 File Offset: 0x0001E938
		internal static MailboxServerLocator CreateWithDomainName(Guid databaseGuid, string domainName)
		{
			return new MailboxServerLocator(databaseGuid, domainName);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00020741 File Offset: 0x0001E941
		internal static MailboxServerLocator CreateWithResourceForestFqdn(Guid databaseGuid, Fqdn resourceForestFqdn)
		{
			return new MailboxServerLocator(databaseGuid, resourceForestFqdn);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002074A File Offset: 0x0001E94A
		internal static MailboxServerLocator Create(Guid databaseGuid, string domainName, string resourceForest)
		{
			return MailboxServerLocator.Create(databaseGuid, domainName, resourceForest, true);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00020755 File Offset: 0x0001E955
		internal static MailboxServerLocator Create(Guid databaseGuid, string domainName, string resourceForest, bool batchRequest)
		{
			if (MailboxServerLocator.UseResourceForest.Value && !string.IsNullOrEmpty(resourceForest))
			{
				return new MailboxServerLocator(databaseGuid, new Fqdn(resourceForest), batchRequest);
			}
			return new MailboxServerLocator(databaseGuid, domainName, batchRequest);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000207AC File Offset: 0x0001E9AC
		public IAsyncResult BeginGetServer(AsyncCallback callback, object asyncState)
		{
			if (this.serverLocator != null)
			{
				throw new InvalidOperationException("BeginGetServerForDatabase executing in progress.");
			}
			this.ResolveMasterServerOrDag();
			if (this.masterServer == null)
			{
				this.Tracer.TraceError<Guid>((long)this.GetHashCode(), "[MailboxServerLocator.BeginGetServerForDatabase] Cannot find any available mailbox server for database {0}.", this.DatabaseGuid);
				throw new MailboxServerLocatorException(this.DatabaseGuid.ToString());
			}
			IAsyncResult result2;
			lock (this.lockObject)
			{
				this.stopWatch = Stopwatch.StartNew();
				if (this.masterServer.VersionNumber < Server.E15MinVersion || this.SkipServerLocatorQuery || MailboxServerLocator.InjectRemoteForestDownLevelServerException.Value)
				{
					MailboxServerLocator.DummyAsyncResult result = new MailboxServerLocator.DummyAsyncResult
					{
						AsyncState = asyncState
					};
					if (callback != null)
					{
						ThreadPool.QueueUserWorkItem(delegate(object o)
						{
							callback(result);
						});
					}
					this.IsSourceCachedData = false;
					result2 = result;
				}
				else
				{
					this.lazyAsyncResult = new LazyAsyncResult(this, asyncState, callback);
					this.ServerLocatorBeginGetServerList(this.BatchRequest);
					result2 = this.lazyAsyncResult;
				}
			}
			return result2;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00020900 File Offset: 0x0001EB00
		public BackEndServer EndGetServer(IAsyncResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			lock (this.lockObject)
			{
				base.CheckDisposed();
			}
			BackEndServer result2;
			try
			{
				MailboxServerLocator.DummyAsyncResult dummyAsyncResult = result as MailboxServerLocator.DummyAsyncResult;
				if (dummyAsyncResult != null)
				{
					this.CheckDownLevelServerForest();
					BackEndServer backEndServer = null;
					if (this.masterServer.VersionNumber < Server.E14MinVersion || this.SkipServerLocatorQuery)
					{
						backEndServer = new BackEndServer(this.masterServer.Fqdn, this.masterServer.VersionNumber);
					}
					else
					{
						lock (this.lockObject)
						{
							backEndServer = this.GetLegacyServerForDatabase(this.DatabaseGuid);
						}
					}
					if (backEndServer != null)
					{
						this.AvailabilityGroupServers = new Dictionary<Guid, BackEndServer>(1);
						this.AvailabilityGroupServers.Add(this.DatabaseGuid, backEndServer);
					}
					result2 = backEndServer;
				}
				else
				{
					lock (this.lockObject)
					{
						if (this.serverLocator == null)
						{
							throw new InvalidOperationException("BeginGetServerForDatabase() was not executed.");
						}
						if (!object.ReferenceEquals(result, this.lazyAsyncResult))
						{
							throw new InvalidOperationException("Calling with the wrong instance of IAsyncResult.");
						}
					}
					this.lazyAsyncResult.InternalWaitForCompletion();
					lock (this.lockObject)
					{
						if (this.asyncException != null)
						{
							this.Tracer.TraceError<Exception>((long)this.GetHashCode(), "[MailboxServerLocator.EndGetServer] Throwing async exception {0}.", this.asyncException);
							throw this.asyncException;
						}
						result2 = this.AvailabilityGroupServers[this.DatabaseGuid];
					}
				}
			}
			finally
			{
				this.Latency = this.stopWatch.ElapsedMilliseconds;
			}
			return result2;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00020B2C File Offset: 0x0001ED2C
		public BackEndServer GetServer()
		{
			return this.EndGetServer(this.BeginGetServer(null, null));
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00020B3C File Offset: 0x0001ED3C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxServerLocator>(this);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00020B44 File Offset: 0x0001ED44
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.lockObject)
				{
					if (this.serverLocator != null)
					{
						this.serverLocator.Dispose();
						this.serverLocator = null;
					}
				}
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00020B9C File Offset: 0x0001ED9C
		private void InitializeConfigSession()
		{
			PartitionId partitionId = PartitionId.LocalForest;
			if (this.ResourceForestFqdn != null)
			{
				partitionId = new PartitionId(this.ResourceForestFqdn);
			}
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 650, "InitializeConfigSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\MailboxServerLocator.cs");
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00020BEC File Offset: 0x0001EDEC
		private void ServerLocatorBeginGetServerList(bool batchRequest)
		{
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[MailboxServerLocator.ServerLocatorBeginGetServerList] Calling ServerLocatorService for on host {0}.", this.masterServer.Fqdn);
			if (this.serverLocator != null)
			{
				this.serverLocator.Dispose();
				this.serverLocator = null;
			}
			this.serverLocator = ServerLocatorServiceClient.Create(this.masterServer.Fqdn, MailboxServerLocator.ServerLocatorCloseTimeout.Value, MailboxServerLocator.ServerLocatorOpenTimeout.Value, MailboxServerLocator.ServerLocatorReceiveTimeout.Value, MailboxServerLocator.ServerLocatorSendTimeout.Value);
			this.IsSourceCachedData = batchRequest;
			if (batchRequest)
			{
				this.serverLocator.BeginGetActiveCopiesForDatabaseAvailabilityGroup(new AsyncCallback(this.ServerLocatorAsyncCallback), batchRequest);
				return;
			}
			this.serverLocator.BeginGetServerForDatabase(this.DatabaseGuid, new AsyncCallback(this.ServerLocatorAsyncCallback), batchRequest);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		private void ServerLocatorAsyncCallback(IAsyncResult locatorAsyncResult)
		{
			lock (this.lockObject)
			{
				this.AvailabilityGroupServers = null;
				this.asyncException = null;
				Exception ex = null;
				bool flag2 = (bool)locatorAsyncResult.AsyncState;
				bool flag3 = false;
				try
				{
					if (flag2)
					{
						try
						{
							DatabaseServerInformation[] backEndServerList = this.serverLocator.EndGetActiveCopiesForDatabaseAvailabilityGroup(locatorAsyncResult);
							this.AvailabilityGroupServers = this.ProcessBatchResults(backEndServerList);
							this.lazyAsyncResult.InvokeCallback();
							return;
						}
						catch (MissingRequestedDatabaseException)
						{
							flag3 = true;
							flag2 = false;
							goto IL_93;
						}
						goto IL_66;
						IL_93:
						goto IL_C0;
					}
					IL_66:
					DatabaseServerInformation backEndServerInfo = this.serverLocator.EndGetServerForDatabase(locatorAsyncResult);
					this.AvailabilityGroupServers = this.ProcessSingleResult(backEndServerInfo);
					this.lazyAsyncResult.InvokeCallback();
					return;
				}
				catch (Exception ex2)
				{
					this.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "[MailboxServerLocator.ServerLocatorAsyncCallback] ServerLocatorService returned error when contacting host {0}. Exception: {1}", this.masterServer.Fqdn, ex2);
					ex = ex2;
				}
				IL_C0:
				if (!(ex is ServerLocatorClientTransientException) && !(ex is ServerLocatorClientException))
				{
					if (!flag3)
					{
						goto IL_116;
					}
				}
				try
				{
					if (!flag3)
					{
						this.masterServer = null;
						this.ResolveMasterServerOrDag();
					}
					if (this.masterServer != null)
					{
						this.ServerLocatorBeginGetServerList(flag2);
						return;
					}
				}
				catch (Exception ex3)
				{
					ex = ex3;
					this.Tracer.TraceError<Exception>((long)this.GetHashCode(), "[MailboxServerLocator.ServerLocatorAsyncCallback] Error occurred during retry. Exception: {0}", ex3);
				}
				IL_116:
				this.asyncException = ex;
				this.lazyAsyncResult.InvokeCallback();
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		private void ResolveMasterServerOrDag()
		{
			if (this.serversList == null)
			{
				Database database = this.InvokeResourceForest<Database>(() => this.configSession.FindDatabaseByGuid<Database>(this.DatabaseGuid));
				if (database == null)
				{
					throw new DatabaseNotFoundException(this.DatabaseGuid.ToString());
				}
				ADObjectId masterServerOrDag = null;
				if (database.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					masterServerOrDag = database.Server;
				}
				else
				{
					masterServerOrDag = database.MasterServerOrAvailabilityGroup;
				}
				if (masterServerOrDag == null)
				{
					return;
				}
				if (database.MasterType == MasterType.DatabaseAvailabilityGroup)
				{
					DatabaseAvailabilityGroup databaseAvailabilityGroup = this.InvokeResourceForest<DatabaseAvailabilityGroup>(() => this.configSession.Read<DatabaseAvailabilityGroup>(masterServerOrDag));
					this.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "[MailboxServerLocator.ResolveMasterServerOrDag] Database {0} is mastered by DatabaseAvailabilityGroup {1}.", this.DatabaseGuid, databaseAvailabilityGroup.Id.ToString());
					this.serversList = new List<ADObjectId>(databaseAvailabilityGroup.Servers);
				}
				else
				{
					this.serversList = new List<ADObjectId>
					{
						masterServerOrDag
					};
				}
			}
			this.masterServer = this.InvokeResourceForest<MiniServer>(() => this.SelectServerFromDag(this.configSession, this.serversList, this.contactedServers));
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[MailboxServerLocator.ResolveMasterServerOrDag] To ServerLocatorService host to use is {0}.", (this.masterServer != null) ? this.masterServer.Fqdn : null);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002101C File Offset: 0x0001F21C
		private Dictionary<Guid, BackEndServer> ProcessSingleResult(DatabaseServerInformation backEndServerInfo)
		{
			if (backEndServerInfo == null)
			{
				this.Tracer.TraceError<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessSingleResult] ServerLocatorServiceClient did not return active server info from master server {0} for database {1}.", this.masterServer.Fqdn, this.DatabaseGuid);
				throw new MailboxServerLocatorException(this.DatabaseGuid.ToString());
			}
			if (string.IsNullOrEmpty(backEndServerInfo.ServerFqdn))
			{
				this.Tracer.TraceError<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessSingleResult] ServerLocatorServiceClient returned empty active server FQDN from master server {0} for database {1}.", this.masterServer.Fqdn, this.DatabaseGuid);
				throw new MailboxServerLocatorException(this.DatabaseGuid.ToString());
			}
			if (backEndServerInfo.ServerVersion == 0)
			{
				this.Tracer.TraceWarning<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessSingleResult] ServerLocatorServiceClient returned empty active server version from master server {0} for database {1}.", this.masterServer.Fqdn, this.DatabaseGuid);
				backEndServerInfo.ServerVersion = this.masterServer.VersionNumber;
			}
			this.Tracer.TraceDebug((long)this.GetHashCode(), "[MailboxServerLocator.ProcessSingleResult] ServerLocatorServiceClient returned active server {2} with version {3} from master server {0} for database {1}.", new object[]
			{
				this.masterServer.Fqdn,
				this.DatabaseGuid,
				backEndServerInfo.ServerFqdn,
				new ServerVersion(backEndServerInfo.ServerVersion)
			});
			return new Dictionary<Guid, BackEndServer>(1)
			{
				{
					this.DatabaseGuid,
					new BackEndServer(backEndServerInfo.ServerFqdn, backEndServerInfo.ServerVersion)
				}
			};
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00021178 File Offset: 0x0001F378
		private Dictionary<Guid, BackEndServer> ProcessBatchResults(DatabaseServerInformation[] backEndServerList)
		{
			if (backEndServerList == null || backEndServerList.Length == 0)
			{
				this.Tracer.TraceError<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] ServerLocatorServiceClient did not return active server info from master server {0} for database {1}.", this.masterServer.Fqdn, this.DatabaseGuid);
				throw new MailboxServerLocatorException(this.DatabaseGuid.ToString());
			}
			this.Tracer.TraceDebug<int, string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] ServerLocatorServiceClient returned {0} servers from master server {0} for database {1}.", backEndServerList.Length, this.masterServer.Fqdn, this.DatabaseGuid);
			Dictionary<Guid, BackEndServer> dictionary = new Dictionary<Guid, BackEndServer>(backEndServerList.Length);
			foreach (DatabaseServerInformation databaseServerInformation in backEndServerList)
			{
				if (string.IsNullOrEmpty(databaseServerInformation.ServerFqdn))
				{
					this.Tracer.TraceWarning<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] ServerLocatorServiceClient returned empty active server FQDN from master server {0} for database {1}.", this.masterServer.Fqdn, databaseServerInformation.DatabaseGuid);
				}
				else
				{
					if (databaseServerInformation.ServerVersion == 0)
					{
						this.Tracer.TraceWarning<string, string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] ServerLocatorServiceClient returned empty active server version of server {0} from master server {1} for database {2}.", databaseServerInformation.ServerFqdn, this.masterServer.Fqdn, databaseServerInformation.DatabaseGuid);
						databaseServerInformation.ServerVersion = this.masterServer.VersionNumber;
						if (!MailboxServerLocator.NoServiceTopologyTryGetServerVersion)
						{
							ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\MailboxServerLocator.cs", "ProcessBatchResults", 985);
							int serverVersion;
							if (currentServiceTopology.TryGetServerVersion(databaseServerInformation.ServerFqdn, out serverVersion, "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\Cafe\\MailboxServerLocator.cs", "ProcessBatchResults", 986))
							{
								databaseServerInformation.ServerVersion = serverVersion;
							}
						}
					}
					this.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] The active server of database {0} is {1}.", databaseServerInformation.DatabaseGuid, databaseServerInformation.ServerFqdn);
					dictionary[databaseServerInformation.DatabaseGuid] = new BackEndServer(databaseServerInformation.ServerFqdn, databaseServerInformation.ServerVersion);
				}
			}
			if (!dictionary.ContainsKey(this.DatabaseGuid))
			{
				this.Tracer.TraceWarning<string, Guid>((long)this.GetHashCode(), "[MailboxServerLocator.ProcessBatchResults] ServerLocatorServiceClient did not return the active server of database {1} from master server {0}.", this.masterServer.Fqdn, this.DatabaseGuid);
				throw new MissingRequestedDatabaseException(this.DatabaseGuid.ToString());
			}
			return dictionary;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00021380 File Offset: 0x0001F580
		private BackEndServer GetLegacyServerForDatabase(Guid databaseGuid)
		{
			this.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[MailboxServerLocator.GetLegacyServerForDatabase] Resolving legacy server for database {0}.", databaseGuid);
			ActiveManager cachingActiveManagerInstance = ActiveManager.GetCachingActiveManagerInstance();
			DatabaseLocationInfo serverForDatabase = cachingActiveManagerInstance.GetServerForDatabase(databaseGuid);
			if (serverForDatabase != null)
			{
				this.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "[MailboxServerLocator.GetLegacyServerForDatabase] Active manager returns server {1} for database {0}.", databaseGuid, serverForDatabase.ServerFqdn);
				return new BackEndServer(serverForDatabase.ServerFqdn, serverForDatabase.ServerVersion);
			}
			throw new MailboxServerLocatorException(this.DatabaseGuid.ToString());
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00021400 File Offset: 0x0001F600
		private MiniServer SelectServerFromDag(ITopologyConfigurationSession configSession, List<ADObjectId> dagServers, List<ADObjectId> contactedServers)
		{
			if (dagServers.Count == 0 || dagServers.Count == contactedServers.Count)
			{
				return null;
			}
			int num = MailboxServerLocator.random.Next(dagServers.Count);
			int num2 = 0;
			ADObjectId adobjectId;
			MiniServer activeServer;
			for (;;)
			{
				adobjectId = dagServers[num];
				num2++;
				if (!contactedServers.Contains(adobjectId))
				{
					activeServer = this.GetActiveServer(configSession, adobjectId, num2 < dagServers.Count);
					if (activeServer != null)
					{
						break;
					}
				}
				num++;
				if (num >= dagServers.Count)
				{
					num = 0;
				}
				if (num2 >= dagServers.Count * 2)
				{
					goto Block_5;
				}
			}
			contactedServers.Add(adobjectId);
			return activeServer;
			Block_5:
			return null;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00021488 File Offset: 0x0001F688
		private T InvokeResourceForest<T>(Func<T> adCall)
		{
			Stopwatch stopwatch = new Stopwatch();
			T result = default(T);
			try
			{
				stopwatch.Start();
				result = adCall();
			}
			finally
			{
				stopwatch.Stop();
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				this.resourceForestLatencies.Add(elapsedMilliseconds);
				this.DomainController = this.configSession.DomainController;
			}
			return result;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000214F4 File Offset: 0x0001F6F4
		private string GetResourceForestFqdnByAcceptedDomainName(string tenantAcceptedDomain)
		{
			Stopwatch stopwatch = new Stopwatch();
			string resourceForestFqdnByAcceptedDomainName;
			try
			{
				stopwatch.Start();
				resourceForestFqdnByAcceptedDomainName = ADAccountPartitionLocator.GetResourceForestFqdnByAcceptedDomainName(tenantAcceptedDomain);
			}
			finally
			{
				stopwatch.Stop();
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				this.glsLatencies.Add(elapsedMilliseconds);
			}
			return resourceForestFqdnByAcceptedDomainName;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00021544 File Offset: 0x0001F744
		private void CheckDownLevelServerForest()
		{
			if (!HttpProxyBackEndHelper.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoCrossForestServerLocate.Enabled)
			{
				return;
			}
			if (MailboxServerLocator.InjectRemoteForestDownLevelServerException.Value)
			{
				throw new RemoteForestDownLevelServerException(this.DatabaseGuid.ToString(), this.ResourceForestFqdn);
			}
			if (this.masterServer.VersionNumber >= Server.E15MinVersion)
			{
				return;
			}
			if (string.Equals(LocalServer.GetServer().Id.DomainId.DistinguishedName, this.masterServer.Id.DomainId.DistinguishedName, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.Tracer.TraceWarning<Guid, string>((long)this.GetHashCode(), "[MailboxServerLocator.CheckDownLevelServerForest] Master server {1} for down level database {0} is not in local forest.", this.DatabaseGuid, this.masterServer.Fqdn);
			throw new RemoteForestDownLevelServerException(this.DatabaseGuid.ToString(), this.ResourceForestFqdn);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002162C File Offset: 0x0001F82C
		private MiniServer GetActiveServer(ITopologyConfigurationSession configSession, ADObjectId serverId, bool skipForMaintenanceMode)
		{
			bool enabled = VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.CheckServerLocatorServersForMaintenanceMode.Enabled;
			bool enabled2 = VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.CheckServerOnlineForActiveServer.Enabled;
			ADPropertyDefinition[] properties;
			if (enabled)
			{
				properties = new ADPropertyDefinition[]
				{
					ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy,
					ServerSchema.ComponentStates,
					ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow
				};
			}
			else if (enabled2)
			{
				properties = new ADPropertyDefinition[]
				{
					ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy,
					ServerSchema.ComponentStates
				};
			}
			else
			{
				properties = new ADPropertyDefinition[]
				{
					ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy
				};
			}
			MiniServer miniServer = configSession.ReadMiniServer(serverId, properties);
			if (miniServer == null)
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "[MailboxServerLocator.GetActiveServer] return null. Server is NULL.");
				return null;
			}
			if (skipForMaintenanceMode)
			{
				if (miniServer.DatabaseCopyAutoActivationPolicy == DatabaseCopyAutoActivationPolicyType.Blocked)
				{
					this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "[MailboxServerLocator.GetActiveServer] return null. Server {0} DatabaseCopyAutoActivationPolicy {1}.", miniServer.ToString(), miniServer.DatabaseCopyAutoActivationPolicy.ToString());
					return null;
				}
				if (enabled && miniServer.DatabaseCopyActivationDisabledAndMoveNow)
				{
					this.Tracer.TraceDebug<MiniServer, bool>((long)this.GetHashCode(), "[MailboxServerLocator.GetActiveServer] return null. Server {0} DatabaseCopyActivationDisabledAndMoveNow is {1}.", miniServer, miniServer.DatabaseCopyActivationDisabledAndMoveNow);
					return null;
				}
				if (enabled2 || enabled)
				{
					MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)miniServer[ServerSchema.ComponentStates];
					if (!ServerComponentStates.IsServerOnline(multiValuedProperty))
					{
						this.Tracer.TraceDebug<MiniServer, string>((long)this.GetHashCode(), "[MailboxServerLocator.GetActiveServer] return null. Server {0} ComponentStates {1}.", miniServer, (multiValuedProperty == null) ? "<NULL>" : string.Join(",", multiValuedProperty.ToArray()));
						return null;
					}
				}
			}
			return miniServer;
		}

		// Token: 0x0400038A RID: 906
		private static readonly TimeSpanAppSettingsEntry ServerLocatorCloseTimeout = new TimeSpanAppSettingsEntry("MailboxServerLocator.ServerLocatorCloseTimeout", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(10.0), ExTraceGlobals.CafeTracer);

		// Token: 0x0400038B RID: 907
		private static readonly TimeSpanAppSettingsEntry ServerLocatorOpenTimeout = new TimeSpanAppSettingsEntry("MailboxServerLocator.ServerLocatorOpenTimeout", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(10.0), ExTraceGlobals.CafeTracer);

		// Token: 0x0400038C RID: 908
		private static readonly TimeSpanAppSettingsEntry ServerLocatorReceiveTimeout = new TimeSpanAppSettingsEntry("MailboxServerLocator.ServerLocatorReceiveTimeout", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(10.0), ExTraceGlobals.CafeTracer);

		// Token: 0x0400038D RID: 909
		private static readonly TimeSpanAppSettingsEntry ServerLocatorSendTimeout = new TimeSpanAppSettingsEntry("MailboxServerLocator.ServerLocatorSendTimeout", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(10.0), ExTraceGlobals.CafeTracer);

		// Token: 0x0400038E RID: 910
		private static readonly BoolAppSettingsEntry InjectRemoteForestDownLevelServerException = new BoolAppSettingsEntry("MailboxServerLocator.InjectRemoteForestDownLevelServerException", false, ExTraceGlobals.CafeTracer);

		// Token: 0x0400038F RID: 911
		public static readonly BoolAppSettingsEntry UseResourceForest = new BoolAppSettingsEntry("MailboxServerLocator.UseResourceForest", VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.UseResourceForest.Enabled, ExTraceGlobals.CafeTracer);

		// Token: 0x04000390 RID: 912
		private static readonly bool NoServiceTopologyTryGetServerVersion = VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoServiceTopologyTryGetServerVersion.Enabled;

		// Token: 0x04000391 RID: 913
		private static readonly Random random = new Random();

		// Token: 0x04000392 RID: 914
		private readonly List<long> glsLatencies;

		// Token: 0x04000393 RID: 915
		private readonly List<long> resourceForestLatencies;

		// Token: 0x04000394 RID: 916
		private object lockObject;

		// Token: 0x04000395 RID: 917
		private ITopologyConfigurationSession configSession;

		// Token: 0x04000396 RID: 918
		private MiniServer masterServer;

		// Token: 0x04000397 RID: 919
		private List<ADObjectId> serversList;

		// Token: 0x04000398 RID: 920
		private List<ADObjectId> contactedServers;

		// Token: 0x04000399 RID: 921
		private ServerLocatorServiceClient serverLocator;

		// Token: 0x0400039A RID: 922
		private LazyAsyncResult lazyAsyncResult;

		// Token: 0x0400039B RID: 923
		private Exception asyncException;

		// Token: 0x0400039C RID: 924
		private Stopwatch stopWatch;

		// Token: 0x020000BB RID: 187
		private class DummyAsyncResult : IAsyncResult
		{
			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000822 RID: 2082 RVA: 0x000218B3 File Offset: 0x0001FAB3
			// (set) Token: 0x06000823 RID: 2083 RVA: 0x000218BB File Offset: 0x0001FABB
			public object AsyncState { get; internal set; }

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000824 RID: 2084 RVA: 0x000218C4 File Offset: 0x0001FAC4
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					lock (this.lockObject)
					{
						if (this.waitEvent == null)
						{
							this.waitEvent = new ManualResetEvent(true);
						}
					}
					return this.waitEvent;
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000825 RID: 2085 RVA: 0x00021918 File Offset: 0x0001FB18
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x0002191B File Offset: 0x0001FB1B
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040003A6 RID: 934
			private object lockObject = new object();

			// Token: 0x040003A7 RID: 935
			private ManualResetEvent waitEvent;
		}
	}
}
