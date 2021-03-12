using System;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.RouteRefresher
{
	// Token: 0x02000005 RID: 5
	public class RouteRefresherDiagnostics : IRouteRefresherDiagnostics
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000237C File Offset: 0x0000057C
		public RouteRefresherDiagnostics(RequestLogger baseLogger)
		{
			this.baseLogger = baseLogger;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000238B File Offset: 0x0000058B
		public void AddErrorInfo(object value)
		{
			this.baseLogger.AppendErrorInfo("RouteRefresher", value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000239E File Offset: 0x0000059E
		public void AddGenericInfo(object value)
		{
			this.baseLogger.AppendGenericInfo("RouteRefresher", value);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B1 File Offset: 0x000005B1
		public void IncrementSuccessfulMailboxServerCacheUpdates()
		{
			PerfCounters.HttpProxyCacheCountersInstance.RouteRefresherSuccessfulMailboxServerCacheUpdates.Increment();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023C3 File Offset: 0x000005C3
		public void IncrementTotalMailboxServerCacheUpdateAttempts()
		{
			PerfCounters.HttpProxyCacheCountersInstance.RouteRefresherTotalMailboxServerCacheUpdateAttempts.Increment();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023D5 File Offset: 0x000005D5
		public void IncrementSuccessfulAnchorMailboxCacheUpdates()
		{
			PerfCounters.HttpProxyCacheCountersInstance.RouteRefresherSuccessfulAnchorMailboxCacheUpdates.Increment();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023E7 File Offset: 0x000005E7
		public void IncrementTotalAnchorMailboxCacheUpdateAttempts()
		{
			PerfCounters.HttpProxyCacheCountersInstance.RouteRefresherTotalAnchorMailboxCacheUpdateAttempts.Increment();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023F9 File Offset: 0x000005F9
		public void LogRouteRefresherLatency(Action operationToTrack)
		{
			this.baseLogger.LatencyTracker.LogLatency(LogKey.RouteRefresherLatency, operationToTrack);
		}

		// Token: 0x04000008 RID: 8
		private readonly RequestLogger baseLogger;
	}
}
