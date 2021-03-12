using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.RouteSelector
{
	// Token: 0x02000008 RID: 8
	internal class RouteSelectorDiagnostics : IRouteSelectorModuleDiagnostics, IRouteSelectorDiagnostics, IRoutingDiagnostics
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002240 File Offset: 0x00000440
		public RouteSelectorDiagnostics(RequestLogger baseLogger)
		{
			this.baseLogger = baseLogger;
			this.accountForestLatencies = new List<long>(2);
			this.globalLocatorLatencies = new List<long>(2);
			this.resourceForestLatencies = new List<long>(2);
			this.serverLocatorLatencies = new List<long>(2);
			this.sharedCacheLatencies = new List<long>(2);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002298 File Offset: 0x00000498
		public static void UpdateRoutingFailurePerfCounter(string serverFqdn, bool wasFailure)
		{
			if (RouteSelectorModule.IsTesting)
			{
				return;
			}
			if (!PerfCounters.RoutingErrorsEnabled)
			{
				return;
			}
			string text = string.Empty;
			if (serverFqdn != null)
			{
				string[] array = serverFqdn.Split(new char[]
				{
					'.'
				});
				if (array[0].Length > 5)
				{
					text = array[0].Substring(0, array[0].Length - 5);
				}
				else
				{
					text = array[0];
				}
			}
			if (wasFailure)
			{
				PerfCounters.UpdateMovingPercentagePerformanceCounter(PerfCounters.GetHttpProxyPerSiteCountersInstance(text).MovingPercentageRoutingFailure);
				PerfCounters.GetHttpProxyPerSiteCountersInstance(text).TotalFailedRequests.Increment();
			}
			if (!string.IsNullOrEmpty(text))
			{
				PerfCounters.IncrementMovingPercentagePerformanceCounterBase(PerfCounters.GetHttpProxyPerSiteCountersInstance(text).MovingPercentageRoutingFailure);
			}
			PerfCounters.IncrementMovingPercentagePerformanceCounterBase(PerfCounters.GetHttpProxyPerSiteCountersInstance(string.Empty).MovingPercentageRoutingFailure);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002347 File Offset: 0x00000547
		public void SetTargetServer(string value)
		{
			this.baseLogger.LogField(LogKey.TargetServer, value);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002357 File Offset: 0x00000557
		public void SetTargetServerVersion(string value)
		{
			this.baseLogger.LogField(LogKey.TargetServerVersion, value);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002367 File Offset: 0x00000567
		public void SetOrganization(string value)
		{
			this.baseLogger.LogField(LogKey.Organization, value);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002377 File Offset: 0x00000577
		public void AddRoutingEntry(string value)
		{
			this.baseLogger.AppendGenericInfo("RoutingEntry", value);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000238A File Offset: 0x0000058A
		public void AddErrorInfo(object value)
		{
			this.baseLogger.AppendErrorInfo("RouteSelector", value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000239D File Offset: 0x0000059D
		public void SaveRoutingLatency(Action operationToTrack)
		{
			this.baseLogger.LatencyTracker.LogLatency(LogKey.CalculateTargetBackEndLatency, operationToTrack);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023B4 File Offset: 0x000005B4
		public void ProcessRoutingKey(IRoutingKey key)
		{
			if (ServerLocator.IsMailboxServerCacheKey(key))
			{
				PerfCounters.HttpProxyCacheCountersInstance.BackEndServerOverallCacheHitsRateBase.Increment();
				PerfCounters.IncrementMovingPercentagePerformanceCounterBase(PerfCounters.HttpProxyCacheCountersInstance.MovingPercentageBackEndServerOverallCacheHitsRate);
			}
			if (ServerLocator.IsAnchorMailboxCacheKey(key))
			{
				PerfCounters.HttpProxyCacheCountersInstance.OverallCacheEffectivenessRateBase.Increment();
				PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxOverallCacheHitsRateBase.Increment();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002410 File Offset: 0x00000610
		public void ProcessRoutingEntry(IRoutingEntry entry)
		{
			if (ServerLocator.IsMailboxServerCacheKey(entry.Key))
			{
				PerfCounters.HttpProxyCacheCountersInstance.BackEndServerOverallCacheHitsRate.Increment();
				PerfCounters.UpdateMovingPercentagePerformanceCounter(PerfCounters.HttpProxyCacheCountersInstance.MovingPercentageBackEndServerOverallCacheHitsRate);
				return;
			}
			if (ServerLocator.IsAnchorMailboxCacheKey(entry.Key))
			{
				PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxOverallCacheHitsRate.Increment();
				PerfCounters.HttpProxyCacheCountersInstance.OverallCacheEffectivenessRate.Increment();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002478 File Offset: 0x00000678
		public void LogLatencies()
		{
			this.baseLogger.LogField(LogKey.AccountForestLatencyBreakup, RouteSelectorDiagnostics.GetBreakupOfLatencies(this.accountForestLatencies));
			this.baseLogger.LogField(LogKey.TotalAccountForestLatency, this.accountForestLatencies.Sum());
			this.baseLogger.LogField(LogKey.GlsLatencyBreakup, RouteSelectorDiagnostics.GetBreakupOfLatencies(this.globalLocatorLatencies));
			this.baseLogger.LogField(LogKey.TotalGlsLatency, this.globalLocatorLatencies.Sum());
			this.baseLogger.LogField(LogKey.ResourceForestLatencyBreakup, RouteSelectorDiagnostics.GetBreakupOfLatencies(this.resourceForestLatencies));
			this.baseLogger.LogField(LogKey.TotalResourceForestLatency, this.resourceForestLatencies.Sum());
			this.baseLogger.LogField(LogKey.SharedCacheLatencyBreakup, RouteSelectorDiagnostics.GetBreakupOfLatencies(this.sharedCacheLatencies));
			this.baseLogger.LogField(LogKey.TotalSharedCacheLatency, this.sharedCacheLatencies.Sum());
			this.baseLogger.LogField(LogKey.ServerLocatorLatency, this.serverLocatorLatencies.Sum());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002578 File Offset: 0x00000778
		public void ProcessLatencyPerfCounters()
		{
			long num = this.serverLocatorLatencies.Sum();
			PerfCounters.HttpProxyCountersInstance.MailboxServerLocatorLatency.RawValue = num;
			PerfCounters.HttpProxyCountersInstance.MailboxServerLocatorAverageLatency.IncrementBy(num);
			PerfCounters.HttpProxyCountersInstance.MailboxServerLocatorAverageLatencyBase.Increment();
			PerfCounters.UpdateMovingAveragePerformanceCounter(PerfCounters.HttpProxyCountersInstance.MovingAverageMailboxServerLocatorLatency, num);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025D2 File Offset: 0x000007D2
		public void AddAccountForestLatency(TimeSpan latency)
		{
			this.accountForestLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025EB File Offset: 0x000007EB
		public void AddActiveManagerLatency(TimeSpan latency)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025ED File Offset: 0x000007ED
		public void AddDiagnosticText(string text)
		{
			this.baseLogger.AppendGenericInfo("SharedCache", text);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002600 File Offset: 0x00000800
		public void AddGlobalLocatorLatency(TimeSpan latency)
		{
			this.globalLocatorLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002619 File Offset: 0x00000819
		public void AddResourceForestLatency(TimeSpan latency)
		{
			this.resourceForestLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002632 File Offset: 0x00000832
		public void AddServerLocatorLatency(TimeSpan latency)
		{
			this.serverLocatorLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000264B File Offset: 0x0000084B
		public void AddSharedCacheLatency(TimeSpan latency)
		{
			this.sharedCacheLatencies.Add(Convert.ToInt64(latency.TotalMilliseconds));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000268C File Offset: 0x0000088C
		private static string GetBreakupOfLatencies(List<long> latencies)
		{
			if (latencies == null)
			{
				throw new ArgumentNullException("latencies");
			}
			StringBuilder result = new StringBuilder();
			latencies.ForEach(delegate(long latency)
			{
				result.Append(latency);
				result.Append(';');
			});
			return result.ToString();
		}

		// Token: 0x04000001 RID: 1
		private readonly RequestLogger baseLogger;

		// Token: 0x04000002 RID: 2
		private readonly List<long> accountForestLatencies;

		// Token: 0x04000003 RID: 3
		private readonly List<long> globalLocatorLatencies;

		// Token: 0x04000004 RID: 4
		private readonly List<long> resourceForestLatencies;

		// Token: 0x04000005 RID: 5
		private readonly List<long> serverLocatorLatencies;

		// Token: 0x04000006 RID: 6
		private readonly List<long> sharedCacheLatencies;
	}
}
