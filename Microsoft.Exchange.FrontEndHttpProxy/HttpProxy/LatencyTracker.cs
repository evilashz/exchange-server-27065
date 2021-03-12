using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200009E RID: 158
	internal class LatencyTracker
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x0001B154 File Offset: 0x00019354
		internal LatencyTracker()
		{
			this.latencyTrackerStopwatch.Start();
			this.glsLatencies = new List<long>(4);
			this.accountForestLatencies = new List<long>(4);
			this.resourceForestLatencies = new List<long>(4);
			this.sharedCacheLatencies = new List<long>(4);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001B1B8 File Offset: 0x000193B8
		internal string GlsLatencyBreakup
		{
			get
			{
				return LatencyTracker.GetBreakupOfLatencies(this.glsLatencies);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0001B1C5 File Offset: 0x000193C5
		internal long TotalGlsLatency
		{
			get
			{
				return this.glsLatencies.Sum();
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001B1D2 File Offset: 0x000193D2
		internal string AccountForestLatencyBreakup
		{
			get
			{
				return LatencyTracker.GetBreakupOfLatencies(this.accountForestLatencies);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0001B1DF File Offset: 0x000193DF
		internal long TotalAccountForestDirectoryLatency
		{
			get
			{
				return this.accountForestLatencies.Sum();
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001B1EC File Offset: 0x000193EC
		internal string ResourceForestLatencyBreakup
		{
			get
			{
				return LatencyTracker.GetBreakupOfLatencies(this.resourceForestLatencies);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0001B1F9 File Offset: 0x000193F9
		internal long TotalResourceForestDirectoryLatency
		{
			get
			{
				return this.resourceForestLatencies.Sum();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001B206 File Offset: 0x00019406
		internal long AdLatency
		{
			get
			{
				return this.TotalAccountForestDirectoryLatency + this.TotalResourceForestDirectoryLatency;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001B215 File Offset: 0x00019415
		internal string SharedCacheLatencyBreakup
		{
			get
			{
				return LatencyTracker.GetBreakupOfLatencies(this.sharedCacheLatencies);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001B222 File Offset: 0x00019422
		internal long TotalSharedCacheLatency
		{
			get
			{
				return this.sharedCacheLatencies.Sum();
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001B22F File Offset: 0x0001942F
		internal static LatencyTracker FromHttpContext(HttpContext httpContext)
		{
			return (LatencyTracker)httpContext.Items[Constants.LatencyTrackerContextKeyName];
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001B248 File Offset: 0x00019448
		internal static void GetLatency(Action operationToTrack, out long latency)
		{
			Stopwatch stopwatch = new Stopwatch();
			latency = 0L;
			try
			{
				stopwatch.Start();
				operationToTrack();
			}
			finally
			{
				stopwatch.Stop();
				latency = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001B28C File Offset: 0x0001948C
		internal static T GetLatency<T>(Func<T> operationToTrack, out long latency)
		{
			Stopwatch stopwatch = new Stopwatch();
			latency = 0L;
			T result;
			try
			{
				stopwatch.Start();
				result = operationToTrack();
			}
			finally
			{
				stopwatch.Stop();
				latency = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001B2D4 File Offset: 0x000194D4
		internal void LogElapsedTime(RequestDetailsLogger logger, string latencyName)
		{
			if (HttpProxySettings.DetailedLatencyTracingEnabled.Value)
			{
				long currentLatency = this.GetCurrentLatency(LatencyTrackerKey.ProxyModuleLatency);
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(logger, latencyName, currentLatency);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001B304 File Offset: 0x00019504
		internal void LogElapsedTimeAsLatency(RequestDetailsLogger logger, LatencyTrackerKey trackerKey, HttpProxyMetadata protocolLogKey)
		{
			long currentLatency = this.GetCurrentLatency(trackerKey);
			if (currentLatency >= 0L)
			{
				logger.UpdateLatency(protocolLogKey, (double)currentLatency);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001B32C File Offset: 0x0001952C
		internal void StartTracking(LatencyTrackerKey trackingKey, bool resetValue = false)
		{
			if (!this.latencyTrackerStartTimes.ContainsKey(trackingKey))
			{
				this.latencyTrackerStartTimes.Add(trackingKey, this.latencyTrackerStopwatch.ElapsedMilliseconds);
				return;
			}
			this.latencyTrackerStartTimes[trackingKey] = this.latencyTrackerStopwatch.ElapsedMilliseconds;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001B36B File Offset: 0x0001956B
		internal long GetCurrentLatency(LatencyTrackerKey trackingKey)
		{
			if (this.latencyTrackerStartTimes.ContainsKey(trackingKey))
			{
				return this.latencyTrackerStopwatch.ElapsedMilliseconds - this.latencyTrackerStartTimes[trackingKey];
			}
			return -1L;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001B396 File Offset: 0x00019596
		internal void HandleGlsLatency(long latency)
		{
			this.glsLatencies.Add(latency);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001B3A4 File Offset: 0x000195A4
		internal void HandleGlsLatency(List<long> latencies)
		{
			this.glsLatencies.AddRange(latencies);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001B3B2 File Offset: 0x000195B2
		internal void HandleAccountLatency(long latency)
		{
			this.accountForestLatencies.Add(latency);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001B3C0 File Offset: 0x000195C0
		internal void HandleResourceLatency(long latency)
		{
			this.resourceForestLatencies.Add(latency);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001B3CE File Offset: 0x000195CE
		internal void HandleResourceLatency(List<long> latencies)
		{
			this.resourceForestLatencies.AddRange(latencies);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001B3DC File Offset: 0x000195DC
		internal void HandleSharedCacheLatency(long latency)
		{
			this.sharedCacheLatencies.Add(latency);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001B410 File Offset: 0x00019610
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

		// Token: 0x040003AA RID: 938
		internal const string SelectHandlerTime = "SelectHandler";

		// Token: 0x040003AB RID: 939
		private readonly List<long> glsLatencies;

		// Token: 0x040003AC RID: 940
		private readonly List<long> accountForestLatencies;

		// Token: 0x040003AD RID: 941
		private readonly List<long> resourceForestLatencies;

		// Token: 0x040003AE RID: 942
		private readonly List<long> sharedCacheLatencies;

		// Token: 0x040003AF RID: 943
		private Stopwatch latencyTrackerStopwatch = new Stopwatch();

		// Token: 0x040003B0 RID: 944
		private Dictionary<LatencyTrackerKey, long> latencyTrackerStartTimes = new Dictionary<LatencyTrackerKey, long>();
	}
}
