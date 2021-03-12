using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.Performance
{
	// Token: 0x0200018D RID: 397
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct ADPerformanceTracker : IDisposable
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x0003D698 File Offset: 0x0003B898
		public ADPerformanceTracker(string marker, IPerformanceDataLogger logger)
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
			this.startSnapshot = PerformanceContext.Current.TakeSnapshot(true);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003D6E8 File Offset: 0x0003B8E8
		public void Dispose()
		{
			PerformanceData performanceData = PerformanceContext.Current.TakeSnapshot(false) - this.startSnapshot;
			this.logger.Log(this.marker, "LdapLatency", performanceData.Latency);
			this.logger.Log(this.marker, "LdapCount", performanceData.Count);
		}

		// Token: 0x0400080B RID: 2059
		public const string LdapCount = "LdapCount";

		// Token: 0x0400080C RID: 2060
		public const string LdapLatency = "LdapLatency";

		// Token: 0x0400080D RID: 2061
		private readonly string marker;

		// Token: 0x0400080E RID: 2062
		private readonly IPerformanceDataLogger logger;

		// Token: 0x0400080F RID: 2063
		private readonly PerformanceData startSnapshot;
	}
}
