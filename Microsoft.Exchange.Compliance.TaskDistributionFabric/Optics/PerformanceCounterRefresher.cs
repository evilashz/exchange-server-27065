using System;
using System.Threading;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Optics
{
	// Token: 0x02000029 RID: 41
	internal class PerformanceCounterRefresher : IDisposable
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00005D07 File Offset: 0x00003F07
		public PerformanceCounterRefresher()
		{
			this.performanceCounterUpdateTimer = new Timer(new TimerCallback(this.UpdateCounters), null, TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(30.0));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005D43 File Offset: 0x00003F43
		public void Dispose()
		{
			if (this.performanceCounterUpdateTimer != null)
			{
				this.performanceCounterUpdateTimer.Dispose();
				this.performanceCounterUpdateTimer = null;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005D60 File Offset: 0x00003F60
		private void UpdateCounters(object state)
		{
			foreach (PerformanceCounterAccessor performanceCounterAccessor in PerformanceCounterAccessorRegistry.Instance.GetAllRegisteredAccessors())
			{
				performanceCounterAccessor.UpdateCounters();
			}
		}

		// Token: 0x04000062 RID: 98
		private Timer performanceCounterUpdateTimer;
	}
}
