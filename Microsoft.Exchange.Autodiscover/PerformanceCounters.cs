using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000002 RID: 2
	internal static class PerformanceCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void Initialize()
		{
			try
			{
				foreach (ExPerformanceCounter exPerformanceCounter in AutodiscoverPerformanceCounters.AllCounters)
				{
					exPerformanceCounter.RawValue = 0L;
				}
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Autodiscover.ConfigurePerformanceCounters.Enabled)
				{
					AutodiscoverDatacenterPerformanceCounters.RemoveAllInstances();
					AutodiscoverDatacenterPerformanceCounters.ResetInstance(string.Empty);
					string text = ConfigurationManager.AppSettings["TrustedClientsForInstanceBasedPerfCounters"];
					if (!string.IsNullOrEmpty(text))
					{
						PerformanceCounters.trustedClientsList.AddRange(text.Split(new string[]
						{
							";"
						}, StringSplitOptions.RemoveEmptyEntries));
					}
					string text2 = ConfigurationManager.AppSettings["InstanceBasedPerfCounterTimeWindowInterval"];
					if (!string.IsNullOrEmpty(text2))
					{
						double.TryParse(text2, out PerformanceCounters.timerForPerTimeWindowCountersInterval);
					}
					PerformanceCounters.timerForPerTimeWindowCounters.Elapsed += PerformanceCounters.UpdatePerTimeWindowCounters;
					PerformanceCounters.timerForPerTimeWindowCounters.Interval = PerformanceCounters.timerForPerTimeWindowCountersInterval;
					PerformanceCounters.timerForPerTimeWindowCounters.Enabled = true;
				}
				AutodiscoverPerformanceCounters.PID.RawValue = (long)Process.GetCurrentProcess().Id;
				PerformanceCounters.performanceCountersInitialized = true;
			}
			catch (InvalidOperationException ex)
			{
				PerformanceCounters.performanceCountersInitialized = false;
				Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCorePerfCounterInitializationFailed, Common.PeriodicKey, new object[]
				{
					ex.Message,
					ex.StackTrace
				});
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002284 File Offset: 0x00000484
		public static void UpdatePerTimeWindowCounters(object source, ElapsedEventArgs e)
		{
			lock (PerformanceCounters.perClientInstanceTotalPerWindowCounters)
			{
				PerformanceCounters.perClientInstanceTotalPerWindowCounters.Keys.ToList<string>().ForEach(delegate(string key)
				{
					long num = PerformanceCounters.perClientInstanceTotalCountersLastWindowValue[key];
					long rawValue = PerformanceCounters.perClientInstanceTotalCounters[key].RawValue;
					PerformanceCounters.perClientInstanceTotalPerWindowCounters[key].RawValue = rawValue - num;
					PerformanceCounters.perClientInstanceTotalCountersLastWindowValue[key] = rawValue;
				});
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002318 File Offset: 0x00000518
		public static void UpdateTotalRequests(bool error)
		{
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverPerformanceCounters.TotalRequests.Increment();
				if (error)
				{
					AutodiscoverPerformanceCounters.TotalErrorResponses.Increment();
				}
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002355 File Offset: 0x00000555
		public static void UpdatePartnerTokenRequests(string userAgent)
		{
			PerformanceCounters.IncrementPerClientInstanceForCounter(userAgent, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequests, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequestsPerTimeWindow);
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequests.Increment();
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023A5 File Offset: 0x000005A5
		public static void UpdatePartnerTokenRequestsFailed(string userAgent)
		{
			PerformanceCounters.IncrementPerClientInstanceForCounter(userAgent, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequestsFailed, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequestsFailedPerTimeWindow);
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalPartnerTokenRequestsFailed.Increment();
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023F5 File Offset: 0x000005F5
		public static void UpdateCertAuthRequestsFailed(string userAgent)
		{
			PerformanceCounters.IncrementPerClientInstanceForCounter(userAgent, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalCertAuthRequestsFailed, AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalCertAuthRequestsFailedPerTimeWindow);
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalCertAuthRequestsFailed.Increment();
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002460 File Offset: 0x00000660
		public static void UpdateAveragePartnerInfoQueryTime(long latency)
		{
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.AveragePartnerInfoQueryTime.RawValue = (long)PerformanceCounters.averagePartnerInfoQueryTime.Update((float)latency);
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000249D File Offset: 0x0000069D
		public static void UpdateRequestsReceivedWithPartnerToken()
		{
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalRequestsReceivedWithPartnerToken.Increment();
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024D3 File Offset: 0x000006D3
		public static void UpdateUnauthorizedRequestsReceivedWithPartnerToken()
		{
			PerformanceCounters.SafeUpdatePerfCounter(delegate
			{
				AutodiscoverDatacenterPerformanceCounters.TotalInstance.TotalUnauthorizedRequestsReceivedWithPartnerToken.Increment();
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024F8 File Offset: 0x000006F8
		private static void SafeUpdatePerfCounter(Action updateAction)
		{
			if (PerformanceCounters.performanceCountersInitialized)
			{
				try
				{
					updateAction();
				}
				catch (InvalidOperationException ex)
				{
					Common.EventLog.LogEvent(AutodiscoverEventLogConstants.Tuple_WarnCorePerfCounterIncrementFailed, Common.PeriodicKey, new object[]
					{
						ex.Message,
						ex.StackTrace
					});
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000259C File Offset: 0x0000079C
		private static void IncrementPerClientInstanceForCounter(string userAgent, ExPerformanceCounter counter, ExPerformanceCounter associatedWindowCounter)
		{
			if (!string.IsNullOrEmpty(userAgent))
			{
				string client = userAgent.Split(new char[]
				{
					'/'
				})[0];
				if (!string.IsNullOrEmpty(client) && PerformanceCounters.trustedClientsList.Contains(client.ToLower()))
				{
					PerformanceCounters.EnsurePerClientPerfCounterInstancesExist(client, counter, associatedWindowCounter);
					PerformanceCounters.SafeUpdatePerfCounter(delegate
					{
						PerformanceCounters.perClientInstanceTotalCounters[client + "_" + counter.CounterName].Increment();
					});
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002634 File Offset: 0x00000834
		private static void EnsurePerClientPerfCounterInstancesExist(string client, ExPerformanceCounter counter, ExPerformanceCounter associatedWindowCounter)
		{
			string key = client + "_" + counter.CounterName;
			if (!PerformanceCounters.perClientInstanceTotalCounters.ContainsKey(key))
			{
				lock (PerformanceCounters.perClientInstanceTotalCounters)
				{
					if (!PerformanceCounters.perClientInstanceTotalCounters.ContainsKey(key))
					{
						lock (PerformanceCounters.perClientInstanceTotalPerWindowCounters)
						{
							PerformanceCounters.perClientInstanceTotalCounters.Add(key, new ExPerformanceCounter("MSExchangeAutodiscover:Datacenter", counter.CounterName, client, null, new ExPerformanceCounter[0]));
							PerformanceCounters.perClientInstanceTotalCounters[key].RawValue = 0L;
							if (associatedWindowCounter != null)
							{
								PerformanceCounters.perClientInstanceTotalPerWindowCounters.Add(key, new ExPerformanceCounter("MSExchangeAutodiscover:Datacenter", associatedWindowCounter.CounterName, client, null, new ExPerformanceCounter[0]));
								PerformanceCounters.perClientInstanceTotalPerWindowCounters[key].RawValue = 0L;
								PerformanceCounters.perClientInstanceTotalCountersLastWindowValue.Add(key, 0L);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string TrustedClientsForInstanceBasedPerfCountersKey = "TrustedClientsForInstanceBasedPerfCounters";

		// Token: 0x04000002 RID: 2
		private const string InstanceBasedPerfCounterTimeWindowInterval = "InstanceBasedPerfCounterTimeWindowInterval";

		// Token: 0x04000003 RID: 3
		private static bool performanceCountersInitialized;

		// Token: 0x04000004 RID: 4
		private static RunningAverageFloat averagePartnerInfoQueryTime = new RunningAverageFloat(25);

		// Token: 0x04000005 RID: 5
		private static Dictionary<string, ExPerformanceCounter> perClientInstanceTotalCounters = new Dictionary<string, ExPerformanceCounter>(10);

		// Token: 0x04000006 RID: 6
		private static Dictionary<string, ExPerformanceCounter> perClientInstanceTotalPerWindowCounters = new Dictionary<string, ExPerformanceCounter>(10);

		// Token: 0x04000007 RID: 7
		private static Dictionary<string, long> perClientInstanceTotalCountersLastWindowValue = new Dictionary<string, long>(10);

		// Token: 0x04000008 RID: 8
		private static List<string> trustedClientsList = new List<string>(5);

		// Token: 0x04000009 RID: 9
		private static Timer timerForPerTimeWindowCounters = new Timer();

		// Token: 0x0400000A RID: 10
		private static double timerForPerTimeWindowCountersInterval = 1000.0;
	}
}
