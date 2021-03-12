using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000024 RID: 36
	internal abstract class TopologyProvider
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000E13C File Offset: 0x0000C33C
		internal static TopologyMode CurrentTopologyMode
		{
			get
			{
				if (TopologyProvider.staticInstance != null)
				{
					return TopologyProvider.GetInstance().TopologyMode;
				}
				int num;
				return TopologyProvider.SelectTopologyMode(out num);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E162 File Offset: 0x0000C362
		internal static string LocalForestFqdn
		{
			get
			{
				if (string.IsNullOrEmpty(TopologyProvider.localForestFqdn))
				{
					if (!ADSessionSettings.SessionSettingsFactory.Default.InDomain())
					{
						TopologyProvider.localForestFqdn = "localhost";
					}
					else
					{
						TopologyProvider.localForestFqdn = NativeHelpers.GetForestName();
					}
				}
				return TopologyProvider.localForestFqdn;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000E197 File Offset: 0x0000C397
		internal static bool IsAdminMode
		{
			get
			{
				return TopologyMode.Ldap == TopologyProvider.CurrentTopologyMode && !TopologyProvider.isRunningOnTopologyService;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000E1AB File Offset: 0x0000C3AB
		internal static bool IsAdamTopology()
		{
			return TopologyProvider.CurrentTopologyMode == TopologyMode.Adam;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		internal static void SetProcessTopologyMode(bool isAdminMode, bool publicMethodCheck)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, bool>(0L, "{0} method sets admin mode to {1}", publicMethodCheck ? "Public" : "Internal", isAdminMode);
			int num = 0;
			if (!publicMethodCheck || (TopologyProvider.staticInstance != null && TopologyProvider.CurrentTopologyMode == TopologyMode.Ldap && AdamTopologyProvider.CheckIfAdamConfigured(out num)) || TopologyProvider.staticInstance == null)
			{
				TopologyProvider.SetProcessTopologyMode(isAdminMode ? TopologyMode.Ldap : TopologyMode.ADTopologyService);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000E218 File Offset: 0x0000C418
		internal static void SetProcessTopologyMode(TopologyMode mode)
		{
			if (TopologyMode.Adam == mode)
			{
				throw new ArgumentException("mode. Adam topology mode can't be specified");
			}
			if (TopologyMode.Ldap == mode && TopologyProvider.IsTopologyServiceProcess())
			{
				TopologyProvider.isRunningOnTopologyService = true;
			}
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<TopologyMode, TopologyMode>(0L, "User set topology mode from {0} to {1}", (TopologyProvider.userSetTopologyMode != null) ? TopologyProvider.userSetTopologyMode.Value : TopologyProvider.CurrentTopologyMode, mode);
			TopologyProvider.userSetTopologyMode = new TopologyMode?(mode);
			int num;
			TopologyMode topologyMode = TopologyProvider.SelectTopologyMode(out num);
			if (TopologyProvider.staticInstance != null && (TopologyProvider.userSetTopologyMode.Value != TopologyProvider.staticInstance.TopologyMode || TopologyProvider.userSetTopologyMode.Value != topologyMode))
			{
				IDisposable disposable = TopologyProvider.staticInstance as IDisposable;
				TopologyProvider topologyProvider = TopologyProvider.InitializeInstance();
				topologyProvider.PopulateConfigNamingContextsForLocalForest();
				topologyProvider.PopulateDomainNamingContextsForLocalForest();
				ConnectionPoolManager.Reset();
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		private static TopologyMode SelectTopologyMode(out int adamPort)
		{
			adamPort = 0;
			if (AdamTopologyProvider.CheckIfAdamConfigured(out adamPort))
			{
				return TopologyMode.Adam;
			}
			if (TopologyProvider.userSetTopologyMode != null)
			{
				return TopologyProvider.userSetTopologyMode.Value;
			}
			if (ServiceTopologyProvider.IsAdTopologyServiceInstalled())
			{
				return TopologyMode.ADTopologyService;
			}
			return TopologyMode.Ldap;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E30C File Offset: 0x0000C50C
		private static TopologyProvider InitializeInstance()
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug(0L, "Starting InitializeInstance and waiting for the lock");
			TopologyProvider result;
			lock (TopologyProvider.instanceLockRoot)
			{
				int adamPort;
				switch (TopologyProvider.SelectTopologyMode(out adamPort))
				{
				case TopologyMode.ADTopologyService:
				{
					ServiceTopologyProvider serviceTopologyProvider = new ServiceTopologyProvider();
					serviceTopologyProvider.SuppressDisposeTracker();
					TopologyProvider.staticInstance = serviceTopologyProvider;
					break;
				}
				case TopologyMode.Adam:
					TopologyProvider.staticInstance = new AdamTopologyProvider(adamPort);
					break;
				case TopologyMode.Ldap:
					TopologyProvider.staticInstance = new LdapTopologyProvider();
					break;
				}
				ExTraceGlobals.TopologyProviderTracer.TraceDebug<int>(0L, "TopologyProvider::InitializeInstance created {0}", (int)TopologyProvider.CurrentTopologyMode);
				result = TopologyProvider.staticInstance;
			}
			return result;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		[Conditional("DEBUG")]
		private static void PopulateTopologyModeStackSwitch()
		{
			new StackTrace(true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		internal static TopologyProvider GetInstance()
		{
			TopologyProvider topologyProvider = TopologyProvider.staticInstance;
			if (topologyProvider == null)
			{
				topologyProvider = TopologyProvider.InitializeInstance();
			}
			return topologyProvider;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E3ED File Offset: 0x0000C5ED
		protected void PopulateConfigNamingContextsForLocalForest()
		{
			this.PopulateConfigNamingContexts(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		protected void PopulateConfigNamingContexts(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADServerInfo configDCInfo = this.GetConfigDCInfo(partitionFqdn, true);
			if (string.IsNullOrEmpty(configDCInfo.ConfigNC) || string.IsNullOrEmpty(configDCInfo.SchemaNC))
			{
				PooledLdapConnection pooledLdapConnection = LdapConnectionPool.CreateOneTimeConnection(null, configDCInfo, LocatorFlags.None);
				try
				{
					if (string.IsNullOrEmpty(pooledLdapConnection.ADServerInfo.ConfigNC))
					{
						this.LogRootDSEReadFailureAndThrow("configurationNamingContext", configDCInfo.FqdnPlusPort);
					}
					this.configNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.ConfigNC);
					if (string.IsNullOrEmpty(pooledLdapConnection.ADServerInfo.SchemaNC))
					{
						this.LogRootDSEReadFailureAndThrow("schemaNamingContext", configDCInfo.FqdnPlusPort);
					}
					this.schemaNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.SchemaNC);
					return;
				}
				finally
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			this.configNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(configDCInfo.ConfigNC);
			this.schemaNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(configDCInfo.SchemaNC);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000E504 File Offset: 0x0000C704
		protected void ClearConfigNamingContexts(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId adobjectId;
			this.configNCs.TryRemove(partitionFqdn, out adobjectId);
			this.schemaNCs.TryRemove(partitionFqdn, out adobjectId);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000E535 File Offset: 0x0000C735
		protected void PopulateDomainNamingContextsForLocalForest()
		{
			this.PopulateDomainNamingContexts(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000E544 File Offset: 0x0000C744
		protected void PopulateDomainNamingContexts(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADServerInfo defaultServerInfo = this.GetDefaultServerInfo(partitionFqdn);
			if (string.IsNullOrEmpty(defaultServerInfo.WritableNC) || string.IsNullOrEmpty(defaultServerInfo.RootDomainNC))
			{
				PooledLdapConnection pooledLdapConnection = LdapConnectionPool.CreateOneTimeConnection(null, defaultServerInfo, LocatorFlags.None);
				try
				{
					if (string.IsNullOrEmpty(pooledLdapConnection.ADServerInfo.WritableNC) && !TopologyProvider.IsAdamTopology())
					{
						this.LogRootDSEReadFailureAndThrow("domainNamingContext", defaultServerInfo.FqdnPlusPort);
					}
					this.domainNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.WritableNC);
					if (string.IsNullOrEmpty(pooledLdapConnection.ADServerInfo.RootDomainNC) && !TopologyProvider.IsAdamTopology())
					{
						this.LogRootDSEReadFailureAndThrow("rootDomainNamingContext", defaultServerInfo.FqdnPlusPort);
					}
					this.rootDomainNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(pooledLdapConnection.ADServerInfo.RootDomainNC);
					return;
				}
				finally
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			this.domainNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(defaultServerInfo.WritableNC);
			this.rootDomainNCs[partitionFqdn] = ADObjectId.ParseExtendedDN(defaultServerInfo.RootDomainNC);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000E65C File Offset: 0x0000C85C
		protected void ClearDomainNamingContextsForLocalForest()
		{
			this.ClearDomainNamingContexts(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E66C File Offset: 0x0000C86C
		protected void ClearDomainNamingContexts(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId adobjectId;
			this.domainNCs.TryRemove(partitionFqdn, out adobjectId);
			this.rootDomainNCs.TryRemove(partitionFqdn, out adobjectId);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		private void LogRootDSEReadFailureAndThrow(string attribute, string server)
		{
			ExTraceGlobals.ADTopologyTracer.TraceError<string, string>(0L, "Could not read {0} from root DSE of {1}", attribute, server);
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ROOTDSE_READ_FAILED, attribute, new object[]
			{
				attribute,
				server
			});
			throw new ADTransientException(DirectoryStrings.ExceptionRootDSE(attribute, server));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		internal ADObjectId GetConfigurationNamingContextForLocalForest()
		{
			return this.GetConfigurationNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		internal ADObjectId GetConfigurationNamingContext(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId result;
			if (!this.configNCs.TryGetValue(partitionFqdn, out result))
			{
				this.PopulateConfigNamingContexts(partitionFqdn);
				this.configNCs.TryGetValue(partitionFqdn, out result);
			}
			return result;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000E732 File Offset: 0x0000C932
		internal ADObjectId GetSchemaNamingContextForLocalForest()
		{
			return this.GetSchemaNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000E740 File Offset: 0x0000C940
		internal ADObjectId GetSchemaNamingContext(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId result;
			if (!this.schemaNCs.TryGetValue(partitionFqdn, out result))
			{
				this.PopulateConfigNamingContexts(partitionFqdn);
				this.schemaNCs.TryGetValue(partitionFqdn, out result);
			}
			return result;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000E77A File Offset: 0x0000C97A
		internal ADObjectId GetDomainNamingContextForLocalForest()
		{
			return this.GetDomainNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000E788 File Offset: 0x0000C988
		internal ADObjectId GetDomainNamingContext(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId result;
			if (!this.domainNCs.TryGetValue(partitionFqdn, out result))
			{
				this.PopulateDomainNamingContexts(partitionFqdn);
				this.domainNCs.TryGetValue(partitionFqdn, out result);
			}
			return result;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000E7C2 File Offset: 0x0000C9C2
		internal ADObjectId GetRootDomainNamingContextForLocalForest()
		{
			return this.GetRootDomainNamingContext(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		internal ADObjectId GetRootDomainNamingContext(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADObjectId result;
			if (!this.rootDomainNCs.TryGetValue(partitionFqdn, out result))
			{
				this.PopulateDomainNamingContexts(partitionFqdn);
				this.rootDomainNCs.TryGetValue(partitionFqdn, out result);
			}
			return result;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000260 RID: 608
		internal abstract TopologyMode TopologyMode { get; }

		// Token: 0x06000261 RID: 609
		public abstract IList<TopologyVersion> GetAllTopologyVersions();

		// Token: 0x06000262 RID: 610
		public abstract IList<TopologyVersion> GetTopologyVersions(IList<string> partitionFqdns);

		// Token: 0x06000263 RID: 611 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public int GetTopologyVersion(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			IList<TopologyVersion> topologyVersions = this.GetTopologyVersions(new List<string>
			{
				partitionFqdn
			});
			if (topologyVersions.Count > 0)
			{
				return topologyVersions[0].Version;
			}
			return -1;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E84B File Offset: 0x0000CA4B
		public IList<ADServerInfo> GetServersForRole(string partitionFqdn, IList<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			return this.InternalGetServersForRole(partitionFqdn, currentlyUsedServers, role, serversRequested, forestWideAffinityRequested);
		}

		// Token: 0x06000265 RID: 613
		public abstract ADServerInfo GetServerFromDomainDN(string distinguishedName, NetworkCredential credential);

		// Token: 0x06000266 RID: 614
		public abstract ADServerInfo GetRemoteServerFromDomainFqdn(string domainFqdn, NetworkCredential credential);

		// Token: 0x06000267 RID: 615 RVA: 0x0000E865 File Offset: 0x0000CA65
		public virtual void SetConfigDC(string partitionFqdn, string serverName, int port)
		{
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentException("partitionFqdn");
			}
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentException("serverName");
			}
			this.ClearConfigNamingContexts(partitionFqdn);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E894 File Offset: 0x0000CA94
		public string GetConfigDCForLocalForest()
		{
			return this.GetConfigDC(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E8A1 File Offset: 0x0000CAA1
		public string GetConfigDC(string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			return this.GetConfigDC(partitionFqdn, true);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public string GetConfigDC(string partitionFqdn, bool throwOnFailure)
		{
			ADServerInfo configDCInfo = this.GetConfigDCInfo(partitionFqdn, throwOnFailure);
			if (configDCInfo != null)
			{
				return configDCInfo.Fqdn;
			}
			return null;
		}

		// Token: 0x0600026B RID: 619
		internal abstract ADServerInfo GetConfigDCInfo(string partitionFqdn, bool throwOnFailure);

		// Token: 0x0600026C RID: 620
		public abstract void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role);

		// Token: 0x0600026D RID: 621 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		protected virtual ADServerInfo GetDefaultServerInfo(string partitionFqdn)
		{
			string serverFqdn = null;
			if (partitionFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase) && NativeHelpers.LocalMachineRoleIsDomainController())
			{
				serverFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			}
			return new ADServerInfo(serverFqdn, partitionFqdn, this.DefaultDCPort, null, 100, AuthType.Kerberos, true);
		}

		// Token: 0x0600026E RID: 622
		protected abstract IList<ADServerInfo> InternalGetServersForRole(string partitionFqdn, IList<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000E916 File Offset: 0x0000CB16
		public virtual int TopoRecheckIntervalMsec
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000E919 File Offset: 0x0000CB19
		internal virtual int DefaultDCPort
		{
			get
			{
				return 389;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000E920 File Offset: 0x0000CB20
		internal virtual int DefaultGCPort
		{
			get
			{
				return 3268;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E927 File Offset: 0x0000CB27
		protected static void EnforceLocalForestPartition(string partitionFqdn)
		{
			if (partitionFqdn != TopologyProvider.localForestFqdn)
			{
				throw new ArgumentException("partitionFqdn");
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000E941 File Offset: 0x0000CB41
		internal static void EnforceNonEmptyPartition(string partitionFqdn)
		{
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentException("partitionFqdn");
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E956 File Offset: 0x0000CB56
		protected static bool IsTopologyServiceProcess()
		{
			return Globals.ProcessName.Equals("Microsoft.Exchange.Directory.TopologyService.exe", StringComparison.OrdinalIgnoreCase) || Globals.ProcessName.Equals("Internal.Exchange.TopologyDiscovery.exe", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000087 RID: 135
		private static TopologyMode? userSetTopologyMode;

		// Token: 0x04000088 RID: 136
		private static bool isRunningOnTopologyService = false;

		// Token: 0x04000089 RID: 137
		private static object instanceLockRoot = new object();

		// Token: 0x0400008A RID: 138
		private static TopologyProvider staticInstance;

		// Token: 0x0400008B RID: 139
		private static string localForestFqdn;

		// Token: 0x0400008C RID: 140
		private ConcurrentDictionary<string, ADObjectId> configNCs = new ConcurrentDictionary<string, ADObjectId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400008D RID: 141
		private ConcurrentDictionary<string, ADObjectId> schemaNCs = new ConcurrentDictionary<string, ADObjectId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400008E RID: 142
		private ConcurrentDictionary<string, ADObjectId> domainNCs = new ConcurrentDictionary<string, ADObjectId>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400008F RID: 143
		private ConcurrentDictionary<string, ADObjectId> rootDomainNCs = new ConcurrentDictionary<string, ADObjectId>(StringComparer.OrdinalIgnoreCase);
	}
}
