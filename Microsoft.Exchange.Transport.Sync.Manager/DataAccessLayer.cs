using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DataAccessLayer
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600013E RID: 318 RVA: 0x000090CE File Offset: 0x000072CE
		// (remove) Token: 0x0600013F RID: 319 RVA: 0x000090D6 File Offset: 0x000072D6
		internal static event EventHandler<SubscriptionInformation> OnSubscriptionAdded
		{
			add
			{
				DataAccessLayer.OnSubscriptionAddedEvent += value;
			}
			remove
			{
				DataAccessLayer.OnSubscriptionAddedEvent -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000140 RID: 320 RVA: 0x000090DE File Offset: 0x000072DE
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x000090E6 File Offset: 0x000072E6
		internal static event EventHandler<SubscriptionInformation> OnSubscriptionRemoved
		{
			add
			{
				DataAccessLayer.OnSubscriptionRemovedEvent += value;
			}
			remove
			{
				DataAccessLayer.OnSubscriptionRemovedEvent -= value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000142 RID: 322 RVA: 0x000090EE File Offset: 0x000072EE
		// (remove) Token: 0x06000143 RID: 323 RVA: 0x000090F6 File Offset: 0x000072F6
		internal static event EventHandler<SubscriptionInformation> OnSubscriptionSyncNow
		{
			add
			{
				DataAccessLayer.OnSubscriptionSyncNowEvent += value;
			}
			remove
			{
				DataAccessLayer.OnSubscriptionSyncNowEvent -= value;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000144 RID: 324 RVA: 0x000090FE File Offset: 0x000072FE
		// (remove) Token: 0x06000145 RID: 325 RVA: 0x00009106 File Offset: 0x00007306
		internal static event EventHandler<SubscriptionInformation> OnWorkTypeBasedSyncNow
		{
			add
			{
				DataAccessLayer.OnWorkTypeBasedSyncNowEvent += value;
			}
			remove
			{
				DataAccessLayer.OnWorkTypeBasedSyncNowEvent -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000146 RID: 326 RVA: 0x0000910E File Offset: 0x0000730E
		// (remove) Token: 0x06000147 RID: 327 RVA: 0x00009116 File Offset: 0x00007316
		internal static event EventHandler<OnSyncCompletedEventArgs> OnSubscriptionSyncCompleted
		{
			add
			{
				DataAccessLayer.OnSubscriptionSyncCompletedEvent += value;
			}
			remove
			{
				DataAccessLayer.OnSubscriptionSyncCompletedEvent -= value;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000148 RID: 328 RVA: 0x0000911E File Offset: 0x0000731E
		// (remove) Token: 0x06000149 RID: 329 RVA: 0x00009126 File Offset: 0x00007326
		internal static event EventHandler<OnDatabaseDismountedEventArgs> OnDatabaseDismounted
		{
			add
			{
				DataAccessLayer.OnDatabaseDismountedEvent += value;
			}
			remove
			{
				DataAccessLayer.OnDatabaseDismountedEvent -= value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600014A RID: 330 RVA: 0x00009130 File Offset: 0x00007330
		// (remove) Token: 0x0600014B RID: 331 RVA: 0x00009164 File Offset: 0x00007364
		private static event EventHandler<SubscriptionInformation> OnSubscriptionAddedEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600014C RID: 332 RVA: 0x00009198 File Offset: 0x00007398
		// (remove) Token: 0x0600014D RID: 333 RVA: 0x000091CC File Offset: 0x000073CC
		private static event EventHandler<SubscriptionInformation> OnSubscriptionRemovedEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600014E RID: 334 RVA: 0x00009200 File Offset: 0x00007400
		// (remove) Token: 0x0600014F RID: 335 RVA: 0x00009234 File Offset: 0x00007434
		private static event EventHandler<SubscriptionInformation> OnSubscriptionSyncNowEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000150 RID: 336 RVA: 0x00009268 File Offset: 0x00007468
		// (remove) Token: 0x06000151 RID: 337 RVA: 0x0000929C File Offset: 0x0000749C
		private static event EventHandler<SubscriptionInformation> OnWorkTypeBasedSyncNowEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000152 RID: 338 RVA: 0x000092D0 File Offset: 0x000074D0
		// (remove) Token: 0x06000153 RID: 339 RVA: 0x00009304 File Offset: 0x00007504
		private static event EventHandler<OnSyncCompletedEventArgs> OnSubscriptionSyncCompletedEvent;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000154 RID: 340 RVA: 0x00009338 File Offset: 0x00007538
		// (remove) Token: 0x06000155 RID: 341 RVA: 0x0000936C File Offset: 0x0000756C
		private static event EventHandler<OnDatabaseDismountedEventArgs> OnDatabaseDismountedEvent;

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000939F File Offset: 0x0000759F
		internal static GlobalDatabaseHandler DatabaseHandler
		{
			get
			{
				return DataAccessLayer.databaseHandler;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000093A8 File Offset: 0x000075A8
		internal static bool Initialized
		{
			get
			{
				bool result;
				lock (DataAccessLayer.startupShutdownLock)
				{
					result = (DataAccessLayer.initialized && !DataAccessLayer.initOrShutdownPending);
				}
				return result;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000093F8 File Offset: 0x000075F8
		internal static bool Initializing
		{
			get
			{
				bool result;
				lock (DataAccessLayer.startupShutdownLock)
				{
					result = (!DataAccessLayer.initialized && DataAccessLayer.initOrShutdownPending);
				}
				return result;
			}
		}

		// Token: 0x17000084 RID: 132
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00009444 File Offset: 0x00007644
		internal static bool GenerateWatsonReports
		{
			set
			{
				DataAccessLayer.generateWatsonReports = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000944C File Offset: 0x0000764C
		internal static int DatabaseCount
		{
			get
			{
				return DataAccessLayer.databaseHandler.GetDatabaseCount();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009458 File Offset: 0x00007658
		internal static SyncQueueManager SyncQueueManager
		{
			get
			{
				return DataAccessLayer.dispatchManager.SyncQueueManager;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00009464 File Offset: 0x00007664
		internal static void ShutdownDispatcher()
		{
			DataAccessLayer.dispatchManager.StopActiveDispatching();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00009470 File Offset: 0x00007670
		internal static Guid[] GetDatabases()
		{
			GlobalDatabaseHandler globalDatabaseHandler = null;
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.Initialized)
				{
					globalDatabaseHandler = DataAccessLayer.databaseHandler;
				}
			}
			if (globalDatabaseHandler != null)
			{
				return globalDatabaseHandler.GetDatabases();
			}
			return null;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000094C4 File Offset: 0x000076C4
		internal static void ReportWatson(string message, Exception exception)
		{
			if (DataAccessLayer.generateWatsonReports)
			{
				ContentAggregationConfig.SyncLogSession.ReportWatson(message, exception);
				return;
			}
			string format = string.Format(CultureInfo.InvariantCulture, "Message: {0}. Exception: {1}.", new object[]
			{
				message,
				exception
			});
			ContentAggregationConfig.SyncLogSession.LogError((TSLID)171UL, format, new object[0]);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00009524 File Offset: 0x00007724
		internal static DatabaseManager GetDatabaseManager(Guid databaseGuid)
		{
			GlobalDatabaseHandler globalDatabaseHandler = null;
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.Initialized)
				{
					globalDatabaseHandler = DataAccessLayer.databaseHandler;
				}
			}
			if (globalDatabaseHandler != null)
			{
				return globalDatabaseHandler.GetDatabaseManager(databaseGuid);
			}
			return null;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00009578 File Offset: 0x00007778
		internal static void TriggerOnDatabaseDismountedEvent(Guid databaseGuid)
		{
			EventHandler<OnDatabaseDismountedEventArgs> onDatabaseDismountedEvent = DataAccessLayer.OnDatabaseDismountedEvent;
			if (onDatabaseDismountedEvent != null)
			{
				onDatabaseDismountedEvent(null, new OnDatabaseDismountedEventArgs(databaseGuid));
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000959C File Offset: 0x0000779C
		internal static void ScheduleMailboxCrawl(Guid databaseGuid, Guid mailboxGuid)
		{
			DatabaseManager databaseManager = DataAccessLayer.GetDatabaseManager(databaseGuid);
			if (databaseManager != null)
			{
				databaseManager.ScheduleMailboxCrawl(mailboxGuid);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000095BC File Offset: 0x000077BC
		internal static SubscriptionCacheManager GetCacheManager(Guid databaseGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			GlobalDatabaseHandler globalDatabaseHandler = null;
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.Initialized)
				{
					globalDatabaseHandler = DataAccessLayer.databaseHandler;
				}
			}
			if (globalDatabaseHandler != null)
			{
				return globalDatabaseHandler.GetCacheManager(databaseGuid);
			}
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000961C File Offset: 0x0000781C
		internal static SubscriptionCacheManager GetCacheManager(int databaseManagerIndex)
		{
			GlobalDatabaseHandler globalDatabaseHandler = null;
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.Initialized)
				{
					globalDatabaseHandler = DataAccessLayer.databaseHandler;
				}
			}
			if (globalDatabaseHandler != null)
			{
				return DataAccessLayer.GetCacheManager(globalDatabaseHandler.GetDatabaseGuid(databaseManagerIndex));
			}
			return null;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009678 File Offset: 0x00007878
		internal static DatabaseManager GetDatabaseManager(int databaseManagerIndex)
		{
			GlobalDatabaseHandler globalDatabaseHandler = null;
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.Initialized)
				{
					globalDatabaseHandler = DataAccessLayer.databaseHandler;
				}
			}
			if (globalDatabaseHandler != null)
			{
				return DataAccessLayer.GetDatabaseManager(globalDatabaseHandler.GetDatabaseGuid(databaseManagerIndex));
			}
			return null;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000096D4 File Offset: 0x000078D4
		internal static void RefreshAppConfig()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000096D8 File Offset: 0x000078D8
		internal static bool Initialize()
		{
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (DataAccessLayer.initialized)
				{
					return true;
				}
				if (DataAccessLayer.initOrShutdownPending)
				{
					throw new InvalidOperationException("Initialize or shutdown already pending");
				}
				DataAccessLayer.initOrShutdownPending = true;
			}
			bool flag2 = false;
			try
			{
				if (!ContentAggregationConfig.Start(true))
				{
					return false;
				}
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)172UL, DataAccessLayer.Tracer, "DAL.Initialize: Starting all components.", new object[0]);
				SyncHealthLogManager.Start();
				DataAccessLayer.databaseHandler = new GlobalDatabaseHandler();
				DataAccessLayer.databaseHandler.Initialize();
				ManagerPerfCounterHandler.Instance.StartUpdatingCounters();
				WorkTypeManager.Instance.Initialize();
				if (!SubscriptionCompletionServer.TryStartServer())
				{
					return false;
				}
				DataAccessLayer.dispatchManager = new DispatchManager(ContentAggregationConfig.SyncLogSession, ContentAggregationConfig.TransportSyncDispatchEnabled, ContentAggregationConfig.PrimingDispatchTime, ContentAggregationConfig.MinimumDispatchWaitForFailedSync, ContentAggregationConfig.DispatchOutageThreshold, new Action<EventLogEntry>(ContentAggregationConfig.LogEvent), SyncHealthLogger.Instance, SyncManagerConfiguration.Instance);
				DataAccessLayer.subscriptionProcessPermitter = new SubscriptionProcessPermitter(ContentAggregationConfig.SyncLogSession, DataAccessLayer.Tracer, SyncManagerConfiguration.Instance);
				DataAccessLayer.OnDatabaseDismounted += DataAccessLayer.dispatchManager.SyncQueueManager.OnDatabaseDismounted;
				DataAccessLayer.OnSubscriptionAdded += new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionAddedHandler);
				DataAccessLayer.OnSubscriptionRemoved += new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionRemovedHandler);
				DataAccessLayer.OnSubscriptionSyncNow += new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionSyncNowHandler);
				DataAccessLayer.OnWorkTypeBasedSyncNow += new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnWorkTypeBasedSyncNowHandler);
				DataAccessLayer.OnSubscriptionSyncCompleted += DataAccessLayer.dispatchManager.OnSubscriptionSyncCompletedHandler;
				DataAccessLayer.dispatchManager.SyncQueueManager.SubscriptionAddedOrRemovedEvent += ManagerPerfCounterHandler.Instance.OnSubscriptionAddedOrRemovedEvent;
				DataAccessLayer.dispatchManager.SyncQueueManager.SubscriptionEnqueuedOrDequeuedEvent += ManagerPerfCounterHandler.Instance.OnSubscriptionSyncEnqueuedOrDequeuedEvent;
				DataAccessLayer.dispatchManager.SyncQueueManager.ReportSyncQueueDispatchLagTimeEvent += ManagerPerfCounterHandler.Instance.OnReportSyncQueueDispatchLagTimeEvent;
				if (!SubscriptionNotificationServer.TryStartServer())
				{
					return false;
				}
				if (!SubscriptionCacheServer.TryStartServer())
				{
					return false;
				}
				ContentAggregationConfig.OnConfigurationChanged += DataAccessLayer.HandleConfigurationChange;
				DataAccessLayer.syncDiagnostics = new SyncDiagnostics();
				DataAccessLayer.syncDiagnostics.Register();
				flag2 = true;
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)208UL, DataAccessLayer.Tracer, "DAL.Initialize: All components initialized.", new object[0]);
			}
			finally
			{
				if (!flag2)
				{
					SyncHealthLogManager.Shutdown();
					if (DataAccessLayer.databaseHandler != null)
					{
						DataAccessLayer.databaseHandler.Shutdown();
					}
					ManagerPerfCounterHandler.Instance.StopUpdatingCounters();
					SubscriptionCompletionServer.StopServer();
					if (DataAccessLayer.dispatchManager != null)
					{
						DataAccessLayer.dispatchManager.Dispose();
						DataAccessLayer.dispatchManager = null;
					}
					SubscriptionNotificationServer.StopServer();
					SubscriptionCacheServer.StopServer();
				}
				lock (DataAccessLayer.startupShutdownLock)
				{
					DataAccessLayer.initialized = flag2;
					DataAccessLayer.initOrShutdownPending = false;
				}
			}
			return true;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009A10 File Offset: 0x00007C10
		internal static void Shutdown()
		{
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (!DataAccessLayer.initialized)
				{
					return;
				}
				if (DataAccessLayer.initOrShutdownPending)
				{
					throw new InvalidOperationException("Initialize or shutdown already pending");
				}
				DataAccessLayer.initOrShutdownPending = true;
			}
			try
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)173UL, DataAccessLayer.Tracer, "DAL.Shutdown: Stopping all components.", new object[0]);
				DataAccessLayer.syncDiagnostics.Unregister();
				DataAccessLayer.syncDiagnostics = null;
				SubscriptionCompletionServer.StopServer();
				SubscriptionNotificationServer.StopServer();
				SubscriptionCacheServer.StopServer();
				DataAccessLayer.OnDatabaseDismounted -= DataAccessLayer.dispatchManager.SyncQueueManager.OnDatabaseDismounted;
				DataAccessLayer.OnSubscriptionAdded -= new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionAddedHandler);
				DataAccessLayer.OnSubscriptionRemoved -= new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionRemovedHandler);
				DataAccessLayer.OnSubscriptionSyncNow -= new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnSubscriptionSyncNowHandler);
				DataAccessLayer.OnWorkTypeBasedSyncNow -= new EventHandler<SubscriptionInformation>(DataAccessLayer.dispatchManager.SyncQueueManager.OnWorkTypeBasedSyncNowHandler);
				DataAccessLayer.OnSubscriptionSyncCompleted -= DataAccessLayer.dispatchManager.OnSubscriptionSyncCompletedHandler;
				DataAccessLayer.dispatchManager.SyncQueueManager.SubscriptionAddedOrRemovedEvent -= ManagerPerfCounterHandler.Instance.OnSubscriptionAddedOrRemovedEvent;
				DataAccessLayer.dispatchManager.SyncQueueManager.SubscriptionEnqueuedOrDequeuedEvent -= ManagerPerfCounterHandler.Instance.OnSubscriptionSyncEnqueuedOrDequeuedEvent;
				DataAccessLayer.dispatchManager.SyncQueueManager.ReportSyncQueueDispatchLagTimeEvent -= ManagerPerfCounterHandler.Instance.OnReportSyncQueueDispatchLagTimeEvent;
				DataAccessLayer.dispatchManager.Dispose();
				DataAccessLayer.dispatchManager = null;
				ManagerPerfCounterHandler.Instance.StopUpdatingCounters();
				ContentAggregationConfig.OnConfigurationChanged -= DataAccessLayer.HandleConfigurationChange;
				DataAccessLayer.databaseHandler.Shutdown();
				DataAccessLayer.databaseHandler = null;
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)211UL, DataAccessLayer.Tracer, "DAL.Shutdown: entering shutdown state.", new object[0]);
				ContentAggregationConfig.Shutdown();
				SyncHealthLogManager.Shutdown();
			}
			finally
			{
				lock (DataAccessLayer.startupShutdownLock)
				{
					DataAccessLayer.initialized = false;
					DataAccessLayer.initOrShutdownPending = false;
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00009C78 File Offset: 0x00007E78
		internal static bool TryRebuildMailboxOnDatabase(Guid databaseGuid, Guid userMailboxGuid, out bool hasCacheChanged)
		{
			hasCacheChanged = false;
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)174UL, DataAccessLayer.Tracer, Guid.Empty, userMailboxGuid, "TryRebuildMailboxOnDatabase: Rebuilding mailbox in database {0}.", new object[]
			{
				databaseGuid
			});
			DatabaseManager databaseManager = DataAccessLayer.GetDatabaseManager(databaseGuid);
			if (databaseManager == null)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)175UL, DataAccessLayer.Tracer, Guid.Empty, userMailboxGuid, "TryRebuildMailboxOnDatabase: Failed to get database {0}.", new object[]
				{
					databaseGuid
				});
				return false;
			}
			return databaseManager.TryRebuildMailbox(userMailboxGuid, out hasCacheChanged);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009D14 File Offset: 0x00007F14
		internal static bool TryReportNewMailboxOnDatabase(MailboxSession mailboxSession, Guid databaseGuid)
		{
			DatabaseManager databaseManager = DataAccessLayer.GetDatabaseManager(databaseGuid);
			bool flag;
			return databaseManager != null && databaseManager.TryRebuildMailbox(mailboxSession.MailboxGuid, out flag);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009D3C File Offset: 0x00007F3C
		internal static bool TryReadSubscriptionsInformation(Guid databaseGuid, Guid mailboxGuid, out IDictionary<Guid, SubscriptionInformation> subscriptionsInformation)
		{
			subscriptionsInformation = null;
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)181UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReadSubscriptionsInformation: Trying to read subscription information for subscriptions for mailbox in database {0}.", new object[]
			{
				databaseGuid
			});
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(databaseGuid);
			if (cacheManager == null)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)182UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReadSubscriptionsInformation: Failed to get database {0}.", new object[]
				{
					databaseGuid
				});
				return false;
			}
			IEnumerable<SubscriptionCacheEntry> enumerable;
			try
			{
				enumerable = cacheManager.ReadAllSubscriptionsFromCache(mailboxGuid);
			}
			catch (CacheTransientException)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)183UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReadSubscriptionsInformation: Failed due to transient exception in database {0}.", new object[]
				{
					databaseGuid
				});
				return false;
			}
			catch (CacheCorruptException)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)184UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReadSubscriptionsInformation: Failed due to corruption exception in database {0}.", new object[]
				{
					databaseGuid
				});
				return false;
			}
			catch (CachePermanentException)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)185UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReadSubscriptionsInformation: Failed due to permanent exception in database {0}.", new object[]
				{
					databaseGuid
				});
				return true;
			}
			subscriptionsInformation = new Dictionary<Guid, SubscriptionInformation>(DataAccessLayer.AverageSubscriptionsPerUser);
			foreach (SubscriptionCacheEntry subscriptionCacheEntry in enumerable)
			{
				SubscriptionInformation value = new SubscriptionInformation(cacheManager, subscriptionCacheEntry);
				subscriptionsInformation[subscriptionCacheEntry.SubscriptionGuid] = value;
			}
			return true;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009F38 File Offset: 0x00008138
		internal static bool TryReadSubscriptionInformation(Guid databaseGuid, Guid mailboxGuid, StoreObjectId subscriptionMessageId, out SubscriptionInformation subscriptionInformation)
		{
			subscriptionInformation = null;
			bool flag = false;
			return DataAccessLayer.InternalTryReadSubscriptionInformation(databaseGuid, mailboxGuid, null, subscriptionMessageId, out subscriptionInformation, out flag);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009F5E File Offset: 0x0000815E
		internal static bool TryReadSubscriptionInformation(Guid databaseGuid, Guid mailboxGuid, Guid subscriptionGuid, out SubscriptionInformation subscriptionInformation, out bool needsBackOff)
		{
			subscriptionInformation = null;
			needsBackOff = false;
			return DataAccessLayer.InternalTryReadSubscriptionInformation(databaseGuid, mailboxGuid, new Guid?(subscriptionGuid), null, out subscriptionInformation, out needsBackOff);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009F78 File Offset: 0x00008178
		internal static bool TryReportMailboxDeleted(Guid databaseGuid, Guid mailboxGuid)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)186UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReportMailboxDeleted: Reporting mailbox in database {0} was deleted.", new object[]
			{
				databaseGuid
			});
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(databaseGuid);
			if (cacheManager == null)
			{
				return false;
			}
			try
			{
				cacheManager.DeleteCacheMessage(mailboxGuid);
			}
			catch (CacheTransientException)
			{
				return false;
			}
			catch (CachePermanentException)
			{
				return true;
			}
			return true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A000 File Offset: 0x00008200
		internal static bool TryReportSubscriptionCompleted(SubscriptionCompletionData subscriptionCompletionData)
		{
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)188UL, DataAccessLayer.Tracer, subscriptionCompletionData.SubscriptionGuid, subscriptionCompletionData.MailboxGuid, "TryReportSubscriptionCompleted:SubscriptionCompletionStatus:{0},MoreDataToDownload:{1},Database:{2},syncFailed:{3},syncPhase:{4},serializedSubscription:{5},syncWatermark:{6}.", new object[]
			{
				subscriptionCompletionData.SubscriptionCompletionStatus,
				subscriptionCompletionData.MoreDataToDownload,
				subscriptionCompletionData.DatabaseGuid,
				subscriptionCompletionData.SyncFailed,
				subscriptionCompletionData.SyncPhase,
				subscriptionCompletionData.SerializedSubscription,
				subscriptionCompletionData.SyncWatermark ?? "<null>"
			});
			Guid? tenantGuid = null;
			string incomingServerName = string.Empty;
			AggregationSubscriptionType? subscriptionType = null;
			SubscriptionInformation subscriptionInformation;
			if (!DataAccessLayer.TryReadSubscriptionInformation(subscriptionCompletionData.DatabaseGuid, subscriptionCompletionData.MailboxGuid, subscriptionCompletionData.SubscriptionMessageID, out subscriptionInformation) || subscriptionInformation == null)
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)193UL, DataAccessLayer.Tracer, subscriptionCompletionData.SubscriptionGuid, subscriptionCompletionData.MailboxGuid, "TryReportSubscriptionCompleted: Subscription not found in cache message: {0}.", new object[]
				{
					subscriptionCompletionData.SubscriptionMessageID
				});
			}
			else
			{
				tenantGuid = new Guid?(subscriptionInformation.TenantGuid);
				incomingServerName = subscriptionInformation.IncomingServerName.ToString();
				subscriptionType = new AggregationSubscriptionType?(subscriptionInformation.SubscriptionType);
				subscriptionCompletionData.LastSuccessfulDispatchTime = subscriptionInformation.LastSuccessfulDispatchTime;
				subscriptionInformation.MarkSyncCompletion(subscriptionCompletionData.DisableSubscription, new SyncPhase?(subscriptionCompletionData.SyncPhase), subscriptionCompletionData.SerializedSubscription, subscriptionCompletionData.SyncWatermark);
				subscriptionInformation.TrySave(null);
			}
			StatefulHubPicker.Instance.ResetHubLoad();
			DataAccessLayer.CollectSubscriptionCompletionDiagnostics(subscriptionCompletionData, tenantGuid, incomingServerName, subscriptionType);
			EventHandler<OnSyncCompletedEventArgs> onSubscriptionSyncCompletedEvent = DataAccessLayer.OnSubscriptionSyncCompletedEvent;
			if (onSubscriptionSyncCompletedEvent != null)
			{
				OnSyncCompletedEventArgs e = new OnSyncCompletedEventArgs(subscriptionCompletionData);
				onSubscriptionSyncCompletedEvent(null, e);
			}
			if (subscriptionCompletionData.InvalidState || subscriptionCompletionData.SubscriptionDeleted)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)189UL, DataAccessLayer.Tracer, subscriptionCompletionData.SubscriptionGuid, subscriptionCompletionData.MailboxGuid, "TryReportSubscriptionCompleted with:{0}", new object[]
				{
					subscriptionCompletionData.SubscriptionCompletionStatus
				});
				DatabaseManager databaseManager = DataAccessLayer.GetDatabaseManager(subscriptionCompletionData.DatabaseGuid);
				if (databaseManager != null)
				{
					databaseManager.ScheduleMailboxCrawl(subscriptionCompletionData.MailboxGuid);
				}
			}
			return true;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A230 File Offset: 0x00008430
		internal static bool TryReportSubscriptionListChanged(Guid mailboxGuid, Guid databaseGuid)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)198UL, DataAccessLayer.Tracer, Guid.Empty, mailboxGuid, "TryReportSubscriptionListChanged: Reporting subscription list change for mailbox {0} in database {1}.", new object[]
			{
				mailboxGuid,
				databaseGuid
			});
			DatabaseManager.MailboxCrawlerInstance.EnqueueMailboxCrawl(databaseGuid, mailboxGuid);
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A290 File Offset: 0x00008490
		internal static bool TryLogSubscriptionCreated(Guid mailboxGuid, Guid tenantGuid, AggregationSubscription subscription)
		{
			return SyncHealthLogManager.TryLogSubscriptionCreation(Environment.MachineName, tenantGuid.ToString(), mailboxGuid.ToString(), subscription.SubscriptionGuid.ToString(), subscription.SubscriptionType.ToString(), subscription.CreationType.ToString(), subscription.Domain, subscription.IncomingServerName, subscription.IncomingServerPort, subscription.AuthenticationType, subscription.EncryptionType, subscription.CreationTime, subscription.AggregationType.ToString());
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A32C File Offset: 0x0000852C
		internal static bool TryLogSubscriptionDeleted(Guid mailboxGuid, Guid tenantGuid, SubscriptionCacheEntry subscriptionCacheEntry, bool wasSubscriptionDeleted)
		{
			return SyncHealthLogManager.TryLogSubscriptionDeletion(Environment.MachineName, tenantGuid.ToString(), mailboxGuid.ToString(), subscriptionCacheEntry.SubscriptionGuid.ToString(), subscriptionCacheEntry.SubscriptionType.ToString(), subscriptionCacheEntry.IncomingServerName, wasSubscriptionDeleted, subscriptionCacheEntry.AggregationType.ToString());
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A398 File Offset: 0x00008598
		internal static bool TryReportSyncNowNeeded(Guid databaseGuid, Guid mailboxGuid, Guid subscriptionId)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)212UL, DataAccessLayer.Tracer, subscriptionId, mailboxGuid, "TryReportSyncNowNeeded: Reporting sync now needed for DB: {0}.", new object[]
			{
				databaseGuid
			});
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(databaseGuid);
			if (cacheManager == null)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)1551UL, DataAccessLayer.Tracer, subscriptionId, mailboxGuid, "TryReportSyncNowNeeded: CacheManager is not available for DB: {0}.", new object[]
				{
					databaseGuid
				});
				return false;
			}
			SubscriptionCacheEntry subscriptionCacheEntry = null;
			try
			{
				subscriptionCacheEntry = cacheManager.ReadSubscriptionFromCache(mailboxGuid, subscriptionId);
			}
			catch (CacheTransientException)
			{
				return false;
			}
			catch (CacheCorruptException)
			{
				return false;
			}
			catch (CacheNotFoundException)
			{
				DataAccessLayer.ScheduleMailboxCrawl(databaseGuid, mailboxGuid);
				return false;
			}
			catch (CachePermanentException)
			{
				return true;
			}
			if (subscriptionCacheEntry == null)
			{
				DataAccessLayer.ScheduleMailboxCrawl(databaseGuid, mailboxGuid);
				return false;
			}
			if (subscriptionCacheEntry.Disabled)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)209UL, DataAccessLayer.Tracer, subscriptionId, mailboxGuid, "TryIssueSyncNowForSubscription: Subscription in database {0} was disabled. Will enable it.", new object[]
				{
					databaseGuid
				});
				try
				{
					subscriptionCacheEntry.Disabled = false;
					cacheManager.UpdateCacheMessage(subscriptionCacheEntry);
				}
				catch (CacheTransientException)
				{
					return false;
				}
				catch (CacheCorruptException)
				{
					return false;
				}
				catch (CacheNotFoundException)
				{
					DataAccessLayer.ScheduleMailboxCrawl(databaseGuid, mailboxGuid);
					return false;
				}
				catch (CachePermanentException)
				{
					return true;
				}
			}
			SubscriptionInformation subscriptionInformation = new SubscriptionInformation(cacheManager, subscriptionCacheEntry);
			if (subscriptionInformation.AggregationType == AggregationType.Migration && subscriptionInformation.SyncPhase == SyncPhase.Incremental)
			{
				subscriptionInformation.UpdateSyncPhase(SyncPhase.Finalization);
				subscriptionInformation.TrySave(ContentAggregationConfig.SyncLogSession);
			}
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)210UL, DataAccessLayer.Tracer, subscriptionId, mailboxGuid, "TryIssueSyncNowForSubscription: Sending sync-now event for subscription in mailbox at database {0}.", new object[]
			{
				databaseGuid
			});
			DataAccessLayer.TriggerSyncNowEvent(subscriptionInformation);
			return true;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A5B0 File Offset: 0x000087B0
		internal static void ReportSubscriptionDispatch(string mailboxServerName, string tenantGuid, string userMailboxGuid, string subscriptionGuid, string incomingServerName, string subscriptionType, string aggregationType, string dispatchedTo, bool successful, bool permanentError, bool transientError, string dispatchError, bool beyondSyncPollingFrequency, int secondsBeyondPollingFrequency, string workType, string dispatchTrigger, string databaseGuid)
		{
			SyncHealthLogManager.TryLogSubscriptionDispatch(mailboxServerName, tenantGuid, userMailboxGuid, subscriptionGuid, incomingServerName, subscriptionType, aggregationType, dispatchedTo, successful, permanentError, transientError, dispatchError, beyondSyncPollingFrequency, secondsBeyondPollingFrequency, workType, dispatchTrigger, databaseGuid);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A5E1 File Offset: 0x000087E1
		internal static XElement GetDatabaseHandlerDiagnosticInfo(SyncDiagnosticMode mode)
		{
			return DataAccessLayer.databaseHandler.GetDiagnosticInfo(mode);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000A5EE File Offset: 0x000087EE
		internal static XElement GetDispatchManagerDiagnosticInfo(SyncDiagnosticMode mode)
		{
			return DataAccessLayer.dispatchManager.GetDiagnosticInfo(mode);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A5FC File Offset: 0x000087FC
		internal static void OnSubscriptionAddedHandler(object sender, SubscriptionInformation subscriptionInformation)
		{
			if (!DataAccessLayer.subscriptionProcessPermitter.IsSubscriptionPermitted(subscriptionInformation))
			{
				return;
			}
			EventHandler<SubscriptionInformation> onSubscriptionAddedEvent = DataAccessLayer.OnSubscriptionAddedEvent;
			if (onSubscriptionAddedEvent == null)
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)213UL, DataAccessLayer.Tracer, subscriptionInformation.SubscriptionGuid, subscriptionInformation.MailboxGuid, "OnSubscriptionAddedHandler: SubscriptionAdded handler is null.", new object[0]);
				return;
			}
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)393UL, DataAccessLayer.Tracer, subscriptionInformation.SubscriptionGuid, subscriptionInformation.MailboxGuid, "OnSubscriptionAddedHandler: Calling SubscriptionAdded handler.", new object[0]);
			onSubscriptionAddedEvent(sender, subscriptionInformation);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A698 File Offset: 0x00008898
		internal static void OnSubscriptionRemovedHandler(object sender, SubscriptionInformation subscriptionInformation)
		{
			EventHandler<SubscriptionInformation> onSubscriptionRemovedEvent = DataAccessLayer.OnSubscriptionRemovedEvent;
			if (onSubscriptionRemovedEvent != null)
			{
				onSubscriptionRemovedEvent(sender, subscriptionInformation);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A6B8 File Offset: 0x000088B8
		internal static void OnWorkTypeBasedSyncNowHandler(WorkType workType, SubscriptionInformation subscriptionInformation)
		{
			EventHandler<SubscriptionInformation> onWorkTypeBasedSyncNowEvent = DataAccessLayer.OnWorkTypeBasedSyncNowEvent;
			if (onWorkTypeBasedSyncNowEvent != null)
			{
				onWorkTypeBasedSyncNowEvent(workType, subscriptionInformation);
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A6DC File Offset: 0x000088DC
		internal static bool TryUpdateCacheMessageSyncPhase(Guid databaseGuid, Guid mailboxGuid, Guid subscriptionGuid, SyncPhase syncPhase, out SubscriptionInformation subscriptionInformation)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			subscriptionInformation = null;
			GlobalSyncLogSession syncLogSession = ContentAggregationConfig.SyncLogSession;
			Trace tracer = DataAccessLayer.Tracer;
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(databaseGuid);
			if (cacheManager == null)
			{
				syncLogSession.LogDebugging((TSLID)957UL, tracer, "TryUpdateCacheMessageSyncPhase could not get cache manager.", new object[0]);
				return false;
			}
			try
			{
				SubscriptionCacheEntry subscriptionCacheEntry = cacheManager.ReadSubscriptionFromCache(mailboxGuid, subscriptionGuid);
				if (subscriptionCacheEntry != null)
				{
					subscriptionCacheEntry.SyncPhase = syncPhase;
					cacheManager.UpdateCacheMessage(subscriptionCacheEntry);
					subscriptionInformation = new SubscriptionInformation(cacheManager, subscriptionCacheEntry);
				}
			}
			catch (CacheTransientException ex)
			{
				syncLogSession.LogDebugging((TSLID)963UL, tracer, "TryUpdateCacheMessageSyncPhase ex:{0}", new object[]
				{
					ex
				});
				return false;
			}
			catch (CachePermanentException ex2)
			{
				syncLogSession.LogDebugging((TSLID)999UL, tracer, "TryUpdateCacheMessageSyncPhase ex:{0}", new object[]
				{
					ex2
				});
				return false;
			}
			syncLogSession.LogDebugging((TSLID)1106UL, tracer, "TryUpdateCacheMessageSyncPhase succeeded.", new object[0]);
			return true;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A804 File Offset: 0x00008A04
		private static void CollectSubscriptionCompletionDiagnostics(SubscriptionCompletionData subscriptionCompletionData, Guid? tenantGuid, string incomingServerName, AggregationSubscriptionType? subscriptionType)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			TimeSpan timeSpan = TimeSpan.MinValue;
			if (subscriptionCompletionData.LastSuccessfulDispatchTime != null && subscriptionType != null)
			{
				timeSpan = utcNow - subscriptionCompletionData.LastSuccessfulDispatchTime.Value;
				ManagerPerfCounterHandler.Instance.SetLastSubscriptionProcessingTime(subscriptionType.Value, Convert.ToInt64(timeSpan.TotalMilliseconds));
			}
			SyncHealthLogManager.TryLogSubscriptionCompletion(Environment.MachineName, (tenantGuid != null) ? tenantGuid.Value.ToString() : string.Empty, subscriptionCompletionData.MailboxGuid.ToString(), subscriptionCompletionData.SubscriptionGuid.ToString(), incomingServerName, (subscriptionType != null) ? subscriptionType.ToString() : string.Empty, subscriptionCompletionData.AggregationType.ToString(), string.Empty, timeSpan, subscriptionCompletionData.MoreDataToDownload);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)1290UL, DataAccessLayer.Tracer, subscriptionCompletionData.SubscriptionGuid, subscriptionCompletionData.MailboxGuid, "TryReportSubscriptionCompleted: SubscriptionProcessingTime: {0}.", new object[]
			{
				timeSpan
			});
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A944 File Offset: 0x00008B44
		private static bool InternalTryReadSubscriptionInformation(Guid databaseGuid, Guid mailboxGuid, Guid? subscriptionGuid, StoreObjectId subscriptionMessageId, out SubscriptionInformation subscriptionInformation, out bool needsBackOff)
		{
			subscriptionInformation = null;
			needsBackOff = false;
			SubscriptionCacheManager cacheManager = DataAccessLayer.GetCacheManager(databaseGuid);
			if (cacheManager == null)
			{
				return true;
			}
			bool result;
			try
			{
				SubscriptionCacheEntry subscriptionCacheEntry;
				if (subscriptionGuid != null)
				{
					subscriptionCacheEntry = cacheManager.ReadSubscriptionFromCache(mailboxGuid, subscriptionGuid.Value);
				}
				else
				{
					subscriptionCacheEntry = cacheManager.ReadSubscriptionFromCache(mailboxGuid, subscriptionMessageId);
				}
				if (subscriptionCacheEntry == null)
				{
					result = true;
				}
				else
				{
					subscriptionInformation = new SubscriptionInformation(cacheManager, subscriptionCacheEntry);
					result = true;
				}
			}
			catch (CacheTransientException)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)190UL, DataAccessLayer.Tracer, (subscriptionGuid != null) ? subscriptionGuid.Value : Guid.Empty, mailboxGuid, "InternalTryReadSubscriptionInformation: Failed due to transient exception in database {0}.", new object[]
				{
					databaseGuid
				});
				needsBackOff = true;
				result = false;
			}
			catch (CacheCorruptException)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)191UL, DataAccessLayer.Tracer, (subscriptionGuid != null) ? subscriptionGuid.Value : Guid.Empty, mailboxGuid, "InternalTryReadSubscriptionInformation: Failed due to cache corrupt in database {0}.", new object[]
				{
					databaseGuid
				});
				result = true;
			}
			catch (CacheNotFoundException)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)192UL, DataAccessLayer.Tracer, (subscriptionGuid != null) ? subscriptionGuid.Value : Guid.Empty, mailboxGuid, "InternalTryReadSubscriptionInformation: Failed due to cache not found exception in database {0}.", new object[]
				{
					databaseGuid
				});
				result = true;
			}
			catch (CachePermanentException)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)194UL, DataAccessLayer.Tracer, (subscriptionGuid != null) ? subscriptionGuid.Value : Guid.Empty, mailboxGuid, "InternalTryReadSubscriptionInformation: Failed due to cachepermanent exception in database {0}.", new object[]
				{
					databaseGuid
				});
				result = true;
			}
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000AB34 File Offset: 0x00008D34
		private static void TriggerSyncNowEvent(SubscriptionInformation subscriptionInformation)
		{
			EventHandler<SubscriptionInformation> onSubscriptionSyncNowEvent = DataAccessLayer.OnSubscriptionSyncNowEvent;
			if (onSubscriptionSyncNowEvent != null)
			{
				onSubscriptionSyncNowEvent(null, subscriptionInformation);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000AB54 File Offset: 0x00008D54
		private static void HandleConfigurationChange()
		{
			lock (DataAccessLayer.startupShutdownLock)
			{
				if (!DataAccessLayer.initialized)
				{
					ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)216UL, DataAccessLayer.Tracer, "Ignoring Configuration Change as DAL is not initialized.", new object[0]);
				}
				else
				{
					SyncHealthLogManager.TryConfigure(ContentAggregationConfig.SyncMailboxHealthLogConfiguration);
				}
			}
		}

		// Token: 0x040000F7 RID: 247
		internal static readonly int AverageSubscriptionsPerUser = 2;

		// Token: 0x040000F8 RID: 248
		private static readonly Trace Tracer = ExTraceGlobals.DataAccessLayerTracer;

		// Token: 0x040000F9 RID: 249
		private static object startupShutdownLock = new object();

		// Token: 0x040000FA RID: 250
		private static bool initialized;

		// Token: 0x040000FB RID: 251
		private static bool initOrShutdownPending;

		// Token: 0x040000FC RID: 252
		private static DispatchManager dispatchManager;

		// Token: 0x040000FD RID: 253
		private static SyncDiagnostics syncDiagnostics;

		// Token: 0x040000FE RID: 254
		private static GlobalDatabaseHandler databaseHandler;

		// Token: 0x040000FF RID: 255
		private static SubscriptionProcessPermitter subscriptionProcessPermitter;

		// Token: 0x04000100 RID: 256
		private static bool generateWatsonReports = true;
	}
}
