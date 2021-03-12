using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000DA RID: 218
	internal class ConnectionPoolManager
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0002FB7D File Offset: 0x0002DD7D
		internal static int LongRunningOperationThresholdSeconds
		{
			get
			{
				return ConnectionPoolManager.longRunningOperationThresholdSeconds;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002FB84 File Offset: 0x0002DD84
		internal static int CallstackTraceRatio
		{
			get
			{
				return ConnectionPoolManager.callstackTraceRatio;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002FB8B File Offset: 0x0002DD8B
		internal static int ObjectsFromEntriesThresholdMilliseconds
		{
			get
			{
				return ConnectionPoolManager.objectsFromEntriesThresholdMilliseconds;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002FB92 File Offset: 0x0002DD92
		private bool IsPartitionScoped
		{
			get
			{
				return !PartitionId.IsLocalForestPartition(this.partitionFqdn);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0002FBA2 File Offset: 0x0002DDA2
		internal static bool LdapEncryptionEnabled
		{
			get
			{
				return ConnectionPoolManager.ldapEncryptionEnabled;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002FBAC File Offset: 0x0002DDAC
		private ConnectionPoolManager(NetworkCredential credential, string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			Random random = new Random();
			this.partitionFqdn = partitionFqdn;
			this.credential = credential;
			int tickCount = Environment.TickCount;
			this.whenTopoVersionLastChecked = tickCount;
			this.whenPoolsRefreshed = tickCount;
			this.poolsRefreshIntervalMsec = ConnectionPoolManager.DefaultPoolsRefreshIntervalMsec + random.Next(ConnectionPoolManager.DefaultPoolsRefreshIntervalVariable);
			this.connectionPools = new LdapConnectionPool[6];
			ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "Creating new ConnectionPoolManager {0}, whenCreated={1}, credential={2}, partitionFqdn={3}", new object[]
			{
				this.GetHashCode(),
				tickCount,
				ConnectionPoolManager.GetCredentialString(this.credential),
				this.partitionFqdn
			});
			int intValueFromRegistry = Globals.GetIntValueFromRegistry("Disable LDAP Encryption", 0, this.GetHashCode());
			ConnectionPoolManager.ldapEncryptionEnabled = (intValueFromRegistry == 0);
			ConnectionPoolManager.longRunningOperationThresholdSeconds = Globals.GetIntValueFromRegistry("LongRunningOperationThresholdSeconds", 15, this.GetHashCode());
			ConnectionPoolManager.callstackTraceRatio = Globals.GetIntValueFromRegistry("CallstackTraceRatio", 0, this.GetHashCode());
			ConnectionPoolManager.objectsFromEntriesThresholdMilliseconds = Globals.GetIntValueFromRegistry("ObjectsFromEntriesThresholdMilliseconds", 100, this.GetHashCode());
			int intValueFromRegistry2 = Globals.GetIntValueFromRegistry("ClientSideSearchTimeoutSeconds", 180, this.GetHashCode());
			if (intValueFromRegistry2 != 180)
			{
				ConnectionPoolManager.clientSideSearchTimeout = TimeSpan.FromSeconds((double)intValueFromRegistry2);
			}
			int intValueFromRegistry3 = Globals.GetIntValueFromRegistry("NotificationClientSideSearchTimeoutSeconds", 30, this.GetHashCode());
			if (intValueFromRegistry3 != 30)
			{
				ConnectionPoolManager.notificationClientSideSearchTimeout = TimeSpan.FromSeconds((double)intValueFromRegistry3);
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002FD1C File Offset: 0x0002DF1C
		private static ConnectionPoolManager TryGetInstance(NetworkCredential credential, string partitionFqdn, out object lockRoot)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			if (PartitionId.IsLocalForestPartition(partitionFqdn))
			{
				lockRoot = ConnectionPoolManager.instanceLockRoot;
				List<ConnectionPoolManager> list = ConnectionPoolManager.staticInstances;
				for (int i = 0; i < list.Count; i++)
				{
					ConnectionPoolManager connectionPoolManager = list[i];
					if (ConnectionPoolManager.CredentialsAreEqual(credential, connectionPoolManager.credential))
					{
						return connectionPoolManager;
					}
				}
				return null;
			}
			SyncWrapper<ConnectionPoolManager> orAdd = ConnectionPoolManager.partitionInstances.GetOrAdd(partitionFqdn, (string param0) => new SyncWrapper<ConnectionPoolManager>());
			lockRoot = orAdd;
			return orAdd.Value;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002FDA4 File Offset: 0x0002DFA4
		private static ConnectionPoolManager GetInstance(NetworkCredential credential, string partitionFqdn)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			object obj;
			ConnectionPoolManager connectionPoolManager = ConnectionPoolManager.TryGetInstance(credential, partitionFqdn, out obj);
			if (connectionPoolManager == null)
			{
				try
				{
					if (!Monitor.TryEnter(obj, 60000))
					{
						int num = 60;
						ExTraceGlobals.ADTopologyTracer.TraceError<int>(0L, "Could not discover topology in {0} seconds, throwing ADTransientException", num);
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_TOPO_INITIALIZATION_TIMEOUT, null, new object[]
						{
							partitionFqdn,
							num
						});
						throw new ADTransientException(DirectoryStrings.ExceptionADTopologyCreationTimeout(60));
					}
					object obj2;
					connectionPoolManager = ConnectionPoolManager.TryGetInstance(credential, partitionFqdn, out obj2);
					if (connectionPoolManager == null)
					{
						if (PartitionId.IsLocalForestPartition(partitionFqdn))
						{
							connectionPoolManager = ConnectionPoolManager.CreateAndPublishInstance(credential);
						}
						else
						{
							connectionPoolManager = ConnectionPoolManager.CreateAndPublishInstance(partitionFqdn);
						}
					}
				}
				finally
				{
					if (Monitor.IsEntered(obj))
					{
						Monitor.Exit(obj);
					}
				}
			}
			return connectionPoolManager;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002FE64 File Offset: 0x0002E064
		private static ConnectionPoolManager CreateAndPublishInstance(NetworkCredential credential)
		{
			ConnectionPoolManager connectionPoolManager = null;
			List<PooledLdapConnection> connectionsToRelease = new List<PooledLdapConnection>();
			ConnectionPoolManager result;
			try
			{
				List<ConnectionPoolManager> list = new List<ConnectionPoolManager>(ConnectionPoolManager.staticInstances);
				if (list.Count >= 5)
				{
					if (list[0].credential != null)
					{
						connectionPoolManager = list[0];
					}
					else
					{
						connectionPoolManager = list[1];
					}
					list.Remove(connectionPoolManager);
				}
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string>(0L, "Creating a static instance with credentials '{0}'", ConnectionPoolManager.GetCredentialString(credential));
				ConnectionPoolManager connectionPoolManager2 = new ConnectionPoolManager(credential, TopologyProvider.LocalForestFqdn);
				bool flag;
				connectionPoolManager2.DiscoverTopologyForLocalForest(null, false, out flag, connectionsToRelease);
				list.Add(connectionPoolManager2);
				ConnectionPoolManager.staticInstances = list;
				ExTraceGlobals.ADTopologyTracer.TraceDebug<int>(0L, "Static instance {0} is ready", connectionPoolManager2.GetHashCode());
				if (credential == null)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_AD_DRIVER_INIT, null, new object[0]);
				}
				result = connectionPoolManager2;
			}
			finally
			{
				if (connectionPoolManager != null)
				{
					connectionPoolManager.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002FF40 File Offset: 0x0002E140
		private static ConnectionPoolManager CreateAndPublishInstance(string partitionFqdn)
		{
			List<PooledLdapConnection> connectionsToRelease = new List<PooledLdapConnection>();
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>(0L, "Creating a static instance with partition '{0}'", partitionFqdn);
			ConnectionPoolManager connectionPoolManager = new ConnectionPoolManager(null, partitionFqdn);
			bool flag;
			connectionPoolManager.DiscoverTopology(null, false, out flag, connectionsToRelease, partitionFqdn);
			SyncWrapper<ConnectionPoolManager> syncWrapper = ConnectionPoolManager.partitionInstances[partitionFqdn];
			syncWrapper.Value = connectionPoolManager;
			return connectionPoolManager;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002FF90 File Offset: 0x0002E190
		private static PooledLdapConnection GetConnection(ConnectionType connectionType, string partitionFqdn, ADObjectId domain, string serverName, int port, NetworkCredential credential)
		{
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			TopologyMode currentTopologyMode = TopologyProvider.CurrentTopologyMode;
			if (credential != null && currentTopologyMode != TopologyMode.ADTopologyService && TopologyMode.Ldap != currentTopologyMode)
			{
				throw new NotSupportedException(DirectoryStrings.ExceptionCannotUseCredentials(currentTopologyMode));
			}
			bool flag = false;
			PooledLdapConnection pooledLdapConnection = null;
			bool flag2 = serverName != null;
			bool flag3 = domain != null;
			bool flag4 = !flag3 && !flag2;
			ExTraceGlobals.GetConnectionTracer.TraceDebug(0L, "ConnectionPoolManager.GetConnection: type={0}, domain={1}, server={2}, port={3}, partition={4}, credentials={5}", new object[]
			{
				(int)connectionType,
				(domain == null) ? "<null>" : domain.ToDNString(),
				serverName ?? "<null>",
				port,
				partitionFqdn,
				ConnectionPoolManager.GetCredentialString(credential)
			});
			ExTraceGlobals.FaultInjectionTracer.TraceTest(4087754045U);
			int num = 0;
			while (pooledLdapConnection == null)
			{
				flag = false;
				ConnectionPoolManager instance = ConnectionPoolManager.GetInstance(credential, partitionFqdn);
				TopologyProvider instance2 = TopologyProvider.GetInstance();
				if (num >= 5)
				{
					ExTraceGlobals.GetConnectionTracer.TraceDebug((long)instance.GetHashCode(), "We've rebuilt the topology too many times for one request, giving up");
					throw new ADTransientException(DirectoryStrings.ExceptionUnableToCreateConnections);
				}
				int tickCount = Environment.TickCount;
				ExTraceGlobals.GetConnectionTracer.TraceDebug<int, int, int>((long)instance.GetHashCode(), "Topo last checked at {0}, it is now {1} and recheck interval is {2}", instance.whenTopoVersionLastChecked, tickCount, instance2.TopoRecheckIntervalMsec);
				if (tickCount > instance.whenTopoVersionLastChecked + instance2.TopoRecheckIntervalMsec)
				{
					instance.whenTopoVersionLastChecked = tickCount;
					if (instance.CheckTopologyVersionForRebuild() || tickCount - instance.whenPoolsRefreshed > instance.poolsRefreshIntervalMsec)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug((long)instance.GetHashCode(), "Topology version has changed, need to rebuild. Or it's time to refresh connection pools");
						instance.RebuildIfNewTopologyAvailable();
						num++;
						continue;
					}
				}
				int num2 = ConnectionPoolManager.whenPartitionPoolsLastChecked;
				if (tickCount - num2 > ConnectionPoolManager.PartitionPoolCleanupIntervalMsec && num2 == Interlocked.CompareExchange(ref ConnectionPoolManager.whenPartitionPoolsLastChecked, tickCount, num2))
				{
					ConnectionPoolManager.CleanupPartitionPools();
				}
				ConnectionPoolType connectionPoolType = ConnectionPoolManager.PoolTypeFromConnectionType(connectionType);
				pooledLdapConnection = instance.GetConnectionFromPool(connectionPoolType, domain, serverName, port, ref flag);
				if (pooledLdapConnection != null)
				{
					ExTraceGlobals.GetConnectionTracer.TraceDebug<int, string>((long)instance.GetHashCode(), "Found conn in general pool {0} to {1}", (int)connectionPoolType, pooledLdapConnection.ADServerInfo.FqdnPlusPort);
					break;
				}
				if (flag4)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning<int>((long)instance.GetHashCode(), "Case 5. General conn not found in pool {0}, need to rebuild", (int)connectionPoolType);
					if (instance.RebuildIfNewTopologyAvailable())
					{
						num++;
					}
					else
					{
						ExTraceGlobals.GetConnectionTracer.TraceError<int>((long)instance.GetHashCode(), "Case 5. rebuild failed, will throw ADTransientException for pool type {0}", (int)connectionPoolType);
						StringBuilder stringBuilder = new StringBuilder();
						foreach (ADServerInfo adserverInfo in instance.connectionPools[(int)connectionPoolType].ADServerInfos)
						{
							stringBuilder.AppendLine(adserverInfo.Fqdn);
						}
						if (connectionType == ConnectionType.GlobalCatalog)
						{
							string forestName = NativeHelpers.GetForestName();
							Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ALL_GC_DOWN, string.Empty, new object[]
							{
								forestName,
								stringBuilder.ToString()
							});
							throw new ADTransientException(DirectoryStrings.ExceptionADTopologyHasNoAvailableServersInForest(forestName));
						}
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ALL_DC_DOWN, string.Empty, new object[]
						{
							stringBuilder.ToString()
						});
						throw new ADTransientException(DirectoryStrings.ExceptionAllDomainControllersUnavailable);
					}
				}
				else if (flag3 || (flag2 && !flag))
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning((long)instance.GetHashCode(), "Cases 6 and 7 - need to look up in custom pool");
					connectionPoolType = ((connectionPoolType != ConnectionPoolType.GCPool) ? ConnectionPoolType.UserDCPool : ConnectionPoolType.UserGCPool);
					pooledLdapConnection = instance.GetConnectionFromPool(connectionPoolType, domain, serverName, port, ref flag);
					if (pooledLdapConnection != null)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<int, string>((long)instance.GetHashCode(), "Found conn in custom pool {0} to {1}", (int)connectionPoolType, pooledLdapConnection.ADServerInfo.FqdnPlusPort);
						break;
					}
					if (flag2 && !flag)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string, int, int>((long)instance.GetHashCode(), "Case 8. Adding {0}:{1} to custom pool {2}", serverName, port, (int)connectionPoolType);
						string writableNC;
						SuitabilityVerifier.CheckIsServerSuitable(serverName, connectionPoolType == ConnectionPoolType.GCPool || connectionPoolType == ConnectionPoolType.UserGCPool, credential, out writableNC);
						ADServerInfo serverInfo = new ADServerInfo(serverName, instance.partitionFqdn, port, writableNC, 100, AuthType.Kerberos, true);
						bool flag5 = false;
						instance.connectionPools[(int)connectionPoolType].AppendCustomServer(serverInfo, ref flag5);
					}
					else if (flag3)
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string>((long)instance.GetHashCode(), "Asking topology for a server from {0}", domain.DistinguishedName);
						if (TopologyProvider.CurrentTopologyMode == TopologyMode.Adam)
						{
							Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ADAM_GET_SERVER_FROM_DOMAIN_DN, domain.DistinguishedName, new object[]
							{
								domain.DistinguishedName
							});
							throw new ADTransientException(DirectoryStrings.ExceptionAdamGetServerFromDomainDN(domain.DistinguishedName));
						}
						ADServerInfo serverFromDomainDN = TopologyProvider.GetInstance().GetServerFromDomainDN(domain.DistinguishedName, credential);
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN, domain.DistinguishedName, new object[]
						{
							domain.DistinguishedName,
							serverFromDomainDN.Fqdn
						});
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string, int, int>((long)instance.GetHashCode(), "Case 9. Adding {0}:{1} to custom pool {2}", serverName, port, (int)connectionPoolType);
						bool flag6 = false;
						instance.connectionPools[(int)connectionPoolType].AppendCustomServer(serverFromDomainDN, ref flag6);
						if (flag6)
						{
							string domain2 = NativeHelpers.CanonicalNameFromDistinguishedName(domain.DistinguishedName);
							ExTraceGlobals.GetConnectionTracer.TraceError<string, int, int>((long)instance.GetHashCode(), "Case 9. {0}:{1} is already in the custom pool {2} and is marked as down. Will log an event and throw ADTransientException.", serverName, port, (int)connectionPoolType);
							Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN_FAILED, domain.DistinguishedName, new object[]
							{
								domain.DistinguishedName
							});
							throw new ADTransientException(DirectoryStrings.ExceptionADTopologyHasNoAvailableServersInDomain(domain2));
						}
					}
				}
			}
			return pooledLdapConnection;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000304B4 File Offset: 0x0002E6B4
		private static void CleanupPartitionPools()
		{
			int tickCount = Environment.TickCount;
			foreach (KeyValuePair<string, SyncWrapper<ConnectionPoolManager>> keyValuePair in ConnectionPoolManager.partitionInstances)
			{
				ConnectionPoolManager value = keyValuePair.Value.Value;
				if (value != null && tickCount - value.whenTopoVersionLastChecked > ConnectionPoolManager.PartitionPoolCleanupInactivityThresholdMsec)
				{
					lock (keyValuePair.Value)
					{
						if (tickCount - value.whenTopoVersionLastChecked > ConnectionPoolManager.PartitionPoolCleanupInactivityThresholdMsec)
						{
							ExTraceGlobals.ADTopologyTracer.TraceDebug(0L, "Disposing a ConnectionPoolManager  {0} for {1} that was used more than {2} sec ago (Last updated {3} sec ago)", new object[]
							{
								keyValuePair.Value.Value.GetHashCode(),
								keyValuePair.Key,
								ConnectionPoolManager.PartitionPoolCleanupInactivityThresholdMsec / 1000,
								(tickCount - keyValuePair.Value.Value.whenTopoVersionLastChecked) / 1000
							});
							SyncWrapper<ConnectionPoolManager> syncWrapper;
							if (ConnectionPoolManager.partitionInstances.TryRemove(keyValuePair.Key, out syncWrapper))
							{
								syncWrapper.Value.Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00030620 File Offset: 0x0002E820
		private static string GetProcessUserSid()
		{
			string value;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				value = current.User.Value;
			}
			return value;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003065C File Offset: 0x0002E85C
		private static bool CredentialsAreEqual(NetworkCredential cred1, NetworkCredential cred2)
		{
			return (cred1 == null && cred2 == null) || (cred1 != null && cred2 != null && (string.Equals(cred1.UserName, cred2.UserName, StringComparison.OrdinalIgnoreCase) && string.Equals(cred1.Domain, cred2.Domain, StringComparison.OrdinalIgnoreCase)) && string.Equals(cred1.Password, cred2.Password, StringComparison.Ordinal));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000306B8 File Offset: 0x0002E8B8
		[Conditional("DEBUG")]
		private static void DbgCheckCallStack()
		{
			string stackTrace = Environment.StackTrace;
			if (!stackTrace.Contains("SetProcessTopologyMode") && !stackTrace.Contains("CleanConnectionManagers"))
			{
				throw new NotSupportedException("Reset should only be called from SetProcessTopologyMode method and test code.");
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000306F0 File Offset: 0x0002E8F0
		private static string GetCredentialString(NetworkCredential credential)
		{
			if (credential == null)
			{
				return string.Empty;
			}
			return credential.Domain + "\\" + credential.UserName;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00030714 File Offset: 0x0002E914
		private static ConnectionPoolType PoolTypeFromConnectionType(ConnectionType connectionType)
		{
			switch (connectionType)
			{
			case ConnectionType.DomainController:
				return ConnectionPoolType.DCPool;
			case ConnectionType.GlobalCatalog:
				return ConnectionPoolType.GCPool;
			case ConnectionType.ConfigurationDomainController:
				return ConnectionPoolType.ConfigDCPool;
			case ConnectionType.ConfigDCNotification:
				return ConnectionPoolType.ConfigDCNotifyPool;
			default:
				throw new ArgumentException(DirectoryStrings.ExArgumentException("connectionType", connectionType.ToString()), "connectionType");
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00030768 File Offset: 0x0002E968
		private void Dispose()
		{
			for (int i = 0; i < 6; i++)
			{
				this.connectionPools[i].DisconnectPool();
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00030790 File Offset: 0x0002E990
		internal static void BlockImpersonatedCallers()
		{
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (current.ImpersonationLevel != TokenImpersonationLevel.None)
				{
					if (current.User.Value.Equals(ConnectionPoolManager.processUserSid, StringComparison.OrdinalIgnoreCase))
					{
						ExTraceGlobals.GetConnectionTracer.TraceDebug<string>(0L, "ConnectionPoolManager.BlockImpersonatedCallers: Impersonation for {0} is allowed because the same account is used, will use connection pool.", current.Name);
					}
					else
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_IMPERSONATED_CALLER, DirectoryStrings.ExceptionImpersonation, new object[]
						{
							current.Name,
							Environment.StackTrace,
							string.Empty,
							string.Empty,
							string.Empty,
							string.Empty,
							string.Empty,
							string.Empty,
							string.Empty
						});
						ExDiagnostics.FailFast(DirectoryStrings.ExceptionImpersonation, false);
					}
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00030878 File Offset: 0x0002EA78
		internal static TimeSpan ClientSideSearchTimeout
		{
			get
			{
				return ConnectionPoolManager.clientSideSearchTimeout;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0003087F File Offset: 0x0002EA7F
		internal static TimeSpan NotificationClientSideSearchTimeout
		{
			get
			{
				return ConnectionPoolManager.notificationClientSideSearchTimeout;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00030886 File Offset: 0x0002EA86
		internal static PooledLdapConnection GetConnection(ConnectionType connectionType, string partitionFqdn)
		{
			ConnectionPoolManager.BlockImpersonatedCallers();
			return ConnectionPoolManager.GetConnection(connectionType, partitionFqdn, null, null, 0, null);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00030898 File Offset: 0x0002EA98
		internal static PooledLdapConnection GetConnection(ConnectionType connectionType, string partitionFqdn, ADObjectId domain)
		{
			return ConnectionPoolManager.GetConnection(connectionType, partitionFqdn, null, domain);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000308A3 File Offset: 0x0002EAA3
		internal static PooledLdapConnection GetConnection(ConnectionType connectionType, string partitionFqdn, NetworkCredential networkCredential, ADObjectId domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (string.IsNullOrEmpty(domain.DistinguishedName))
			{
				throw new ArgumentNullException("domain.DistinguishedName");
			}
			ConnectionPoolManager.BlockImpersonatedCallers();
			return ConnectionPoolManager.GetConnection(connectionType, partitionFqdn, domain, null, 0, networkCredential);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000308DC File Offset: 0x0002EADC
		internal static PooledLdapConnection GetConnection(ConnectionType connectionType, string partitionFqdn, NetworkCredential networkCredential, string serverName, int port)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			TopologyProvider.EnforceNonEmptyPartition(partitionFqdn);
			ADServerSettings.ThrowIfServerNameDoesntMatchPartitionId(serverName, partitionFqdn);
			string text = null;
			bool flag = false;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				bool flag2 = Globals.ProcessName.Equals("w3wp.exe", StringComparison.OrdinalIgnoreCase) && "psws".Equals(Globals.CurrentAppName, StringComparison.OrdinalIgnoreCase);
				flag = (TopologyProvider.IsAdminMode && !Globals.ProcessName.Equals("wsmprovhost.exe", StringComparison.OrdinalIgnoreCase) && !flag2 && (current.ImpersonationLevel == TokenImpersonationLevel.Delegation || current.ImpersonationLevel == TokenImpersonationLevel.Impersonation));
				text = current.Name;
			}
			if (flag && !string.IsNullOrEmpty(serverName))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ALLOW_IMPERSONATION, null, new object[]
				{
					text
				});
				if (networkCredential == null)
				{
					string writableNC;
					SuitabilityVerifier.CheckIsServerSuitable(serverName, connectionType == ConnectionType.GlobalCatalog, networkCredential, out writableNC);
					ADServerInfo serverInfo = new ADServerInfo(serverName, partitionFqdn, (connectionType == ConnectionType.GlobalCatalog) ? TopologyProvider.GetInstance().DefaultGCPort : TopologyProvider.GetInstance().DefaultDCPort, writableNC, 100, AuthType.Kerberos, true);
					ExTraceGlobals.GetConnectionTracer.TraceDebug<string>(0L, "ConnectionPoolManager.GetConnection: Impersonation for {0} is allowed, one time connection will be created", text);
					return LdapConnectionPool.CreateOneTimeConnection(null, serverInfo, LocatorFlags.None);
				}
			}
			else
			{
				ConnectionPoolManager.BlockImpersonatedCallers();
			}
			return ConnectionPoolManager.GetConnection(connectionType, partitionFqdn, null, serverName, port, networkCredential);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00030A20 File Offset: 0x0002EC20
		internal static bool IsServerInPreferredSite(string partitionFqdn, ADServer adServer)
		{
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentNullException("partitionFqdn");
			}
			if (adServer == null)
			{
				throw new ArgumentException("adServer");
			}
			ConnectionPoolManager instance = ConnectionPoolManager.GetInstance(null, partitionFqdn);
			LdapConnectionPool ldapConnectionPool = instance.connectionPools[2];
			return ldapConnectionPool.IsServerInAnyKnownSite(adServer);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00030A6C File Offset: 0x0002EC6C
		internal static void Reset()
		{
			List<ConnectionPoolManager> list;
			lock (ConnectionPoolManager.instanceLockRoot)
			{
				list = ConnectionPoolManager.staticInstances;
				ConnectionPoolManager.staticInstances = new List<ConnectionPoolManager>(0);
			}
			foreach (ConnectionPoolManager connectionPoolManager in list)
			{
				connectionPoolManager.Dispose();
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00030AF4 File Offset: 0x0002ECF4
		private bool DiscoverTopologyForLocalForest(ConnectionPoolManager previousInstance, bool doNotReplaceIfSameTopology, out bool configDcChanged, List<PooledLdapConnection> connectionsToRelease)
		{
			return this.DiscoverTopology(previousInstance, doNotReplaceIfSameTopology, out configDcChanged, connectionsToRelease, TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00030B08 File Offset: 0x0002ED08
		private bool DiscoverTopology(ConnectionPoolManager previousInstance, bool doNotReplaceIfSameTopology, out bool configDcChanged, List<PooledLdapConnection> connectionsToRelease, string partitionFqdn)
		{
			bool flag = false;
			configDcChanged = false;
			TopologyProvider instance = TopologyProvider.GetInstance();
			this.topologyVersion = instance.GetTopologyVersion(partitionFqdn);
			ADProviderPerf.UpdateProcessCounter(Counter.ProcessTopologyVersion, UpdateType.Update, (uint)this.topologyVersion);
			ExTraceGlobals.ADTopologyTracer.TraceDebug<Type, string, int>((long)this.GetHashCode(), "Discovering topology, provider={0}, partition={1}, topology version={2}", instance.GetType(), partitionFqdn, this.topologyVersion);
			bool flag2 = false;
			int tickCount = Environment.TickCount;
			if (previousInstance != null && tickCount - previousInstance.whenPoolsRefreshed > previousInstance.poolsRefreshIntervalMsec)
			{
				flag2 = true;
				ExTraceGlobals.ADTopologyTracer.TraceDebug<int, int>((long)previousInstance.GetHashCode(), "Refreshing connection pools because they have been used for more than {0} sec ago (Last refreshed {1} sec ago)", previousInstance.poolsRefreshIntervalMsec / 1000, (tickCount - previousInstance.whenPoolsRefreshed) / 1000);
			}
			int[][] array = new int[6][];
			for (int i = 0; i < 6; i++)
			{
				bool flag3 = false;
				int[] array2 = new int[0];
				IList<ADServerInfo> list = new List<ADServerInfo>(0);
				if (i != 4 && i != 5 && (!this.IsPartitionScoped || i != 3))
				{
					string[] array3 = new string[0];
					if (previousInstance != null && !flag2)
					{
						ADServerInfo[] adserverInfos = previousInstance.connectionPools[i].ADServerInfos;
						array3 = new string[adserverInfos.Length];
						for (int j = 0; j < adserverInfos.Length; j++)
						{
							ExTraceGlobals.ADTopologyTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Previous server={0} (from instance {1})", adserverInfos[j].Fqdn, previousInstance.GetHashCode());
							array3[j] = adserverInfos[j].Fqdn;
						}
					}
					ADServerRole adserverRole = LdapConnectionPool.RoleFromConnectionPoolType((ConnectionPoolType)i);
					list = instance.GetServersForRole(partitionFqdn, new List<string>(array3), adserverRole, (adserverRole == ADServerRole.ConfigurationDomainController) ? 1 : Globals.LdapConnectionPoolSize, false);
					array2 = new int[list.Count];
					for (int k = 0; k < list.Count; k++)
					{
						array2[k] = list[k].Mapping;
					}
					if (list.Count != array3.Length)
					{
						ExTraceGlobals.ADTopologyTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Discovery detected different server length {0} instead of {1}", list.Count, array3.Length);
						flag3 = true;
					}
					else
					{
						for (int l = 0; l < array2.Length; l++)
						{
							if (array2[l] == -1)
							{
								ExTraceGlobals.ADTopologyTracer.TraceDebug<int>((long)this.GetHashCode(), "Discovery detected a new server at index {0}", l);
								flag3 = true;
								break;
							}
						}
					}
					if (flag3 && adserverRole == ADServerRole.ConfigurationDomainController)
					{
						ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "The configDC has changed to {0}", (list.Count > 0) ? list[0].Fqdn : "<empty>");
						configDcChanged = true;
					}
				}
				else if ((i == 4 || i == 5) && previousInstance != null)
				{
					int num = 0;
					if (i == 5)
					{
						num = 1;
					}
					List<ADServerInfo> list2 = new List<ADServerInfo>(previousInstance.connectionPools[i].ADServerInfos);
					List<ADServerInfo> list3 = new List<ADServerInfo>(previousInstance.connectionPools[num].ADServerInfos);
					List<string> list4 = new List<string>(list2.Count + list3.Count);
					bool[] array4 = new bool[2];
					array4[0] = true;
					bool[] array5 = array4;
					for (int m = 0; m < array5.Length; m++)
					{
						List<ADServerInfo> list5 = array5[m] ? list2 : list3;
						foreach (ADServerInfo adserverInfo in list5)
						{
							list4.Add(adserverInfo.Fqdn);
						}
					}
					list = this.GetServersForRoleForUserPools(partitionFqdn, list4, LdapConnectionPool.RoleFromConnectionPoolType((ConnectionPoolType)i));
				}
				array[i] = array2;
				this.connectionPools[i] = new LdapConnectionPool((ConnectionPoolType)i, list.ToArray<ADServerInfo>(), this.credential);
				flag = (flag || flag3);
			}
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Discovery detected {0} topology change, {1}performing replace and disconnect", flag ? "a" : "NO", (flag || !doNotReplaceIfSameTopology) ? string.Empty : "NOT ");
			if (!flag && doNotReplaceIfSameTopology)
			{
				return false;
			}
			if (previousInstance != null)
			{
				previousInstance.connectionPools[4].MergePool(previousInstance.connectionPools[0], connectionsToRelease);
				previousInstance.connectionPools[5].MergePool(previousInstance.connectionPools[1], connectionsToRelease);
				array[4] = this.CreateMapping(this.connectionPools[4].ADServerInfos, previousInstance.connectionPools[4].ADServerInfos);
				array[5] = this.CreateMapping(this.connectionPools[5].ADServerInfos, previousInstance.connectionPools[5].ADServerInfos);
				this.connectionPools[0].MoveConnectionAcrossPool(previousInstance.connectionPools[4], connectionsToRelease);
				this.connectionPools[1].MoveConnectionAcrossPool(previousInstance.connectionPools[5], connectionsToRelease);
			}
			for (int n = 0; n < 6; n++)
			{
				if (previousInstance != null)
				{
					this.connectionPools[n].MoveConnectionsAndDisconnect(array[n], previousInstance.connectionPools[n], connectionsToRelease);
				}
			}
			this.connectionPools[4].RemoveEmptyConnectionFromPool();
			this.connectionPools[5].RemoveEmptyConnectionFromPool();
			if (flag2)
			{
				this.whenPoolsRefreshed = tickCount;
			}
			else if (previousInstance != null)
			{
				this.whenPoolsRefreshed = previousInstance.whenPoolsRefreshed;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (ADServerInfo adserverInfo2 in this.connectionPools[0].ADServerInfos)
			{
				stringBuilder.AppendLine(adserverInfo2.Fqdn);
			}
			foreach (ADServerInfo adserverInfo3 in this.connectionPools[1].ADServerInfos)
			{
				stringBuilder2.AppendLine(adserverInfo3.Fqdn);
			}
			ADServerInfo[] adserverInfos4 = this.connectionPools[2].ADServerInfos;
			string text = string.Empty;
			if (adserverInfos4 != null && adserverInfos4.Length > 0)
			{
				text = adserverInfos4[0].Fqdn;
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			foreach (ADServerInfo adserverInfo4 in this.connectionPools[4].ADServerInfos)
			{
				stringBuilder3.AppendLine(adserverInfo4.Fqdn);
			}
			foreach (ADServerInfo adserverInfo5 in this.connectionPools[5].ADServerInfos)
			{
				stringBuilder4.AppendLine(adserverInfo5.Fqdn);
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_TOPOLOGY_UPDATE, null, new object[]
			{
				stringBuilder.ToString(),
				stringBuilder2.ToString(),
				text,
				stringBuilder3.ToString(),
				stringBuilder4.ToString(),
				this.topologyVersion,
				instance.GetType().Name
			});
			return true;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00031184 File Offset: 0x0002F384
		private int[] CreateMapping(ADServerInfo[] targetServers, ADServerInfo[] sourceServers)
		{
			int[] array = new int[targetServers.Length];
			for (int i = 0; i < targetServers.Length; i++)
			{
				targetServers[i].Mapping = -1;
				array[i] = -1;
				for (int j = 0; j < sourceServers.Length; j++)
				{
					if (targetServers[i].Equals(sourceServers[j]))
					{
						targetServers[i].Mapping = j;
						array[i] = j;
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000311E0 File Offset: 0x0002F3E0
		private IList<ADServerInfo> GetServersForRoleForUserPools(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role)
		{
			IList<ADServerInfo> allSuitableServersForRole = this.GetAllSuitableServersForRole(partitionFqdn, role);
			IList<ADServerInfo> list = new List<ADServerInfo>(allSuitableServersForRole.Count);
			foreach (ADServerInfo adserverInfo in allSuitableServersForRole)
			{
				if (currentlyUsedServers.IndexOf(adserverInfo.Fqdn) >= 0)
				{
					list.Add(adserverInfo);
				}
			}
			if (ExTraceGlobals.ADTopologyTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "GetServersForRoleForUserPools - returning {0} DCs for role {1} in forest {2}. The list is: {3}.", new object[]
				{
					list.Count,
					role,
					partitionFqdn,
					string.Join<ADServerInfo>(",", list)
				});
			}
			return list;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000312A8 File Offset: 0x0002F4A8
		private IList<ADServerInfo> GetAllSuitableServersForRole(string partitionFqdn, ADServerRole role)
		{
			string[] collection = new string[0];
			TopologyProvider instance = TopologyProvider.GetInstance();
			IList<ADServerInfo> serversForRole = instance.GetServersForRole(partitionFqdn, new List<string>(collection), role, 100, true);
			if (ExTraceGlobals.ADTopologyTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "GetAllSuitableServersForRole - GetServersForRole returned {0} DCs in forest {1}. The list is: {2}.", serversForRole.Count, partitionFqdn, string.Join<ADServerInfo>(",", serversForRole));
			}
			IList<ADServerInfo> list = new List<ADServerInfo>(0);
			foreach (ADServerInfo adserverInfo in serversForRole)
			{
				if (adserverInfo.IsServerSuitable)
				{
					list.Add(adserverInfo);
				}
			}
			if (ExTraceGlobals.ADTopologyTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "GetAllSuitableServersForRole - There are {0} suitable DCs for role {1} in forest {2}. The list is: {3}.", new object[]
				{
					list.Count,
					role,
					partitionFqdn,
					string.Join<ADServerInfo>(",", list)
				});
			}
			return list;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000313B8 File Offset: 0x0002F5B8
		private PooledLdapConnection GetConnectionFromPool(ConnectionPoolType connectionPoolType, ADObjectId domain, string serverName, int port, ref bool serverConnectionPresentButDownOrDisconnected)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 100;
			bool flag3 = serverName != null;
			Exception ex = null;
			PooledLdapConnection connection;
			for (;;)
			{
				connection = this.connectionPools[(int)connectionPoolType].GetConnection(domain, serverName, port, ref flag, ref flag2, ref serverConnectionPresentButDownOrDisconnected, ref ex);
				if (connection != null)
				{
					break;
				}
				if (flag)
				{
					goto Block_2;
				}
				if (flag2)
				{
					ExTraceGlobals.GetConnectionTracer.TraceWarning<ConnectionPoolType, int, int>((long)this.GetHashCode(), "Case 3. Pool {0} ({1}) has pending connections, sleeping {2} msec", connectionPoolType, this.connectionPools[(int)connectionPoolType].GetHashCode(), num);
					Thread.Sleep(num);
				}
				else if (flag3 && serverConnectionPresentButDownOrDisconnected)
				{
					goto Block_5;
				}
				if (!flag2)
				{
					goto Block_9;
				}
			}
			return connection;
			Block_2:
			ExTraceGlobals.GetConnectionTracer.TraceWarning<int, int>((long)this.GetHashCode(), "Case 2. Pool {0} ({1}) is disabled, rediscovery has apparently occurred", (int)connectionPoolType, this.connectionPools[(int)connectionPoolType].GetHashCode());
			return null;
			Block_5:
			ExTraceGlobals.GetConnectionTracer.TraceWarning<string, int, int>((long)this.GetHashCode(), "Case 1. Wanted only {0} from pool {1} ({2}) but the server is down or disconnected. Will attempt rebuild", serverName, (int)connectionPoolType, this.connectionPools[(int)connectionPoolType].GetHashCode());
			if (this.RebuildIfNewTopologyAvailable())
			{
				return null;
			}
			ExTraceGlobals.GetConnectionTracer.TraceError<string>((long)this.GetHashCode(), "Rebuild failed, we got the same topology and server '{0}' is still there marked as 'down'. Throwing ADTransientException with 'Server unavailable' message", serverName);
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NO_CONNECTION_TO_SERVER, serverName, new object[]
			{
				serverName
			});
			if (ex == null)
			{
				throw new ADTransientException(DirectoryStrings.ExceptionFailedToRebuildConnection(serverName), new ActiveDirectoryServerDownException(DirectoryStrings.ExceptionServerUnavailable(serverName)));
			}
			ADInvalidCredentialException ex2 = ex as ADInvalidCredentialException;
			ADInvalidServiceCredentialException ex3 = ex as ADInvalidServiceCredentialException;
			if (ex2 != null || ex3 != null)
			{
				throw ex;
			}
			throw new ADTransientException(DirectoryStrings.ExceptionFailedToRebuildConnection(serverName), ex);
			Block_9:
			return null;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00031518 File Offset: 0x0002F718
		private bool CheckTopologyVersionForRebuild()
		{
			int num = TopologyProvider.GetInstance().GetTopologyVersion(this.partitionFqdn);
			if (num == this.topologyVersion || num == -1)
			{
				ExTraceGlobals.ADTopologyTracer.TraceWarning<int, int, string>((long)this.GetHashCode(), "Topology version is not good for rebuild: current version={0}, available topo version={1}, partition={2}", this.topologyVersion, num, this.partitionFqdn);
				return false;
			}
			ExTraceGlobals.ADTopologyTracer.TraceDebug<int, int, string>((long)this.GetHashCode(), "Topology version is good for rebuild: current version={0}, available topo version={1}, partition={2}", this.topologyVersion, num, this.partitionFqdn);
			return true;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0003158C File Offset: 0x0002F78C
		internal static void ForceRebuild()
		{
			lock (ConnectionPoolManager.instanceLockRoot)
			{
				object obj2;
				ConnectionPoolManager connectionPoolManager = ConnectionPoolManager.TryGetInstance(null, TopologyProvider.LocalForestFqdn, out obj2);
				if (connectionPoolManager != null)
				{
					connectionPoolManager.Rebuild(true);
				}
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000315E0 File Offset: 0x0002F7E0
		private bool RebuildIfNewTopologyAvailable()
		{
			return this.Rebuild(false);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000315EC File Offset: 0x0002F7EC
		private bool Rebuild(bool force)
		{
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Trying to rebuild a {0}Partition-scoped ConnectionPoolManager", this.IsPartitionScoped ? string.Empty : "Non-");
			List<PooledLdapConnection> list = new List<PooledLdapConnection>();
			bool result;
			try
			{
				if (this.IsPartitionScoped)
				{
					result = this.RebuildPartitionScopedConnectionPoolManager(force, list);
				}
				else
				{
					result = this.RebuildNonPartitionScopedConnectionPoolManager(force, list);
				}
			}
			finally
			{
				if (list.Count > 0)
				{
					ExTraceGlobals.ADTopologyTracer.TraceDebug<int>((long)this.GetHashCode(), "Returning {0} connections to a pool. This may result in disposing those connections if they were not moved to a new pool.", list.Count);
					foreach (PooledLdapConnection pooledLdapConnection in list)
					{
						pooledLdapConnection.ReturnToPool();
					}
				}
			}
			return result;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00031730 File Offset: 0x0002F930
		private bool RebuildNonPartitionScopedConnectionPoolManager(bool force, List<PooledLdapConnection> connectionsToRelease)
		{
			bool configDcChanged = false;
			object obj = null;
			bool flag = false;
			lock (ConnectionPoolManager.instanceLockRoot)
			{
				ConnectionPoolManager connectionPoolManager = ConnectionPoolManager.TryGetInstance(this.credential, this.partitionFqdn, out obj);
				flag = (this == connectionPoolManager);
				if (flag)
				{
					ConnectionPoolManager connectionPoolManager2;
					if (!this.TryCheckTopoVersionAndDiscover(force, out configDcChanged, out connectionPoolManager2, connectionsToRelease))
					{
						return false;
					}
					List<ConnectionPoolManager> list = new List<ConnectionPoolManager>(ConnectionPoolManager.staticInstances);
					list.Remove(this);
					list.Add(connectionPoolManager2);
					ConnectionPoolManager.staticInstances = list;
					ExTraceGlobals.ADTopologyTracer.TraceDebug<int>((long)this.GetHashCode(), "Rebuild of Non-Partition-scoped ConnectionPoolManager is complete, new instance {0}", connectionPoolManager2.GetHashCode());
				}
			}
			if (flag && this.partitionFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				int num = Interlocked.CompareExchange(ref this.taskCountOfReissueNotification, 1, 0);
				if (num < 1)
				{
					Task.Factory.StartNew(delegate()
					{
						try
						{
							ADNotificationListener.ReissueNotificationRequests(true, configDcChanged);
						}
						catch (Exception ex)
						{
							Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ISSUE_NOTIFICATION_FAILURE, "RebuildNonPartitionScopedConnectionPoolManager", new object[]
							{
								ex.ToString()
							});
						}
						finally
						{
							this.taskCountOfReissueNotification = 0;
						}
					});
				}
			}
			return true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00031858 File Offset: 0x0002FA58
		private bool RebuildPartitionScopedConnectionPoolManager(bool force, List<PooledLdapConnection> connectionsToRelease)
		{
			bool flag = false;
			SyncWrapper<ConnectionPoolManager> syncWrapper;
			if (!ConnectionPoolManager.partitionInstances.TryGetValue(this.partitionFqdn, out syncWrapper))
			{
				return true;
			}
			bool result;
			lock (syncWrapper)
			{
				ConnectionPoolManager value = syncWrapper.Value;
				bool flag3 = this == value;
				if (flag3)
				{
					ConnectionPoolManager connectionPoolManager;
					if (!this.TryCheckTopoVersionAndDiscover(force, out flag, out connectionPoolManager, connectionsToRelease))
					{
						return false;
					}
					syncWrapper.Value = connectionPoolManager;
					ExTraceGlobals.ADTopologyTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Rebuild of Partition-scoped ConnectionPoolManager is complete, new instance {0}, partition {1}", connectionPoolManager.GetHashCode(), this.partitionFqdn);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00031900 File Offset: 0x0002FB00
		private bool TryCheckTopoVersionAndDiscover(bool force, out bool configDcChanged, out ConnectionPoolManager newInstance, List<PooledLdapConnection> connectionsToRelease)
		{
			newInstance = null;
			ConnectionPoolManager connectionPoolManager = new ConnectionPoolManager(this.credential, this.partitionFqdn);
			bool flag = this.CheckTopologyVersionForRebuild();
			if (!connectionPoolManager.DiscoverTopology(this, !force && !flag, out configDcChanged, connectionsToRelease, this.partitionFqdn))
			{
				ExTraceGlobals.ADTopologyTracer.TraceError((long)this.GetHashCode(), "Rebuild of ConnectionPoolManager cannot be performed: available topology is the same as the current one");
				return false;
			}
			newInstance = connectionPoolManager;
			return true;
		}

		// Token: 0x04000424 RID: 1060
		private const int MaxTopoRebuildRetries = 5;

		// Token: 0x04000425 RID: 1061
		private const int TopologyInitializationTimeoutMsec = 60000;

		// Token: 0x04000426 RID: 1062
		private const int MaxPoolCount = 5;

		// Token: 0x04000427 RID: 1063
		private const int MaxPartitionPoolCount = 100;

		// Token: 0x04000428 RID: 1064
		private const int LongRunningOperationThresholdSecondsDefault = 15;

		// Token: 0x04000429 RID: 1065
		private const string LongRunningOperationThresholdValueName = "LongRunningOperationThresholdSeconds";

		// Token: 0x0400042A RID: 1066
		private const string ClientSideSearchTimeoutName = "ClientSideSearchTimeoutSeconds";

		// Token: 0x0400042B RID: 1067
		private const int ClientSideSearchTimeoutDefaultSeconds = 180;

		// Token: 0x0400042C RID: 1068
		private const string NotificationClientSideSearchTimeoutName = "NotificationClientSideSearchTimeoutSeconds";

		// Token: 0x0400042D RID: 1069
		private const int NotificationClientSideSearchTimeoutDefaultSeconds = 30;

		// Token: 0x0400042E RID: 1070
		private const string CallstackTraceRatioName = "CallstackTraceRatio";

		// Token: 0x0400042F RID: 1071
		private const int CallstackTraceRatioDefault = 0;

		// Token: 0x04000430 RID: 1072
		private const string ObjectsFromEntriesThresholdMillisecondsName = "ObjectsFromEntriesThresholdMilliseconds";

		// Token: 0x04000431 RID: 1073
		private const int ObjectsFromEntriesThresholdMillisecondsDefault = 100;

		// Token: 0x04000432 RID: 1074
		private const bool LdapEncryptionEnabledDefault = true;

		// Token: 0x04000433 RID: 1075
		private const string LdapEncryptionDisabledName = "Disable LDAP Encryption";

		// Token: 0x04000434 RID: 1076
		private const int LdapEncryptionDisabledDefault = 0;

		// Token: 0x04000435 RID: 1077
		private const uint ADTransientExceptionLid = 4087754045U;

		// Token: 0x04000436 RID: 1078
		private static int longRunningOperationThresholdSeconds = 15;

		// Token: 0x04000437 RID: 1079
		private static TimeSpan clientSideSearchTimeout = TimeSpan.FromSeconds(180.0);

		// Token: 0x04000438 RID: 1080
		private static TimeSpan notificationClientSideSearchTimeout = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000439 RID: 1081
		private static int callstackTraceRatio = 0;

		// Token: 0x0400043A RID: 1082
		private static int objectsFromEntriesThresholdMilliseconds = 100;

		// Token: 0x0400043B RID: 1083
		private static string processUserSid = ConnectionPoolManager.GetProcessUserSid();

		// Token: 0x0400043C RID: 1084
		private static List<ConnectionPoolManager> staticInstances = new List<ConnectionPoolManager>(0);

		// Token: 0x0400043D RID: 1085
		private static ConcurrentDictionary<string, SyncWrapper<ConnectionPoolManager>> partitionInstances = new ConcurrentDictionary<string, SyncWrapper<ConnectionPoolManager>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400043E RID: 1086
		private static object instanceLockRoot = new object();

		// Token: 0x0400043F RID: 1087
		private static int whenPartitionPoolsLastChecked;

		// Token: 0x04000440 RID: 1088
		private static readonly int PartitionPoolCleanupIntervalMsec = 60000;

		// Token: 0x04000441 RID: 1089
		private static readonly int PartitionPoolCleanupInactivityThresholdMsec = 600000;

		// Token: 0x04000442 RID: 1090
		private static readonly int DefaultPoolsRefreshIntervalMsec = 3600000;

		// Token: 0x04000443 RID: 1091
		private static readonly int DefaultPoolsRefreshIntervalVariable = 900000;

		// Token: 0x04000444 RID: 1092
		private readonly int poolsRefreshIntervalMsec;

		// Token: 0x04000445 RID: 1093
		private LdapConnectionPool[] connectionPools;

		// Token: 0x04000446 RID: 1094
		private int topologyVersion;

		// Token: 0x04000447 RID: 1095
		private int whenTopoVersionLastChecked;

		// Token: 0x04000448 RID: 1096
		private int whenPoolsRefreshed;

		// Token: 0x04000449 RID: 1097
		private NetworkCredential credential;

		// Token: 0x0400044A RID: 1098
		private readonly string partitionFqdn;

		// Token: 0x0400044B RID: 1099
		private int taskCountOfReissueNotification;

		// Token: 0x0400044C RID: 1100
		private static bool ldapEncryptionEnabled = true;
	}
}
