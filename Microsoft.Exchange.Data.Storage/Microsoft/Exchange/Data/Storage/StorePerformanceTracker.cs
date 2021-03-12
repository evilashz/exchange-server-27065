using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000556 RID: 1366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct StorePerformanceTracker : IDisposable
	{
		// Token: 0x060039B3 RID: 14771 RVA: 0x000EC7DC File Offset: 0x000EA9DC
		public StorePerformanceTracker(string marker, IPerformanceDataLogger logger)
		{
			if (string.IsNullOrEmpty(marker))
			{
				throw new ArgumentNullException("marker");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this.marker = marker;
			this.logger = logger;
			this.startSnapshot = RpcDataProvider.Instance.TakeSnapshot(true);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x000EC82C File Offset: 0x000EAA2C
		public void Dispose()
		{
			PerformanceData performanceData = RpcDataProvider.Instance.TakeSnapshot(false) - this.startSnapshot;
			this.logger.Log(this.marker, "StoreRpcLatency", performanceData.Latency);
			this.logger.Log(this.marker, "StoreRpcCount", performanceData.Count);
		}

		// Token: 0x04001ECC RID: 7884
		public const string StoreRpcCount = "StoreRpcCount";

		// Token: 0x04001ECD RID: 7885
		public const string StoreRpcLatency = "StoreRpcLatency";

		// Token: 0x04001ECE RID: 7886
		private readonly string marker;

		// Token: 0x04001ECF RID: 7887
		private readonly IPerformanceDataLogger logger;

		// Token: 0x04001ED0 RID: 7888
		private readonly PerformanceData startSnapshot;
	}
}
