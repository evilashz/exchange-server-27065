using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics.Performance
{
	// Token: 0x020001D4 RID: 468
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct StopwatchPerformanceTracker : IDisposable
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x0003703A File Offset: 0x0003523A
		public StopwatchPerformanceTracker(string marker, IPerformanceDataLogger logger)
		{
			if (string.IsNullOrEmpty(marker))
			{
				throw new ArgumentNullException("marker");
			}
			this.marker = marker;
			this.logger = (logger ?? NullPerformanceDataLogger.Instance);
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00037071 File Offset: 0x00035271
		public void Dispose()
		{
			this.Stop();
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00037079 File Offset: 0x00035279
		public void Stop()
		{
			this.stopwatch.Stop();
			this.logger.Log(this.marker, "ElapsedTime", this.stopwatch.Elapsed);
		}

		// Token: 0x040009AC RID: 2476
		public const string ElapsedTime = "ElapsedTime";

		// Token: 0x040009AD RID: 2477
		private readonly string marker;

		// Token: 0x040009AE RID: 2478
		private readonly IPerformanceDataLogger logger;

		// Token: 0x040009AF RID: 2479
		private readonly Stopwatch stopwatch;
	}
}
