using System;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F6 RID: 246
	internal class MonitoredScope : DisposeTracker
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public MonitoredScope(string groupName, string funcName, params IScopedPerformanceMonitor[] monitors) : this(new ScopeInfo(groupName, funcName), monitors)
		{
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001C600 File Offset: 0x0001A800
		protected MonitoredScope(ScopeInfo scopeInfo, params IScopedPerformanceMonitor[] monitors)
		{
			this.scopeInfo = scopeInfo;
			this.monitors = monitors;
			this.monitorStarted = new bool[monitors.Length];
			for (int i = 0; i < monitors.Length; i++)
			{
				this.monitorStarted[i] = monitors[i].Start(scopeInfo);
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001C650 File Offset: 0x0001A850
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				for (int i = 0; i < this.monitors.Length; i++)
				{
					if (this.monitorStarted[i])
					{
						this.monitors[i].End(this.scopeInfo);
					}
				}
			}
		}

		// Token: 0x04000485 RID: 1157
		private readonly ScopeInfo scopeInfo;

		// Token: 0x04000486 RID: 1158
		private readonly IScopedPerformanceMonitor[] monitors;

		// Token: 0x04000487 RID: 1159
		private readonly bool[] monitorStarted;
	}
}
