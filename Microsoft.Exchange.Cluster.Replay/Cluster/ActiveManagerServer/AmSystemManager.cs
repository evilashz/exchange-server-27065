using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.DxStore.HA;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc.ActiveManager;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000094 RID: 148
	internal class AmSystemManager : ChangePoller
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		private AmSystemManager() : base(true)
		{
			this.Config = AmConfig.UnknownConfig;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001C208 File Offset: 0x0001A408
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AmSystemManagerTracer;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001C20F File Offset: 0x0001A40F
		internal static AmSystemManager Instance
		{
			get
			{
				return AmSystemManager.sm_instance;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001C216 File Offset: 0x0001A416
		internal static void TestResetDefaultInstance()
		{
			AmSystemManager.sm_instance = new AmSystemManager();
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001C222 File Offset: 0x0001A422
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001C22A File Offset: 0x0001A42A
		internal AmUnhandledExceptionHandler UnhandledExceptionHandler { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001C233 File Offset: 0x0001A433
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001C23B File Offset: 0x0001A43B
		internal AmDatabaseQueueManager DatabaseQueueManager { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001C244 File Offset: 0x0001A444
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001C24C File Offset: 0x0001A44C
		internal AmSystemEventQueue SystemEventQueue { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001C255 File Offset: 0x0001A455
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001C25D File Offset: 0x0001A45D
		internal AmConfigManager ConfigManager { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001C266 File Offset: 0x0001A466
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001C26E File Offset: 0x0001A46E
		internal AmClusterServiceMonitor ClusterServiceMonitor { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001C277 File Offset: 0x0001A477
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001C27F File Offset: 0x0001A47F
		internal AmStoreServiceMonitor StoreServiceMonitor { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001C288 File Offset: 0x0001A488
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0001C290 File Offset: 0x0001A490
		internal AmDbNodeAttemptTable DbNodeAttemptTable { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001C299 File Offset: 0x0001A499
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0001C2A1 File Offset: 0x0001A4A1
		internal AmStoreStateMarker StoreStateMarker { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001C2AA File Offset: 0x0001A4AA
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001C2B2 File Offset: 0x0001A4B2
		internal AmPeriodicEventManager PeriodicEventManager { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001C2BB File Offset: 0x0001A4BB
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001C2C3 File Offset: 0x0001A4C3
		internal AmClusterMonitor ClusterMonitor { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001C2D4 File Offset: 0x0001A4D4
		internal AmNetworkMonitor NetworkMonitor { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001C2DD File Offset: 0x0001A4DD
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001C2E5 File Offset: 0x0001A4E5
		internal AmServerNameCacheManager ServerNameCacheManager { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001C2EE File Offset: 0x0001A4EE
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001C2F6 File Offset: 0x0001A4F6
		internal AmDelayedConfigDisposer DelayedConfigDisposer { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001C307 File Offset: 0x0001A507
		internal AmPerfCounterUpdater AmPerfCounterUpdater { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001C310 File Offset: 0x0001A510
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001C318 File Offset: 0x0001A518
		internal AmServiceKillStatusContainer ServiceKillStatusContainer { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001C321 File Offset: 0x0001A521
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0001C329 File Offset: 0x0001A529
		internal AmLastKnownGoodConfig LastKnownGoodConfig { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001C332 File Offset: 0x0001A532
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0001C33A File Offset: 0x0001A53A
		internal AmTransientFailoverSuppressor TransientFailoverSuppressor { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001C343 File Offset: 0x0001A543
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001C34B File Offset: 0x0001A54B
		internal AmPamMonitor PamMonitor { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001C354 File Offset: 0x0001A554
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0001C35C File Offset: 0x0001A55C
		internal AmDatabaseStateTracker DatabaseStateTracker { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001C365 File Offset: 0x0001A565
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0001C36D File Offset: 0x0001A56D
		internal AmSystemFailoverOnReplayDownTracker SystemFailoverOnReplayDownTracker { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001C376 File Offset: 0x0001A576
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001C37E File Offset: 0x0001A57E
		internal AmCachedLastLogUpdater PamCachedLastLogUpdater { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001C387 File Offset: 0x0001A587
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001C38F File Offset: 0x0001A58F
		internal AmClusdbPeriodicCleanup ClusdbPeriodicCleanup { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001C398 File Offset: 0x0001A598
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		internal DataStorePeriodicChecker DataStorePeriodicChecker { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001C3A9 File Offset: 0x0001A5A9
		internal bool IsShutdown
		{
			get
			{
				return this.m_fShutdown;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001C3B1 File Offset: 0x0001A5B1
		internal EventWaitHandle ShutdownEvent
		{
			get
			{
				return this.m_shutdownEvent;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001C3B9 File Offset: 0x0001A5B9
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0001C3C1 File Offset: 0x0001A5C1
		internal ExDateTime? SystemShutdownStartTime { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		internal bool IsSystemShutdownInProgress
		{
			get
			{
				return this.SystemShutdownStartTime != null;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001C3E7 File Offset: 0x0001A5E7
		internal AmServerDbStatusInfoCache StatusInfoCache
		{
			get
			{
				return this.m_dbStatusInfoCache;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001C3EF File Offset: 0x0001A5EF
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x0001C3F7 File Offset: 0x0001A5F7
		internal AmConfig Config { get; private set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001C400 File Offset: 0x0001A600
		internal ManualOneShotEvent ConfigInitializedEvent
		{
			get
			{
				return this.m_configInitializedEvent;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001C408 File Offset: 0x0001A608
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001C410 File Offset: 0x0001A610
		private bool StoreKilledToForceDismount { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001C419 File Offset: 0x0001A619
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001C421 File Offset: 0x0001A621
		private bool DatabasesForceDismountedLocally { get; set; }

		// Token: 0x060005DD RID: 1501 RVA: 0x0001C42A File Offset: 0x0001A62A
		public override void PrepareToStop()
		{
			AmTrace.Entering("AmSystemManager.PrepareToStop()", new object[0]);
			base.PrepareToStop();
			if (this.SystemEventQueue != null)
			{
				this.SystemEventQueue.Stop();
			}
			AmTrace.Leaving("AmSystemManager.PrepareToStop()", new object[0]);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001C468 File Offset: 0x0001A668
		internal bool EnqueueEvent(AmEvtBase evt, bool isHighPriority)
		{
			bool flag = false;
			lock (this.m_locker)
			{
				if (!this.m_fShutdown && this.SystemEventQueue != null)
				{
					flag = this.SystemEventQueue.Enqueue(evt, isHighPriority);
					if (flag)
					{
						bool flag3 = !AmPeriodicEventManager.IsPeriodicEvent(evt) || RegistryParameters.AmEnableCrimsonLoggingPeriodicEventProcessing;
						if (flag3)
						{
							ReplayCrimsonEvents.AmSystemEventEnqueued.Log<AmEvtBase>(evt);
						}
					}
					this.SystemEventQueue.ArrivalEvent.Set();
				}
			}
			return flag;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		internal void ProcessEvents()
		{
			while (!this.m_fShutdown)
			{
				this.SystemEventQueue.ArrivalEvent.WaitOne(RegistryParameters.AmSystemManagerEventWaitTimeoutInMSec, false);
				if (this.m_fShutdown)
				{
					return;
				}
				while (!this.m_fShutdown)
				{
					AmEvtBase amEvtBase = this.SystemEventQueue.Dequeue();
					if (amEvtBase == null)
					{
						break;
					}
					AmFaultInject.SleepIfRequired(AmSleepTag.GenericSystemEventProcessingDelay);
					this.InvokeHandler(amEvtBase);
				}
				if (this.PeriodicEventManager != null)
				{
					this.PeriodicEventManager.EnqueuePeriodicEventIfRequired();
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001C566 File Offset: 0x0001A766
		internal void StartClusterServiceMonitor()
		{
			if (this.ClusterServiceMonitor == null)
			{
				this.ClusterServiceMonitor = new AmClusterServiceMonitor();
				this.ClusterServiceMonitor.Start();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C586 File Offset: 0x0001A786
		internal void UpdateAmPerfCounterUpdaterDatabasesList(List<Guid> allDatabases)
		{
			if (this.AmPerfCounterUpdater != null)
			{
				this.AmPerfCounterUpdater.Update(allDatabases);
				return;
			}
			AmTrace.Diagnostic("UpdateAmPerfCounterUpdaterDatabasesList() was called, but AmPerfCounterUpdater is null.", new object[0]);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C5E4 File Offset: 0x0001A7E4
		protected override void PollerThread()
		{
			AmTrace.Entering("AmSystemManager.PollerThread", new object[0]);
			this.OneTimeInitialize();
			while (!this.m_fShutdown)
			{
				Exception ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					try
					{
						this.Initialize();
						this.ProcessEvents();
					}
					finally
					{
						this.Cleanup();
					}
				});
				if (ex != null)
				{
					AmTrace.Diagnostic("System manager encountered an exception while trying to process events: {0}", new object[]
					{
						ex
					});
				}
				if (!this.m_fShutdown)
				{
					Thread.Sleep(5000);
				}
			}
			this.OneTimeCleanup();
			AmTrace.Leaving("AmSystemManager.PollerThread", new object[0]);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001C66C File Offset: 0x0001A86C
		private void OneTimeInitialize()
		{
			AmTrace.Entering("AmSystemManager.OneTimeInitialize", new object[0]);
			lock (this.m_locker)
			{
				if (this.DelayedConfigDisposer == null)
				{
					this.DelayedConfigDisposer = new AmDelayedConfigDisposer();
					this.DelayedConfigDisposer.Start();
				}
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		private void OneTimeCleanup()
		{
			AmTrace.Entering("AmSystemManager.OneTimeCleanup", new object[0]);
			lock (this.m_locker)
			{
				if (this.DelayedConfigDisposer != null)
				{
					this.DelayedConfigDisposer.Stop();
					this.DelayedConfigDisposer = null;
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C738 File Offset: 0x0001A938
		public static void SetEnabledSubComponentFlags(EnableSubComponentFlag flag)
		{
			AmSystemManager.enabledSubComponentFlags = flag;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C740 File Offset: 0x0001A940
		private static bool IsSubComponentEnabled(EnableSubComponentFlag componentFlag)
		{
			return AmSystemManager.enabledSubComponentFlags.HasFlag(componentFlag);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C758 File Offset: 0x0001A958
		private void Initialize()
		{
			AmTrace.Entering("AmSystemManager.Initialize", new object[0]);
			lock (this.m_locker)
			{
				if (this.UnhandledExceptionHandler == null)
				{
					this.UnhandledExceptionHandler = new AmUnhandledExceptionHandler();
				}
				if (this.LastKnownGoodConfig == null)
				{
					this.LastKnownGoodConfig = AmLastKnownGoodConfig.ConstructLastKnownGoodConfigFromPersistentState();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.TransientFailoverSuppressor) && this.TransientFailoverSuppressor == null)
				{
					this.TransientFailoverSuppressor = new AmTransientFailoverSuppressor();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.ServiceKillStatusContainer) && this.ServiceKillStatusContainer == null)
				{
					this.ServiceKillStatusContainer = new AmServiceKillStatusContainer();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.ServerNameCacheManager) && this.ServerNameCacheManager == null)
				{
					this.ServerNameCacheManager = new AmServerNameCacheManager();
					this.ServerNameCacheManager.Start();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.DbNodeAttemptTable) && this.DbNodeAttemptTable == null)
				{
					this.DbNodeAttemptTable = new AmDbNodeAttemptTable();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.SystemEventQueue) && this.SystemEventQueue == null)
				{
					this.SystemEventQueue = new AmSystemEventQueue();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.DatabaseQueueManager) && this.DatabaseQueueManager == null)
				{
					this.DatabaseQueueManager = new AmDatabaseQueueManager();
				}
				if (this.ConfigManager == null)
				{
					this.ConfigManager = new AmConfigManager(Dependencies.ReplayAdObjectLookup);
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.PamCachedLastLogUpdater) && this.PamCachedLastLogUpdater == null)
				{
					this.PamCachedLastLogUpdater = new AmCachedLastLogUpdater();
					this.PamCachedLastLogUpdater.Start();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.StoreStateMarker) && this.StoreStateMarker == null)
				{
					this.StoreStateMarker = new AmStoreStateMarker();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.PeriodicEventManager) && this.PeriodicEventManager == null)
				{
					this.PeriodicEventManager = new AmPeriodicEventManager();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.ClusterMonitor) && this.ClusterMonitor == null)
				{
					this.ClusterMonitor = new AmClusterMonitor();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.NetworkMonitor) && this.NetworkMonitor == null)
				{
					this.NetworkMonitor = new AmNetworkMonitor();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.AmPerfCounterUpdater) && this.AmPerfCounterUpdater == null)
				{
					this.AmPerfCounterUpdater = new AmPerfCounterUpdater();
					this.AmPerfCounterUpdater.Start();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.ClusdbPeriodicCleanup) && this.ClusdbPeriodicCleanup == null && RegistryParameters.ClusdbPeriodicCleanupIntervalInSecs > 0)
				{
					this.ClusdbPeriodicCleanup = new AmClusdbPeriodicCleanup();
					this.ClusdbPeriodicCleanup.Start();
				}
				if (AmSystemManager.IsSubComponentEnabled(EnableSubComponentFlag.DataStorePeriodicChecker) && this.DataStorePeriodicChecker == null && !RegistryParameters.DistributedStoreDisableDualClientMode)
				{
					this.DataStorePeriodicChecker = new DataStorePeriodicChecker();
					this.DataStorePeriodicChecker.Start();
				}
			}
			this.ConfigManager.Start();
			AmTrace.Leaving("AmSystemManager.Initialize", new object[0]);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		private void Cleanup()
		{
			AmTrace.Entering("AmSystemManager.Cleanup", new object[0]);
			if (this.m_fShutdown)
			{
				AmTrace.Debug("Setting system manager config to unknown since the system manager is shutting down", new object[0]);
				this.Config = new AmConfig(ReplayStrings.AmServiceShuttingDown);
			}
			else
			{
				AmTrace.Debug("Setting system manager config to unknown since System manager event processing has abnormally terminated.", new object[0]);
				this.Config = new AmConfig();
			}
			if (this.ConfigManager != null)
			{
				this.ConfigManager.Stop();
			}
			if (this.PamMonitor != null)
			{
				this.PamMonitor.Stop();
			}
			if (this.ServerNameCacheManager != null)
			{
				this.ServerNameCacheManager.Stop();
			}
			if (this.PeriodicEventManager != null)
			{
				this.PeriodicEventManager.Stop();
			}
			if (this.SystemEventQueue != null)
			{
				this.SystemEventQueue.Stop();
			}
			if (this.DatabaseQueueManager != null)
			{
				if (this.m_fShutdown)
				{
					TimeSpan timeSpan = TimeSpan.FromSeconds((double)RegistryParameters.DbQueueMgrStopLimitInSecs);
					try
					{
						InvokeWithTimeout.Invoke(delegate()
						{
							this.DatabaseQueueManager.Stop();
						}, timeSpan);
						goto IL_119;
					}
					catch (TimeoutException)
					{
						AmTrace.Diagnostic(string.Format("DatabaseQueueManager hit timeout ({0}) during service stop.", timeSpan), new object[0]);
						goto IL_119;
					}
				}
				this.DatabaseQueueManager.Stop();
			}
			IL_119:
			if (this.ClusterServiceMonitor != null)
			{
				this.ClusterServiceMonitor.Stop();
			}
			if (this.StoreServiceMonitor != null)
			{
				this.StoreServiceMonitor.Stop();
			}
			if (this.AmPerfCounterUpdater != null)
			{
				this.AmPerfCounterUpdater.Stop();
			}
			if (this.DbNodeAttemptTable != null)
			{
				this.DbNodeAttemptTable.ClearFailedTime();
			}
			if (this.StoreStateMarker != null)
			{
				this.StoreStateMarker.Clear();
			}
			if (this.ServiceKillStatusContainer != null)
			{
				this.ServiceKillStatusContainer.Cleanup();
			}
			if (this.TransientFailoverSuppressor != null)
			{
				this.TransientFailoverSuppressor.RemoveAllEntries(false);
			}
			if (this.DataStorePeriodicChecker != null)
			{
				this.DataStorePeriodicChecker.Stop();
			}
			if (this.UnhandledExceptionHandler != null)
			{
				this.UnhandledExceptionHandler.Cleanup();
			}
			if (this.ConfigInitializedEvent != null)
			{
				this.ConfigInitializedEvent.Close();
			}
			if (this.DatabaseStateTracker != null)
			{
				this.DatabaseStateTracker.Cleanup();
			}
			if (this.SystemFailoverOnReplayDownTracker != null)
			{
				this.SystemFailoverOnReplayDownTracker.Cleanup();
			}
			if (this.PamCachedLastLogUpdater != null)
			{
				this.PamCachedLastLogUpdater.Stop();
			}
			if (this.ClusdbPeriodicCleanup != null)
			{
				this.ClusdbPeriodicCleanup.Stop();
			}
			lock (this.m_locker)
			{
				this.SystemEventQueue = null;
				this.DatabaseQueueManager = null;
				this.ConfigManager = null;
				this.ClusterServiceMonitor = null;
				this.StoreServiceMonitor = null;
				this.DbNodeAttemptTable = null;
				this.PeriodicEventManager = null;
				this.StoreStateMarker = null;
				this.AmPerfCounterUpdater = null;
				this.ServiceKillStatusContainer = null;
				this.ClusdbPeriodicCleanup = null;
				this.DataStorePeriodicChecker = null;
			}
			AmTrace.Leaving("AmSystemManager.Cleanup", new object[0]);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		private void AssertTimerCallback(object obj)
		{
			string text = obj.ToString();
			ReplayCrimsonEvents.AmSystemEventProcessingIsTakingTooLong.Log<string, int>(text, RegistryParameters.AmSystemEventAssertOnHangTimeoutInMSec);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001CD10 File Offset: 0x0001AF10
		private void InvokeHandler(AmEvtBase evt)
		{
			bool isSuccess = false;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Exception ex = null;
			string text = evt.ToString();
			bool flag = !AmPeriodicEventManager.IsPeriodicEvent(evt) || RegistryParameters.AmEnableCrimsonLoggingPeriodicEventProcessing;
			if (flag)
			{
				ReplayCrimsonEvents.AmSystemEventProcessingStarted.Log<string>(text);
			}
			Timer timer = new Timer(new TimerCallback(this.AssertTimerCallback), evt, RegistryParameters.AmSystemEventAssertOnHangTimeoutInMSec, -1);
			try
			{
				ex = AmHelper.HandleKnownExceptions(delegate(object param0, EventArgs param1)
				{
					this.InvokeHandlerInternal(evt);
					isSuccess = true;
				});
			}
			finally
			{
				timer.Change(-1, -1);
				timer.Dispose();
				stopwatch.Stop();
				if (isSuccess)
				{
					if (flag)
					{
						ReplayCrimsonEvents.AmSystemEventProcessingFinished.Log<string, TimeSpan>(text, stopwatch.Elapsed);
					}
				}
				else
				{
					bool flag2 = ex == null;
					if (flag || flag2)
					{
						ReplayCrimsonEvents.AmSystemEventProcessingFinishedWithException.Log<string, TimeSpan, string>(text, stopwatch.Elapsed, flag2 ? "<unhandled exception>" : ex.Message);
					}
				}
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001CE34 File Offset: 0x0001B034
		private bool IsEventPeriodic(Type eventType)
		{
			return eventType == typeof(AmEvtPeriodicDbStateAnalyze) || eventType == typeof(AmEvtPeriodicDbStateRestore) || eventType == typeof(AmEvtPeriodicCheckMismountedDatabase);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001CEC8 File Offset: 0x0001B0C8
		private void RefreshADConfigAsync(Type evtType)
		{
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				try
				{
					Dependencies.ADConfig.Refresh(string.Format("AmSystemManager evt={0}", evtType));
				}
				catch (Exception arg)
				{
					AmSystemManager.Tracer.TraceError<Exception>(0L, "Dependencies.ADConfig.Refresh failed with {0}", arg);
				}
			});
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		private void InvokeHandlerInternal(AmEvtBase evt)
		{
			Type type = evt.GetType();
			if (!this.IsEventPeriodic(type))
			{
				this.RefreshADConfigAsync(type);
			}
			AmSystemManager.Tracer.TraceDebug<AmEvtBase>(0L, "Started processing event: {0}", evt);
			if (type == typeof(AmEvtConfigChanged))
			{
				this.OnEvtConfigChanged((AmEvtConfigChanged)evt);
			}
			else if (type == typeof(AmEvtNodeStateChanged))
			{
				this.OnEventNodeStateChanged((AmEvtNodeStateChanged)evt);
			}
			else if (type == typeof(AmEvtNodeDownForLongTime))
			{
				this.OnEventNodeDownForLongTime((AmEvtNodeDownForLongTime)evt);
			}
			else if (type == typeof(AmEvtNodeAdded))
			{
				this.OnEvtNodeAdded((AmEvtNodeAdded)evt);
			}
			else if (type == typeof(AmEvtNodeRemoved))
			{
				this.OnEvtNodeRemoved((AmEvtNodeRemoved)evt);
			}
			else if (type == typeof(AmEvtClusterStateChanged))
			{
				this.OnEvtClusterStateChanged((AmEvtClusterStateChanged)evt);
			}
			else if (type == typeof(AmEvtClussvcStopped))
			{
				this.OnEvtClussvcStopped((AmEvtClussvcStopped)evt);
			}
			else if (type == typeof(AmEvtSystemStartup))
			{
				this.OnEvtSystemStartup((AmEvtSystemStartup)evt);
			}
			else if (type == typeof(AmEvtStoreServiceStarted))
			{
				this.OnEvtStoreServiceStarted((AmEvtStoreServiceStarted)evt);
			}
			else if (type == typeof(AmEvtStoreServiceStopped))
			{
				this.OnEvtStoreServiceStopped((AmEvtStoreServiceStopped)evt);
			}
			else if (type == typeof(AmEvtPeriodicDbStateAnalyze))
			{
				this.OnEvtPeriodicDbStateAnalyze((AmEvtPeriodicDbStateAnalyze)evt);
			}
			else if (type == typeof(AmEvtPeriodicDbStateRestore))
			{
				this.OnEvtPeriodicDbStateRestore((AmEvtPeriodicDbStateRestore)evt);
			}
			else if (type == typeof(AmEvtPeriodicCheckMismountedDatabase))
			{
				this.OnEvtPeriodicCheckMismountedDatabase((AmEvtPeriodicCheckMismountedDatabase)evt);
			}
			else if (type == typeof(AmEvtSwitchoverOnShutdown))
			{
				this.OnEvtSwitchoverOnShutdown((AmEvtSwitchoverOnShutdown)evt);
			}
			else if (type == typeof(AmEvtMoveAllDatabasesOnAdminRequest))
			{
				this.OnEvtMoveAllDatabasesOnAdminRequest((AmEvtMoveAllDatabasesOnAdminRequest)evt);
			}
			else if (type == typeof(AmEvtMoveAllDatabasesOnComponentRequest))
			{
				this.OnEvtMoveAllDatabasesOnComponentRequest((AmEvtMoveAllDatabasesOnComponentRequest)evt);
			}
			else if (type == typeof(AmEvtRejoinNodeForDac))
			{
				this.OnEvtRejoinNodeForDac((AmEvtRejoinNodeForDac)evt);
			}
			else if (type == typeof(AmEvtMapiNetworkFailure))
			{
				this.OnEventMapiNetworkFailure((AmEvtMapiNetworkFailure)evt);
			}
			else if (type == typeof(AmEvtGroupOwnerButUnknown))
			{
				this.OnEventGroupOwnerButUnknown((AmEvtGroupOwnerButUnknown)evt);
			}
			else if (type == typeof(AmEvtSystemFailoverOnReplayDown))
			{
				this.OnEvtSystemFailoverOnReplayDown((AmEvtSystemFailoverOnReplayDown)evt);
			}
			else
			{
				AmTrace.Diagnostic("System manager event processing encountered an unknown event: {0}", new object[]
				{
					evt
				});
			}
			AmSystemManager.Tracer.TraceDebug<AmEvtBase>(0L, "Finished processing event: {0}", evt);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001D204 File Offset: 0x0001B404
		private void OnEvtConfigChanged(AmEvtConfigChanged evt)
		{
			AmTrace.Entering("AmSystemManager.OnEvtConfigChanged().", new object[0]);
			AmConfig previousConfig = evt.PreviousConfig;
			AmConfig newConfig = evt.NewConfig;
			if (newConfig != null && !newConfig.IsUnknown)
			{
				AmSystemManager.Tracer.TraceDebug(0L, "AmSystemManager.OnEvtConfigChanged(). Update LastKnownGoodConfig");
				this.LastKnownGoodConfig.Update(newConfig);
				AmSystemManager.Tracer.TraceDebug(0L, "AmSystemManager.OnEvtConfigChanged(). Update LastKnownGoodConfig - done");
			}
			this.Config = newConfig;
			this.ControlDatabaseStateTracker(newConfig);
			this.ControlSystemFailoverOnReplayDownTracker(newConfig, previousConfig);
			this.ConfigInitializedEvent.Set();
			AmSystemManager.Tracer.TraceDebug<AmRole, AmRole, AmConfigChangedFlags>(0L, "Config changed. (New role={0}, Prev role={1}, Change flags='{2}')", newConfig.Role, previousConfig.Role, evt.ChangeFlags);
			if (previousConfig.Role != newConfig.Role)
			{
				ReplayCrimsonEvents.RoleChanged.Log<AmRole, AmRole>(newConfig.Role, previousConfig.Role);
				ThirdPartyManager.Instance.AmRoleNotify(newConfig);
				if (this.PamCachedLastLogUpdater != null)
				{
					this.PamCachedLastLogUpdater.Cleanup();
				}
				if (newConfig.IsPAM)
				{
					if (this.TransientFailoverSuppressor != null)
					{
						this.TransientFailoverSuppressor.Initialize();
					}
				}
				else if (this.TransientFailoverSuppressor != null)
				{
					this.TransientFailoverSuppressor.RemoveAllEntries(false);
				}
			}
			if (evt.ChangeFlags == AmConfigChangedFlags.None || evt.ChangeFlags == AmConfigChangedFlags.LastError)
			{
				AmSystemManager.Tracer.TraceDebug<AmConfigChangedFlags, string>(0L, "Ignoring config change. (ChangeFlags={0}, LastError={1})", evt.ChangeFlags, newConfig.LastError);
			}
			else
			{
				if (newConfig.IsUnknown)
				{
					AmSystemManager.Tracer.TraceDebug(0L, "Detected Unknown role. Store service monitor will be stopped. It will be restarted when a valid AM role is discovered");
					this.StopStoreServiceMonitor();
					if (newConfig.IsUnknownTriggeredByADError)
					{
						AmSystemManager.Tracer.TraceDebug(0L, "Ensure we do not keep group ownership when in the Unknown role");
						this.TryToDisownGroup();
					}
					else
					{
						AmSystemManager.Tracer.TraceDebug(0L, "AmConfig is in unknown state, but it is not triggered by AD issue");
					}
				}
				else
				{
					this.m_checkDbWatch.Reset();
					this.m_checkDbWatch.Start();
					if (newConfig.IsDecidingAuthority)
					{
						if (newConfig.IsDebugOptionsEnabled())
						{
							ReplayCrimsonEvents.DebugOptionsEnabled.Log();
						}
						if (newConfig.IsPAM && previousConfig.IsSAM)
						{
							AmServerName currentPAM = previousConfig.DagConfig.CurrentPAM;
							if (!AmServerName.IsNullOrEmpty(currentPAM))
							{
								AmNodeState nodeState = newConfig.DagConfig.GetNodeState(currentPAM);
								if (nodeState == AmNodeState.Down)
								{
									AmEvtNodeStateChanged amEvtNodeStateChanged = new AmEvtNodeStateChanged(currentPAM, nodeState);
									amEvtNodeStateChanged.Notify();
								}
							}
						}
						AmEvtSystemStartup amEvtSystemStartup = new AmEvtSystemStartup();
						amEvtSystemStartup.Notify();
					}
					else if (newConfig.IsSAM)
					{
						AmSystemManager.Tracer.TraceDebug(0L, "Config change detected and check databases issued on a SAM");
						AmEvtPeriodicCheckMismountedDatabase amEvtPeriodicCheckMismountedDatabase = new AmEvtPeriodicCheckMismountedDatabase();
						amEvtPeriodicCheckMismountedDatabase.Notify();
					}
					if (newConfig.IsStandalone)
					{
						this.StopClusterServiceMonitor();
					}
					if (newConfig.IsPamOrSam)
					{
						this.StartClusterServiceMonitor();
					}
					this.StoreKilledToForceDismount = false;
					this.DatabasesForceDismountedLocally = false;
					this.StartStoreServiceMonitor();
				}
				this.ControlPamMonitor(newConfig);
			}
			AmTrace.Leaving("AmSystemManager.OnEvtConfigChanged().", new object[0]);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		private void ControlDatabaseStateTracker(AmConfig cfg)
		{
			AmDatabaseStateTracker databaseStateTracker = this.DatabaseStateTracker;
			this.DatabaseStateTracker = null;
			if (databaseStateTracker != null)
			{
				databaseStateTracker.Cleanup();
			}
			if (cfg.IsPAM && !RegistryParameters.DatabaseStateTrackerDisabled)
			{
				this.DatabaseStateTracker = new AmDatabaseStateTracker();
				this.DatabaseStateTracker.Initialize();
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		private void ControlSystemFailoverOnReplayDownTracker(AmConfig cfg, AmConfig prevCfg)
		{
			if (!RegistryParameters.OnReplDownFailoverEnabled)
			{
				return;
			}
			AmSystemFailoverOnReplayDownTracker amSystemFailoverOnReplayDownTracker = this.SystemFailoverOnReplayDownTracker;
			if (cfg.IsPAM)
			{
				if (amSystemFailoverOnReplayDownTracker == null || prevCfg == null || prevCfg.Role != AmRole.PAM)
				{
					amSystemFailoverOnReplayDownTracker = new AmSystemFailoverOnReplayDownTracker();
					amSystemFailoverOnReplayDownTracker.InitializeFromClusdb();
					this.SystemFailoverOnReplayDownTracker = amSystemFailoverOnReplayDownTracker;
					return;
				}
			}
			else if (amSystemFailoverOnReplayDownTracker != null)
			{
				this.SystemFailoverOnReplayDownTracker = null;
				amSystemFailoverOnReplayDownTracker.Cleanup();
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001D548 File Offset: 0x0001B748
		private void ControlPamMonitor(AmConfig cfg)
		{
			if (cfg.IsSAM)
			{
				if (this.PamMonitor == null)
				{
					this.PamMonitor = new AmPamMonitor();
					this.PamMonitor.Start();
					return;
				}
			}
			else if (this.PamMonitor != null)
			{
				this.PamMonitor.Stop();
				this.PamMonitor = null;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001D598 File Offset: 0x0001B798
		private void OnEventNodeStateChanged(AmEvtNodeStateChanged evt)
		{
			ReplayCrimsonEvents.NodeStateChangeDetected.Log<string, AmNodeState>(evt.NodeName.Fqdn, evt.State);
			if (this.Config.IsPAM)
			{
				if (evt.State == AmNodeState.Down)
				{
					if (this.Config.IsIgnoreServerDebugOptionEnabled(evt.NodeName))
					{
						ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption.Log<string, string, string>(evt.NodeName.NetbiosName, AmDebugOptions.IgnoreServerFromAutomaticActions.ToString(), "System failover request ignored");
						return;
					}
					AmSystemFailover amSystemFailover = new AmSystemFailover(evt.NodeName, AmDbActionReason.NodeDown, false, false);
					amSystemFailover.Run();
					return;
				}
				else if (AmClusterNode.IsNodeUp(evt.State))
				{
					if (AmSystemManager.Instance.DbNodeAttemptTable != null)
					{
						AmSystemManager.Instance.DbNodeAttemptTable.ClearFailedTime(evt.NodeName);
					}
					if (AmSystemManager.Instance.TransientFailoverSuppressor != null)
					{
						AmSystemManager.Instance.TransientFailoverSuppressor.RemoveEntry(evt.NodeName, true, "NodeUp");
					}
				}
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001D680 File Offset: 0x0001B880
		private void OnEventNodeDownForLongTime(AmEvtNodeDownForLongTime evt)
		{
			if (this.Config.IsPAM)
			{
				AmSystemFailover amSystemFailover = new AmSystemFailover(evt.NodeName, AmDbActionReason.NodeDownConfirmed, true, false);
				amSystemFailover.Run();
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		private void OnEventMapiNetworkFailure(AmEvtMapiNetworkFailure evt)
		{
			AmSystemManager.Tracer.TraceDebug<AmServerName>(0L, "OnEventMapiNetworkFailure({0})", evt.NodeName);
			IAmCluster cluster = this.ClusterMonitor.Cluster;
			if (cluster == null)
			{
				AmSystemManager.Tracer.TraceError((long)this.GetHashCode(), "Ignoring event because AmClusterMonitor.Cluster is null");
				return;
			}
			if (this.NetworkMonitor.AreAnyMapiNicsUp(evt.NodeName))
			{
				AmSystemManager.Tracer.TraceDebug<AmServerName>(0L, "Ignoring event. NICs must now be up on {0}.", evt.NodeName);
				ReplayEventLogConstants.Tuple_AmIgnoredMapiNetFailureBecauseMapiLooksUp.LogEvent(evt.NodeName.NetbiosName, new object[]
				{
					evt.NodeName.NetbiosName
				});
				return;
			}
			if (evt.NodeName.IsLocalComputerName)
			{
				this.HandleLocalMapiNetworkFailure(cluster, evt);
				return;
			}
			this.HandleRemoteMapiNetworkFailure(cluster, evt);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001D770 File Offset: 0x0001B970
		private void HandleLocalMapiNetworkFailure(IAmCluster cluster, AmEvtMapiNetworkFailure evt)
		{
			bool flag = false;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1585, "HandleLocalMapiNetworkFailure", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\ActiveManager\\AmSystemManager.cs");
				topologyConfigurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds(5.0));
				topologyConfigurationSession.FindServerByName(Environment.MachineName);
				flag = true;
			}
			catch (ADTransientException arg)
			{
				AmSystemManager.Tracer.TraceError<ADTransientException>(0L, "HandleLocalMapiNetworkFailure got AD error: {0}", arg);
			}
			Exception ex;
			if (flag)
			{
				AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = new AmClusterNodeNetworkStatus();
				if (!this.NetworkMonitor.AreAnyMapiNicsUp(evt.NodeName))
				{
					amClusterNodeNetworkStatus.ClusterErrorOverride = true;
					ReplayCrimsonEvents.AmMapiAccessExpectedByAD.Log();
				}
				ex = AmClusterNodeStatusAccessor.Write(cluster, evt.NodeName, amClusterNodeNetworkStatus);
				if (ex != null)
				{
					ReplayCrimsonEvents.AmNodeStatusUpdateFailed.Log<string, string>(amClusterNodeNetworkStatus.ToString(), ex.Message);
				}
				ReplayEventLogConstants.Tuple_AmIgnoredMapiNetFailureBecauseADIsWorking.LogEvent(evt.NodeName.NetbiosName, new object[]
				{
					evt.NodeName.NetbiosName
				});
				return;
			}
			AmClusterNodeNetworkStatus amClusterNodeNetworkStatus2 = new AmClusterNodeNetworkStatus();
			amClusterNodeNetworkStatus2.HasADAccess = false;
			ex = AmClusterNodeStatusAccessor.Write(cluster, evt.NodeName, amClusterNodeNetworkStatus2);
			if (ex != null)
			{
				ReplayCrimsonEvents.AmNodeStatusUpdateFailed.Log<string, string>(amClusterNodeNetworkStatus2.ToString(), ex.Message);
			}
			ReplayEventLogConstants.Tuple_AMDetectedMapiNetworkFailure.LogEvent(evt.NodeName.NetbiosName, new object[]
			{
				evt.NodeName.NetbiosName
			});
			this.StopStoreServiceMonitor();
			this.KillStoreToForceDismount();
			this.TryToDisownGroup();
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001D904 File Offset: 0x0001BB04
		private void HandleRemoteMapiNetworkFailure(IAmCluster cluster, AmEvtMapiNetworkFailure evt)
		{
			if (!this.Config.IsPAM)
			{
				ReplayEventLogConstants.Tuple_AmIgnoredMapiNetFailureBecauseNotThePam.LogEvent(evt.NodeName.NetbiosName, new object[]
				{
					evt.NodeName.NetbiosName,
					AmServerName.LocalComputerName.NetbiosName
				});
				return;
			}
			Exception ex;
			AmNodeState nodeState = cluster.GetNodeState(evt.NodeName, out ex);
			if (ex != null)
			{
				AmSystemManager.Tracer.TraceError<Exception>(0L, "OnEventMapiNetworkFailure:GetNodeState fails:{0}", ex);
				return;
			}
			if (!AmClusterNode.IsNodeUp(nodeState))
			{
				ReplayEventLogConstants.Tuple_AmIgnoredMapiNetFailureBecauseNodeNotUp.LogEvent(evt.NodeName.NetbiosName, new object[]
				{
					evt.NodeName.NetbiosName
				});
				return;
			}
			TimeSpan value = new TimeSpan(0, 0, 5);
			DateTime t = DateTime.UtcNow.Add(value);
			AmClusterNodeNetworkStatus amClusterNodeNetworkStatus;
			for (;;)
			{
				amClusterNodeNetworkStatus = AmClusterNodeStatusAccessor.Read(cluster, evt.NodeName, out ex);
				if ((amClusterNodeNetworkStatus != null && !amClusterNodeNetworkStatus.IsHealthy) || DateTime.UtcNow > t)
				{
					break;
				}
				Thread.Sleep(1000);
			}
			if (amClusterNodeNetworkStatus != null && amClusterNodeNetworkStatus.IsHealthy && amClusterNodeNetworkStatus.ClusterErrorOverride)
			{
				ReplayEventLogConstants.Tuple_AmIgnoredMapiNetFailureBecauseADIsWorking.LogEvent(evt.NodeName.NetbiosName, new object[]
				{
					evt.NodeName.NetbiosName
				});
				return;
			}
			ReplayEventLogConstants.Tuple_AMDetectedMapiNetworkFailure.LogEvent(evt.NodeName.NetbiosName, new object[]
			{
				evt.NodeName.NetbiosName
			});
			AmSystemFailover amSystemFailover = new AmSystemFailover(evt.NodeName, AmDbActionReason.MapiNetFailure, false, false);
			amSystemFailover.Run();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001DA90 File Offset: 0x0001BC90
		private void OnEvtSystemStartup(AmEvtSystemStartup evt)
		{
			if (this.Config.IsDecidingAuthority)
			{
				AmStartupAutoMounter amStartupAutoMounter = new AmStartupAutoMounter();
				amStartupAutoMounter.Run();
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		private void OnEvtStoreServiceStarted(AmEvtStoreServiceStarted evt)
		{
			if (this.Config.IsDecidingAuthority)
			{
				if (this.Config.IsIgnoreServerDebugOptionEnabled(evt.NodeName))
				{
					ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption.Log<string, string, string>(evt.NodeName.NetbiosName, AmDebugOptions.IgnoreServerFromAutomaticActions.ToString(), "OnEvtStoreServiceStarted");
					return;
				}
				if (AmSystemManager.Instance.StoreStateMarker.CheckIfStoreStartMarkedAndClear(evt.NodeName))
				{
					AmStoreStartupAutoMounter amStoreStartupAutoMounter = new AmStoreStartupAutoMounter(evt.NodeName);
					amStoreStartupAutoMounter.Run();
					return;
				}
				AmTrace.Info("Ignoring automount request for node '{0}' since system startup mounter had already taken care of it", new object[]
				{
					evt.NodeName
				});
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001DB50 File Offset: 0x0001BD50
		private void OnEvtStoreServiceStopped(AmEvtStoreServiceStopped evt)
		{
			if (this.Config.IsDecidingAuthority)
			{
				if (this.Config.IsIgnoreServerDebugOptionEnabled(evt.NodeName))
				{
					ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption.Log<string, string, string>(evt.NodeName.NetbiosName, AmDebugOptions.IgnoreServerFromAutomaticActions.ToString(), "OnEvtStoreServiceStopped");
					return;
				}
				AmBatchMarkDismounted amBatchMarkDismounted = new AmBatchMarkDismounted(evt.NodeName, AmDbActionReason.StoreStopped);
				amBatchMarkDismounted.Run();
				if (!evt.IsGracefulStop && this.Config.IsPAM)
				{
					AmSystemFailover amSystemFailover = new AmSystemFailover(evt.NodeName, AmDbActionReason.StoreStopped, true, false);
					amSystemFailover.Run();
				}
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		private void OnEvtPeriodicDbStateAnalyze(AmEvtPeriodicDbStateAnalyze evt)
		{
			if (this.Config.IsDecidingAuthority)
			{
				AmPeriodicDatabaseStateAnalyzer amPeriodicDatabaseStateAnalyzer = new AmPeriodicDatabaseStateAnalyzer();
				amPeriodicDatabaseStateAnalyzer.Run();
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001DC08 File Offset: 0x0001BE08
		private void OnEvtPeriodicDbStateRestore(AmEvtPeriodicDbStateRestore evt)
		{
			if (this.Config.IsDecidingAuthority)
			{
				AmPeriodicDatabaseStateRestorer amPeriodicDatabaseStateRestorer = new AmPeriodicDatabaseStateRestorer(evt.Context);
				amPeriodicDatabaseStateRestorer.Run();
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001DC34 File Offset: 0x0001BE34
		private void OnEvtPeriodicCheckMismountedDatabase(AmEvtPeriodicCheckMismountedDatabase evt)
		{
			if (this.Config.IsSAM)
			{
				AmPeriodicSplitbrainChecker amPeriodicSplitbrainChecker = new AmPeriodicSplitbrainChecker();
				amPeriodicSplitbrainChecker.Run();
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		private void OnEvtSwitchoverOnShutdown(AmEvtSwitchoverOnShutdown evt)
		{
			if (this.Config.IsPAM)
			{
				if (this.Config.IsIgnoreServerDebugOptionEnabled(evt.NodeName))
				{
					ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption.Log<string, string, string>(evt.NodeName.NetbiosName, AmDebugOptions.IgnoreServerFromAutomaticActions.ToString(), "OnEvtSwitchoverOnShutdown");
					return;
				}
				new AmSystemSwitchover(evt.NodeName, AmDbActionReason.SystemShutdown)
				{
					CompletionCallback = new BatchOperationCompletedDelegate(evt.SwitchoverCompletedCallback),
					CustomStatus = AmDbActionStatus.AcllSuccessful
				}.Run();
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		private void OnEvtMoveAllDatabasesOnAdminRequest(AmEvtMoveAllDatabasesOnAdminRequest evt)
		{
			if (this.Config.IsPAM)
			{
				if (AmSystemManager.Instance.TransientFailoverSuppressor != null)
				{
					AmSystemManager.Instance.TransientFailoverSuppressor.AdminRequestedForRemoval(evt.NodeName, "Move-ActiveMailboxDatabase -Server");
				}
				new AmSystemSwitchoverOnAdminMove(evt.MoveArgs)
				{
					CompletionCallback = new BatchOperationCompletedDelegate(evt.SwitchoverCompletedCallback)
				}.Run();
				return;
			}
			throw new AmInvalidConfiguration(this.Config.LastError);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001DD54 File Offset: 0x0001BF54
		private void OnEvtMoveAllDatabasesOnComponentRequest(AmEvtMoveAllDatabasesOnComponentRequest evt)
		{
			if (this.Config.IsPAM)
			{
				new AmSystemSwitchoverOnComponentMove(evt.MoveArgs)
				{
					CompletionCallback = new BatchOperationCompletedDelegate(evt.SwitchoverCompletedCallback)
				}.Run();
				return;
			}
			throw new AmInvalidConfiguration(this.Config.LastError);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001DDA3 File Offset: 0x0001BFA3
		private void OnEvtClussvcStopped(AmEvtClussvcStopped evt)
		{
			this.InitiateDismountAllLocalDatabasesToAvoidSplitBrain("OnEvtClussvcStopped");
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		private void OnEvtClusterStateChanged(AmEvtClusterStateChanged evt)
		{
			this.InitiateDismountAllLocalDatabasesToAvoidSplitBrain("OnEvtClusterStateChanged");
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		private void InitiateDismountAllLocalDatabasesToAvoidSplitBrain(string hint)
		{
			bool flag = false;
			int num;
			int num2;
			if (!RegistryParameters.IsTransientFailoverSuppressionEnabled)
			{
				flag = true;
			}
			else if (!AmTransientFailoverSuppressor.CheckIfMajorityNodesReachable(out num, out num2))
			{
				flag = true;
			}
			else
			{
				ReplayCrimsonEvents.IgnoredImmediateDismountSinceMajorityReachable.Log<int, int>(num, num2);
			}
			if (flag)
			{
				if (this.DatabasesForceDismountedLocally)
				{
					AmSystemManager.Tracer.TraceError<string>(0L, "Force-dismount-DBs already issued once. Possible message storm? Event hint: {0}", hint);
					return;
				}
				this.DatabasesForceDismountedLocally = true;
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					this.DismountAllLocalDatabasesToAvoidSplitBrain(obj as string);
				}, hint);
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001DE40 File Offset: 0x0001C040
		private void DismountAllLocalDatabasesToAvoidSplitBrain(string hint)
		{
			if (this.Config.IsIgnoreServerDebugOptionEnabled(AmServerName.LocalComputerName))
			{
				ReplayCrimsonEvents.OperationNotPerformedDueToDebugOption.Log<string, string, string>(AmServerName.LocalComputerName.NetbiosName, AmDebugOptions.IgnoreServerFromAutomaticActions.ToString(), hint);
				return;
			}
			AmStoreHelper.DismountAll(hint);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001DE7C File Offset: 0x0001C07C
		private void KillStoreToForceDismount()
		{
			if (this.StoreKilledToForceDismount)
			{
				AmSystemManager.Tracer.TraceError(0L, "Store already killed. Possible message storm?");
				return;
			}
			this.StoreKilledToForceDismount = true;
			AmSystemManager.Tracer.TraceDebug(0L, "killing store");
			string text = "KillStoreToForceDismount";
			ReplayEventLogConstants.Tuple_AmKilledStoreToForceDismount.LogEvent(null, new object[0]);
			ReplayCrimsonEvents.ForceDismountingDatabases.Log<AmServerName, string>(AmServerName.LocalComputerName, text);
			Exception ex = ServiceOperations.KillService("MSExchangeIS", text);
			if (ex != null)
			{
				ReplayEventLogConstants.Tuple_AmFailedToStopService.LogEvent(string.Empty, new object[]
				{
					"MSExchangeIS",
					ex.ToString()
				});
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001DF1C File Offset: 0x0001C11C
		private void TryToDisownGroup()
		{
			IAmCluster amCluster = null;
			IAmClusterGroup amClusterGroup = null;
			try
			{
				amCluster = ClusterFactory.Instance.Open();
				amClusterGroup = amCluster.FindCoreClusterGroup();
				AmServerName ownerNode = amClusterGroup.OwnerNode;
				if (!ownerNode.IsLocalComputerName)
				{
					AmSystemManager.Tracer.TraceDebug<AmServerName>(0L, "TryToDisownGroup skipped because group owner is {0}", ownerNode);
				}
				else
				{
					ReplayCrimsonEvents.AmUnknownRoleButGroupOwner.Log();
					List<AmServerName> serversReportedAsPubliclyUp = AmSystemManager.Instance.NetworkMonitor.GetServersReportedAsPubliclyUp();
					TimeSpan timeout = new TimeSpan(0, 0, 30);
					foreach (AmServerName amServerName in serversReportedAsPubliclyUp)
					{
						if (!amServerName.IsLocalComputerName)
						{
							using (AmClusterNodeStatusAccessor amClusterNodeStatusAccessor = new AmClusterNodeStatusAccessor(amCluster, amServerName, DxStoreKeyAccessMode.Read))
							{
								AmClusterNodeNetworkStatus amClusterNodeNetworkStatus = amClusterNodeStatusAccessor.Read();
								if (amClusterNodeNetworkStatus == null || !amClusterNodeNetworkStatus.HasADAccess)
								{
									AmSystemManager.Tracer.TraceError<AmServerName>(0L, "Skipped move because {0} is reporting AD failed", amServerName);
									continue;
								}
							}
							try
							{
								AmSystemManager.Tracer.TraceDebug<AmServerName>(0L, "TryToDisownGroup: Trying to move ClusterGroup to {0}", amServerName);
								ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerMoveAttemptStart.Log<AmServerName>(amServerName);
								AmClusterGroup.MoveClusterGroupWithTimeout(AmServerName.LocalComputerName, amServerName, timeout);
								AmSystemManager.Tracer.TraceDebug<AmServerName>(0L, "PAM should now be on {0}", amServerName);
								ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerMoveSuccess.Log<AmServerName>(amServerName);
								return;
							}
							catch (ClusterException ex)
							{
								AmSystemManager.Tracer.TraceError<AmServerName, ClusterException>(0L, "TryToMoveGroup({0}) failed: {1}", amServerName, ex);
								ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerMoveFail.Log<AmServerName, string>(amServerName, ex.Message);
							}
						}
					}
					AmSystemManager.Tracer.TraceError(0L, "No new PAM was appointed");
					ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerNoOwnerChosen.Log();
				}
			}
			catch (SerializationException ex2)
			{
				AmSystemManager.Tracer.TraceError<SerializationException>(0L, "TryToMoveGroup failed: {0}", ex2);
				ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerClusterFail.Log<string>(ex2.ToString());
			}
			catch (ClusterException ex3)
			{
				AmSystemManager.Tracer.TraceError<ClusterException>(0L, "TryToMoveGroup failed: {0}", ex3);
				if (amCluster != null)
				{
					ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerClusterFail.Log<string>(ex3.Message);
				}
			}
			finally
			{
				if (amClusterGroup != null)
				{
					amClusterGroup.Dispose();
				}
				if (amCluster != null)
				{
					amCluster.Dispose();
				}
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		private void OnEventGroupOwnerButUnknown(AmEvtGroupOwnerButUnknown evt)
		{
			if (this.Config.IsPamOrSam)
			{
				AmRole role = this.Config.Role;
				AmSystemManager.Tracer.TraceDebug<AmRole>(0L, "TryToDisownGroup: no longer Unknown: {0}", role);
				ReplayCrimsonEvents.AmUnknownRoleButGroupOwnerNoLongerUnknown.Log<AmRole>(role);
				return;
			}
			this.TryToDisownGroup();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001E1F2 File Offset: 0x0001C3F2
		private void OnEvtNodeAdded(AmEvtNodeAdded evt)
		{
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001E204 File Offset: 0x0001C404
		private void OnEvtNodeRemoved(AmEvtNodeRemoved evt)
		{
			AmSystemManager.Instance.ConfigManager.TriggerRefresh(true);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001E218 File Offset: 0x0001C418
		private void OnEvtSystemFailoverOnReplayDown(AmEvtSystemFailoverOnReplayDown evt)
		{
			AmSystemFailover amSystemFailover = new AmSystemFailover(evt.ServerName, AmDbActionReason.ReplayDown, false, true);
			amSystemFailover.Run();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001E23C File Offset: 0x0001C43C
		private void OnEvtRejoinNodeForDac(AmEvtRejoinNodeForDac evt)
		{
			IADDatabaseAvailabilityGroup dag = evt.Dag;
			if (ClusterFactory.Instance.IsEvicted(AmServerName.LocalComputerName))
			{
				ReplayEventLogConstants.Tuple_NodeNotInCluster.LogEvent(AmServerName.LocalComputerName.NetbiosName, new object[]
				{
					AmServerName.LocalComputerName.NetbiosName,
					dag.Name
				});
				return;
			}
			if (!dag.StartedMailboxServers.Contains(AmServerName.LocalComputerName.Fqdn))
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2305, "OnEvtRejoinNodeForDac", "f:\\15.00.1497\\sources\\dev\\cluster\\src\\Replay\\ActiveManager\\AmSystemManager.cs");
				DatabaseAvailabilityGroup databaseAvailabilityGroup = tenantOrTopologyConfigurationSession.Read<DatabaseAvailabilityGroup>(dag.Id);
				MultiValuedProperty<string> startedMailboxServers = databaseAvailabilityGroup.StartedMailboxServers;
				if (!startedMailboxServers.Contains(AmServerName.LocalComputerName.Fqdn))
				{
					startedMailboxServers.Add(AmServerName.LocalComputerName.Fqdn);
					databaseAvailabilityGroup.StartedMailboxServers = startedMailboxServers;
					try
					{
						tenantOrTopologyConfigurationSession.Save(databaseAvailabilityGroup);
					}
					catch (ADExternalException arg)
					{
						AmSystemManager.Tracer.TraceDebug<ADExternalException>(0L, "Failed to update AD (error={0})", arg);
					}
					catch (ADTransientException arg2)
					{
						AmSystemManager.Tracer.TraceDebug<ADTransientException>(0L, "Failed to update AD (error={0})", arg2);
					}
				}
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001E364 File Offset: 0x0001C564
		private void StartStoreServiceMonitor()
		{
			if (this.StoreServiceMonitor == null)
			{
				this.StoreServiceMonitor = new AmStoreServiceMonitor();
				this.StoreServiceMonitor.Start();
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001E384 File Offset: 0x0001C584
		private void StopStoreServiceMonitor()
		{
			if (this.StoreServiceMonitor != null)
			{
				this.StoreServiceMonitor.Stop();
				this.StoreServiceMonitor = null;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
		private void StopClusterServiceMonitor()
		{
			if (this.ClusterServiceMonitor != null)
			{
				this.ClusterServiceMonitor.Stop();
				this.ClusterServiceMonitor = null;
			}
		}

		// Token: 0x04000261 RID: 609
		private static AmSystemManager sm_instance = new AmSystemManager();

		// Token: 0x04000262 RID: 610
		private static EnableSubComponentFlag enabledSubComponentFlags = EnableSubComponentFlag.All;

		// Token: 0x04000263 RID: 611
		private object m_locker = new object();

		// Token: 0x04000264 RID: 612
		private Stopwatch m_checkDbWatch = new Stopwatch();

		// Token: 0x04000265 RID: 613
		private AmServerDbStatusInfoCache m_dbStatusInfoCache = new AmServerDbStatusInfoCache();

		// Token: 0x04000266 RID: 614
		private ManualOneShotEvent m_configInitializedEvent = new ManualOneShotEvent("ConfigInitializedEventOccurred");
	}
}
