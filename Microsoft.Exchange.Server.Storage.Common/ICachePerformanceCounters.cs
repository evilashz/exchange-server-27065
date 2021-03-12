using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000068 RID: 104
	public interface ICachePerformanceCounters
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005CF RID: 1487
		ExPerformanceCounter CacheSize { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005D0 RID: 1488
		ExPerformanceCounter CacheLookups { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005D1 RID: 1489
		ExPerformanceCounter CacheMisses { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005D2 RID: 1490
		ExPerformanceCounter CacheHits { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005D3 RID: 1491
		ExPerformanceCounter CacheInserts { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005D4 RID: 1492
		ExPerformanceCounter CacheRemoves { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005D5 RID: 1493
		ExPerformanceCounter CacheExpirationQueueLength { get; }
	}
}
