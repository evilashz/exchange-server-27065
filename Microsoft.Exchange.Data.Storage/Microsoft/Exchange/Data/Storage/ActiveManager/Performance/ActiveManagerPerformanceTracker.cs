using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.Storage.ActiveManager.Performance
{
	// Token: 0x02000309 RID: 777
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ActiveManagerPerformanceTracker : IDisposable
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x0008DC3C File Offset: 0x0008BE3C
		public ActiveManagerPerformanceTracker(string marker, IPerformanceDataLogger logger)
		{
			ArgumentValidator.ThrowIfNull("marker", marker);
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.marker = marker;
			this.logger = logger;
			this.initialSnapshots = new PerformanceData[ActiveManagerPerformanceData.Providers.Length];
			for (int i = 0; i < ActiveManagerPerformanceData.Providers.Length; i++)
			{
				this.initialSnapshots[i] = ActiveManagerPerformanceData.Providers[i].Provider.TakeSnapshot(true);
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0008DCB4 File Offset: 0x0008BEB4
		public void Dispose()
		{
			for (int i = 0; i < ActiveManagerPerformanceData.Providers.Length; i++)
			{
				PerformanceData pd = ActiveManagerPerformanceData.Providers[i].Provider.TakeSnapshot(false);
				PerformanceData performanceData = pd - this.initialSnapshots[i];
				this.logger.Log(this.marker, ActiveManagerPerformanceData.Providers[i].LogCount, performanceData.Count);
				this.logger.Log(this.marker, ActiveManagerPerformanceData.Providers[i].LogLatency, performanceData.Latency);
			}
		}

		// Token: 0x04001476 RID: 5238
		private readonly string marker;

		// Token: 0x04001477 RID: 5239
		private readonly IPerformanceDataLogger logger;

		// Token: 0x04001478 RID: 5240
		private readonly PerformanceData[] initialSnapshots;
	}
}
