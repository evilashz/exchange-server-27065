using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000375 RID: 885
	internal static class RemoteDataProvider
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000A6BE9 File Offset: 0x000A4DE9
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoredDatabaseTracer;
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000A6BF0 File Offset: 0x000A4DF0
		public static TcpListener GetTcpListener()
		{
			return RemoteDataProvider.s_tcpListener;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000A6BF8 File Offset: 0x000A4DF8
		public static bool StartListening(bool useExchangeSid = true)
		{
			ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "Network TCP listener start listening.");
			bool result;
			lock (RemoteDataProvider.s_singletonLock)
			{
				if (RemoteDataProvider.s_initialized)
				{
					ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "StartListening returned because it is already initialized");
					result = RemoteDataProvider.s_initialized;
				}
				else if (ThirdPartyManager.IsInitialized && ThirdPartyManager.IsThirdPartyReplicationEnabled)
				{
					RemoteDataProvider.s_tprEnabled = true;
					ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "StartListening does nothing because TPR is enabled");
					result = true;
				}
				else
				{
					RemoteDataProvider.s_tprEnabled = false;
					if (RemoteDataProvider.s_selfCheckTimer == null)
					{
						RemoteDataProvider.s_selfCheckTimer = new Timer(new TimerCallback(RemoteDataProvider.SelfCheck), null, RegistryParameters.RemoteDataProviderSelfCheckInterval, RegistryParameters.RemoteDataProviderSelfCheckInterval);
					}
					try
					{
						if (useExchangeSid)
						{
							RemoteDataProvider.s_exchangeGroupSid = ObjectSecurity.ExchangeServersUsgSid;
						}
						if (RemoteDataProvider.s_tcpListener == null)
						{
							TcpListener.Config config = new TcpListener.Config();
							config.ListenPort = (int)NetworkManager.GetReplicationPort();
							config.LocalNodeName = Environment.MachineName;
							config.AuthConnectionHandOff = new TcpListener.AuthenticatedConnectionHandler(NetworkChannel.ServiceRequests);
							TcpListener tcpListener = new TcpListener();
							Exception ex = tcpListener.StartListening(config);
							if (ex != null)
							{
								ReplayEventLogConstants.Tuple_TcpListenerFailedToStart.LogEvent(null, new object[]
								{
									ex
								});
								ExTraceGlobals.MonitoredDatabaseTracer.TraceError<Exception>(0L, "Network TCP listener could not be started: {0}", ex);
								return false;
							}
							RemoteDataProvider.s_tcpListener = tcpListener;
						}
						ClusterBatchWriter.Start();
						RemoteDataProvider.s_initialized = true;
						ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "Network TCP listener successfully activated");
					}
					catch (ADTransientException ex2)
					{
						ReplayEventLogConstants.Tuple_TcpListenerFailedToStart.LogEvent(null, new object[]
						{
							ex2
						});
					}
					catch (ADExternalException ex3)
					{
						ReplayEventLogConstants.Tuple_TcpListenerFailedToStart.LogEvent(null, new object[]
						{
							ex3
						});
					}
					catch (ADOperationException ex4)
					{
						ReplayEventLogConstants.Tuple_TcpListenerFailedToStart.LogEvent(null, new object[]
						{
							ex4
						});
					}
					finally
					{
						if (!RemoteDataProvider.s_initialized)
						{
							ClusterBatchWriter.Stop();
						}
					}
					result = RemoteDataProvider.s_initialized;
				}
			}
			return result;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A6E5C File Offset: 0x000A505C
		public static void MonitorDatabases(List<string> databasesToStopMonitoring, List<string> databasesToRestartMonitoring, Dictionary<string, ReplayConfiguration> configurationsFound)
		{
			List<MonitoredDatabase> list = new List<MonitoredDatabase>(databasesToStopMonitoring.Count);
			lock (RemoteDataProvider.s_singletonLock)
			{
				if (RemoteDataProvider.s_tprEnabled)
				{
					return;
				}
				MonitoredDatabase[] allInstances = RemoteDataProvider.s_databases.GetAllInstances();
				foreach (MonitoredDatabase monitoredDatabase in allInstances)
				{
					if (databasesToStopMonitoring.Contains(monitoredDatabase.Identity))
					{
						ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug<string>(0L, "Config for MonitoredDatabase is gone: {0}", monitoredDatabase.Config.DatabaseName);
						SourceSeedTable.Instance.CancelSeedingIfAppropriate(SourceSeedTable.CancelReason.CopyRemoved, monitoredDatabase.DatabaseGuid);
						list.Add(monitoredDatabase);
						RemoteDataProvider.s_databases.RemoveInstance(monitoredDatabase);
					}
				}
				foreach (KeyValuePair<string, ReplayConfiguration> keyValuePair in configurationsFound)
				{
					ReplayConfiguration value = keyValuePair.Value;
					bool flag2 = value.Type == ReplayConfigType.RemoteCopySource;
					bool flag3 = flag2 || (value.Type == ReplayConfigType.RemoteCopyTarget && !value.ReplayState.Suspended);
					MonitoredDatabase monitoredDatabase2 = null;
					if (RemoteDataProvider.s_databases.TryGetInstance(value.Identity, out monitoredDatabase2))
					{
						if (flag3 && !databasesToRestartMonitoring.Contains(monitoredDatabase2.Identity))
						{
							RemoteDataProvider.Tracer.TraceDebug<string>((long)monitoredDatabase2.GetHashCode(), "Config not changed, or the associated RI was not restarted. Continuing to monitor: {0}", value.DatabaseName);
							continue;
						}
						RemoteDataProvider.Tracer.TraceDebug<string>((long)monitoredDatabase2.GetHashCode(), "Config for MonitoredDatabase is changing or a restart was requested: {0}", value.DatabaseName);
						SourceSeedTable.Instance.CancelSeedingIfAppropriate(SourceSeedTable.CancelReason.ConfigChanged, monitoredDatabase2.DatabaseGuid);
						list.Add(monitoredDatabase2);
					}
					if (!flag2)
					{
						RemoteDataProvider.Tracer.TraceDebug<string>(0L, "Not starting monitor for {0}", value.DatabaseName);
						if (monitoredDatabase2 != null)
						{
							RemoteDataProvider.s_databases.RemoveInstance(monitoredDatabase2);
						}
					}
					else
					{
						if (monitoredDatabase2 != null && !monitoredDatabase2.IsPassiveCopy)
						{
							monitoredDatabase2.StopMonitoring();
						}
						MonitoredDatabase monitoredDatabase3 = new MonitoredDatabase(value);
						if (monitoredDatabase3.StartMonitoring() == null)
						{
							if (monitoredDatabase2 != null)
							{
								RemoteDataProvider.s_databases.UpdateInstance(monitoredDatabase2, monitoredDatabase3);
							}
							else
							{
								RemoteDataProvider.s_databases.AddInstance(monitoredDatabase3);
							}
							RemoteDataProvider.Tracer.TraceDebug<string>((long)monitoredDatabase3.GetHashCode(), "Now monitoring: {0}", value.DatabaseName);
						}
						else
						{
							RemoteDataProvider.Tracer.TraceError<ReplayConfiguration>((long)monitoredDatabase3.GetHashCode(), "Unable to monitor remote requests for configuration {0}", value);
							if (monitoredDatabase2 != null)
							{
								RemoteDataProvider.s_databases.RemoveInstance(monitoredDatabase2);
							}
						}
					}
				}
			}
			foreach (MonitoredDatabase monitoredDatabase4 in list)
			{
				monitoredDatabase4.StopMonitoring();
			}
			list.Clear();
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A7154 File Offset: 0x000A5354
		public static void StopMonitoredDatabase(string dbGuidStr)
		{
			MonitoredDatabase monitoredDatabase = null;
			lock (RemoteDataProvider.s_singletonLock)
			{
				if (RemoteDataProvider.s_databases.TryGetInstance(dbGuidStr, out monitoredDatabase) && monitoredDatabase != null)
				{
					RemoteDataProvider.s_databases.RemoveInstance(monitoredDatabase);
				}
			}
			if (monitoredDatabase != null)
			{
				monitoredDatabase.StopMonitoring();
				return;
			}
			RemoteDataProvider.Tracer.TraceDebug<string>(0L, "StopMonitoredDatabase found no database for {0}", dbGuidStr);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000A71C8 File Offset: 0x000A53C8
		public static void StartMonitoredDatabase(ReplayConfiguration config)
		{
			lock (RemoteDataProvider.s_singletonLock)
			{
				MonitoredDatabase monitoredDatabase = null;
				if (RemoteDataProvider.s_databases.TryGetInstance(config.Identity, out monitoredDatabase))
				{
					throw new MonitoredDatabaseInitException(config.DatabaseName, "MonitoredDatabase should not have existed");
				}
				MonitoredDatabase monitoredDatabase2 = new MonitoredDatabase(config);
				MonitoredDatabaseInitException ex = monitoredDatabase2.StartMonitoring();
				if (ex != null)
				{
					throw ex;
				}
				RemoteDataProvider.s_databases.AddInstance(monitoredDatabase2);
				RemoteDataProvider.Tracer.TraceDebug<string>((long)monitoredDatabase2.GetHashCode(), "StartMonitoredDatabase has activated {0}", monitoredDatabase2.DatabaseName);
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000A7264 File Offset: 0x000A5464
		public static void StopMonitoring()
		{
			ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "StopMonitoring:starting");
			TcpListener tcpListener = null;
			lock (RemoteDataProvider.s_singletonLock)
			{
				RemoteDataProvider.s_initialized = false;
				tcpListener = RemoteDataProvider.s_tcpListener;
				RemoteDataProvider.s_tcpListener = null;
				ClusterBatchWriter.Stop();
			}
			if (tcpListener != null)
			{
				tcpListener.Stop();
			}
			if (tcpListener != null)
			{
				MonitoredDatabase[] allInstances = RemoteDataProvider.s_databases.GetAllInstances();
				foreach (MonitoredDatabase monitoredDatabase in allInstances)
				{
					RemoteDataProvider.s_databases.RemoveInstance(monitoredDatabase);
					monitoredDatabase.StopMonitoring();
				}
			}
			ExTraceGlobals.MonitoredDatabaseTracer.TraceDebug(0L, "StopMonitoring:complete");
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000A7320 File Offset: 0x000A5520
		public static void SelfCheck(object callbackContext)
		{
			if (!Monitor.TryEnter(RemoteDataProvider.s_selfCheckTimer))
			{
				ExTraceGlobals.RemoteDataProviderTracer.TraceError(0L, "SelfCheck is taking longer than its period.");
				return;
			}
			try
			{
				if (RemoteDataProvider.s_databases != null)
				{
					MonitoredDatabase[] allInstances = RemoteDataProvider.s_databases.GetAllInstances();
					foreach (MonitoredDatabase monitoredDatabase in allInstances)
					{
						monitoredDatabase.SelfCheck();
					}
				}
			}
			finally
			{
				Monitor.Exit(RemoteDataProvider.s_selfCheckTimer);
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000A7394 File Offset: 0x000A5594
		public static Guid? StringToGuid(string s)
		{
			try
			{
				return new Guid?(new Guid(s));
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return null;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000A73DC File Offset: 0x000A55DC
		public static bool GuidTryParse(string str, out Guid guid)
		{
			Guid? guid2 = RemoteDataProvider.StringToGuid(str);
			if (guid2 != null)
			{
				guid = guid2.Value;
				return true;
			}
			guid = Guid.NewGuid();
			return false;
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000A7414 File Offset: 0x000A5614
		public static MonitoredDatabase GetMonitoredDatabase(string dbGuid)
		{
			MonitoredDatabase result = null;
			if (!RemoteDataProvider.s_databases.TryGetInstance(dbGuid, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x000A7438 File Offset: 0x000A5638
		public static MonitoredDatabase GetMonitoredDatabase(Guid dbGuid)
		{
			string dbGuid2 = dbGuid.ToString();
			return RemoteDataProvider.GetMonitoredDatabase(dbGuid2);
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000A745C File Offset: 0x000A565C
		internal static bool AuthorizeRequest(WindowsIdentity wid)
		{
			List<SecurityIdentifier> list = new List<SecurityIdentifier>(2);
			if (RemoteDataProvider.s_exchangeGroupSid != null)
			{
				list.Add(RemoteDataProvider.s_exchangeGroupSid);
			}
			list.Add(RemoteDataProvider.s_localAdminsSid);
			IdentityReferenceCollection groups = wid.Groups;
			foreach (IdentityReference left in groups)
			{
				foreach (SecurityIdentifier right in list)
				{
					if (left == right)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000F39 RID: 3897
		private static volatile bool s_initialized = false;

		// Token: 0x04000F3A RID: 3898
		private static bool s_tprEnabled = false;

		// Token: 0x04000F3B RID: 3899
		private static TcpListener s_tcpListener = null;

		// Token: 0x04000F3C RID: 3900
		private static object s_singletonLock = new object();

		// Token: 0x04000F3D RID: 3901
		private static MonitoredDatabaseTable s_databases = new MonitoredDatabaseTable();

		// Token: 0x04000F3E RID: 3902
		private static SecurityIdentifier s_exchangeGroupSid = null;

		// Token: 0x04000F3F RID: 3903
		private static SecurityIdentifier s_localAdminsSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

		// Token: 0x04000F40 RID: 3904
		private static Timer s_selfCheckTimer;
	}
}
