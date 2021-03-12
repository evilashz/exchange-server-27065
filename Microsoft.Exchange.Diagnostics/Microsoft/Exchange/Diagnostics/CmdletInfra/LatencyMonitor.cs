using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000104 RID: 260
	internal class LatencyMonitor : IScopedPerformanceMonitor
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001E151 File Offset: 0x0001C351
		public LatencyMonitor(LatencyTracker latencyTracker)
		{
			this.latencyTracker = latencyTracker;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001E160 File Offset: 0x0001C360
		public bool Start(ScopeInfo scopeInfo)
		{
			return this.latencyTracker != null && this.latencyTracker.StartInternalTracking(scopeInfo.GroupName, scopeInfo.FuncName, false);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001E184 File Offset: 0x0001C384
		public void End(ScopeInfo scopeInfo)
		{
			if (this.latencyTracker == null)
			{
				return;
			}
			this.latencyTracker.EndInternalTracking(scopeInfo.GroupName, scopeInfo.FuncName);
		}

		// Token: 0x040004BD RID: 1213
		private readonly LatencyTracker latencyTracker;
	}
}
