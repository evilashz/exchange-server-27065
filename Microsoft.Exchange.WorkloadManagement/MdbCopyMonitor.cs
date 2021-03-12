using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.WorkloadManagement.EventLogs;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001E RID: 30
	internal class MdbCopyMonitor
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00005240 File Offset: 0x00003440
		protected MdbCopyMonitor()
		{
			this.timer = new GuardedTimer(delegate(object state)
			{
				this.ReadDataFromAD();
			});
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005288 File Offset: 0x00003488
		public static Hookable<MdbCopyMonitor> Instance
		{
			get
			{
				return MdbCopyMonitor.instance;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005290 File Offset: 0x00003490
		public virtual List<Guid> GetMdbsForServer(string serverFqdn)
		{
			this.Initialize();
			List<Guid> result;
			try
			{
				this.lockObject.EnterReadLock();
				List<Guid> list = null;
				if (this.mdbByServerFqdn != null)
				{
					this.mdbByServerFqdn.TryGetValue(serverFqdn, out list);
				}
				result = list;
			}
			finally
			{
				try
				{
					this.lockObject.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000052FC File Offset: 0x000034FC
		public virtual List<string> GetServersForMdb(Guid mdbGuid)
		{
			this.Initialize();
			List<string> result;
			try
			{
				this.lockObject.EnterReadLock();
				List<string> list = null;
				if (this.serverFqdnByMdb != null)
				{
					this.serverFqdnByMdb.TryGetValue(mdbGuid, out list);
				}
				result = list;
			}
			finally
			{
				try
				{
					this.lockObject.ExitReadLock();
				}
				catch (SynchronizationLockException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005368 File Offset: 0x00003568
		private static bool TryRunAdOperation(ADOperation adCall, out Exception exception)
		{
			exception = null;
			try
			{
				ADNotificationAdapter.RunADOperation(adCall);
			}
			catch (ADTransientException ex)
			{
				exception = ex;
				return false;
			}
			catch (ADOperationException ex2)
			{
				exception = ex2;
				return false;
			}
			return true;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000053B0 File Offset: 0x000035B0
		private void Initialize()
		{
			if (!this.isTimerInitialized)
			{
				lock (this.timer)
				{
					if (!this.isTimerInitialized)
					{
						this.isTimerInitialized = true;
						this.ResetTimer();
					}
				}
			}
			this.RegisterADChangeNotification();
			bool condition = this.initializationComplete.WaitOne(TimeSpan.FromMinutes(15.0));
			ExAssert.RetailAssert(condition, "Waiting for MdbCopyMonitor initialization timed out.");
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005434 File Offset: 0x00003634
		private void ResetTimer()
		{
			lock (this.timer)
			{
				this.timer.Change(CiHealthMonitorConfiguration.MdbCopyUpdateDelay, CiHealthMonitorConfiguration.MdbCopyUpdateInterval);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000054D4 File Offset: 0x000036D4
		private void RegisterADChangeNotification()
		{
			if (Interlocked.Exchange(ref this.isAdNotificationRegistered, 1) == 0)
			{
				Exception arg;
				if (!MdbCopyMonitor.TryRunAdOperation(delegate
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 251, "RegisterADChangeNotification", "f:\\15.00.1497\\sources\\dev\\WorkloadManagement\\src\\WorkloadManagement\\ResourceMonitors\\ContentIndexing\\MdbCopyMonitor.cs");
					ADObjectId databasesContainerId = topologyConfigurationSession.GetDatabasesContainerId();
					ADNotificationAdapter.RegisterChangeNotification<DatabaseCopy>(databasesContainerId, delegate(ADNotificationEventArgs args)
					{
						this.ResetTimer();
					});
				}, out arg))
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceError<Exception>((long)this.GetHashCode(), "[MdbCopyMonitor::RegisterADChangeNotification] Failed to sign up for AD notifications, exception: {0}", arg);
					Interlocked.Exchange(ref this.isAdNotificationRegistered, 0);
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005640 File Offset: 0x00003840
		private void ReadDataFromAD()
		{
			try
			{
				Dictionary<ADObjectId, string> serverFqdnCache = new Dictionary<ADObjectId, string>();
				MailboxDatabase[] mailboxDatabases = null;
				Exception ex;
				bool flag = MdbCopyMonitor.TryRunAdOperation(delegate
				{
					serverFqdnCache.Clear();
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 288, "ReadDataFromAD", "f:\\15.00.1497\\sources\\dev\\WorkloadManagement\\src\\WorkloadManagement\\ResourceMonitors\\ContentIndexing\\MdbCopyMonitor.cs");
					Server localServer = LocalServerCache.LocalServer;
					if (localServer == null)
					{
						throw new LocalServerNotFoundException(string.Empty);
					}
					serverFqdnCache.Add(localServer.Id, localServer.Fqdn);
					mailboxDatabases = localServer.GetMailboxDatabases();
					foreach (MailboxDatabase mailboxDatabase2 in mailboxDatabases)
					{
						if (mailboxDatabase2.ActivationPreference != null)
						{
							foreach (KeyValuePair<ADObjectId, int> keyValuePair2 in mailboxDatabase2.ActivationPreference)
							{
								if (!serverFqdnCache.ContainsKey(keyValuePair2.Key))
								{
									Server server = topologyConfigurationSession.Read<Server>(keyValuePair2.Key);
									serverFqdnCache.Add(keyValuePair2.Key, server.Fqdn);
								}
							}
						}
					}
				}, out ex);
				if (flag)
				{
					Dictionary<string, List<Guid>> dictionary = new Dictionary<string, List<Guid>>(serverFqdnCache.Count);
					Dictionary<Guid, List<string>> dictionary2 = new Dictionary<Guid, List<string>>(mailboxDatabases.Length);
					foreach (MailboxDatabase mailboxDatabase in mailboxDatabases)
					{
						if (mailboxDatabase.ActivationPreference != null)
						{
							List<string> list = new List<string>(4);
							dictionary2.Add(mailboxDatabase.Guid, list);
							foreach (KeyValuePair<ADObjectId, int> keyValuePair in mailboxDatabase.ActivationPreference)
							{
								string text = serverFqdnCache[keyValuePair.Key];
								list.Add(text);
								List<Guid> list2;
								if (!dictionary.TryGetValue(text, out list2))
								{
									list2 = new List<Guid>(40);
									dictionary.Add(text, list2);
								}
								list2.Add(mailboxDatabase.Guid);
							}
						}
					}
					try
					{
						this.lockObject.EnterWriteLock();
						this.mdbByServerFqdn = dictionary;
						this.serverFqdnByMdb = dictionary2;
						goto IL_1AC;
					}
					finally
					{
						try
						{
							this.lockObject.ExitWriteLock();
						}
						catch (SynchronizationLockException)
						{
						}
					}
				}
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<Exception>((long)this.GetHashCode(), "[MdbCopyMonitor::ReadDataFromAD] Failed to read data from AD, exception: {0}", ex);
				WorkloadManagerEventLogger.LogEvent(WorkloadManagementEventLogConstants.Tuple_CiMdbCopyMonitorFailure, string.Empty, new object[]
				{
					ex.ToString()
				});
				try
				{
					this.lockObject.EnterWriteLock();
					this.mdbByServerFqdn = null;
					this.serverFqdnByMdb = null;
				}
				finally
				{
					try
					{
						this.lockObject.ExitWriteLock();
					}
					catch (SynchronizationLockException)
					{
					}
				}
				IL_1AC:;
			}
			finally
			{
				this.initializationComplete.Set();
			}
		}

		// Token: 0x0400007D RID: 125
		private const int DefaultCopiesPerMdb = 4;

		// Token: 0x0400007E RID: 126
		private const int DefaultMdbCopiesPerServer = 40;

		// Token: 0x0400007F RID: 127
		private static readonly Hookable<MdbCopyMonitor> instance = Hookable<MdbCopyMonitor>.Create(true, new MdbCopyMonitor());

		// Token: 0x04000080 RID: 128
		private readonly ManualResetEvent initializationComplete = new ManualResetEvent(false);

		// Token: 0x04000081 RID: 129
		private readonly ReaderWriterLockSlim lockObject = new ReaderWriterLockSlim();

		// Token: 0x04000082 RID: 130
		private readonly GuardedTimer timer;

		// Token: 0x04000083 RID: 131
		private Dictionary<string, List<Guid>> mdbByServerFqdn;

		// Token: 0x04000084 RID: 132
		private Dictionary<Guid, List<string>> serverFqdnByMdb;

		// Token: 0x04000085 RID: 133
		private int isAdNotificationRegistered;

		// Token: 0x04000086 RID: 134
		private bool isTimerInitialized;
	}
}
