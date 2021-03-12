using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Microsoft.Exchange.Configuration.RedirectionModule;
using Microsoft.Exchange.Configuration.RedirectionModule.EventLog;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RedirectionModule;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Configuration.TenantMonitoring
{
	// Token: 0x02000003 RID: 3
	internal static class IntervalCounterInstanceCache
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002458 File Offset: 0x00000658
		static IntervalCounterInstanceCache()
		{
			int intValueFromRegistry = Globals.GetIntValueFromRegistry("SYSTEM\\CurrentControlSet\\Services\\MSExchange Tenant Monitoring", "UpdateIntervalSeconds", 1200, 0);
			int intValueFromRegistry2 = Globals.GetIntValueFromRegistry("SYSTEM\\CurrentControlSet\\Services\\MSExchange Tenant Monitoring", "InactivityTimeoutSeconds", 3600, 0);
			IntervalCounterInstanceCache.updateInterval = TimeSpan.FromSeconds((double)intValueFromRegistry);
			IntervalCounterInstanceCache.inactivityTimeout = TimeSpan.FromSeconds((double)intValueFromRegistry2);
			if (PerformanceCounterCategory.Exists("MSExchangeTenantMonitoring"))
			{
				IntervalCounterInstanceCache.counterUpdateTimer = new Timer(IntervalCounterInstanceCache.updateInterval.TotalMilliseconds);
				IntervalCounterInstanceCache.counterUpdateTimer.AutoReset = true;
				IntervalCounterInstanceCache.counterUpdateTimer.Elapsed += IntervalCounterInstanceCache.UpdateCounters;
				AppDomain.CurrentDomain.DomainUnload += IntervalCounterInstanceCache.ApplicationDomainUnload;
				IntervalCounterInstanceCache.counterUpdateTimer.Start();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000253E File Offset: 0x0000073E
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002545 File Offset: 0x00000745
		public static TimeSpan UpdateInterval
		{
			get
			{
				return IntervalCounterInstanceCache.updateInterval;
			}
			set
			{
				IntervalCounterInstanceCache.updateInterval = value;
				if (IntervalCounterInstanceCache.counterUpdateTimer != null)
				{
					IntervalCounterInstanceCache.counterUpdateTimer.Stop();
					IntervalCounterInstanceCache.counterUpdateTimer.Interval = IntervalCounterInstanceCache.updateInterval.TotalMilliseconds;
					IntervalCounterInstanceCache.counterUpdateTimer.Start();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000257C File Offset: 0x0000077C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002583 File Offset: 0x00000783
		public static TimeSpan InactivityTimeout
		{
			get
			{
				return IntervalCounterInstanceCache.inactivityTimeout;
			}
			set
			{
				IntervalCounterInstanceCache.inactivityTimeout = value;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000258B File Offset: 0x0000078B
		public static void ApplicationDomainUnload(object sender, EventArgs e)
		{
			if (IntervalCounterInstanceCache.counterUpdateTimer != null)
			{
				IntervalCounterInstanceCache.counterUpdateTimer.Stop();
				IntervalCounterInstanceCache.counterUpdateTimer.Dispose();
				IntervalCounterInstanceCache.counterUpdateTimer = null;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025B0 File Offset: 0x000007B0
		internal static void IncrementIntervalCounter(string instanceName, CounterType counterType)
		{
			IntervalCounterInstance instance = IntervalCounterInstanceCache.GetInstance(instanceName);
			if (instance == null)
			{
				return;
			}
			instance.Increment(counterType);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025D0 File Offset: 0x000007D0
		private static void UpdateCounters(object source, ElapsedEventArgs e)
		{
			ExTraceGlobals.TenantMonitoringTracer.TraceFunction(0L, "Enter UpdateCounters.");
			List<string> list = new List<string>(16);
			try
			{
				IntervalCounterInstanceCache.readerWriterLock.AcquireReaderLock(5000);
			}
			catch (TimeoutException ex)
			{
				IntervalCounterInstanceCache.LogReaderWriterLockTimeoutEvent(ex, "IntervalCounterInstanceCache.UpdateCounters.AcquireReaderLock");
				ExTraceGlobals.TenantMonitoringTracer.TraceFunction(0L, "Exit UpdateCounters.");
				return;
			}
			try
			{
				foreach (KeyValuePair<string, IntervalCounterInstance> keyValuePair in IntervalCounterInstanceCache.instanceDictionary)
				{
					ExTraceGlobals.TenantMonitoringTracer.Information<string, DateTime, TimeSpan>(0L, "Update counter for key {0}. Last Update {1}. Current inactivityTimeout {2}", keyValuePair.Key, keyValuePair.Value.LastUpdateTime, IntervalCounterInstanceCache.inactivityTimeout);
					if (keyValuePair.Value.LastUpdateTime + IntervalCounterInstanceCache.inactivityTimeout < DateTime.UtcNow)
					{
						list.Add(keyValuePair.Key);
					}
					keyValuePair.Value.CalculateIntervalDataAndUpdateCounters(keyValuePair.Key);
				}
			}
			finally
			{
				IntervalCounterInstanceCache.readerWriterLock.ReleaseReaderLock();
			}
			if (list.Count > 0)
			{
				try
				{
					IntervalCounterInstanceCache.readerWriterLock.AcquireWriterLock(5000);
				}
				catch (TimeoutException ex2)
				{
					IntervalCounterInstanceCache.LogReaderWriterLockTimeoutEvent(ex2, "IntervalCounterInstanceCache.UpdateCounters.AcquireWriterLock");
					ExTraceGlobals.TenantMonitoringTracer.TraceFunction(0L, "Exit UpdateCounters.");
					return;
				}
				try
				{
					foreach (string text in list)
					{
						ExTraceGlobals.TenantMonitoringTracer.Information<string>(0L, "Remove entry {0} from cache.", text);
						IntervalCounterInstanceCache.instanceDictionary.Remove(text);
						MSExchangeTenantMonitoring.RemoveInstance(text);
					}
				}
				finally
				{
					IntervalCounterInstanceCache.readerWriterLock.ReleaseWriterLock();
				}
			}
			ExTraceGlobals.TenantMonitoringTracer.TraceFunction(0L, "Exit UpdateCounters.");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000027CC File Offset: 0x000009CC
		private static IntervalCounterInstance GetInstance(string instanceName)
		{
			try
			{
				IntervalCounterInstanceCache.readerWriterLock.AcquireReaderLock(5000);
			}
			catch (TimeoutException ex)
			{
				IntervalCounterInstanceCache.LogReaderWriterLockTimeoutEvent(ex, "IntervalCounterInstanceCache.GetInstance.AcquireReaderLock");
				return null;
			}
			IntervalCounterInstance intervalCounterInstance;
			try
			{
				IntervalCounterInstanceCache.instanceDictionary.TryGetValue(instanceName, out intervalCounterInstance);
			}
			finally
			{
				IntervalCounterInstanceCache.readerWriterLock.ReleaseReaderLock();
			}
			if (intervalCounterInstance == null)
			{
				try
				{
					IntervalCounterInstanceCache.readerWriterLock.AcquireWriterLock(5000);
				}
				catch (TimeoutException ex2)
				{
					IntervalCounterInstanceCache.LogReaderWriterLockTimeoutEvent(ex2, "IntervalCounterInstanceCache.GetInstance.AcquireWriterLock");
					return null;
				}
				try
				{
					if (!IntervalCounterInstanceCache.instanceDictionary.TryGetValue(instanceName, out intervalCounterInstance))
					{
						ExTraceGlobals.TenantMonitoringTracer.Information<string>(0L, "Create perf counter instance for {0}.", instanceName);
						intervalCounterInstance = new IntervalCounterInstance();
						IntervalCounterInstanceCache.instanceDictionary[instanceName] = intervalCounterInstance;
					}
				}
				finally
				{
					IntervalCounterInstanceCache.readerWriterLock.ReleaseWriterLock();
				}
			}
			return intervalCounterInstance;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028B4 File Offset: 0x00000AB4
		private static void LogReaderWriterLockTimeoutEvent(TimeoutException ex, string methodName)
		{
			ExTraceGlobals.TenantMonitoringTracer.TraceError<string, TimeoutException>(0L, "Method {0} throws TimeoutException {1}.", methodName, ex);
			Logger.LogEvent(IntervalCounterInstanceCache.eventLogger, TaskEventLogConstants.Tuple_ReaderWriterLock_Timeout, methodName, new object[]
			{
				ex
			});
		}

		// Token: 0x04000003 RID: 3
		private const int LockTimeoutInMilliseconds = 5000;

		// Token: 0x04000004 RID: 4
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.RedirectionTracer.Category, "MSExchange LiveId Redirection Module");

		// Token: 0x04000005 RID: 5
		private static Dictionary<string, IntervalCounterInstance> instanceDictionary = new Dictionary<string, IntervalCounterInstance>(100, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000006 RID: 6
		private static FastReaderWriterLock readerWriterLock = new FastReaderWriterLock();

		// Token: 0x04000007 RID: 7
		private static Timer counterUpdateTimer;

		// Token: 0x04000008 RID: 8
		private static TimeSpan updateInterval;

		// Token: 0x04000009 RID: 9
		private static TimeSpan inactivityTimeout;
	}
}
