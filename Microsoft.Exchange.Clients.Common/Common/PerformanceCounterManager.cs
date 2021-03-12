using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000031 RID: 49
	internal static class PerformanceCounterManager
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009DE5 File Offset: 0x00007FE5
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00009DEC File Offset: 0x00007FEC
		internal static bool ArePerfCountersEnabled { get; set; }

		// Token: 0x06000161 RID: 353 RVA: 0x00009DF4 File Offset: 0x00007FF4
		internal static void InitializeIMQueueSizes(int signInFailureQueueSize, int sentMessageFailureQueueSize)
		{
			PerformanceCounterManager.instantMessagingLogonResultsQueueSize = signInFailureQueueSize;
			PerformanceCounterManager.instantMessagingLogonResultsQueue = new FixedSizeQueueBool(signInFailureQueueSize);
			PerformanceCounterManager.sentInstantMessageResultsQueueSize = sentMessageFailureQueueSize;
			PerformanceCounterManager.sentInstantMessageResultsQueue = new FixedSizeQueueBool(sentMessageFailureQueueSize);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00009E18 File Offset: 0x00008018
		internal static Dictionary<string, UniqueUserData> UniqueUserTable
		{
			get
			{
				return PerformanceCounterManager.uniqueUserTable;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009E1F File Offset: 0x0000801F
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00009E26 File Offset: 0x00008026
		internal static int MailboxOfflineExResultsQueueSize
		{
			get
			{
				return PerformanceCounterManager.mailboxOfflineExResultsQueueSize;
			}
			set
			{
				PerformanceCounterManager.mailboxOfflineExResultsQueueSize = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009E2E File Offset: 0x0000802E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00009E35 File Offset: 0x00008035
		internal static int ConnectionFailedTransientExResultsQueueSize
		{
			get
			{
				return PerformanceCounterManager.connectionFailedTransientExResultsQueueSize;
			}
			set
			{
				PerformanceCounterManager.connectionFailedTransientExResultsQueueSize = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00009E3D File Offset: 0x0000803D
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00009E44 File Offset: 0x00008044
		internal static int StorageTransientExResultsQueueSize
		{
			get
			{
				return PerformanceCounterManager.storageTransientExResultsQueueSize;
			}
			set
			{
				PerformanceCounterManager.storageTransientExResultsQueueSize = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009E4C File Offset: 0x0000804C
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00009E53 File Offset: 0x00008053
		internal static int StoragePermanentExResultsQueueSize
		{
			get
			{
				return PerformanceCounterManager.storagePermanentExResultsQueueSize;
			}
			set
			{
				PerformanceCounterManager.storagePermanentExResultsQueueSize = value;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009E5B File Offset: 0x0000805B
		internal static void InitializeExPerfCountersQueueSizes()
		{
			PerformanceCounterManager.mailboxOfflineExResultsQueue = new FixedSizeQueueBool(PerformanceCounterManager.mailboxOfflineExResultsQueueSize);
			PerformanceCounterManager.connectionFailedTransientExResultsQueue = new FixedSizeQueueBool(PerformanceCounterManager.connectionFailedTransientExResultsQueueSize);
			PerformanceCounterManager.storageTransientExResultsQueue = new FixedSizeQueueBool(PerformanceCounterManager.storageTransientExResultsQueueSize);
			PerformanceCounterManager.storagePermanentExResultsQueue = new FixedSizeQueueBool(PerformanceCounterManager.storagePermanentExResultsQueueSize);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009E9C File Offset: 0x0000809C
		internal static void InitializePerformanceCounters()
		{
			try
			{
				OwaSingleCounters.CurrentUsers.RawValue = 0L;
				OwaSingleCounters.CurrentUniqueUsers.RawValue = 0L;
				OwaSingleCounters.TotalUsers.RawValue = 0L;
				OwaSingleCounters.TotalUniqueUsers.RawValue = 0L;
				OwaSingleCounters.PeakUserCount.RawValue = 0L;
				OwaSingleCounters.CurrentUsersLight.RawValue = 0L;
				OwaSingleCounters.CurrentUniqueUsersLight.RawValue = 0L;
				OwaSingleCounters.TotalUsersLight.RawValue = 0L;
				OwaSingleCounters.TotalUniqueUsersLight.RawValue = 0L;
				OwaSingleCounters.PeakUserCountLight.RawValue = 0L;
				OwaSingleCounters.CurrentUsersPremium.RawValue = 0L;
				OwaSingleCounters.CurrentUniqueUsersPremium.RawValue = 0L;
				OwaSingleCounters.TotalUsersPremium.RawValue = 0L;
				OwaSingleCounters.TotalUniqueUsersPremium.RawValue = 0L;
				OwaSingleCounters.PeakUserCountPremium.RawValue = 0L;
				OwaSingleCounters.AttachmentsUploaded.RawValue = 0L;
				OwaSingleCounters.ItemsCreated.RawValue = 0L;
				OwaSingleCounters.ItemsDeleted.RawValue = 0L;
				OwaSingleCounters.ItemsUpdated.RawValue = 0L;
				OwaSingleCounters.MailViewsLoaded.RawValue = 0L;
				OwaSingleCounters.MailViewRefreshes.RawValue = 0L;
				OwaSingleCounters.MessagesSent.RawValue = 0L;
				OwaSingleCounters.IRMMessagesSent.RawValue = 0L;
				OwaSingleCounters.AverageResponseTime.RawValue = 0L;
				PerformanceCounterManager.responseTimeAverage = 0.0;
				OwaSingleCounters.IMAverageSignOnTime.RawValue = 0L;
				PerformanceCounterManager.imSignOnTimeAverage = 0.0;
				OwaSingleCounters.TotalSessionsEndedByLogoff.RawValue = 0L;
				OwaSingleCounters.TotalSessionsEndedByTimeout.RawValue = 0L;
				OwaSingleCounters.CalendarViewsLoaded.RawValue = 0L;
				OwaSingleCounters.CalendarViewsRefreshed.RawValue = 0L;
				OwaSingleCounters.AverageSearchTime.RawValue = 0L;
				PerformanceCounterManager.searchTimeAverage = 0.0;
				OwaSingleCounters.TotalSearches.RawValue = 0L;
				OwaSingleCounters.TotalRequests.RawValue = 0L;
				OwaSingleCounters.TotalRequestsFailed.RawValue = 0L;
				OwaSingleCounters.SearchesTimedOut.RawValue = 0L;
				OwaSingleCounters.TotalSpellchecks.RawValue = 0L;
				OwaSingleCounters.AverageSpellcheckTime.RawValue = 0L;
				OwaSingleCounters.InvalidCanaryRequests.RawValue = 0L;
				OwaSingleCounters.PID.RawValue = (long)Process.GetCurrentProcess().Id;
				OwaSingleCounters.NamesChecked.RawValue = 0L;
				OwaSingleCounters.PasswordChanges.RawValue = 0L;
				OwaSingleCounters.CurrentProxiedUsers.RawValue = 0L;
				OwaSingleCounters.ProxiedUserRequests.RawValue = 0L;
				OwaSingleCounters.ProxiedResponseTimeAverage.RawValue = 0L;
				PerformanceCounterManager.proxiedResponseTimeAverage = 0.0;
				OwaSingleCounters.ProxyRequestBytes.RawValue = 0L;
				OwaSingleCounters.ProxyResponseBytes.RawValue = 0L;
				OwaSingleCounters.WssBytes.RawValue = 0L;
				OwaSingleCounters.UncBytes.RawValue = 0L;
				OwaSingleCounters.WssRequests.RawValue = 0L;
				OwaSingleCounters.UncRequests.RawValue = 0L;
				OwaSingleCounters.ASQueries.RawValue = 0L;
				OwaSingleCounters.ASQueriesFailurePercent.RawValue = 0L;
				OwaSingleCounters.StoreLogonFailurePercent.RawValue = 0L;
				OwaSingleCounters.CASIntraSiteRedirectionLatertoEarlierVersion.RawValue = 0L;
				OwaSingleCounters.CASIntraSiteRedirectionEarliertoLaterVersion.RawValue = 0L;
				OwaSingleCounters.CASCrossSiteRedirectionLatertoEarlierVersion.RawValue = 0L;
				OwaSingleCounters.CASCrossSiteRedirectionEarliertoLaterVersion.RawValue = 0L;
				OwaSingleCounters.ActiveMailboxSubscriptions.RawValue = 0L;
				OwaSingleCounters.TotalMailboxNotifications.RawValue = 0L;
				OwaSingleCounters.TotalUserContextReInitializationRequests.RawValue = 0L;
				OwaSingleCounters.MailboxOfflineExceptionFailuresPercent.RawValue = 0L;
				OwaSingleCounters.ConnectionFailedTransientExceptionPercent.RawValue = 0L;
				OwaSingleCounters.StorageTransientExceptionPercent.RawValue = 0L;
				OwaSingleCounters.StoragePermanentExceptionPercent.RawValue = 0L;
				OwaSingleCounters.IMCurrentUsers.RawValue = 0L;
				OwaSingleCounters.IMTotalInstantMessagesReceived.RawValue = 0L;
				OwaSingleCounters.IMTotalInstantMessagesSent.RawValue = 0L;
				OwaSingleCounters.IMTotalLogonFailures.RawValue = 0L;
				OwaSingleCounters.IMTotalMessageDeliveryFailures.RawValue = 0L;
				OwaSingleCounters.IMTotalPresenceQueries.RawValue = 0L;
				OwaSingleCounters.IMTotalUsers.RawValue = 0L;
				OwaSingleCounters.IMLogonFailuresPercent.RawValue = 0L;
				OwaSingleCounters.IMSentMessageDeliveryFailuresPercent.RawValue = 0L;
				OwaSingleCounters.OwaToEwsRequestFailureRate.RawValue = 0L;
				OwaSingleCounters.RequestTimeouts.RawValue = 0L;
				OwaSingleCounters.SenderPhotosTotalLDAPCalls.RawValue = 0L;
				OwaSingleCounters.SenderPhotosTotalLDAPCallsWithPicture.RawValue = 0L;
				OwaSingleCounters.SenderPhotosNegativeCacheCount.RawValue = 0L;
				OwaSingleCounters.SenderPhotosDataFromNegativeCacheCount.RawValue = 0L;
				OwaSingleCounters.AggregatedUserConfigurationPartsRebuilt.RawValue = 0L;
				OwaSingleCounters.AggregatedUserConfigurationPartsRequested.RawValue = 0L;
				OwaSingleCounters.AggregatedUserConfigurationPartsRead.RawValue = 0L;
				OwaSingleCounters.SessionDataCacheBuildsStarted.RawValue = 0L;
				OwaSingleCounters.SessionDataCacheBuildsCompleted.RawValue = 0L;
				OwaSingleCounters.SessionDataCacheWaitedForPreload.RawValue = 0L;
				OwaSingleCounters.SessionDataCacheUsed.RawValue = 0L;
				OwaSingleCounters.SessionDataCacheWaitTimeout.RawValue = 0L;
				PerformanceCounterManager.ArePerfCountersEnabled = true;
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, string>(0L, "Failed to initialize perfmon counters, perf data will not be available. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
			}
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUniqueUsers.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUniqueUsersLight.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUniqueUsersPremium.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUsers.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUsersLight.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentUsersPremium.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.CurrentProxiedUsers.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterValues.Add(OwaSingleCounters.IMCurrentUsers.CounterName, 0L);
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUniqueUsers.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUniqueUsersLight.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUniqueUsersPremium.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUsers.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUsersLight.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentUsersPremium.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.CurrentProxiedUsers.CounterName, new object());
			PerformanceCounterManager.currentUserCounterLocks.Add(OwaSingleCounters.IMCurrentUsers.CounterName, new object());
			ThrottlingPerfCounterWrapper.Initialize(BudgetType.Owa);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public static UniqueUserData GetUniqueUserData(string userName)
		{
			UniqueUserData result;
			lock (PerformanceCounterManager.uniqueUserTable)
			{
				UniqueUserData uniqueUserData = null;
				if (!PerformanceCounterManager.uniqueUserTable.TryGetValue(userName, out uniqueUserData))
				{
					result = null;
				}
				else
				{
					result = uniqueUserData;
				}
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A50C File Offset: 0x0000870C
		public static void UpdateSearchTimePerformanceCounter(long newValue)
		{
			PerformanceCounterManager.UpdateMovingAveragePerformanceCounter(OwaSingleCounters.AverageSearchTime, newValue, ref PerformanceCounterManager.searchTimeAverage, PerformanceCounterManager.searchTimeAverageLock);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A523 File Offset: 0x00008723
		public static void UpdateSpellcheckTimePerformanceCounter(long newValue)
		{
			PerformanceCounterManager.UpdateMovingAveragePerformanceCounter(OwaSingleCounters.AverageSpellcheckTime, newValue, ref PerformanceCounterManager.spellcheckTimeAverage, PerformanceCounterManager.spellcheckTimeAverageLock);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A53A File Offset: 0x0000873A
		public static void UpdateResponseTimePerformanceCounter(long newValue, bool isProxy)
		{
			PerformanceCounterManager.UpdateMovingAveragePerformanceCounter(OwaSingleCounters.AverageResponseTime, newValue, ref PerformanceCounterManager.responseTimeAverage, PerformanceCounterManager.responseTimeAverageLock);
			if (isProxy)
			{
				PerformanceCounterManager.UpdateMovingAveragePerformanceCounter(OwaSingleCounters.ProxiedResponseTimeAverage, newValue, ref PerformanceCounterManager.proxiedResponseTimeAverage, PerformanceCounterManager.proxiedResponseTimeAverageLock);
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A569 File Offset: 0x00008769
		public static void UpdateImSignOnTimePerformanceCounter(long newValue)
		{
			PerformanceCounterManager.UpdateMovingAveragePerformanceCounter(OwaSingleCounters.IMAverageSignOnTime, newValue, ref PerformanceCounterManager.imSignOnTimeAverage, PerformanceCounterManager.imSignOnTimeAverageLock);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A580 File Offset: 0x00008780
		private static void UpdateMovingAveragePerformanceCounter(ExPerformanceCounter performanceCounter, long newValue, ref double averageValue, object lockObject)
		{
			lock (lockObject)
			{
				averageValue = (1.0 - PerformanceCounterManager.averageMultiplier) * averageValue + PerformanceCounterManager.averageMultiplier * (double)newValue;
				performanceCounter.RawValue = (long)averageValue;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A5DC File Offset: 0x000087DC
		public static void IncrementCurrentUsersCounterBy(ExPerformanceCounter performanceCounter, long incrementValue)
		{
			if (performanceCounter == null)
			{
				throw new ArgumentNullException("performanceCounter");
			}
			if (!PerformanceCounterManager.currentUserCounterLocks.ContainsKey(performanceCounter.CounterName))
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "The performance counter: \"{0}\" is not supported to be updated by method Globals.IncrementCurrentUsersCounterBy().", performanceCounter.CounterName);
				return;
			}
			object obj = PerformanceCounterManager.currentUserCounterLocks[performanceCounter.CounterName];
			lock (obj)
			{
				long num = PerformanceCounterManager.currentUserCounterValues[performanceCounter.CounterName];
				num += incrementValue;
				performanceCounter.RawValue = num;
				PerformanceCounterManager.currentUserCounterValues[performanceCounter.CounterName] = num;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A688 File Offset: 0x00008888
		public static void AddAvailabilityServiceResult(bool result)
		{
			OwaSingleCounters.ASQueries.Increment();
			lock (PerformanceCounterManager.availabilityServiceResultsQueue)
			{
				PerformanceCounterManager.availabilityServiceResultsQueue.AddSample(result);
				OwaSingleCounters.ASQueriesFailurePercent.RawValue = (long)Math.Round(100.0 * (1.0 - PerformanceCounterManager.availabilityServiceResultsQueue.Mean));
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000A708 File Offset: 0x00008908
		private static long AddResultToQueue(bool result, FixedSizeQueueBool queue, int queueSize)
		{
			long result2 = -1L;
			lock (queue)
			{
				queue.AddSample(result);
				if (queue.Count >= queueSize)
				{
					result2 = (long)Math.Round(100.0 * (1.0 - queue.Mean));
				}
			}
			return result2;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A774 File Offset: 0x00008974
		public static void AddStoreLogonResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.storeLogonResultsQueue, 1);
			if (num != -1L)
			{
				OwaSingleCounters.StoreLogonFailurePercent.RawValue = num;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public static void AddInstantMessagingLogonResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.instantMessagingLogonResultsQueue, PerformanceCounterManager.instantMessagingLogonResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.IMLogonFailuresPercent.RawValue = num;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public static void AddSentInstantMessageResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.sentInstantMessageResultsQueue, PerformanceCounterManager.sentInstantMessageResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.IMSentMessageDeliveryFailuresPercent.RawValue = num;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000A800 File Offset: 0x00008A00
		public static void AddEwsRequestResult(bool result)
		{
			lock (PerformanceCounterManager.ewsRequestResultQueue)
			{
				PerformanceCounterManager.ewsRequestResultQueue.AddSample(result);
				if (PerformanceCounterManager.ewsRequestResultQueue.Count > 50)
				{
					OwaSingleCounters.OwaToEwsRequestFailureRate.RawValue = (long)Math.Round(100.0 * (1.0 - PerformanceCounterManager.ewsRequestResultQueue.Mean));
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000A880 File Offset: 0x00008A80
		public static void AddMailboxOfflineExResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.mailboxOfflineExResultsQueue, PerformanceCounterManager.mailboxOfflineExResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.MailboxOfflineExceptionFailuresPercent.RawValue = num;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public static void AddConnectionFailedTransientExResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.connectionFailedTransientExResultsQueue, PerformanceCounterManager.connectionFailedTransientExResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.ConnectionFailedTransientExceptionPercent.RawValue = num;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public static void AddStorageTransientExResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.storageTransientExResultsQueue, PerformanceCounterManager.storageTransientExResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.StorageTransientExceptionPercent.RawValue = num;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000A910 File Offset: 0x00008B10
		public static void AddStoragePermanantExResult(bool result)
		{
			long num = PerformanceCounterManager.AddResultToQueue(result, PerformanceCounterManager.storagePermanentExResultsQueue, PerformanceCounterManager.storagePermanentExResultsQueueSize);
			if (num != -1L)
			{
				OwaSingleCounters.StoragePermanentExceptionPercent.RawValue = num;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A940 File Offset: 0x00008B40
		public static void ProcessUserContextReInitializationRequest()
		{
			if (PerformanceCounterManager.ArePerfCountersEnabled)
			{
				OwaSingleCounters.TotalUserContextReInitializationRequests.Increment();
			}
			long rawValue = OwaSingleCounters.TotalUserContextReInitializationRequests.RawValue;
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (utcNow.UtcTicks - PerformanceCounterManager.lastUserContextReInitializationIntervalStartTimeInTicks >= PerformanceCounterManager.userContextReInitializationCheckDuration.Ticks)
			{
				OwaSingleCounters.TotalUserContextReInitializationRequests.RawValue = 0L;
				Interlocked.Exchange(ref PerformanceCounterManager.lastUserContextReInitializationIntervalStartTimeInTicks, utcNow.UtcTicks);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public static UniqueUserData GetUserData(string userName)
		{
			bool flag;
			return PerformanceCounterManager.GetUserData(userName, out flag);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public static UniqueUserData GetUserData(string userName, out bool isNewUser)
		{
			UniqueUserData uniqueUserData = null;
			lock (PerformanceCounterManager.uniqueUserTable)
			{
				if (!PerformanceCounterManager.uniqueUserTable.ContainsKey(userName))
				{
					uniqueUserData = new UniqueUserData();
					PerformanceCounterManager.uniqueUserTable.Add(userName, uniqueUserData);
					isNewUser = true;
				}
				else
				{
					uniqueUserData = PerformanceCounterManager.uniqueUserTable[userName];
					isNewUser = false;
				}
			}
			return uniqueUserData;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000AA38 File Offset: 0x00008C38
		public static void IncrementUserPerfCounters(string userName, bool isProxy, bool isLightExperience)
		{
			if (isProxy)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentProxiedUsers, 1L);
				return;
			}
			bool flag;
			UniqueUserData userData = PerformanceCounterManager.GetUserData(userName, out flag);
			if (flag)
			{
				OwaSingleCounters.TotalUniqueUsers.Increment();
			}
			if (userData.CurrentSessionCount == 0)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsers, 1L);
			}
			if (isLightExperience)
			{
				if (userData.CurrentLightSessionCount == 0)
				{
					PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsersLight, 1L);
				}
				if (userData.IsFirstLightSession)
				{
					OwaSingleCounters.TotalUniqueUsersLight.Increment();
				}
			}
			else
			{
				if (userData.CurrentPremiumSessionCount == 0)
				{
					PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsersPremium, 1L);
				}
				if (userData.IsFirstPremiumSession)
				{
					OwaSingleCounters.TotalUniqueUsersPremium.Increment();
				}
			}
			PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsers, 1L);
			OwaSingleCounters.TotalUsers.Increment();
			if (OwaSingleCounters.CurrentUsers.RawValue > OwaSingleCounters.PeakUserCount.RawValue)
			{
				OwaSingleCounters.PeakUserCount.RawValue = OwaSingleCounters.CurrentUsers.RawValue;
			}
			if (isLightExperience)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsersLight, 1L);
				OwaSingleCounters.TotalUsersLight.Increment();
				if (OwaSingleCounters.CurrentUsersLight.RawValue > OwaSingleCounters.PeakUserCountLight.RawValue)
				{
					OwaSingleCounters.PeakUserCountLight.RawValue = OwaSingleCounters.CurrentUsersLight.RawValue;
					return;
				}
			}
			else
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsersPremium, 1L);
				OwaSingleCounters.TotalUsersPremium.Increment();
				if (OwaSingleCounters.CurrentUsersPremium.RawValue > OwaSingleCounters.PeakUserCountPremium.RawValue)
				{
					OwaSingleCounters.PeakUserCountPremium.RawValue = OwaSingleCounters.CurrentUsersPremium.RawValue;
				}
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public static void DecrementUserPerfCounters(string userName, bool isProxy, bool isLightExperience)
		{
			if (isProxy)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentProxiedUsers, -1L);
				return;
			}
			PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsers, -1L);
			UniqueUserData userData = PerformanceCounterManager.GetUserData(userName);
			if (isLightExperience)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsersLight, -1L);
				if (userData.CurrentLightSessionCount == 0)
				{
					PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsersLight, -1L);
				}
			}
			else
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUsersPremium, -1L);
				if (userData.CurrentPremiumSessionCount == 0)
				{
					PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsersPremium, -1L);
				}
			}
			if (userData.CurrentSessionCount == 0)
			{
				PerformanceCounterManager.IncrementCurrentUsersCounterBy(OwaSingleCounters.CurrentUniqueUsers, -1L);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000AC28 File Offset: 0x00008E28
		public static void UpdatePerfCounteronUserContextCreation(string userName, bool isProxy, bool isLightExperience, bool arePerfCountersEnabled)
		{
			if (arePerfCountersEnabled)
			{
				PerformanceCounterManager.IncrementUserPerfCounters(userName, isProxy, isLightExperience);
			}
			UniqueUserData userData = PerformanceCounterManager.GetUserData(userName);
			userData.IncreaseSessionCounter(isProxy, isLightExperience);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000AC50 File Offset: 0x00008E50
		public static void UpdatePerfCounteronUserContextDeletion(string userName, bool isProxy, bool isLightExperience, bool arePerfCountersEnabled)
		{
			if (string.IsNullOrEmpty(userName))
			{
				ExTraceGlobals.CoreTracer.TraceError(0L, "UpdatePerfCounteronUserContextDeletion got null or empty value for parameter 'userName'.");
				return;
			}
			UniqueUserData uniqueUserData = PerformanceCounterManager.GetUniqueUserData(userName);
			uniqueUserData.DecreaseSessionCounter(isProxy, isLightExperience);
			if (arePerfCountersEnabled)
			{
				PerformanceCounterManager.DecrementUserPerfCounters(userName, isProxy, isLightExperience);
			}
		}

		// Token: 0x040002C9 RID: 713
		private const int UserContextReInitializationCheckInterval = 60;

		// Token: 0x040002CA RID: 714
		private const int MaxUserContextReInitializationRequestsPerInterval = 1000;

		// Token: 0x040002CB RID: 715
		private static double averageMultiplier = 0.04;

		// Token: 0x040002CC RID: 716
		private static FixedSizeQueueBool availabilityServiceResultsQueue = new FixedSizeQueueBool(100);

		// Token: 0x040002CD RID: 717
		private static FixedSizeQueueBool storeLogonResultsQueue = new FixedSizeQueueBool(100);

		// Token: 0x040002CE RID: 718
		private static FixedSizeQueueBool instantMessagingLogonResultsQueue;

		// Token: 0x040002CF RID: 719
		private static FixedSizeQueueBool sentInstantMessageResultsQueue;

		// Token: 0x040002D0 RID: 720
		private static int instantMessagingLogonResultsQueueSize = 100;

		// Token: 0x040002D1 RID: 721
		private static int sentInstantMessageResultsQueueSize = 100;

		// Token: 0x040002D2 RID: 722
		private static FixedSizeQueueBool ewsRequestResultQueue = new FixedSizeQueueBool(100);

		// Token: 0x040002D3 RID: 723
		private static FixedSizeQueueBool mailboxOfflineExResultsQueue;

		// Token: 0x040002D4 RID: 724
		private static FixedSizeQueueBool connectionFailedTransientExResultsQueue;

		// Token: 0x040002D5 RID: 725
		private static FixedSizeQueueBool storageTransientExResultsQueue;

		// Token: 0x040002D6 RID: 726
		private static FixedSizeQueueBool storagePermanentExResultsQueue;

		// Token: 0x040002D7 RID: 727
		private static int mailboxOfflineExResultsQueueSize = 1024;

		// Token: 0x040002D8 RID: 728
		private static int connectionFailedTransientExResultsQueueSize = 1024;

		// Token: 0x040002D9 RID: 729
		private static int storageTransientExResultsQueueSize = 1024;

		// Token: 0x040002DA RID: 730
		private static int storagePermanentExResultsQueueSize = 1024;

		// Token: 0x040002DB RID: 731
		private static object responseTimeAverageLock = new object();

		// Token: 0x040002DC RID: 732
		private static double responseTimeAverage;

		// Token: 0x040002DD RID: 733
		private static object imSignOnTimeAverageLock = new object();

		// Token: 0x040002DE RID: 734
		private static double imSignOnTimeAverage;

		// Token: 0x040002DF RID: 735
		private static object proxiedResponseTimeAverageLock = new object();

		// Token: 0x040002E0 RID: 736
		private static double proxiedResponseTimeAverage;

		// Token: 0x040002E1 RID: 737
		private static double searchTimeAverage;

		// Token: 0x040002E2 RID: 738
		private static object searchTimeAverageLock = new object();

		// Token: 0x040002E3 RID: 739
		private static double spellcheckTimeAverage;

		// Token: 0x040002E4 RID: 740
		private static object spellcheckTimeAverageLock = new object();

		// Token: 0x040002E5 RID: 741
		private static Dictionary<string, long> currentUserCounterValues = new Dictionary<string, long>();

		// Token: 0x040002E6 RID: 742
		private static Dictionary<string, object> currentUserCounterLocks = new Dictionary<string, object>();

		// Token: 0x040002E7 RID: 743
		private static TimeSpan userContextReInitializationCheckDuration = new TimeSpan(0, 0, 3600);

		// Token: 0x040002E8 RID: 744
		private static long lastUserContextReInitializationIntervalStartTimeInTicks = ExDateTime.UtcNow.UtcTicks;

		// Token: 0x040002E9 RID: 745
		private static Dictionary<string, UniqueUserData> uniqueUserTable = new Dictionary<string, UniqueUserData>(StringComparer.OrdinalIgnoreCase);
	}
}
